using System;
using System.Threading.Tasks;
using Hubee.Validation.Sdk.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hubee.NotificationApp.Core.ModuleNotification.CrateNotification.v1;
using Hubee.NotificationApp.Core.ModuleNotification.CrateNotification.v1.Requests;

namespace Hubee.NotificationApp.Api.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly UseCase _useCase;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(UseCase useCase, ILogger<NotificationController> logger)
        {
            _useCase = useCase;
            _logger = logger;
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
                    _logger.LogWarning($"[NotificationController][Create]{Environment.NewLine}Invalid request{Environment.NewLine}{request}{Environment.NewLine}{errors}", request, errors);
                    return BadRequest(errors);
                }

                _logger.LogInformation($"[NotificationController][Create] Execute UseCase{Environment.NewLine}{@request}", request);
                await _useCase.ExecuteAsync(request);

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
