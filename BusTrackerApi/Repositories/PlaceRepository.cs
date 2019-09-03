using BusTrackerApi.DbConnector;
using BusTrackerApi.Domains;

namespace BusTrackerApi.Repositories
{
    public class PlaceRepository : RepositoryBase<Place>
    {
        public PlaceRepository(IQueryableConnector queryableConnector) : base(queryableConnector)
        {
        }
    }
}
