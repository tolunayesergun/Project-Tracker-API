using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectTracker_API.DataAccess.RepositoryBases.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region AsyncFunctions

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> GetByIdAsync(int id);

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<List<TEntity>> GetListWithJoinAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);

        Task AddStackAsync(TEntity entity);

        Task AddListAsync(List<TEntity> entityList);

        Task DeleteStackAsync(TEntity entity);

        Task<bool> AddAsync(TEntity entity);

        Task<int> AddAsyncReturnId(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        #endregion AsyncFunctions

        #region SyncFunctions

        TEntity Get(Expression<Func<TEntity, bool>> filter = null);

        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        #endregion SyncFunctions
    }
}