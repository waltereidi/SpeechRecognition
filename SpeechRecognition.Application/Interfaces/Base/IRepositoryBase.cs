using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SpeechRecognition.Application.Interfaces
{
    public interface IRepositoryBase<TEntity, in TId> where TEntity : class
    {
        #region Get and List

        Task<IList<TEntity>> ListAsync(
            bool tracking = false,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default);

        Task<TEntity?> GetByAsync(
            TId id,
            bool tracking = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default);

        Task<TEntity?> GetByAsync(
            bool tracking = false,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region Add

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        #endregion

        #region Update

        Task Update(TEntity entity);

        #endregion

        #region Delete

        Task Delete(TEntity entity);

        #endregion
    }
}

#region Explicação Contravariância

//public interface IHandler<T>
//{
//    void Handle(T item);
//}

//IHandler<string> h = new StringHandler(); // OK
//IHandler<object> o = h; // ERRO

//---------------------------------------------

//public interface IHandler<in T>
//{
//    void Handle(T item);
//}

//IHandler<string> h = new StringHandler(); // OK
//IHandler<object> o = h; // SUCESSO

#endregion


