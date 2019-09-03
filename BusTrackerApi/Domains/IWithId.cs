using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Domains
{
    public interface IWithId
    {
        Guid Id { get; set; }
    }
}
