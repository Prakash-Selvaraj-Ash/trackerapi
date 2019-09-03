using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.Entity;

namespace BusTrackerApi.Services.BusTrack
{
    public class BusTrackService : BaseCrudService<BusTracker>, IBusTrackService
    {
        public BusTrackService(
            IRepository<BusTracker> repository, 
            IEntityService entityService) : base(repository, entityService)
        {
        }
    }
}
