using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using TodoApp.Models;

namespace TodoApp.Models
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        static MongoDbContext()
        {
            BsonSerializer.RegisterSerializer(new ToDoItemSerializer());
        }
        
        public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
        {
            try
            {
                var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
                _database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to MongoDB: {ex.Message}");
                throw; // Rethrow the exception to ensure it's not silently ignored
            }
        }

        public IMongoCollection<ToDoItem> Todos
        {
            get { return _database.GetCollection<ToDoItem>("Todos"); }
        }
    }
}
