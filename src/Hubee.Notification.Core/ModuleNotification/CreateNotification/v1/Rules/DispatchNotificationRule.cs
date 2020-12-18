using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Notifications;
using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using Hubee.NotificationApp.Core.Shared.v1.Exceptions;
using System.Threading.Tasks;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Rules
{
    public class DispatchNotificationRule
    {
        private readonly IEmailNotificationPort _emailNotificationPort;

        public DispatchNotificationRule(IEmailNotificationPort emailNotificationPort)
        {
            _emailNotificationPort = emailNotificationPort;
        }

        public async Task DispatchAsync(DispatchData dispatchData)
        {

            switch (dispatchData.NotificationType)
            {
                case NotificationType.Email:
                    await _emailNotificationPort.DispatchAsync(dispatchData);
                    break;
                default:
                    throw new NotificationTypeNotSupportedException(dispatchData.NotificationType.ToString());
            }
        }
    }
}