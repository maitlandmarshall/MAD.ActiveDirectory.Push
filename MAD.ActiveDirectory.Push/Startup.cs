using Hangfire;
using MAD.ActiveDirectory.Push.Actions;
using MAD.ActiveDirectory.Push.Jobs;
using MAD.ActiveDirectory.Push.Models;
using MAD.ActiveDirectory.Push.Services;
using MAD.Integration.Common.Jobs;
using MAD.Integration.Common.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddIntegrationSettings<ActiveDirectoryConfig>();
            serviceDescriptors.AddSingleton(y => y.GetRequiredService<IOptions<ActiveDirectoryConfig>>().Value);
            serviceDescriptors.AddTransient<PrincipalContextFactory>();

            serviceDescriptors.AddScoped<EnumerateUsers>();
            serviceDescriptors.AddScoped<UserJobController>();
            serviceDescriptors.AddScoped<UpdateUser>();

            serviceDescriptors.AddDbContext<ADDbContext>(optionsAction: (svc, builder) => builder.UseSqlServer(svc.GetRequiredService<ActiveDirectoryConfig>().ConnectionString));
        }
    }
}