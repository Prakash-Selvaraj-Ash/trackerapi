using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.Entity;

namespace BusTrackerApi.Services.Place
{
    public class PlaceService : BaseCrudService<Domains.Place>, IPlaceService
    {
        public PlaceService(
            IRepository<Domains.Place> repository,
            IEntityService entityService) : base(repository, entityService)
        {
        }
    }
}
