using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.Track
{
    public interface ITrackService
    {
        TrackResponse AddTrack(LiveTracker addTrackRequest);
        TrackResponse GetTrack(Guid busId);
    }
}
