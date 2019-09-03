using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    public class CreateRoutePlaceRequest
    {
        public Guid RouteId { get; set; }
        public Guid PlaceId { get; set; }
        public int SequenceNumber { get; set; }
    }
}
