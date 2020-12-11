using System.Collections.Generic;
using System.Linq;

namespace Hubee.NotificationApp.Infra.Models.Mjml
{
    internal class MjmlResponse
    {
        public List<object> errors { get; set; }
        public string html { get; set; }
        public string mjml { get; set; }
        public string mjml_version { get; set; }

        public bool IsValid()
        {
            return this.errors != null && !this.errors.Any();
        }
    }
}
