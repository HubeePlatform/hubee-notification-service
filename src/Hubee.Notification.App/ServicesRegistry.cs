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
            //services.AddScoped<INotificationStoragePort, NotificationStorageAdapter>();
        }

        private static void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<Core.ModuleNotification.CrateNotification.v1.UseCase>();
        }
    }
}
