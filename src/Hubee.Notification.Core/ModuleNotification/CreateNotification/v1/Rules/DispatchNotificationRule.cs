using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Notifications;
using System.Threading.Tasks;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Rules
{
    public class DispatchNotificationRule
    {
        private readonly INotificationPort _notificationPort;

        public DispatchNotificationRule(INotificationPort notificationPort)
        {
            _notificationPort = notificationPort;
        }

        public async Task DispatchAsync(DispatchData dispatchData)
        {
            await _notificationPort.DispatchAsync(dispatchData);
        }
    }
}