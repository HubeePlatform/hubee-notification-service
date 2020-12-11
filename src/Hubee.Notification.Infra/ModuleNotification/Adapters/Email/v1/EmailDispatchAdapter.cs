using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Notifications;
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

        public EmailDispatchAdapter(ILogger<EmailDispatchAdapter> logger)
        {
            _logger = logger;
        }

        public async Task DispatchAsync(DispatchData data)
        {
            try
            {
                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("@UsernameEmail", "Jose Carlos Macoratti"),
                    Subject = "Macoratti .net - " + "@subject",
                    Body = "@message",
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                };

                mailMessage.To.Add(new MailAddress("@toEmail"));

                using SmtpClient smtp = new SmtpClient("@PrimaryDomain", int.Parse("@PrimaryPort"))
                {
                    Credentials = new NetworkCredential("@UsernameEmail", @"UsernamePassword"),
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
