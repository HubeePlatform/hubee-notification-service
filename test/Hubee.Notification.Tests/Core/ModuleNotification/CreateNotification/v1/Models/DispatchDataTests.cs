using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models;
using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hubee.NotificationApp.Tests.Core.ModuleNotification.CreateNotification.v1.Models
{
    public class DispatchDataTests
    {
        [Fact]
        public void When_CreateDispatchData_Then_HaveValidData()
        {
            const string title = "Title hubee";
            const string message = "Message hubee";
            var receiver = new List<string>() { "luiz.gmail.com" };
            const NotificationType notificationType = NotificationType.Email;

            var data = new DispatchData(notificationType, receiver, title, message);

            Assert.Equal(title, data.Title);
            Assert.Equal(message, data.Message);
            Assert.Equal(receiver.FirstOrDefault(), data.Receiver.FirstOrDefault());
            Assert.Equal(notificationType, data.NotificationType);
        }
    }
}