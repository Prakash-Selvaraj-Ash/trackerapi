using System;
using System.Linq;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Extensions;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.Entity;
using BusTrackerApi.Services.GoogleMap;

namespace BusTrackerApi.Services.Bus
{
    public class BusService : BaseCrudService<Domains.Bus>, IBusService
    {
        private readonly IRepository<RouteAssociation> _routeAssociationRepository;
        private readonly IRepository<Domains.Place> _placeRepository;
        private readonly IRepository<BusTracker> _busTrackerRepository;
        private readonly IGoogleMapService _googleMapService;

        public BusService(
            IRepository<Domains.Bus> repository,
            IRepository<BusTracker> busTrackerRepository,
            IRepository<RouteAssociation> routeAssociationRepository,
            IRepository<Domains.Place> placeRepository,
            IGoogleMapService googleMapService,
            IEntityService entityService) : base(repository, entityService)
        {
            _routeAssociationRepository = routeAssociationRepository;
            _placeRepository = placeRepository;
            _busTrackerRepository = busTrackerRepository;
            _googleMapService = googleMapService;
        }

        public async Task<BusRouteResponse> GetBusRoute(Guid routeId)
        {
            var associatedPlace =
                (from busTracker in _busTrackerRepository.Set.Where(bt => bt.RouteId == routeId)

                join routeAssociation in _routeAssociationRepository.Set
                on busTracker.RouteId equals routeAssociation.RouteId

                join place in _placeRepository.Set
                on routeAssociation.PlaceId equals place.Id

                orderby routeAssociation.SequenceNumber

                select
                new
                {
                    place,
                    busTracker
                }).ToArray();

            var originPlace = associatedPlace.First().busTracker;
            var destinationPlace = associatedPlace.Last().place;
            var directions = await _googleMapService.GetDirections(
                new GeoCoordinateDto(originPlace.StartLattitude, originPlace.StartLongitude),
                associatedPlace.SkipLast(1).Select(p => new GeoCoordinateDto(p.place.Lattitude, p.place.Longitude)),
                new GeoCoordinateDto(destinationPlace.Lattitude, destinationPlace.Longitude));

            return new BusRouteResponse
            {
                Places = associatedPlace.Select(p => p.place).ToList<PlaceResponse>().ToArray(),
                GoogleDirection = directions
            };

        }
    }
}
