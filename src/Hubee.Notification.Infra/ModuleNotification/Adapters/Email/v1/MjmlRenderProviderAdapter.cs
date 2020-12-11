using Flurl.Http;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Providers;
using Hubee.NotificationApp.Infra.Models.Mjml;
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
            ILogger<MjmlRenderProviderAdapter> logger,
            IConfiguration configuration
            )
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> Render(string template)
        {
            try
            {
                var MjmlConsumersApi = _configuration["MjmlConfig:ConsumersApi"];
                var username = _configuration["MjmlConfig:ApplicationId"];
                var password = _configuration["MjmlConfig:PublicKey"];

                if (string.IsNullOrEmpty(MjmlConsumersApi))
                    throw new Exception($"MjmlConfig:AdminApi is empty");

                var mjmlResponse = await MjmlConsumersApi
                    .AllowHttpStatus(HttpStatusCode.OK)
                    .WithBasicAuth(username, password)
                    .PostJsonAsync(new { }).ReceiveJson<MjmlResponse>();

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
