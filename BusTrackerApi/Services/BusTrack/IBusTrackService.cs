using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.BusTrack
{
    public interface IBusTrackService : IBaseCrudService<Domains.BusTracker>
    {
        Task UpdateLastDestination(UpdateLastDestinationDto updateLastDestinationDto);
        Task UpdateCurrentLocation(UpdateCurrentLocationDto updateCurrentLocationDto);
        BusTrackResponseDto GetBusRouteByBusId(Guid busId);
        BusTrackResponseDto GetBusRouteByStudentId(Guid studentId);
    }
}
