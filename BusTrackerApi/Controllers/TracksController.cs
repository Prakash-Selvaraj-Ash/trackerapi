using System;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Extensions;
using BusTrackerApi.Services.Track;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : Controller
    {
        private readonly ITrackService _trackService;

        public TracksController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpPost]
        public async Task<TrackResponse> StartTrack(AddTrackRequest trackRequest)
        {
            var tracker = trackRequest.To<LiveTracker>();
            return await _trackService.AddTrack(tracker);
        }

        [HttpGet("byBusId")]
        public TrackResponse GetTrackByBus(Guid busId)
        {
            return _trackService.GetCachedTrack(busId);
        }

        [HttpGet("byUserId")]
        public TrackResponse GetTrackByStudent(Guid studentId)
        {
            return _trackService.GetTrackByStudentId(studentId);
        }
    }
}