using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dashboard.Core.Interfaces.Repositories
{
    /// <summary>
    /// Entity Framework repository for db model
    /// </summary>
    public interface IEfRepository<T> : IDisposable
        where T : class
    {
        Task<T> AddAsync(T t);
        Task<int> CountAsync();
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindOneByAsync(Expression<Func<T, bool>> match);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T t, object key);
        Task<int> SaveAsync();
    }
}
