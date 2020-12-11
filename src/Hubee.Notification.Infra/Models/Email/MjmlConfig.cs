using System;

namespace Hubee.NotificationApp.Infra.Models.Email
{
    public class MjmlConfig
    {
        public string ApplicationId { get; set; }
        public string Endpoint { get; set; }
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }

        public MjmlConfig GetValueInEnvironmentVariable()
        {
            if (!string.IsNullOrEmpty(this.ApplicationId) && !string.IsNullOrEmpty(this.PublicKey))
                return this;

            this.ApplicationId = Environment.GetEnvironmentVariable("HUBEE_MJML_APPLICATION_ID");
            this.PublicKey = Environment.GetEnvironmentVariable("HUBEE_MJML_PUBLIC_KEY");
            return this;
        }
        public bool IsValid()
        {
            return
                !string.IsNullOrEmpty(this.Endpoint) &&
                !string.IsNullOrEmpty(this.ApplicationId) &&
                !string.IsNullOrEmpty(this.PublicKey);
        }
    }
}
