using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_domain.Messaging.Interfaces
{
    public interface IQueueConsumer
    {
        Task StartAsync<T>(string queueName, IMessageHandler<T> handler,
            CancellationToken cancellationToken = default);
    }
}
