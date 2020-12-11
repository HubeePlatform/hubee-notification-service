using System;

namespace Hubee.NotificationApp.Infra.Models.Email
{
    public class EmailConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public EmailConfig GetValueInEnvironmentVariable()
        {
            if (!string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Password))
                return this;

            this.Username = Environment.GetEnvironmentVariable("HUBEE_NOTIFICATION_EMAIL_CREDENTIAL_USERNAME");
            this.Password = Environment.GetEnvironmentVariable("HUBEE_NOTIFICATION_EMAIL_CREDENTIAL_PASSWORD");
            return this;
        }

        public bool IsValid()
        {
            return
             !string.IsNullOrEmpty(this.DisplayName) &&
             !string.IsNullOrEmpty(this.Host) &&
             !string.IsNullOrEmpty(this.Port) &&
             !string.IsNullOrEmpty(this.Username) &&
             !string.IsNullOrEmpty(this.Password);
        }
    }
}