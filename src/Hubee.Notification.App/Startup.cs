using System;
using System.IO;
using System.Reflection;
using Hubee.Common.Events.Sdk.Events.Notification;
using Hubee.MessageBroker.Sdk.Extensions;
using Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Database.Context;
using Hubee.NotificationApp.Infra.ModuleNotification.EventHandlers;
using Hubee.ServiceDiscovery.Sdk.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Hubee.NotificationApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddHealthChecks();

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            services.AddDbContext<NotificationContext>();

            services.RegisterAll();
            services.AddEventBus(Configuration, o => { o.AddConsumer<CreateNotificationEventHandler>(); });
            services.AddServiceDiscovery(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                }

                options.RoutePrefix = string.Empty;

                options.DocExpansion(DocExpansion.List);
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck");
            });

            app.UseEventBus(o =>
            {
                o.Subscribe<CreateNotificationEvent, CreateNotificationEventHandler>();
            });

            UpdateDatabase(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (
                var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<NotificationContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
