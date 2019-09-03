using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    public class AddTrackRequest
    {
        public Guid BusId { get; set; }
        public Guid StudentId { get; set; }
        public string ConnectionId { get; set; }
    }
}
