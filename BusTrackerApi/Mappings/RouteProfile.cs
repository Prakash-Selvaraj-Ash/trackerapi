using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Mappings
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {
            CreateMap<CreateRouteRequest, Route>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));

            CreateMap<CreateRoutePlaceRequest, RouteAssociation>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));

            CreateMap<Route, RouteResponse>()
                .ForMember(dest => dest.Places, opts => opts.Ignore());
        }
    }
}
