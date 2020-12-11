using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Notifications;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Providers;
using Hubee.NotificationApp.Core.ModuleNotification.CreateNotification.v1.Ports.Repositories;
using Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Database.v1;
using Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Email.v1;
using Microsoft.Extensions.DependencyInjection;

namespace Hubee.NotificationApp.Api
{
    public static class ServicesRegistry
    {
        public static void RegisterAll(this IServiceCollection services)
        {
            RegisterAdapters(services);
            RegisterUseCases(services);
        }

        private static void RegisterAdapters(IServiceCollection services)
        {
            services.AddScoped<ITemplateRepositoryPort, TemplateRepositoryAdapter>();
            services.AddScoped<IEmailNotificationPort, EmailDispatchAdapter>();
            services.AddScoped<ITemplateRenderProviderPort, MjmlRenderProviderAdapter>();
        }

        private static void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<Core.ModuleNotification.CreateNotification.v1.UseCase>();
        }
    }
}