using Flurl.Http;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Providers;
using Hubee.NotificationApp.Infra.Models.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Email.v1
{
    public class MjmlRenderProviderAdapter : ITemplateRenderProviderPort
    {
        private readonly ILogger<MjmlRenderProviderAdapter> _logger;
        private readonly IConfiguration _configuration;

        public MjmlRenderProviderAdapter(
                IConfiguration configuration,
                ILogger<MjmlRenderProviderAdapter> logger
            )
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> Render(string template)
        {
            try
            {
                var config = new MjmlConfig();
                _configuration.GetSection("MjmlConfig").Bind(config);

                if (!config.GetValueInEnvironmentVariable().IsValid())
                    throw new InvalidOperationException($"Appsettings with the section {nameof(MjmlConfig)} is empty");

                var mjmlResponse = await config.Endpoint
                    .AllowHttpStatus(HttpStatusCode.OK)
                    .WithBasicAuth(config.ApplicationId, config.PublicKey)
                    .PostJsonAsync(new { mjml = template }).ReceiveJson<MjmlResponse>();

                return mjmlResponse.html;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
