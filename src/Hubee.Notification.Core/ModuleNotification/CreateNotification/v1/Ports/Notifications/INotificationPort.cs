using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models;
using System.Threading.Tasks;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Notifications
{
    public interface INotificationPort
    {
        Task DispatchAsync(DispatchData data);
    }
}
