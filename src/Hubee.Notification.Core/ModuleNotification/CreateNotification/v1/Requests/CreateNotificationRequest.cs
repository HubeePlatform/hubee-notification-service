using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Events;
using Hubee.Validation.Sdk.Core.Models;
using System;

namespace Hubee.NotificationApp.Core.ModuleNotification.CrateNotification.v1.Requests
{
    public class CreateNotificationRequest: ValidatableSchema, ICreateNotificationEvent
    {
        public Guid Id { get; set; }
        public int NotificationType { get; set; }
        public int TemplateType { get; set; }
        public int TemplateVersion { get; set; }
        public string Receiver { get; set; }
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
    }
}
