namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models
{
    public class DispatchData
    {
        public DispatchData(string receiver, string title, string message)
        {
            Receiver = receiver;
            Title = title;
            Message = message;
        }

        public string Receiver { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
    }
}