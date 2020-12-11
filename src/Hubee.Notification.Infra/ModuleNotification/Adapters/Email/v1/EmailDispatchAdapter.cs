using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Notifications;
using Hubee.NotificationApp.Infra.Models.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Email.v1
{
    public class EmailDispatchAdapter : IEmailNotificationPort
    {
        private readonly ILogger<EmailDispatchAdapter> _logger;
        private readonly IConfiguration _configuration;

        public EmailDispatchAdapter(
            ILogger<EmailDispatchAdapter> logger,
            IConfiguration configuration
            )
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task DispatchAsync(DispatchData data)
        {
            try
            {
                var config = new EmailConfig();
                _configuration.GetSection("EmailConfig").Bind(config);

                if (!config.GetValueInEnvironmentVariable().IsValid())
                    throw new InvalidOperationException($"Appsettings with the section {nameof(EmailConfig)} is empty");

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(config.Username, config.DisplayName),
                    Subject = data.Title,
                    Body = data.Message,
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                };

                data.Receiver.ForEach(r => mailMessage.To.Add(new MailAddress(r)));
             
                using SmtpClient smtp = new SmtpClient(config.Host, int.Parse(config.Port))
                {
                    Credentials = new NetworkCredential(config.Username, config.Password),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
