using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_application.DTO
{
    public class SensorDataDTO
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public DateTime Date { get; set; }
        public double SoilMoisturePercentage { get; set; }
        public int TemperatureC {  get; set; }
        public double PrecipitationLevelPercentage { get; set; }
    }
}
