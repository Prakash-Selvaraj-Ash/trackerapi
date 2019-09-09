using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    [DataContract(Name = "PlaceWithETAResponse")]
    public class PlaceWithETAResponse
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Lattitude")]
        public double Lattitude { get; set; }

        [DataMember(Name = "Longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "Duration")]
        public double Duration { get; set; }
    }
}
