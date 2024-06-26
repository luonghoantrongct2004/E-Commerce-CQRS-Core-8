using MongoDB.Driver;
using System.Collections;
using System.Linq.Expressions;

namespace E.DAL.Repository;

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
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
    }

    public async Task UpdateAsync(Guid id, T entity)
    {
        var result = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);
    }
}