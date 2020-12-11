using System.Threading.Tasks;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Providers
{
    public interface ITemplateRenderProviderPort
    {
        public Task<string> Render(string template);
    }
}