using System.Linq;
using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Services.Place;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : Controller
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public PlacesController(
            IPlaceService placeService,
            IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        [HttpGet]
        public PlaceResponse[] ReadAll()
        {
            var places = _placeService.ReadAll().ToArray();
            return places.Select(p => _mapper.Map<PlaceResponse>(p)).ToArray();
        }

        [HttpPost]
        public PlaceResponse Create(CreatePlaceRequest placeRequest)
        {
            var domain = _mapper.Map<Place>(placeRequest);
            var createdPlace = _placeService.Create(domain);
            return _mapper.Map<PlaceResponse>(createdPlace);
        }
    }
}