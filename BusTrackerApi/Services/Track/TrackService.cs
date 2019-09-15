using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.DTOS.DistanceMatrix;
using BusTrackerApi.Extensions;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.Entity;
using BusTrackerApi.Services.GoogleMap;
using BusTrackerApi.Services.Route;
using Newtonsoft.Json;

namespace BusTrackerApi.Services.Track
{
    public class TrackService : BaseCrudService<LiveTracker>, ITrackService
    {
        private readonly IRepository<BusTracker> _busTrackerRepository;
        private readonly IRepository<Domains.Student> _studentRepository;
        private readonly IRepository<LiveTracker> _liveTrackerRepository;
        private readonly IRouteService _routeService;
        private readonly IGoogleMapService _googleMapService;
        private readonly IEntityService _entityService;

        public TrackService(
            IRepository<LiveTracker> liveTrackerRepository,
            IRepository<BusTracker> busTrackerRepository,
            IRepository<Domains.Place> placeRepository,
            IRepository<Domains.Student> studentRepository,
            IGoogleMapService googleMapService,
            IRouteService routeService,
            IEntityService entityService) : base(liveTrackerRepository, entityService)
        {
            _busTrackerRepository = busTrackerRepository;
            _studentRepository = studentRepository;
            _entityService = entityService;
            _liveTrackerRepository = liveTrackerRepository;
            _routeService = routeService;
            _googleMapService = googleMapService;
        }

        public void AddOrUpdateLiveTracker(string userId, string connectionId)
        {
            Guid studentId = Guid.Parse(userId);
            var studentDetail = _studentRepository.ReadById(studentId);
            var busTracker = _busTrackerRepository.Set.SingleOrDefault(bt => bt.RouteId == studentDetail.RouteId);

            if (busTracker == null) { return; }

            var liveTracker = _liveTrackerRepository.Set.SingleOrDefault(lt => lt.BusId == busTracker.BusId && lt.StudentId == studentId);

            if (liveTracker != null)
            {
                liveTracker.ConnectionId = connectionId;
                _entityService.Save();
                return;
            }

            Create(new LiveTracker { StudentId = studentId, BusId = busTracker.BusId, ConnectionId = connectionId });
        }

        public async Task<TrackResponse> AddTrack(LiveTracker addTrackRequest)
        {
            var liveTracker = Create(addTrackRequest);

            return await GetTrack(liveTracker.BusId);
        }

        public TrackResponse GetCachedTrack(Guid busId)
        {
            var tracker = _busTrackerRepository.Set.Single(bt => bt.BusId == busId);

            var response = new TrackResponse
            {
                CurrentLocationCoordinate = new GeoCoordinateDto(tracker.CurrentLattitude, tracker.CurrentLongitude),
                LastDestination = tracker.LastDestination?.To<PlaceResponse>(),
                Places = JsonConvert.DeserializeObject<IEnumerable<PlaceWithETAResponse>>(tracker.CurrentRouteStatus)
            };

            return response;
        }

        public async Task<TrackResponse> GetTrack(Guid busId)
        {
            var dbBusTracker = _busTrackerRepository.Set.Single(bt => bt.BusId == busId);
            var places = _routeService.ReadPlaces(dbBusTracker.RouteId);

            var placesWithEta = (await GetPlacesWithEta(places, dbBusTracker)).ToArray();

            return new TrackResponse
            {
                CurrentLocationCoordinate = new GeoCoordinateDto(dbBusTracker.CurrentLattitude, dbBusTracker.CurrentLongitude),
                LastDestination = dbBusTracker.LastDestination?.To<PlaceResponse>(),
                Places = placesWithEta
            };
        }

        public TrackResponse GetTrackByStudentId(Guid studentId)
        {
            var studentDetail = _studentRepository.ReadById(studentId);
            var busTracker = _busTrackerRepository.Set.Single(bt => bt.RouteId == studentDetail.RouteId);
            return GetCachedTrack(busTracker.BusId);
        }

        private async Task<IEnumerable<PlaceWithETAResponse>> GetPlacesWithEta(
            Domains.Place[] places,  
            BusTracker busTracker)
        {
            IEnumerable<Domains.Place> placesVia = places;
            if (busTracker.LastDestinationId.HasValue)
            {
                placesVia = placesVia.SkipWhile(p => p.Id != busTracker.LastDestinationId.Value).Skip(1);
            }

            var distanceResponse = await _googleMapService.GetDuration(
                new GeoCoordinateDto(busTracker.CurrentLattitude, busTracker.CurrentLongitude),
                placesVia.Select(p => new GeoCoordinateDto(p.Lattitude, p.Longitude)));
            var distanceMatrixResponse = JsonConvert.DeserializeObject<DistanceMatrixResponse>(distanceResponse);

            var durations = distanceMatrixResponse.Rows.First().RowElements
                .Select(r => r.Duration);

            var placesWithEta = placesVia.Select((place, index) =>
            {
                var placeResponse = place.To<PlaceWithETAResponse>();
                placeResponse.Duration = durations.Take(index + 1).Sum(d => d.Seconds) / 60;
                return placeResponse;
            });

            return placesWithEta;
        }
    }
}
