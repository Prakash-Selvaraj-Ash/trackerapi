using System.Linq;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Extensions;
using BusTrackerApi.Services.Bus;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : Controller
    {
        private readonly IBusService _busService;

        public BusesController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet]
        public OkObjectResult GetAll()
        {
            var buses = _busService.ReadAll().ToArray();
            return Ok(buses);
        }

        [HttpPost]
        public OkResult CreateBus(CreateBusRequest createBusRequest)
        {
            var bus = createBusRequest.To<Bus>();
            _busService.Create(bus);
            return Ok();
        }
    }
}