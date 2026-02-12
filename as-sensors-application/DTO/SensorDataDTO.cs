using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_application.DTO
{
    public class SensorDataDTO
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public double SoilMoisturePercentage { get; set; }
        public int TemperatureC {  get; set; }
        public double PrecipitationLevelPercentage { get; set; }
    }
}
