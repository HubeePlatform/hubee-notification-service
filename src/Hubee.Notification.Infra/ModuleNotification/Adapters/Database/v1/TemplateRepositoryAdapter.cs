using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Repositories;
using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Database.Context;
using Microsoft.Extensions.Logging;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Database.v1
{
    public class TemplateRepositoryAdapter : ITemplateRepositoryPort
    {
        private readonly ILogger<TemplateRepositoryAdapter> _logger;
        private readonly NotificationContext _context;

        public TemplateRepositoryAdapter(
            ILogger<TemplateRepositoryAdapter> logger,
            NotificationContext context
            )
        {
            _logger = logger;
            _context = context;
        }
        public async Task<Template> GetByTypeAsync(NotificationType notificationType, TemplateType templateType, TemplateVersion version)
        {
            return await _context.Templates.FirstOrDefaultAsync(
                x =>
                x.NotificationType.Equals(notificationType) &&
                x.TemplateType.Equals(templateType) &&
                x.TemplateVersion.Equals(version));
        }
    }
}