using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using System;

namespace BusTrackerApi.Mappings
{
    public class BusTrackerProfile : Profile
    {
        public BusTrackerProfile()
        {
            CreateMap<CreateBusTrackRequest, BusTracker>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));
        }
    }
}
