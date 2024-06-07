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