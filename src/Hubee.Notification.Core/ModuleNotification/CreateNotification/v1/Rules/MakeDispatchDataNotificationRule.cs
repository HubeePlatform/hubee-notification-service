using System.Threading.Tasks;
using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Models;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Requests;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Providers;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Rules
{
    public class MakeDispatchDataNotificationRule
    {
        private readonly ITemplateRenderProviderPort _templateRenderProviderPort;

        public MakeDispatchDataNotificationRule(ITemplateRenderProviderPort templateRenderProviderPort)
        {
            _templateRenderProviderPort = templateRenderProviderPort;
        }

        public async Task<DispatchData> Make(CreateNotificationRequest request, Template template)
        {
            var title = template.Title;
            var templateRendering = template.IsRendered
                ? await _templateRenderProviderPort.Render(template.Content)
                : template.Content;

            var templateMapper = request.TemplateMapper;

            if (templateMapper != null)
            {
                templateMapper.Message?.ForEach(x => templateRendering = templateRendering.Replace(x.Key, x.Value));
                templateMapper.Title?.ForEach(x => title = title.Replace(x.Key, x.Value));
            }

            return new DispatchData(
                (NotificationType)request.NotificationType,
                request.Receiver,
                title,
                templateRendering
                );
        }
    }
}