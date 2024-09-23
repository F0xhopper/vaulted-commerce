// DataAccessLayer/MongoDbContext.cs
using MongoDB.Driver;
using vaulted_commerce.DataAccessLayer.Entities;

namespace vaulted_commerce.DataAccessLayer.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dbName);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    }
}
