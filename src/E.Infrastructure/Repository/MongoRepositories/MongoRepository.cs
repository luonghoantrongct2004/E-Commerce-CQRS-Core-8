using E.Infrastructure.Repository.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace E.Infrastructure.Repository.MongoRepositories;

public class MongoRepository<T> : IReadRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDatabase database, string collection)
    {
        _collection = database.GetCollection<T>(collection);
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }
    public IQueryable<T> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public async Task<List<T>> FindAll(string option = null,
        string searchString = null, string sortBy = null,
        string sortDirection = "asc")
    {
        var query = _collection.AsQueryable();
        if (!string.IsNullOrEmpty(option) && !string.IsNullOrEmpty(searchString))
        {
            query = query.Where(BuildFilter(option, searchString));
        }
        if (!string.IsNullOrEmpty(sortBy))
        {
            query = (IMongoQueryable<T>)ApplySorting(query, sortBy, sortDirection);
        }

        return await query.ToListAsync();
    }


    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _collection.Find(predicate).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        var result = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));

        if (result.DeletedCount == 0)
        {
            throw new Exception($"Not found Id {id}");
        }
    }

    public async Task UpdateAsync(Guid id, T entity)
    {
        var result = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);
        if (result.ModifiedCount == 0)
        {
            throw new Exception($"Not found Id {id}");
        }
    }

    public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }
    private static Expression<Func<T, bool>> BuildFilter(string option,
        string searchString)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, option);
        var constant = Expression.Constant(searchString);
        var body = Expression.Equal(property, constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private static IQueryable<T> ApplySorting(IQueryable<T> query, string sortBy,
        string sortDirection)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, sortBy);
        var lamda = Expression.Lambda(property, parameter);

        var methodName = sortDirection.ToLower() == "desc"
            ? "OrderByDescending" : "OrderBy";
        var result = Expression.Call(typeof(Queryable), methodName,
            new Type[] { query.ElementType, property.Type },
            query.Expression, Expression.Quote(lamda));

        return query.Provider.CreateQuery<T>(result);
    }
}