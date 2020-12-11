using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;

namespace Hubee.NotificationApp.Tests.Core.TestData
{
    public static class TemplateData
    {
        public static Template GetValidTemplate()
        {
            return new Template(
                NotificationType.Email,
                TemplateType.PasswordRecovery,
                TemplateVersion.V1,
                "Title test",
                "Content test",
                true
                );
        }
    }
}