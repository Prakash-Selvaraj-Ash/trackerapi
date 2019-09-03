using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    public class CreateRouteRequest
    {
        public Guid SourceId { get; set; }
        public Guid DestinationId { get; set; }
    }
}
