using Google.Cloud.Firestore;
using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.Infra.Firestore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SpeechRecognition.Application.Interfaces;

namespace SpeechRecognition.Infra.Firestore
{
    // Implementação genérica de repositório para Firestore que espelha os métodos usados pelo RepositoryBase EF
    public class FirestoreRepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
        where TEntity : Entity<TId>, new()
    {
        private readonly FirestoreDb _db;
        private readonly string _collectionName;

        public FirestoreRepositoryBase(FirestoreDb db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _collectionName = ResolveCollectionName();
        }

        private string ResolveCollectionName()
        {
            var attr = typeof(TEntity).GetCustomAttributes(typeof(FirestoreCollectionAttribute), false)
                .FirstOrDefault() as FirestoreCollectionAttribute;
            if (attr != null && !string.IsNullOrWhiteSpace(attr.CollectionName))
                return attr.CollectionName;

            // fallback: nome da entidade em plural simples
            return typeof(TEntity).Name.EndsWith("s", StringComparison.OrdinalIgnoreCase)
                ? typeof(TEntity).Name
                : typeof(TEntity).Name + "s";
        }

        #region List and Get

        public async Task<IList<TEntity>> ListAsync(
            bool tracking = false,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            // Firestore não suporta todos os constructs de IQueryable genéricos.
            // Faz leitura da coleção e aplica filtro em memória quando predicate != null.
            var col = _db.Collection(_collectionName);
            var snap = await col.GetSnapshotAsync(cancellationToken);
            var list = snap.Documents.Select(d => ConvertSnapshot(d)).ToList();

            IEnumerable<TEntity> result = list;
            if (predicate != null)
            {
                // aplica em memória
                result = result.AsQueryable().Where(predicate);
            }

            if (orderBy != null)
            {
                result = orderBy(result.AsQueryable());
            }

            return result.ToList();
        }

        public async Task<TEntity?> GetByAsync(TId id, bool tracking = false, Func<IQueryable<TEntity>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            if (id == null) return default;
            var doc = _db.Collection(_collectionName).Document(IdToString(id));
            var snap = await doc.GetSnapshotAsync(cancellationToken);
            if (!snap.Exists) return default;
            return ConvertSnapshot(snap);
        }

        public async Task<TEntity?> GetByAsync(bool tracking = false, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            if (predicate == null)
            {
                // pega primeiro documento da coleção
                var col = _db.Collection(_collectionName);
                var snap = await col.Limit(1).GetSnapshotAsync(cancellationToken);
                var doc = snap.Documents.FirstOrDefault();
                return doc == null ? default : ConvertSnapshot(doc);
            }

            // fallback: buscar todos e aplicar predicate em memória
            var all = await ListAsync(false, null, null, null, cancellationToken);
            return all.AsQueryable().Where(predicate).FirstOrDefault();
        }

        #endregion

        #region Add

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var col = _db.Collection(_collectionName);

            if (!IsIdEmpty(entity.Id))
            {
                var doc = col.Document(IdToString(entity.Id!));
                await doc.SetAsync(EntityToDictionary(entity), cancellationToken: cancellationToken);
            }
            else
            {
                var added = await col.AddAsync(EntityToDictionary(entity), cancellationToken: cancellationToken);
                // tenta atribuir o id gerado ao entity.Id quando apropriado
                TrySetGeneratedId(entity, added.Id);
            }
        }

        #endregion

        #region Update

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var doc = _db.Collection(_collectionName).Document(IdToString(entity.Id!));
            // Firestore SetAsync em modo overwite - aqui usamos SetAsync sincrono via Task.Run, mas é preferível async.
            _ = doc.SetAsync(EntityToDictionary(entity));
        }

        #endregion

        #region Delete

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var doc = _db.Collection(_collectionName).Document(IdToString(entity.Id!));
            _ = doc.DeleteAsync();
        }

        #endregion

        #region Helpers

        private static bool IsIdEmpty(TId? id)
        {
            if (id == null) return true;
            if (id is Guid g) return g == Guid.Empty;
            if (id is string s) return string.IsNullOrWhiteSpace(s);
            if (id is long l) return l == 0L;
            if (id is int i) return i == 0;
            return false;
        }

        private static string IdToString(TId id)
        {
            return id?.ToString() ?? throw new ArgumentNullException(nameof(id));
        }

        private void TrySetGeneratedId(TEntity entity, string generatedId)
        {
            var idProp = typeof(TEntity).GetProperty("Id");
            if (idProp == null) return;

            var targetType = idProp.PropertyType;
            try
            {
                object? converted = null;
                if (targetType == typeof(string))
                    converted = generatedId;
                else if (targetType == typeof(Guid) || targetType == typeof(Guid?))
                    converted = Guid.Parse(generatedId);
                else if (targetType == typeof(int) && int.TryParse(generatedId, out var i))
                    converted = i;
                else if (targetType == typeof(long) && long.TryParse(generatedId, out var l))
                    converted = l;

                if (converted != null)
                    idProp.SetValue(entity, converted);
            }
            catch
            {
                // se falhar, não interrompe o fluxo
            }
        }

        private Dictionary<string, object?> EntityToDictionary(TEntity entity)
        {
            // Serializa o objeto e desserializa para dicionário para enviar ao Firestore.
            // Mantém o campo Id também (útil para buscas).
            var json = JsonSerializer.Serialize(entity, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = false });
            var dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json) ?? new Dictionary<string, object?>();
            // remove campos nulos indesejados se necessário
            return dict;
        }

        private TEntity ConvertSnapshot(DocumentSnapshot snap)
        {
            // tenta converter via ToDictionary + desserialização
            var dict = snap.ToDictionary();
            // adiciona o id do documento se não existir no dict
            if (!dict.ContainsKey("id"))
                dict["id"] = snap.Id;

            // Serializa o dict e desserializa para TEntity
            var json = JsonSerializer.Serialize(dict);
            var entity = JsonSerializer.Deserialize<TEntity>(json) ?? new TEntity();
            // garante que Id esteja populado (caso target property se chame Id)
            var idProp = typeof(TEntity).GetProperty("Id");
            if (idProp != null && (idProp.GetValue(entity) == null || IsIdEmpty((TId?)idProp.GetValue(entity)!)))
            {
                try
                {
                    if (idProp.PropertyType == typeof(string))
                        idProp.SetValue(entity, (object?)snap.Id);
                    else if (idProp.PropertyType == typeof(Guid))
                        idProp.SetValue(entity, Guid.Parse(snap.Id));
                    else if (idProp.PropertyType == typeof(long) && long.TryParse(snap.Id, out var lv))
                        idProp.SetValue(entity, lv);
                }
                catch { /* ignore */ }
            }
            return entity;
        }

        public Task<IList<TEntity>> ListAsync(bool tracking = false, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetByAsync(TId id, bool tracking = false, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetByAsync(bool tracking = false, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task IRepositoryBase<TEntity, TId>.Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        Task IRepositoryBase<TEntity, TId>.Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}