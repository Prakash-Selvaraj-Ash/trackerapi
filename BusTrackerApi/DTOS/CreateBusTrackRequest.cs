using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    public class CreateBusTrackRequest
    {
        public Guid BusId { get; set; }
        public Guid RouteId { get; set; }
        public Guid LastDestinationId { get; set; }
        public double CurrentLattitude { get; set; }
        public double CurrentLongitude { get; set; }
    }
}
