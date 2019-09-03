using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    public class CreatePlaceRequest
    {
        public string Name { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
