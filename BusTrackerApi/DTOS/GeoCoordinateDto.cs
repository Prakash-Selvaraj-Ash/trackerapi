using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    public class GeoCoordinateDto
    {
        public GeoCoordinateDto(double lattitude, double longitude)
        {
            Lattitude = lattitude;
            Longitude = longitude;
        }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Lattitude},{Longitude}";
        }
    }
}
