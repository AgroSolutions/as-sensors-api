using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_domain.Messaging.Interfaces
{
    public interface IMessageHandler<T>
    {
        Task HandleAsync(T message, CancellationToken cancellationToken = default);
    }
}
