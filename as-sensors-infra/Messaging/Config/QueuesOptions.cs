using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_infra.Messaging.Config
{
    public class QueuesOptions
    {
        public string CreateSensorQueue {  get; set; } = string.Empty;
        public string UpdateFieldStatusQueue { get; set; } = string.Empty;
    }
}
