using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace as_sensors_domain.Entities
{
    public class SensorData
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonRepresentation(BsonType.String)]
        public Guid SensorId { get; set; }
        public DateTime Date { get; set; }
        public double SoilMoisturePercentage { get; set; }
        public int TemperatureC { get; set; }
        public double PrecipitationLevelPercentage { get; set; }
    }
}
