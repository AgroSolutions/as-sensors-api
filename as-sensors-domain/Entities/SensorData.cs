using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace as_sensors_domain.Entities
{
    public class SensorData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public DateTime Date { get; set; }
        public double SoilMoisturePercentage { get; set; }
        public int TemperatureC { get; set; }
        public double PrecipitationLevelPercentage { get; set; }
    }
}
