using System;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Hubs;
using BusTrackerApi.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using BusTrackerApi.Services.Track;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusTrackerApi.Services.BroadCast
{
    public class BroadCastService : IBroadCastService
    {
        private readonly IHubContext<BroadCastHub, IHubClient> _hubContext;
        private readonly IRepository<LiveTracker> _liveTrackerRepository;
        private readonly ITrackService _trackService;

        public BroadCastService(
            IHubContext<BroadCastHub, IHubClient> hubContext,
            IRepository<LiveTracker> liveTrackerRepository,
            ITrackService trackService)
        {
            _hubContext = hubContext;
            _liveTrackerRepository = liveTrackerRepository;
            _trackService = trackService;
        }
        public async Task<TrackResponse> BroadCast(Guid busId)
        {
            try
            {
                var connectionIds = _liveTrackerRepository.Set
                    .Where(t => t.BusId == busId)
                    .Select(l => l.ConnectionId)
                    .ToArray();

                var response = _trackService.GetCachedTrack(busId);

                if (response != null && connectionIds.Length > 0)
                {
                    var serializedString = JsonConvert.SerializeObject(response);
                    await _hubContext.Clients.Clients(connectionIds).BroadCastTrack(serializedString);
                }

                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
