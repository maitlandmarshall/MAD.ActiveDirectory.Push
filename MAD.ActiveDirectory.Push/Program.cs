using MAD.ActiveDirectory.Push.Jobs;
using MAD.ActiveDirectory.Push.Models;
using MAD.Integration.Common;
using MAD.Integration.Common.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MAD.ActiveDirectory.Push
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            MigrateAndInitialize(host.Services);
            host.Run();
        }

        static void MigrateAndInitialize(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<ADDbContext>();
            dbContext.Database.Migrate();
            JobFactory.CreateRecurringJob<UserJobController>(nameof(UserJobController) + nameof(UserJobController.ExtractAndLoad), y => y.ExtractAndLoad());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            IntegrationHost.CreateDefaultBuilder(args)
                .UseAppInsights()
                .UseAspNetCore()
                .UseStartup<Startup>();
    }
}
