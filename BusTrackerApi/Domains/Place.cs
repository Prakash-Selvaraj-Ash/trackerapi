using System;
using System.Collections;
using System.Collections.Generic;

namespace BusTrackerApi.Domains
{
    public class Place : IDomain, IWithId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
