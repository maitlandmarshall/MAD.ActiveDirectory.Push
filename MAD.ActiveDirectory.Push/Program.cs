using MAD.Integration.Common;
using Microsoft.Extensions.Hosting;
using System;

namespace MAD.ActiveDirectory.Push
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            IntegrationHost.CreateDefaultBuilder(args)
                .UseAppInsights()
                .UseAspNetCore()
                .UseStartup<Startup>();
    }
}
