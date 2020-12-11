using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using System.Threading.Tasks;

namespace Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Repositories
{
    public interface ITemplateRepositoryPort
    {
        Task<Template> GetByTypeAsync(NotificationType notificationType, TemplateType templateType, TemplateVersion version);
    }
}