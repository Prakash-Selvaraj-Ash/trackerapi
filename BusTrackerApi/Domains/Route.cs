using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Domains
{
    public class Route : IDomain, IWithId
    {
        public Guid Id { get; set; }
        [ForeignKey("SourceId")]
        public virtual Place Source { get; set; }
        public Guid SourceId { get; set; }
        [ForeignKey("DestinationId")]
        public virtual Place Destination { get; set; }
        public Guid DestinationId { get; set; }
    }
}
