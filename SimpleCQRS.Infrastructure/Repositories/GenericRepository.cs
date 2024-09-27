using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using SimpleCQRS.Domain.Interfaces.Repositories;
using SimpleCQRS.Infrastructure.Data;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace SimpleCQRS.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SimpleCqrsContext _context;

        /// <summary>
        /// ILogger<GenericRepository<T>>
        /// </summary>
        ///private readonly ILogger<GenericRepository<T>> _logger;

        /// <summary>
        /// DbSet<T>
        /// </summary>
        private readonly DbSet<T> _dbSet;

        public GenericRepository(SimpleCqrsContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="predicate">Expression<Func<T, bool>>?</param>
        /// <param name="orderBy">Func<IQueryable<T></param>
        /// <param name="include">IOrderedQueryable<T>>?</param>
        /// <param name="disableTracking">Func<IQueryable<T>, IIncludableQueryable<T, object?>>?</param>
        /// <returns>T?</returns>
        public async Task<T?> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }



        /// <summary>
        /// Get collection of items
        /// </summary>
        /// <param name="predicate">Expression<Func<T, bool>>?</param>
        /// <param name="orderBy">Func<IQueryable<T></param>
        /// <param name="include">IOrderedQueryable<T>>?</param>
        /// <param name="disableTracking">Func<IQueryable<T>, IIncludableQueryable<T, object?>>?</param>
        /// <returns>IEnumerable<T></returns>
        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, bool disableTracking = true)
        {
             IQueryable<T> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }


        /// <summary>
        /// Added new item to the Entity
        /// </summary>
        /// <param name="entity">T</param>
        /// <returns></returns>
        public async Task AddedAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            //_logger.LogInformation($"Adding new entity {EntityType.Name}");
        }

        /// <summary>
        /// Added new collection of items to the Entity
        /// </summary>
        /// <param name="entities">IEnumerable<T></param>
        /// <returns></returns>
        public async Task AddedAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);

            //_logger.LogInformation($"Adding {entities.Count()} entity of {EntityType.Name}");
        }


        /// <summary>
        /// Delete item by id
        /// </summary>
        /// <param name="id">id item</param>
        public void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
            }

         }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="entity">TEntity</param>
        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

           
        }

        /// <summary>
        /// Delete collection of items
        /// </summary>
        /// <param name="entities">IEnumerable<TEntity></param>
        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Update collection of items
        /// </summary>
        /// <param name="entities">IEnumerable<TEntity></param>
        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
         }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="entity">TEntity</param>
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Get count item for an entity
        /// </summary>
        /// <param name="predicate">Expression<Func<TEntity, bool>>?</param>
        /// <returns>int</returns>
        public async Task<int> GetCount(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync();
        }
    }
}
