using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Database.Context
{
    public class NotificationContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }

        public NotificationContext(DbContextOptions<NotificationContext> options): base(options)
        { 

        }

        public NotificationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                return;
            }

            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("HUBEE_CONNECTION_STRING"));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TemplateConfiguration());
        }
    }
}
