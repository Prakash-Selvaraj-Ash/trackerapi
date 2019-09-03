using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Mappings
{
    public class BusProfile : Profile
    {
        public BusProfile()
        {
            CreateMap<CreateBusRequest, Bus>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));
        }
    }
}
