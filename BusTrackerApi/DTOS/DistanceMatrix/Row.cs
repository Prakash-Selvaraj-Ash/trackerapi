using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS.DistanceMatrix
{
    [DataContract]
    public class Row
    {
        [DataMember(Name = "elements")]
        public IEnumerable<RowElement> RowElements { get; set; }
    }

}
