using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BusTrackerApi.Domains;
using BusTrackerApi.DTOS;
using BusTrackerApi.Repositories;

namespace BusTrackerApi.Services.PushService
{
    public class PushNotificationService : IPushNotifyService
    {
        private readonly IRepository<RouteAssociation> _routeAssociationRepository;
        private readonly IRepository<Domains.Student> _studentRepository;

        public PushNotificationService(
            IRepository<RouteAssociation> routeAssociationRepository,
            IRepository<Domains.Student> studentRepository)
        {
            _routeAssociationRepository = routeAssociationRepository;
            _studentRepository = studentRepository;
        }

        public async Task<bool> NotifyBusStarted(BusTracker tracker)
        {
            var studentsToNotify = _studentRepository.Set
                .Where(s => s.RouteId == tracker.RouteId)
                .Select(s => s.FcmId)
                .ToArray();

            var routeAssociations = _routeAssociationRepository.Set
                .Where(ra => ra.RouteId == tracker.RouteId)
                .OrderBy(ra => ra.SequenceNumber)
                .ToArray();

            var origin = routeAssociations[0];
            var destination = routeAssociations.Last();

            return await SendPushNotification(
                studentsToNotify,
                "eMTe Bus Status",
                $"Bus started for route '{origin.Place.Name}' to '{destination.Place.Name}'",
                null);
        }

        public async Task<bool> NotifyUsers(BusTracker tracker)
        {
            var routeAssociations = _routeAssociationRepository.Set
                .Where(ra => ra.RouteId == tracker.RouteId)
                .OrderBy(ra => ra.SequenceNumber)
                .ToArray();

            var nextPlace = routeAssociations
                .SkipWhile(ra => ra.PlaceId != tracker.LastDestinationId)
                .Skip(1)
                .First()
                .Place;

            var studentsToNotify = _studentRepository.Set
                .Where(s => s.PlaceId == tracker.LastDestinationId || s.PlaceId == nextPlace.Id)
                .ToArray();

            var studentsToNotifyForCompletedTrip = studentsToNotify.Where(s => s.PlaceId == tracker.LastDestinationId);
            var studentsToNotifyForNextStop = studentsToNotify.Except(studentsToNotifyForCompletedTrip);

            var deviceIdsForCompletedTrip = studentsToNotifyForCompletedTrip.Select(s => s.FcmId).ToArray();
            var deviceIdsForNextStop = studentsToNotifyForNextStop.Select(s => s.FcmId).ToArray();

            await SendPushNotification(
                deviceIdsForCompletedTrip, 
                "eMTe Bus Status", 
                $"Bus reached {tracker.LastDestination.Name}", 
                null);

            await SendPushNotification(
                deviceIdsForNextStop,
                "eMTe Bus Status",
                $"Bus reached {tracker.LastDestination.Name} & Next stop {nextPlace.Name} is yours",
                null);

            return true;
        }

        public async Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data)
        {
            var message = new Message()
            {
                notification = new Notification()
                {
                    title = title,
                    text = body
                },
                registration_ids = deviceTokens
            };

            return await SendPushNotification(message);
        }

        public async Task<bool> SendPushNotification(Message message)
        {
            var jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
            request.Headers.TryAddWithoutValidation("Authorization", "key =AAAA7acIscQ:APA91bFfrYLQxT1gaNmeYy2KT6b1Q5jzLESCl1r8k--z-YWIfYLvyuDlDYGo9BsR1CiyXe9Xum6R84BDQVuTG1hItuEoR2KVtsH3wfX6DW7YOAMttsQV9fd7edKrfzj4qp7SVfY4q9an");
            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            HttpResponseMessage result;

            using (var client = new HttpClient())
            {
                result = await client.SendAsync(request);
            }

            return true;
        }
    }
}
