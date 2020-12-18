using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Requests;
using Hubee.NotificationApp.Tests.Core.TestData;
using Xunit;

namespace Hubee.NotificationApp.Tests.Core.ModuleNotification.CreateNotification.v1.Requests
{
    public class CreateNotificationRequestTests
    {
        [Fact]
        public void When_ValidCreateNotificationRequestEvent_Then_HaveValidData()
        {
            var createNotificationEvent = CreateNotificationEventData.GetValidEvent();
            var createNotificationRequest = CreateNotificationRequest.Make(createNotificationEvent);

            Assert.Equal(createNotificationEvent.NotificationType, createNotificationRequest.NotificationType);
            Assert.Equal(createNotificationEvent.TemplateType, createNotificationRequest.TemplateType);
            Assert.Equal(createNotificationEvent.TemplateVersion, createNotificationRequest.TemplateVersion);
            Assert.Equal(createNotificationEvent.TemplateMapper, createNotificationRequest.TemplateMapper);
            Assert.Equal(createNotificationEvent.Receiver, createNotificationRequest.Receiver);
        }
    }
}
