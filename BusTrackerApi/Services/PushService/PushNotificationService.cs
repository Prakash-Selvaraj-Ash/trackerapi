using BusTrackerApi.DTOS;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.PushService
{
    public class PushNotificationService : IPushNotifyService
    {
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
