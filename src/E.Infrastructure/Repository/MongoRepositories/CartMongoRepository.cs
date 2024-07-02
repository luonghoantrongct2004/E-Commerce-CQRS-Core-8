using E.Domain.Entities.Carts;
using E.Infrastructure.Repository.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace E.Infrastructure.Repository.MongoRepositories;

public class CartMongoRepository : IReadRepository<CartDetails>
{
    private readonly IMongoCollection<CartDetails> _collection;

    public CartMongoRepository(IMongoDatabase database, string collection)
    {
        _collection = database.GetCollection<CartDetails>(collection);
    }

    public async Task AddAsync(CartDetails entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task<CartDetails> FirstOrDefaultAsync(Expression<Func<CartDetails, bool>> predicate)
    {
        return await _collection.Find(predicate).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CartDetails>> GetAllAsync()
    {
        return await _collection.Find(Builders<CartDetails>.Filter.Empty).ToListAsync();
    }

    public async Task<CartDetails> GetByIdAsync(Guid id)
    {
        return await _collection.Find(Builders<CartDetails>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(Guid productId)
    {
        var result = await _collection.DeleteOneAsync(Builders<CartDetails>.Filter.Eq("ProductId", productId));
        if (result.DeletedCount == 0)
        {
            throw new Exception($"Not found Id {productId}");
        }
    }

    public async Task UpdateAsync(Guid id, CartDetails entity)
    {
        var result = await _collection.ReplaceOneAsync(Builders<CartDetails>.Filter.Eq("Id", id), entity);
        if (result.ModifiedCount == 0)
        {
            throw new Exception($"Not found Id {id}");
        }
    }

    public async Task RemoveProductFromCartAsync(Guid userId, Guid productId)
    {
        var filter = Builders<CartDetails>.Filter.And(
            Builders<CartDetails>.Filter.Eq(c => c.UserId, userId),
            Builders<CartDetails>.Filter.ElemMatch(c => c.Products, p => p.Id == productId)
        );

        var update = Builders<CartDetails>.Update.PullFilter(c => c.Products, p => p.Id == productId);

        var result = await _collection.UpdateOneAsync(filter, update);

        if (result.ModifiedCount == 0)
        {
            throw new Exception($"Product with Id {productId} not found in the cart of UserId {userId}");
        }
    }
}
