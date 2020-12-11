using Moq;
using Xunit;
using System.Threading.Tasks;
using Hubee.Common.Events.Sdk.Events.Notification;
using Hubee.NotificationApp.Core.Shared.v1.Exceptions;
using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Requests;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Providers;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Repositories;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Notifications;
using Hubee.NotificationApp.Tests.Core.TestData;
using System.Collections.Generic;

namespace Hubee.NotificationApp.Tests.Core.ModuleNotification.CreateNotification.v1
{
    public class UseCaseTests
    {
        private readonly Mock<IEmailNotificationPort> _emailNotificationPortMock;
        private readonly Mock<ITemplateRepositoryPort> _templateRepositoryPortMock;
        private readonly Mock<ITemplateRenderProviderPort> _templateRenderProviderPortMock;

        public UseCaseTests()
        {
            _emailNotificationPortMock = new Mock<IEmailNotificationPort>();
            _templateRepositoryPortMock = new Mock<ITemplateRepositoryPort>();
            _templateRenderProviderPortMock = new Mock<ITemplateRenderProviderPort>();
        }

        private UseCase GetUseCase()
        {
            return new UseCase(
                 _emailNotificationPortMock.Object,
                 _templateRenderProviderPortMock.Object,
                 _templateRepositoryPortMock.Object
                 );
        }

        private CreateNotificationRequest GetValidRequest()
        {
            return new CreateNotificationRequest(
                (int)NotificationType.Email,
                (int)TemplateType.PasswordRecovery,
                (int)TemplateVersion.V1,
                new List<string>() { "luiz@gmail.com" },
                new TemplateMapper()
                );
        }

        [Fact]
        public async Task When_CreateNotificationWithEmptyRequest_Then_ThrowsEmptyRequestException()
        {
            var useCase = GetUseCase();

            await Assert.ThrowsAsync<EmptyRequestException>(() => useCase.ExecuteAsync(null));

            _templateRepositoryPortMock.Verify(x => x.GetByTypeAsync(It.IsAny<NotificationType>(), It.IsAny<TemplateType>(), It.IsAny<TemplateVersion>()), Times.Never);
            _templateRenderProviderPortMock.Verify(x => x.Render(It.IsAny<string>()), Times.Never);
            _emailNotificationPortMock.Verify(x => x.DispatchAsync(It.IsAny<DispatchData>()), Times.Never);
        }

        [Fact]
        public async Task When_CreateNotificationWithInvalidRequest_Then_ThrowsInvalidRequestException()
        {
            var useCase = GetUseCase();

            var createNotificationRequest = new CreateNotificationRequest(0, 0, 0, null, null);

            await Assert.ThrowsAsync<InvalidRequestException>(() => useCase.ExecuteAsync(createNotificationRequest));

            _templateRepositoryPortMock.Verify(x => x.GetByTypeAsync(It.IsAny<NotificationType>(), It.IsAny<TemplateType>(), It.IsAny<TemplateVersion>()), Times.Never);
            _templateRenderProviderPortMock.Verify(x => x.Render(It.IsAny<string>()), Times.Never);
            _emailNotificationPortMock.Verify(x => x.DispatchAsync(It.IsAny<DispatchData>()), Times.Never);
        }

        [Fact]
        public async Task When_CreateNotificationWithInvalidTemplate_Then_ThrowsTemplateNotFoundException()
        {
            const int invalidNotificationType = 99;
            const int invalidTemplateType = 99;
            const int invalidTemplateVersion = 99;

            _templateRepositoryPortMock
                .Setup(t => t.GetByTypeAsync((NotificationType)invalidNotificationType, (TemplateType)invalidTemplateType, (TemplateVersion)invalidTemplateVersion))
                .ReturnsAsync(default(Template));

            var useCase = GetUseCase();
            var request = GetValidRequest();

            request.NotificationType = invalidNotificationType;
            request.TemplateType = invalidTemplateType;
            request.TemplateVersion = invalidTemplateVersion;

            await Assert.ThrowsAsync<TemplateNotFoundException>(() => useCase.ExecuteAsync(request));

            _templateRepositoryPortMock.Verify(x => x.GetByTypeAsync(It.IsAny<NotificationType>(), It.IsAny<TemplateType>(), It.IsAny<TemplateVersion>()), Times.Once);
            _templateRenderProviderPortMock.Verify(x => x.Render(It.IsAny<string>()), Times.Never);
            _emailNotificationPortMock.Verify(x => x.DispatchAsync(It.IsAny<DispatchData>()), Times.Never);
        }

        [Fact]
        public async Task When_CreateNotificationWithInvalidNotificationType_Then_ThrowsNotificationTypeNotSupportedException()
        {
            const int invalidNotificationType = 99;

            _templateRepositoryPortMock
                .Setup(t => t.GetByTypeAsync((NotificationType)invalidNotificationType, It.IsAny<TemplateType>(), It.IsAny<TemplateVersion>()))
                .ReturnsAsync(TemplateData.GetValidTemplate());

            var useCase = GetUseCase();
            var request = GetValidRequest();

            request.NotificationType = invalidNotificationType;

            await Assert.ThrowsAsync<NotificationTypeNotSupportedException>(() => useCase.ExecuteAsync(request));

            _templateRepositoryPortMock.Verify(x => x.GetByTypeAsync((NotificationType)invalidNotificationType, It.IsAny<TemplateType>(), It.IsAny<TemplateVersion>()), Times.Once);
            _templateRenderProviderPortMock.Verify(x => x.Render(It.IsAny<string>()), Times.Once);
            _emailNotificationPortMock.Verify(x => x.DispatchAsync(It.IsAny<DispatchData>()), Times.Never);
        }

        [Fact]
        public async Task When_CreateNotification_Then_DispatchDataForReceiver()
        {
            var template = TemplateData.GetValidTemplate();
            var useCase = GetUseCase();
            var request = GetValidRequest();

            request.TemplateMapper = TemplateMapperData.GetValidTemplateMapper();

            _templateRepositoryPortMock
                .Setup(t => t.GetByTypeAsync(template.NotificationType, template.TemplateType, template.TemplateVersion))
                .ReturnsAsync(template);

            _templateRenderProviderPortMock
                .Setup(t => t.Render(template.Content))
                .ReturnsAsync(template.Content);

            await useCase.ExecuteAsync(request);

            _templateRepositoryPortMock.Verify(x => x.GetByTypeAsync((NotificationType)request.NotificationType, (TemplateType)request.TemplateType, (TemplateVersion)request.TemplateVersion), Times.Once);
            _templateRenderProviderPortMock.Verify(x => x.Render(It.IsAny<string>()), Times.Once);
            _emailNotificationPortMock.Verify(x => x.DispatchAsync(It.IsAny<DispatchData>()), Times.Once);
        }
    }
}