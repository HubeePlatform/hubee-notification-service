using System;

namespace Hubee.NotificationApp.Core.Shared.v1.Exceptions
{
    public class TemplateNotFoundException : Exception
    {
        public TemplateNotFoundException(int type, int version) : base($"Template type '{type}' and version '{version}' not found")
        {

        }
    }
}