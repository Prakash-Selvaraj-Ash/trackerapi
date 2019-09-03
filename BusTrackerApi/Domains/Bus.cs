using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Domains
{
    public class Bus : IDomain, IWithId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
