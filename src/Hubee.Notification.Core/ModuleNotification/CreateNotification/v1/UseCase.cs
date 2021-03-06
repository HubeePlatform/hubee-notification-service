using System.Threading.Tasks;
using Hubee.NotificationApp.Core.Shared.v1.Exceptions;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Rules;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Requests;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Notifications;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Providers;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Repositories;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1
{
    public class UseCase
    {
        private readonly DispatchNotificationRule _dispatchNotificationRule;
        private readonly MakeDispatchDataNotificationRule _makeDispatchDataNotificationRule;
        private readonly RetrieveTemplateRule _retrieveTemplateRule;

        public UseCase(
            IEmailNotificationPort emailNotificationPort,
            ITemplateRenderProviderPort templateRenderProviderPort,
            ITemplateRepositoryPort templateRepositoryPort)
        {
            _dispatchNotificationRule = new DispatchNotificationRule(emailNotificationPort);
            _retrieveTemplateRule = new RetrieveTemplateRule(templateRepositoryPort);
            _makeDispatchDataNotificationRule = new MakeDispatchDataNotificationRule(templateRenderProviderPort);
        }

        public async Task ExecuteAsync(CreateNotificationRequest request)
        {
            if (request is null)
                throw new EmptyRequestException(nameof(CreateNotificationRequest));

            if (request.ValidationResult.IsInvalid())
                throw new InvalidRequestException(request.ValidationResult.Stringify());

            var template = await _retrieveTemplateRule.RetrieveTemplateAsync(request.NotificationType, request.TemplateType, request.TemplateVersion);

            if (template is null)
                throw new TemplateNotFoundException(request.TemplateType, request.TemplateVersion);

            var dispatchData = await _makeDispatchDataNotificationRule.Make(request, template);

            await _dispatchNotificationRule.DispatchAsync(dispatchData);
        }
    }
}