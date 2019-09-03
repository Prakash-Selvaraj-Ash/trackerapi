using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Domains
{
    public class Student : IDomain, IWithId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Place Place { get; set; }
        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }
        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }
        public Guid RouteId { get; set; }
        public string FcmId { get; set; }
    }
}
