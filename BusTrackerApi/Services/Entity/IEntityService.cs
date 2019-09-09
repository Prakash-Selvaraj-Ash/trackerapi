using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.Entity
{
    public interface IEntityService
    {
        void Save();
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
