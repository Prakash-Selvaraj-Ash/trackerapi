using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Hubs;
using BusTrackerApi.Repositories;
using BusTrackerApi.Services.BroadCast;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BroadCastController : ControllerBase
    {
        private readonly IBroadCastService _service;

        public BroadCastController(IBroadCastService service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<TrackResponse> Notify(Guid busId)
        {
            TrackResponse response = await _service.BroadCast(busId);
            return response;
        }
    }
}