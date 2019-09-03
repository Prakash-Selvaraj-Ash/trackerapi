using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Domains
{
    public class RouteAssociation : IDomain, IWithId
    {
        public Guid Id { get; set; }
        [ForeignKey("PlaceId")]
        public virtual Place Place { get; set; }
        public Guid PlaceId { get; set; }
        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }
        public Guid RouteId { get; set; }
        public int SequenceNumber { get; set; }
    }
}
