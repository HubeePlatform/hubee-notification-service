using System;

namespace Hubee.NotificationApp.Core.Shared.v1.Exceptions
{
    public class EmptyRequestException : Exception
    {
        public EmptyRequestException(string requestName) : base($"Request '{requestName}' can't be empty")
        {

        }
    }
}