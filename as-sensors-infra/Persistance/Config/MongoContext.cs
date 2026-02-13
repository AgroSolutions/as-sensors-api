using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace as_sensors_infra.Persistance.Config
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient("mongodb+srv://fiap:admin123@cluster0.lzxq3kz.mongodb.net/?appName=Cluster0");
            Database = client.GetDatabase("sensor");
        }
    }
}
