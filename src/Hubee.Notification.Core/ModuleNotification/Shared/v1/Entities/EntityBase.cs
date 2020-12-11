using System;

namespace Hubee.NotificationApp.Core.ModuleNotification.Shared.v1.Entities
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }
}
