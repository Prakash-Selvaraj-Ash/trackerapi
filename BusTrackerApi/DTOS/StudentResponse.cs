using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    [DataContract]
    public class StudentResponse
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }
        [DataMember(Name="Name")]
        public string Name { get; set; }
        [DataMember(Name = "Place")]
        public PlaceResponse Place { get; set; }
        [DataMember(Name = "RouteId")]
        public Guid RouteId { get; set; }
    }
}
