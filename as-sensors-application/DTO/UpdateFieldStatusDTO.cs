using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_application.DTO
{
    public class UpdateFieldStatusDTO
    {
        public required Guid fieldId {  get; set; }
        public required string status { get; set; }
        public required DateTime date { get; set; }
    }
}
