using System.Linq.Expressions;

namespace E.Infrastructure.Repository.Interfaces;

public interface IReadRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate);

    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    Task<T> GetByIdAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync();

    Task RemoveAsync(Guid id);

    Task UpdateAsync(Guid id, T entity);
}