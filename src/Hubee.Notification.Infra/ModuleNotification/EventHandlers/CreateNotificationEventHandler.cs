using Hubee.Common.Events.Sdk.Events.Notification;
using Hubee.MessageBroker.Sdk.Core.Handles;
using Hubee.MessageBroker.Sdk.Core.Models.Headers;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using CreateNotificationUseCase = Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.UseCase;

namespace Hubee.NotificationApp.Infra.ModuleNotification.EventHandlers
{
    public class CreateNotificationEventHandler : GenericMessageHandle<CreateNotificationEvent>
    {
        private readonly ILogger<CreateNotificationEventHandler> _logger;
        private readonly CreateNotificationUseCase _createNotificationUseCase;

        public CreateNotificationEventHandler(
            ILogger<CreateNotificationEventHandler> logger,
            CreateNotificationUseCase createNotificationUseCase
            )
        {
            _logger = logger;
            _createNotificationUseCase = createNotificationUseCase;
        }

        public override async Task Handle(CreateNotificationEvent message, EventHeader header)
        {
            try
            {
                var request = CreateNotificationRequest.Make(message);

                if (request.ValidationResult.IsInvalid())
                {
                    var errors = request.ValidationResult.GetErrors();
                    _logger.LogWarning("[NotificationApp][ICreateNotificationEvent]\nInvalid request\n{@request}\n{@errors}", request, errors);
                    return;
                }

                _logger.LogInformation("[NotificationApp][ICreateNotificationEvent] Execute UseCase\n{@request}", request);

                await _createNotificationUseCase.ExecuteAsync(CreateNotificationRequest.Make(message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}