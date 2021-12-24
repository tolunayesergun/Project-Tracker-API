using Microsoft.EntityFrameworkCore;
using ProjectTracker_API.DataAccess.RepositoryBases.Abstract;
using ProjectTracker_API.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectTracker_API.DataAccess.RepositoryBases.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        #region AsyncFunctions

        public async Task AddStackAsync(TEntity entity)
        {
            using var context = new DataBaseContext();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddListAsync(List<TEntity> entityList)
        {
            using var context = new DataBaseContext();
            await context.AddRangeAsync(entityList);
            await context.SaveChangesAsync();
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            try
            {
                using var context = new DataBaseContext();
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch{ return false; }
        }

        public async Task<int> AddAsyncReturnId(TEntity entity)
        {
            try
            {
                using var context = new DataBaseContext();
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return entity.Id;
            }
            catch { return -1; }
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new DataBaseContext();
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<List<TEntity>> GetListWithJoinAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            using var context = new DataBaseContext();
            return filter == null
                ? await context.Set<TEntity>().IncludeMultiple(includes).ToListAsync()
                : await context.Set<TEntity>().IncludeMultiple(includes).Where(filter).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new DataBaseContext();

            return filter == null
              ? await context.Set<TEntity>().SingleOrDefaultAsync()
              : await context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            using var context = new DataBaseContext();
            var entity = await context.Set<TEntity>().FindAsync(id);

            if (entity != null) { context.Entry(entity).State = EntityState.Detached; }

            return entity;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                using var context = new DataBaseContext();
                context.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeleteStackAsync(TEntity entity)
        {
            using var context = new DataBaseContext();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using var context = new DataBaseContext();
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        #endregion AsyncFunctions

        #region SyncFunctions

        public void Add(TEntity entity)
        {
            using var context = new DataBaseContext();
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using var context = new DataBaseContext();
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new DataBaseContext();

            return filter == null
              ? context.Set<TEntity>().SingleOrDefault()
              : context.Set<TEntity>().SingleOrDefault(filter);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new DataBaseContext();
            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }

        public void Update(TEntity entity)
        {
            using var context = new DataBaseContext();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }

        #endregion SyncFunctions
    }

    public static class RepositoryExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            return includes == null ? query : includes.Aggregate(query, (current, include) => current.Include(include));
        }
    }
}