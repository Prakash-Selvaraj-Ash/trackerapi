using System;
using System.Threading;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Extensions;
using BusTrackerApi.Services.BusTrack;
using BusTrackerApi.Services.PushService;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusTracksController : Controller
    {
        private readonly IBusTrackService _busTrackService;
        private readonly IPushNotifyService _pushNotifyService;

        public BusTracksController(
            IBusTrackService busTrackService,
            IPushNotifyService notifyService)
        {
            _busTrackService = busTrackService;
            _pushNotifyService = notifyService;
        }

        [HttpPost("StartBus")]
        public async Task<OkObjectResult> StartBusTrack(CreateBusTrackRequest createBusTrackRequest, CancellationToken token = default)
        {
            var busTracker = createBusTrackRequest.To<BusTracker>();
            var createdBusTracker = await _busTrackService.CreateAsync(busTracker, token);
            var dbBusTracker = await _busTrackService.ReadByIdAsync(createdBusTracker.Id, token);

            await _pushNotifyService.NotifyBusStarted(dbBusTracker);

            return Ok(dbBusTracker.To<BusTrackResponseDto>());
        }

        [HttpGet("byBusId")]
        public OkObjectResult GetBusTrackByBusId(Guid busId)
        {
            return Ok(_busTrackService.GetBusRouteByBusId(busId));
        }

        [HttpGet("byUserId")]
        public OkObjectResult GetBusTrackByStudentId(Guid userId)
        {
            return Ok(_busTrackService.GetBusRouteByStudentId(userId));
        }

        [HttpPut("UpdateLastDestination")]
        public async Task<OkResult> UpdateLastDestination(UpdateLastDestinationDto updateLastDestinationDto)
        {
            await _busTrackService.UpdateLastDestination(updateLastDestinationDto);
            return Ok();
        }

        [HttpPut("UpdateCurrentLocation")]
        public async Task<OkResult> UpdateCurrentLocation(UpdateCurrentLocationDto updateCurrentLocationDto)
        {
            await _busTrackService.UpdateCurrentLocation(updateCurrentLocationDto);
            return Ok();
        }
    }
}
