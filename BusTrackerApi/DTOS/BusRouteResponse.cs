using System.Runtime.Serialization;

namespace BusTrackerApi.DTOS
{
    [DataContract]
    public class BusRouteResponse
    {
        [DataMember(Name ="Places")]
        public PlaceResponse[] Places { get; set; }

        [DataMember(Name ="GoogleDirection")]
        public string GoogleDirection { get; set; }
    }
}
