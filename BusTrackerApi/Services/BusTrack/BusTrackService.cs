using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Extensions;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.BroadCast;
using BusTrackerApi.Services.Bus;
using BusTrackerApi.Services.Entity;
using BusTrackerApi.Services.PushService;
using BusTrackerApi.Services.Track;
using Newtonsoft.Json;

namespace BusTrackerApi.Services.BusTrack
{
    public class BusTrackService : BaseCrudService<BusTracker>, IBusTrackService
    {
        private readonly ITrackService _trackService;
        private readonly IBusService _busService;
        private readonly IEntityService _entityService;
        private readonly IBroadCastService _broadCastService;
        private readonly IPushNotifyService _pushNotifyService;
        private readonly IRepository<Domains.Student> _studentRepository;
        private readonly IRepository<BusTracker> _busTrackerRepository;

        public BusTrackService(
            IRepository<BusTracker> repository, 
            IRepository<Domains.Student> studentRepository,
            ITrackService trackService,
            IBusService busService,
            IBroadCastService broadCastService,
            IPushNotifyService pushNotifyService,
            IEntityService entityService) : base(repository, entityService)
        {
            _busTrackerRepository = repository;
            _studentRepository = studentRepository;
            _trackService = trackService;
            _busService = busService;
            _entityService = entityService;
            _broadCastService = broadCastService;
            _pushNotifyService = pushNotifyService;
        }

        public override async Task<BusTracker> CreateAsync(BusTracker domain, CancellationToken token)
        {
            var createdTracker = await base.CreateAsync(domain, token);
            var trackResponse = await _trackService.GetTrack(createdTracker.BusId);
            var googleResponse = await _busService.GetBusRoute(createdTracker.RouteId);
            createdTracker.CurrentRouteStatus = JsonConvert.SerializeObject(trackResponse.Places);
            createdTracker.GDirection = googleResponse.GoogleDirection;
            await _entityService.SaveAsync(token);

            return createdTracker;
        }

        public async Task UpdateLastDestination(UpdateLastDestinationDto updateLastDestinationDto)
        {
            var tracker = _busTrackerRepository.Set.Single(bt => bt.BusId == updateLastDestinationDto.BusId);
            tracker.LastDestinationId = updateLastDestinationDto.LastDestinationId;
            tracker.CurrentLattitude = updateLastDestinationDto.CurrentLocation.Lattitude;
            tracker.CurrentLongitude = updateLastDestinationDto.CurrentLocation.Longitude;

            await _entityService.SaveAsync(CancellationToken.None);

            var trackResponse = await _trackService.GetTrack(tracker.BusId);
            tracker.CurrentRouteStatus = JsonConvert.SerializeObject(trackResponse.Places);

            await _entityService.SaveAsync(CancellationToken.None);

            await _broadCastService.BroadCast(updateLastDestinationDto.BusId);
            await _pushNotifyService.NotifyUsers(tracker);
        }

        public async Task UpdateCurrentLocation(UpdateCurrentLocationDto updateCurrentLocationDto)
        {
            var tracker = _busTrackerRepository.Set.Single(bt => bt.BusId == updateCurrentLocationDto.BusId);
            tracker.CurrentLattitude = updateCurrentLocationDto.CurrentLocation.Lattitude;
            tracker.CurrentLongitude = updateCurrentLocationDto.CurrentLocation.Longitude;

            await _entityService.SaveAsync(CancellationToken.None);

            var trackResponse = await _trackService.GetTrack(tracker.BusId);
            tracker.CurrentRouteStatus = JsonConvert.SerializeObject(trackResponse.Places);

            await _entityService.SaveAsync(CancellationToken.None);
            await _broadCastService.BroadCast(updateCurrentLocationDto.BusId);
        }

        public BusTrackResponseDto GetBusRouteByBusId(Guid busId)
        {
            var busTracker = _busTrackerRepository.Set.Single(bt => bt.BusId == busId);
            return busTracker.To<BusTrackResponseDto>();
        }

        public BusTrackResponseDto GetBusRouteByStudentId(Guid studentId)
        {
            var student = _studentRepository.ReadById(studentId);
            var busTracker = _busTrackerRepository.Set.SingleOrDefault(bt => bt.RouteId == student.RouteId);
            return busTracker?.To<BusTrackResponseDto>();
        }
    }
}
