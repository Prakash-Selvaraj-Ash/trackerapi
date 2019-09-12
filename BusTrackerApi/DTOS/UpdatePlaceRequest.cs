using System;
using System.Runtime.Serialization;

namespace BusTrackerApi.DTOS
{
    [DataContract]
    public class UpdatePlaceRequest
    {
        [DataMember(Name = "PlaceId")]
        public Guid PlaceId { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Lattitude")]
        public double Lattitude { get; set; }
        [DataMember(Name = "Longitude")]
        public double Longitude { get; set; }
    }
}
