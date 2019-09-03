using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Repositories
{
    public class RouteAssociationRepository : RepositoryBase<RouteAssociation>
    {
        public RouteAssociationRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
