using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace as_sensors_application.DTO
{
    public class SensorDataDTOResponse
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public DateTime Date { get; set; }
        public double SoilMoisturePercentage { get; set; }
        public int TemperatureC { get; set; }
        public double PrecipitationLevelPercentage { get; set; }
    }
}
