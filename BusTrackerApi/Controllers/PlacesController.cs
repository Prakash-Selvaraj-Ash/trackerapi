using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        [HttpPut]
        public async Task<PlaceResponse> Create([FromBody]UpdatePlaceRequest placeRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbDomain = await _placeService.ReadByIdAsync(placeRequest.PlaceId, cancellationToken);

            dbDomain.Lattitude = placeRequest.Lattitude;
            dbDomain.Longitude = placeRequest.Longitude;
            dbDomain.Name = placeRequest.Name;

            var updatedPlace = _placeService.Update(dbDomain);

            return updatedPlace.To<PlaceResponse>();
        }
    }
}