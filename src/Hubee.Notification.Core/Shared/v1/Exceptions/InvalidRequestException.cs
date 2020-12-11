using System;

namespace Hubee.NotificationApp.Core.Shared.v1.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string errorsAsText) : base(errorsAsText)
        {

        }
    }
}