using System;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Repositories;
using System.Linq;
using BusTrackerApi.Services.Route;
using AutoMapper;
using BusTrackerApi.Services.Entity;

namespace BusTrackerApi.Services.Track
{
    public class TrackService : BaseCrudService<Domains.LiveTracker>, ITrackService
    {
        private readonly IRepository<BusTracker> _busTrackerRepository;
        private readonly IRepository<Domains.Place> _placeRepository;
        private readonly IRepository<Domains.Route> _routeRepository;

        public TrackService(
            IRepository<LiveTracker> liveTrackerRepository,
            IRepository<BusTracker> busTrackerRepository,
            IRepository<Domains.Place> placeRepository,
            IRepository<Domains.Route> routeRepository,
            IEntityService entityService) : base(liveTrackerRepository, entityService)
        {
            _busTrackerRepository = busTrackerRepository;
            _placeRepository = placeRepository;
            _routeRepository = routeRepository;
        }
        public TrackResponse AddTrack(LiveTracker addTrackRequest)
        {
            var liveTracker = Create(addTrackRequest);

            return GetTrack(liveTracker.BusId);
        }

        public TrackResponse GetTrack(Guid busId)
        {
            var trackResponse = from busTracker in _busTrackerRepository.Set.Where(bt => bt.BusId == busId)

                                join place in _placeRepository.Set
                                on busTracker.LastDestinationId equals place.Id

                                select new TrackResponse
                                {
                                    LastDestination = place.Name,
                                    LastDestinationCoordinate = new GeoCoordinateDto(place.Lattitude, place.Longitude),
                                    CurrentLocationCoordinate = new GeoCoordinateDto(busTracker.CurrentLattitude, busTracker.CurrentLongitude)
                                };


            var response = trackResponse.FirstOrDefault();
            return response;
        }
    }
}
