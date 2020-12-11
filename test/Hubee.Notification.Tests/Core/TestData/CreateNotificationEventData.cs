using Hubee.Common.Events.Sdk.Events.Notification;
using System.Collections.Generic;

namespace Hubee.NotificationApp.Tests.Core.TestData
{
    public static class CreateNotificationEventData
    {
        public static ICreateNotificationEvent GetValidEvent()
        {
            return new Event()
            {
                NotificationType = 1,
                TemplateType = 1,
                TemplateVersion = 1,
                Receiver = new List<string> { "luiz.gmail.com" },
                TemplateMapper = null
            };
        }
    }

    public class Event : ICreateNotificationEvent
    {
        public int NotificationType { get; set; }

        public int TemplateType { get; set; }

        public int TemplateVersion { get; set; }

        public List<string> Receiver { get; set; }

        public TemplateMapper TemplateMapper { get; set; }
    }
}
