using System;
using System.Linq;
using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Extensions;
using BusTrackerApi.Services.Place;
using BusTrackerApi.Services.Route;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public RouteResponse[] ReadAll()
        {
            var Routes = _routeService.ReadAll().ToArray();
            return Routes.Select(r =>
            {
                var routeResponse = r.To<RouteResponse>();
                routeResponse.Places = _routeService
                    .ReadPlaces(routeResponse.Id)
                    .Select(p => p.To<PlaceResponse>()).ToArray();
                return routeResponse;
            }).ToArray();
        }

        [HttpGet("{id}")]
        public RouteResponse ReadById(Guid id)
        {
            var route = _routeService.ReadById(id);
            var routeResponse = route.To<RouteResponse>();
            routeResponse.Places = _routeService
                .ReadPlaces(routeResponse.Id)
                .Select(p => p.To<PlaceResponse>()).ToArray();
            return routeResponse;
        }


        [HttpPost]
        public RouteResponse Create(CreateRouteRequest routeRequest)
        {
            var domain = routeRequest.To<Route>();
            var createdRoute = _routeService.Create(domain);

            AddPlace(new CreateRoutePlaceRequest
            {
                RouteId = createdRoute.Id,
                PlaceId = routeRequest.SourceId,
                SequenceNumber = 1
            });

            AddPlace(new CreateRoutePlaceRequest
            {
                RouteId = createdRoute.Id,
                PlaceId = routeRequest.DestinationId,
                SequenceNumber = 2
            });

            var routeResponse = createdRoute.To<RouteResponse>();

            routeResponse.Places = _routeService
                .ReadPlaces(createdRoute.Id)
                .Select(p => p.To<PlaceResponse>()).ToArray();

            return routeResponse;
        }

        [HttpPost]
        [Route("AddPlace")]
        public OkResult AddPlace(CreateRoutePlaceRequest request)
        {
            var domain = request.To<RouteAssociation>();
            _routeService.AddPlace(domain);
            return Ok();
        }
    }
}