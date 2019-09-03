using System;
using System.Linq;
using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
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
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public RoutesController(
            IRouteService routeService,
            IPlaceService placeService,
            IMapper mapper)
        {
            _routeService = routeService;
            _placeService = placeService;
            _mapper = mapper;
        }

        [HttpGet]
        public RouteResponse[] ReadAll()
        {
            var Routes = _routeService.ReadAll().ToArray();
            return Routes.Select(r =>
            {
                var routeResponse = _mapper.Map<RouteResponse>(r);
                routeResponse.Places = _routeService
                    .ReadPlaces(routeResponse.Id)
                    .Select(p => _mapper.Map<PlaceResponse>(p)).ToArray();
                return routeResponse;
            }).ToArray();
        }

        [HttpGet("{id}")]
        public RouteResponse ReadById(Guid id)
        {
            var route = _routeService.ReadById(id);
            var routeResponse = _mapper.Map<RouteResponse>(route);
            routeResponse.Places = _routeService
                .ReadPlaces(routeResponse.Id)
                .Select(p => _mapper.Map<PlaceResponse>(p)).ToArray();
            return routeResponse;
        }


        [HttpPost]
        public RouteResponse Create(CreateRouteRequest routeRequest)
        {
            var domain = _mapper.Map<Route>(routeRequest);
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

            var routeResponse = _mapper.Map<RouteResponse>(createdRoute);

            routeResponse.Places = _routeService
                .ReadPlaces(createdRoute.Id)
                .Select(p => _mapper.Map<PlaceResponse>(p)).ToArray();

            return routeResponse;
        }

        [HttpPost]
        [Route("AddPlace")]
        public OkResult AddPlace(CreateRoutePlaceRequest request)
        {
            var domain = _mapper.Map<RouteAssociation>(request);
            _routeService.AddPlace(domain);
            return Ok();
        }
    }
}