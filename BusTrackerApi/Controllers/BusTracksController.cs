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

        [HttpGet]
        public async Task<OkObjectResult> ReadAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            var busTracks = await _busTrackService.ReadAllAsync(cancellationToken);
            return Ok(busTracks);
        }

        [HttpPost("StartBus")]
        public async Task<OkObjectResult> StartBusTrack(CreateBusTrackRequest createBusTrackRequest, CancellationToken token = default(CancellationToken))
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
        public IActionResult GetBusTrackByStudentId(Guid userId)
        {
            var result = _busTrackService.GetBusRouteByStudentId(userId);

            if (result == null) { return NotFound(); }
            else { return Ok(result); }
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

        [HttpDelete]
        public OkResult DeleteAll()
        {
            _busTrackService.DeleteAll();
            return Ok();
        }
    }
}
