using BusTrackerApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.Services.PushService
{
    public interface IPushNotifyService
    {
        Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data);
        Task<bool> SendPushNotification(Message pushMessage);
    }
}
