using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Context.MongoDb;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDbSettings:DbConnection");
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}