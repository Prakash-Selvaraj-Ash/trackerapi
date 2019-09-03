using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Services.BusTrack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusTracksController : Controller
    {
        private readonly IBusTrackService _busTrackService;
        private readonly IMapper _mapper;

        public BusTracksController(
            IBusTrackService busTrackService,
            IMapper mapper)
        {
            _busTrackService = busTrackService;
            _mapper = mapper;
        }

        [HttpPost]
        public OkResult StartBusTrack(CreateBusTrackRequest createBusTrackRequest)
        {
            var busTracker = _mapper.Map<BusTracker>(createBusTrackRequest);
            _busTrackService.Create(busTracker);
            return Ok();
        }
    }
}
