using System.Threading.Tasks;
using BusTrackerApi.DTOS;
using BusTrackerApi.Services.PushService;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushNotificationController : Controller
    {
        private readonly IPushNotifyService _pushNotifyService;
        public PushNotificationController(IPushNotifyService pushNotifyService)
        {
            _pushNotifyService = pushNotifyService;
        }

        [HttpPost]
        public async Task<bool> Push(Message pushMessage)
        {
            var result = await _pushNotifyService.SendPushNotification(pushMessage);
            return result;
        }
    }
}