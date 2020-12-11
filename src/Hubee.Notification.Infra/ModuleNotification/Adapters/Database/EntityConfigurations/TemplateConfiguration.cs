using Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hubee.NotificationApp.Infra.ModuleNotification.Adapters.Database.EntityConfigurations
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable(nameof(Template));
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(e => e.NotificationType)
                .IsRequired();

            builder
                .Property(e => e.TemplateType)
                .IsRequired();

            builder
                .Property(e => e.TemplateVersion)
                .IsRequired();

            builder
                .Property(e => e.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(e => e.Content)
                .HasColumnType("TEXT")
                .IsRequired();

            builder
                .Property(e => e.IsRendered)
                .IsRequired();

            builder.HasQueryFilter(e => e.DeletedAt == null);
        }
    }
}