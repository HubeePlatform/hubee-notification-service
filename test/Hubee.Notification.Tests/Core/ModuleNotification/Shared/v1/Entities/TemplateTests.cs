using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using Xunit;

namespace Hubee.NotificationApp.Tests.Core.ModuleNotification.Shared.v1.Entities
{
    public class TemplateTests
    {
        [Fact]
        public void When_CreateTemplate_Then_HaveValidData()
        {
            const string title = "Title hubee";
            const string content = "Content hubee";
            const NotificationType notificationType = NotificationType.Email;
            const TemplateType templateType = TemplateType.PasswordRecovery;
            const TemplateVersion templateVersion = TemplateVersion.V1;
            const bool isRendered = true;

            var template = new Template(notificationType, templateType, templateVersion, title, content, isRendered);

            Assert.Equal(title, template.Title);
            Assert.Equal(content, template.Content);
            Assert.Equal(notificationType, template.NotificationType);
            Assert.Equal(templateType, template.TemplateType);
            Assert.Equal(templateVersion, template.TemplateVersion);
            Assert.Equal(isRendered, template.IsRendered);
        }
    }
}