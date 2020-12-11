using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using System.Collections.Generic;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models
{
    public class DispatchData
    {
        public DispatchData(NotificationType type, List<string> receiver, string title, string message)
        {
            NotificationType = type;
            Receiver = receiver;
            Title = title;
            Message = message;
        }

        public NotificationType NotificationType { get; private set; }
        public List<string> Receiver { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
    }
}