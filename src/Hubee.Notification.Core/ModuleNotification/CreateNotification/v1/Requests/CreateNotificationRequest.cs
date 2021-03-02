using Hubee.Common.Events.Sdk.Events.Notification;
using Hubee.Validation.Sdk.Core.Models;
using System.Collections.Generic;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Requests
{
    public class CreateNotificationRequest : ValidatableSchema, CreateNotificationEvent
    {
        public int NotificationType { get; set; }
        public int TemplateType { get; set; }
        public int TemplateVersion { get; set; }
        public List<string> Receiver { get; set; }
        public TemplateMapper TemplateMapper { get; set; }

        public override object GetSchemaRules()
        {
            return new
            {
                NotificationType = "required|min:1",
                TemplateType = "required|min:1",
                TemplateVersion = "required|min:1",
                Receiver = "required",
            };
        }

        public CreateNotificationRequest(int notificationType, int templateType, int templateVersion, List<string> receiver, TemplateMapper templateMapper)
        {
            NotificationType = notificationType;
            TemplateType = templateType;
            TemplateVersion = templateVersion;
            Receiver = receiver;
            TemplateMapper = templateMapper;
        }

        public static CreateNotificationRequest Make(CreateNotificationEvent message)
        {
            return new CreateNotificationRequest
                (
                    message.NotificationType,
                    message.TemplateType,
                    message.TemplateVersion,
                    message.Receiver,
                    message.TemplateMapper
                );
        }
    }
}
