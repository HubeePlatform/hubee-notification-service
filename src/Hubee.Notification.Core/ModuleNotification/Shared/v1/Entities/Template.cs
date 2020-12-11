namespace Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities
{
    public class Template : EntityBase
    {
        public Template(
            NotificationType notificationType,
            TemplateType templateType,
            TemplateVersion templateVersion,
            string title,
            string content,
            bool isRendered
            )
        {
            this.NotificationType = notificationType;
            this.TemplateType = templateType;
            this.TemplateVersion = templateVersion;
            this.Title = title;
            this.Content = content;
            this.IsRendered = isRendered;
        }

        public NotificationType NotificationType { get; private set; }
        public TemplateType TemplateType { get; private set; }
        public TemplateVersion TemplateVersion { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public bool IsRendered { get; private set; }
    }
}