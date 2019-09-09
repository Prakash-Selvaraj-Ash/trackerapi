using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    [DataContract]
    public class UpdateCurrentLocationDto
    {
        [DataMember(Name = "BusId")]
        public Guid BusId { get; set; }
        [DataMember(Name = "CurrentLocation")]
        public GeoCoordinateDto CurrentLocation { get; set; }
    }
}
