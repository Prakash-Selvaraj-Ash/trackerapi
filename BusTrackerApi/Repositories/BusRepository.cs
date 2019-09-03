using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;
using System;
using System.Linq;

namespace BusTrackerApi.Repositories
{
    public class BusRepository : RepositoryBase<Bus>
    {
        public BusRepository(IQueryableConnector queryableConnector) : base(queryableConnector) { }
    }
}
