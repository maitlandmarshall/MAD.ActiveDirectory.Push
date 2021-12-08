using MAD.ActiveDirectory.Push.Jobs;
using MAD.ActiveDirectory.Push.Models;
using MAD.ActiveDirectory.Push.Services;
using MAD.Integration.Common.Jobs;
using MAD.Integration.Common.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MAD.ActiveDirectory.Push
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddIntegrationSettings<ActiveDirectoryConfig>();
            serviceDescriptors.AddSingleton(y => y.GetRequiredService<IOptions<ActiveDirectoryConfig>>().Value);
            serviceDescriptors.AddTransient<PrincipalContextFactory>();

            serviceDescriptors.AddScoped<AdUserReadService>();
            serviceDescriptors.AddScoped<AdUserWriteService>();
            serviceDescriptors.AddScoped<UserJobController>();

            serviceDescriptors.AddTransient<AadAuthClient>();
            serviceDescriptors.AddTransient<AdWritebackDataClient>();
            serviceDescriptors.AddTransient<AdWritebackJob>();
            serviceDescriptors.AddTransient<AdUserUpdateTransactionDatabaseLogger>();

            serviceDescriptors.AddDbContext<ADDbContext>(optionsAction: (svc, builder) => builder.UseSqlServer(svc.GetRequiredService<ActiveDirectoryConfig>().ConnectionString));
        }

        public void PostConfigure(ADDbContext dbContext, IRecurringJobFactory recurringJobFactory)
        {
            dbContext.Database.Migrate();

            recurringJobFactory.CreateRecurringJob<UserJobController>(nameof(UserJobController) + nameof(UserJobController.ExtractAndLoad), y => y.ExtractAndLoad());
        }
    }
}