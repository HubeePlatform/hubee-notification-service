using System;

namespace Hubee.NotificationApp.Core.Shared.v1.Exceptions
{
    public class NotificationTypeNotSupportedException : Exception
    {
        public NotificationTypeNotSupportedException(string type) : base($"Notification type '{type}' not supported")
        {

        }
    }
}