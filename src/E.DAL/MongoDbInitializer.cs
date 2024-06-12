using MongoDB.Driver;

namespace E.DAL;

public class MongoDbInitializer
{
    private readonly MongoDbContext _context;

    public MongoDbInitializer(MongoDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        var collections = typeof(MongoDbContext).GetProperties()
            .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(IMongoCollection<>));

        foreach (var collectionProperty in collections)
        {
            var collectionName = collectionProperty.Name;
            var entityType = collectionProperty.PropertyType.GetGenericArguments()[0];
            var collectionExists = _context.Database.ListCollectionNames().ToList().Contains(collectionName);

            if (!collectionExists)
            {
                _context.Database.CreateCollection(collectionName);
            }
        }
    }
}
