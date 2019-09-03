using System;
using System.Linq;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.Entity;

namespace BusTrackerApi.Services.Route
{
    public class RouteService : BaseCrudService<Domains.Route>, IRouteService
    {
        private readonly IRepository<Domains.Route> _repository;
        private readonly IRepository<RouteAssociation> _routeAssociationRepository;
        private readonly IRepository<Domains.Place> _placeRepository;
        private readonly IEntityService _entityService;

        public RouteService(
            IRepository<Domains.Route> repository,
            IRepository<RouteAssociation> routeAssociationRepository,
            IRepository<Domains.Place> placeRepository,
            IEntityService entityService) : base(repository, entityService)
        {
            _repository = repository;
            _routeAssociationRepository = routeAssociationRepository;
            _placeRepository = placeRepository;
            _entityService = entityService;
        }

        public void AddPlace(RouteAssociation routeAssociation)
        {
            _routeAssociationRepository.Create(routeAssociation);
            _entityService.Save();
        }

        public Domains.Place[] ReadPlaces(Guid routeId)
        {
            var associatedPlace = from routeAssociation in _routeAssociationRepository.Set
                                  where routeAssociation.RouteId == routeId

                                  join place in _placeRepository.Set
                                  on routeAssociation.PlaceId equals place.Id
                                  orderby routeAssociation.SequenceNumber

                                  select place;

            return associatedPlace.ToArray();
        }
    }
}
