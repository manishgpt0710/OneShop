using System;
using System.Linq.Expressions;
using OneShop.Domain.Common;

namespace OneShop.Application.Common.Interfaces
{
    public interface IRepository<T> where T : BaseEntity 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}

