using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_domain.Messaging.Interfaces
{
    public interface IQueuePublisher
    {
        Task PublishAsync<T>(T message, string queueName, string? exchange = null, CancellationToken cancellationToken = default);
    }
}
