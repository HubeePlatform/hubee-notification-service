using System;
using System.Threading.Tasks;
using Hubee.Validation.Sdk.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Requests;
using Hubee.MessageBroker.Sdk.Interfaces;
using Hubee.Common.Events.Sdk.Events.Notification;

namespace Hubee.NotificationApp.Api.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IEventBusService _eventBus;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(
            ILogger<NotificationController> logger,
            IEventBusService eventBus
            )
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        /// <summary>
        /// Create a Notification
        /// </summary>
        /// <response code="201">New Notification was created</response>
        /// <response code="400">Malformed CreateNotificationRquest</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateNotificationRequest request)
        {
            try
            {
                request.ValidateSchema();

                if (request.ValidationResult.IsInvalid())
                {
                    var errors = request.ValidationResult.GetErrors();
                    _logger.LogWarning("[NotificationController][CreateAsync]\nInvalid request\n{@request}\n{@errors}", request, errors);
                    return BadRequest(errors);
                }

                _logger.LogInformation("[NotificationController][CreateAsync] Execute UseCase\n{@request}", request);
                await _eventBus.Publish<ICreateNotificationEvent>(request);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
