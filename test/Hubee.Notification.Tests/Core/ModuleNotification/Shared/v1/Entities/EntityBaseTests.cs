using Hubee.NotificationApp.Tests.Core.TestData;
using System;
using Xunit;

namespace Hubee.NotificationApp.Tests.Core.ModuleNotification.Shared.v1.Entities
{
    public class EntityBaseTests
    {
        [Fact]
        public void When_EntityIsCreated_Then_CreatedDateIsPopulated()
        {
            var currentTick = DateTime.UtcNow.Ticks;
            var template = TemplateData.GetValidTemplate();

            Assert.NotEqual(Guid.Empty, template.Id);
            Assert.True(currentTick <= template.CreatedAt.Ticks);
            Assert.Null(template.DeletedAt);
        }

        [Fact]
        public void When_EntityIsDeleted_Then_DeletedDateIsPopulated()
        {
            var template = TemplateData.GetValidTemplate();

            template.Delete();

            Assert.NotNull(template.DeletedAt);
        }
    }
}