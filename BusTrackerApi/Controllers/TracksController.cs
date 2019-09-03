using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Services.Track;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;

        public TracksController(
            ITrackService trackService,
            IMapper mapper)
        {
            _trackService = trackService;
            _mapper = mapper;
        }

        [HttpPost]
        public TrackResponse StartTrack(AddTrackRequest trackRequest)
        {
            var tracker = _mapper.Map<LiveTracker>(trackRequest);
            return _trackService.AddTrack(tracker);
        }
    }
}