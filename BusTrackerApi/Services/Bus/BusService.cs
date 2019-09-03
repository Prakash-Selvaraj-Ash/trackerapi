using BusTrackerApi.Repositories;
using BusTrackerApi.Services.Entity;

namespace BusTrackerApi.Services.Bus
{
    public class BusService : BaseCrudService<Domains.Bus>, IBusService
    {
        public BusService(
            IRepository<Domains.Bus> repository,
            IEntityService entityService) : base(repository, entityService)
        {
        }
    }
}
