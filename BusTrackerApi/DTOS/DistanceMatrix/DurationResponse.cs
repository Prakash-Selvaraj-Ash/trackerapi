using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS.DistanceMatrix
{
    [DataContract]
    public class DurationResponse
    {
        [DataMember(Name = "text")]
        public string Mins { get; set; }

        [DataMember(Name = "value")]
        public double Seconds { get; set; }
    }
}
