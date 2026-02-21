using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_application.DTO
{
    public class SensorDTOResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid FieldId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
