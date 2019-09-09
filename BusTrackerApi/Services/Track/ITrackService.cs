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
        Task<TrackResponse> AddTrack(LiveTracker addTrackRequest);
        Task<TrackResponse> GetTrack(Guid busId);
        TrackResponse GetCachedTrack(Guid busId);
        TrackResponse GetTrackByStudentId(Guid studentId);
        void AddOrUpdateLiveTracker(string userId, string connectionId);
    }
}
