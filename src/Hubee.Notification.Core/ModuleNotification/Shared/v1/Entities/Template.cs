namespace Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities
{
    public class Template : EntityBase
    {
        public NotificationType NotificationType { get; set; }
        public TemplateType TemplateType { get; set; }
        public TemplateVersion TemplateVersion { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsRendered { get; set; }
    }
}