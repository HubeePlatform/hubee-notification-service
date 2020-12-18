using Hubee.Common.Events.Sdk.Events.Notification;
using System.Collections.Generic;

namespace Hubee.NotificationApp.Tests.Core.TestData
{
    public static class TemplateMapperData
    {
        public static TemplateMapper GetValidTemplateMapper()
        {
            var titleMapper = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("@nome", "Luiz Henrique Miranda de assis"),
            };

            var titleMessage = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("@nome", "Luiz Henrique Miranda de assis"),
                new KeyValuePair<string, string>("@idade", "25 anos"),
            };

            return new TemplateMapper
            {
                Title = titleMapper,
                Message = titleMessage
            };
        }
    }
}