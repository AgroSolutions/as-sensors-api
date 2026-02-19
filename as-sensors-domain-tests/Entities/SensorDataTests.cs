using as_sensors_domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Xunit;

namespace as_sensors_domain_tests.Entities
{
    public class SensorDataTests
    {
        [Fact]
        public void SensorData_Id_is_auto_generated_on_new_instance()
        {
            // Arrange / Act
            var entity = new SensorData();

            // Assert
            Assert.NotEqual(Guid.Empty, entity.Id);
        }

        [Fact]
        public void SensorData_Id_and_SensorId_are_serialized_as_string()
        {
            // Arrange
            var entity = new SensorData
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                SensorId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Date = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                SoilMoisturePercentage = 12.34,
                TemperatureC = 25,
                PrecipitationLevelPercentage = 56.78
            };

            // Act
            var doc = entity.ToBsonDocument();

            // Assert: _id
            Assert.True(doc.Contains("_id"));
            Assert.Equal(BsonType.String, doc["_id"].BsonType);
            Assert.Equal(entity.Id.ToString(), doc["_id"].AsString);

            // Assert: SensorId
            Assert.True(doc.Contains("SensorId"));
            Assert.Equal(BsonType.String, doc["SensorId"].BsonType);
            Assert.Equal(entity.SensorId.ToString(), doc["SensorId"].AsString);
        }
    }
}
