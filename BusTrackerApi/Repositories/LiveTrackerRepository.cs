using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Repositories
{
    public class LiveTrackerRepository : RepositoryBase<LiveTracker>
    {
        public LiveTrackerRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
