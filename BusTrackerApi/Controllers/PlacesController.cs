using System.Linq;
using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Extensions;
using BusTrackerApi.Services.Place;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : Controller
    {
        private readonly IPlaceService _placeService;

        public PlacesController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        public PlaceResponse[] ReadAll()
        {
            var places = _placeService.ReadAll().ToArray();
            return places.Select(p => p.To<PlaceResponse>()).ToArray();
        }

        [HttpPost]
        public PlaceResponse Create(CreatePlaceRequest placeRequest)
        {
            var domain = placeRequest.To<Place>();
            var createdPlace = _placeService.Create(domain);
            return createdPlace.To<PlaceResponse>();
        }
    }
}