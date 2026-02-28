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
            var mongoSettings = settings.Value;

            var client = new MongoClient(mongoSettings.ConnectionString);
            Database = client.GetDatabase(mongoSettings.DatabaseName);
        }
    }
}
