using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Services.Bus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : Controller
    {
        private readonly IBusService _busService;
        private readonly IMapper _mapper;

        public BusesController(
            IBusService busService,
            IMapper mapper)
        {
            _busService = busService;
            _mapper = mapper;
        }

        [HttpPost]
        public OkResult CreateBus(CreateBusRequest createBusRequest)
        {
            var bus = _mapper.Map<Bus>(createBusRequest);
            _busService.Create(bus);
            return Ok();
        }
    }
}