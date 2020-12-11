using System.Collections.Generic;

namespace Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Events
{
    public interface ICreateNotificationEvent
    {
        public int NotificationType { get; set; }
        public int TemplateType { get; set; }
        public int TemplateVersion { get; set; }
        public string Receiver { get; set; }
        public TemplateMapper TemplateMapper { get; set; }
    }

    public class TemplateMapper
    {
        public List<KeyValuePair<string, string>> Title { get; set; }
        public List<KeyValuePair<string, string>> Message { get; set; }
    }
}
