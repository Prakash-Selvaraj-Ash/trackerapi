using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Repositories
{
    public class RouteRepository : RepositoryBase<Route>
    {
        public RouteRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
