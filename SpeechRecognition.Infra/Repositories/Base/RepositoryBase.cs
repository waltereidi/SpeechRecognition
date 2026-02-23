using Microsoft.EntityFrameworkCore;
using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.CrossCutting.Framework;
using System.Linq.Expressions;

namespace SpeechRecognition.Infra.Repositories.Base
{

    public class RepositoryBase<TContext, TEntity , TId>(TContext context) : IRepositoryBase<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TContext : DbContext
    {
        protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

        #region List and Get
        public async Task<IList<TEntity>> ListAsync(bool tracking = false, 
            Expression<Func<TEntity, bool>>? predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
            CancellationToken cancellationToken = default
        )
        {
            var query = BuildQuery(tracking, predicate, orderBy);
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByAsync(TId id, bool tracking = false, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
            CancellationToken cancellationToken = default
        )
        {
            var query = BuildQuery(tracking, p => p.Id!.Equals(id), orderBy);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<TEntity?> GetByAsync(bool tracking = false, 
            Expression<Func<TEntity, bool>>? predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
            CancellationToken cancellationToken = default
        )
        {
            var query = BuildQuery(tracking, predicate, orderBy);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
        #endregion

        #region Add

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
        }

        #endregion

        #region Update

        public async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        #endregion

        #region Delete

        public async Task Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        #endregion

        #region Helper

        private IQueryable<TEntity> BuildQuery(
            bool tracking,
            Expression<Func<TEntity, bool>>? predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy)
        {
            //1 - AsNoTracking
            var query = tracking
                ? DbSet.AsQueryable()
                : DbSet.AsNoTracking();

            //2 - Where (predicate)
            if (predicate is not null)
                query = query.Where(predicate);


            //4 - OrderBy
            if (orderBy is not null)
                query = orderBy(query);

            return query;
        }



        #endregion
    }
}
