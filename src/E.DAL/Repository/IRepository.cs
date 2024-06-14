using MongoDB.Driver;
using System.Linq.Expressions;

namespace E.DAL.Repository;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity);

    Task<T> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync();
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    void Update(T entity);

    void Remove(T entity);
}
public interface IReadRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task RemoveAsync(Guid id);
    Task UpdateAsync(Guid id, T entity);
}