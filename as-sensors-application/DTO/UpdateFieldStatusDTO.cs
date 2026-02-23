using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_application.DTO
{
    public class UpdateFieldStatusDTO
    {
        public required Guid FieldId {  get; set; }
        public required string Status { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
