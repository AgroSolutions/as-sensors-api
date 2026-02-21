using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_application.DTO
{
    public class SensorDTOResquest
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid FieldId { get; set; }
    }
}
