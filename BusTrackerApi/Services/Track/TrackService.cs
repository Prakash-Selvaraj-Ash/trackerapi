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
        private readonly IRepository<Domains.Student> _studentRepository;
        private readonly IRepository<LiveTracker> _liveTrackerRepository;
        private readonly IEntityService _entityService;

        public TrackService(
            IRepository<LiveTracker> liveTrackerRepository,
            IRepository<BusTracker> busTrackerRepository,
            IRepository<Domains.Place> placeRepository,
            IRepository<Domains.Student> studentRepository,
            IEntityService entityService) : base(liveTrackerRepository, entityService)
        {
            _busTrackerRepository = busTrackerRepository;
            _placeRepository = placeRepository;
            _studentRepository = studentRepository;
            _entityService = entityService;
            _liveTrackerRepository = liveTrackerRepository;
        }

        public void AddOrUpdateLiveTracker(string userId, string connectionId)
        {
            Guid studentId = Guid.Parse(userId);
            var studentDetail = _studentRepository.ReadById(studentId);
            var busTracker = _busTrackerRepository.Set.Single(bt => bt.RouteId == studentDetail.RouteId);
            var liveTracker = _liveTrackerRepository.Set.SingleOrDefault(lt => lt.BusId == busTracker.BusId && lt.StudentId == studentId);

            if (liveTracker != null)
            {
                liveTracker.ConnectionId = connectionId;
                _entityService.Save();
                return;
            }

            Create(new LiveTracker { StudentId = studentId, BusId = busTracker.BusId, ConnectionId = connectionId });
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
