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
using SpeechRecognition.Infra.FireStore.Repositories;

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

            if (!snapshot.Exists)
                return null;

            return snapshot.ConvertTo<TEntity>();
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
            await _collection.AddAsync(entity, cancellationToken);
        }

        #endregion

        #region Update

        public async Task Update(TEntity entity)
        {
            var id = FirestoreIdResolver.GetId(entity);

            var docRef = _collection.Document(id);

            await docRef.SetAsync(entity, SetOptions.Overwrite);
        }

        #endregion

        #region Delete

        public async Task Delete(TEntity entity)
        {
            var id = FirestoreIdResolver.GetId(entity);

            var docRef = _collection.Document(id);

            await docRef.DeleteAsync();
        }

        #endregion
    }
}