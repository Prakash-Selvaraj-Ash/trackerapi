using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Mappings
{
    public class TrackProfile : Profile
    {
        public TrackProfile()
        {
            CreateMap<AddTrackRequest, LiveTracker>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));
        }
    }
}
