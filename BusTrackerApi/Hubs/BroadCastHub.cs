using BusTrackerApi.Services.Track;
using Microsoft.AspNetCore.SignalR;
using System;

namespace BusTrackerApi.Hubs
{
    public class BroadCastHub : Hub<IHubClient>
    {
        private readonly ITrackService _trackService;

        public BroadCastHub(ITrackService trackService)
        {
            _trackService = trackService;
        }

        public void MapUserAndConnection(string userId)
        {
            Console.WriteLine($"'MapUserAndConnection' invoked. Parameter value: '{userId}");
            Console.WriteLine($"Connection id for {userId}: {Context.ConnectionId}");

            _trackService.AddOrUpdateLiveTracker(userId, Context.ConnectionId);
        }
    }
}
