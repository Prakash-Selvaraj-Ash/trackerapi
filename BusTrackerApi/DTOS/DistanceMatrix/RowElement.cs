using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS.DistanceMatrix
{
    [DataContract]
    public class RowElement
    {
        [DataMember(Name = "duration")]
        public DurationResponse Duration { get; set; }
    }
}
