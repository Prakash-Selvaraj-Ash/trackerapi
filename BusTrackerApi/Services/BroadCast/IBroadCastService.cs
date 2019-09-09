using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.BroadCast
{
    public interface IBroadCastService
    {
        Task<TrackResponse> BroadCast(Guid busId);
    }
}
