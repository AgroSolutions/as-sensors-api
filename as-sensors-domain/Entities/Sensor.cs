using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace as_sensors_domain.Entities
{
    public class Sensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid FieldId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
