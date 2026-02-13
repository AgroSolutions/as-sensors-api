using as_sensors_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_infra.Persistance.Repository.Interfaces
{
    public interface ISensorRepository
    {
        Task<Sensor> InsertAsync(Sensor sensor, CancellationToken ct = default);

    }
}
