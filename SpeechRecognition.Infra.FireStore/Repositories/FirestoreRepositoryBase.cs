using Google.Cloud.Firestore;
using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.Infra.Firestore.Attributes;
using SpeechRecognition.Infra.FireStore.Documents;
using SpeechRecognition.Infra.FireStore.Documents.Base;
using SpeechRecognition.Infra.FireStore.Mapping;
using SpeechRecognition.Infra.FireStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static Grpc.Core.Metadata;

namespace SpeechRecognition.Infra.Firestore
{
    // Implementação genérica de repositório para Firestore que espelha os métodos usados pelo RepositoryBase EF

    public class FirestoreRepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
        where TEntity : class
    {
        protected readonly FirestoreDb _db;
        protected readonly CollectionReference _collection;

        public FirestoreRepositoryBase( FirestoreDb db )
        {
            _db = db;
            _collection = _db.Collection( nameof(TEntity) );
        }

        #region Get and List

        public async Task<IList<TEntity>> ListAsync(
            bool tracking = false,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            Query query = _collection;

            query = FirestoreQueryBuilder.ApplyPredicate(query, predicate);

            var snapshot = await query.GetSnapshotAsync(cancellationToken);

            var result = snapshot.Documents
                .Select(d => d.ConvertTo<TEntity>())
                .ToList();

            if (orderBy != null)
                result = orderBy(result.AsQueryable()).ToList();

            return result;
        }

        public async Task<TEntity?> GetByAsync(
            TId id,
            bool tracking = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            var docRef = _collection.Document(id!.ToString());

            var snapshot = await docRef.GetSnapshotAsync(cancellationToken);

            // 2. Converte para Aggregate (manual)
            var mapper = new FireStoreMapperCommand<TEntity>();

            return mapper.MapToDomain<TEntity>(snapshot);
        }

        public async Task<TEntity?> GetByAsync(
            bool tracking = false,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            var list = await ListAsync(tracking, predicate, orderBy, cancellationToken);

            return list.FirstOrDefault();
        }

        #endregion

        #region Add

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                var mapper = new FireStoreMapperCommand<TEntity>();

                var doc =mapper.MapToDocument(entity);
                // tentativa padrão (usando o conversor do SDK)
                await _collection
                    .Document(doc.Id) // 👈 usa o ID do seu document
                    .SetAsync(doc, cancellationToken: cancellationToken);
                return;
            }
            catch (Exception ex)
            {
                // se falhar devido à incapacidade de criar um converter, faz fallback para dicionário
                // Não tentamos capturar tipos de exceção específicos do SDK para não depender de mensagens internas,
                // mas você pode filtrar pelo tipo/InnerException se desejar.
            }

        }
        //public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        //{
        //    await _collection.AddAsync(entity, cancellationToken);
        //}
        private static Dictionary<string, object?> EntityToDictionary(TEntity entity)
        {
            // Serializa para JSON camelCase e desserializa para Dictionary<string, object?>
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                // opcional: permitir ciclos se necessário
                // ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json = JsonSerializer.Serialize(entity, options);
            var dict = JsonSerializer.Deserialize<Dictionary<string, object?>>(json, options);
            return dict ?? new Dictionary<string, object?>();
        }
        #endregion

        #region Update

        public async Task Update(TEntity entity)
        {
            var mapper = new FireStoreMapperCommand<TEntity>();

            var doc = mapper.MapToDocument(entity);

            var docRef = _collection.Document(doc.Id);

            await docRef.SetAsync(doc, SetOptions.Overwrite);
        }

        #endregion

        #region Delete

        public async Task Delete(TEntity entity)
        {
            var mapper = new FireStoreMapperCommand<TEntity>();

            var doc = mapper.MapToDocument(entity);

            var docRef = _collection.Document(doc.Id);

            await docRef.DeleteAsync();
        }

        #endregion
    }
}