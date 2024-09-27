using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SimpleCQRS.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetAsync(Expression<Func<T, bool>>? predicate = null, 
            Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null, 
            Func<IQueryable<T>,IIncludableQueryable<T, object?>>? include = null,bool disableTracking = true);

        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, bool disableTracking = true);


        Task AddedAsync(T entity);
        Task AddedAsync(IEnumerable<T> entities);

        void Update(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(object id);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        Task<int> GetCount(Expression<Func<T, bool>>? predicate = null);
    }
}
