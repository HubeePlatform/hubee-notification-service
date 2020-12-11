using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hubee.Logging.Sdk.Extension;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hubee.NotificationApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            CreateWebHostBuilder(args, configuration).Build().Run();
        }
        
        private static IConfigurationRoot GetConfiguration()
        {
            var webHostBuilder = new WebHostBuilder();
            var environment = webHostBuilder.GetSetting("environment");
            
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables();
            
            return builder.Build();
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfigurationRoot configuration) =>
           WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .UseHubeeLogging(configuration);
    }

}
