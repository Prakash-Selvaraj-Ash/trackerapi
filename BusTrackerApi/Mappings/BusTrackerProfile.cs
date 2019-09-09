using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;

namespace BusTrackerApi.Mappings
{
    public class BusTrackerProfile : Profile
    {
        public BusTrackerProfile()
        {
            CreateMap<CreateBusTrackRequest, BusTracker>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));

            CreateMap<BusTracker, BusTrackResponseDto>()
                .ForMember(dest => dest.CurrentRouteStatus, opts => opts.MapFrom(src => Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<PlaceWithETAResponse>>(src.CurrentRouteStatus)));
        }
    }
}
