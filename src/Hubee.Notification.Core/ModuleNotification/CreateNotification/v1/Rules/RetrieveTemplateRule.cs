using System.Threading.Tasks;
using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Repositories;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Rules
{
    public class RetrieveTemplateRule
    {
        private readonly ITemplateRepositoryPort _templateRepositoryPort;

        public RetrieveTemplateRule(ITemplateRepositoryPort templateRepositoryPort)
        {
            _templateRepositoryPort = templateRepositoryPort;
        }

        public async Task<Template> RetrieveTemplateAsync(int notificationType, int templateType, int version)
        {
            return await _templateRepositoryPort.GetByTypeAsync((NotificationType)notificationType, (TemplateType)templateType, (TemplateVersion)version);
        }
    }
}