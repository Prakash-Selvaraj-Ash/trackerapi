using BusTrackerApi.DTOS;
using System.Threading.Tasks;

namespace BusTrackerApi.Hubs
{
    public interface IHubClient
    {
        Task BroadCastTrack(TrackResponse trackResponse);
    }
}
