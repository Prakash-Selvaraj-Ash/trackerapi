using AutoMapper;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Mappings
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<Place, PlaceResponse>();
            CreateMap<UpdatePlaceRequest, Place>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.PlaceId));
            CreateMap<Place, PlaceWithETAResponse>()
                .ForMember(dest => dest.Duration, opts => opts.Ignore());
            CreateMap<CreatePlaceRequest, Place>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));
        }
    }
}
