﻿using System.Runtime.Serialization;

namespace BusTrackerApi.DTOS
{
    [DataContract]
    public class CreatePlaceRequest
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Lattitude")]
        public double Lattitude { get; set; }
        [DataMember(Name = "Lattitude")]
        public double Longitude { get; set; }
    }
}
