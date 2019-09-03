using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;

namespace BusTrackerApi.Repositories
{
    public class BusTrackerRepository : RepositoryBase<BusTracker>
    {
        public BusTrackerRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
