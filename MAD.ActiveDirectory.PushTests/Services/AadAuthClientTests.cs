using MAD.ActiveDirectory.Push.Models;
using MAD.ActiveDirectory.PushTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Services.Tests
{
    [TestClass()]
    public class AadAuthClientTests
    {
        [TestMethod()]
        public async Task GetAccessTokenTest()
        {
            var cfg = ActiveDirectoryConfigFactory.Create();
            var aadAuthClient = new AadAuthClient(cfg);

            var at = await aadAuthClient.GetAccessToken();
        }

        [TestMethod()]
        public async Task GetAdWriteBackDataTest()
        {
            var cfg = ActiveDirectoryConfigFactory.Create();
            var aadAuthClient = new AadAuthClient(cfg);
            var adWritebackDataClient = new AdWritebackDataClient(aadAuthClient, cfg);

            var result = await adWritebackDataClient.Get();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task AdWritebackTest()
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);

            var svc = services.BuildServiceProvider();
            var adWritebackDataClient = svc.GetRequiredService<AdWritebackDataClient>();
            var adUserWriteService = svc.GetRequiredService<AdUserWriteService>();
            var logger = svc.GetRequiredService<AdUserUpdateTransactionDatabaseLogger>();
            var dbContext = svc.GetRequiredService<ADDbContext>();

            var writebackData = await adWritebackDataClient.Get(new[] { "'AD OU'[OU_DistinguishedName] = \"Bangalore\"" });

            await dbContext.Database.MigrateAsync();

            foreach (var wbd in writebackData)
            {
                using var updateTransaction = adUserWriteService.StartUpdateTransaction(wbd);
                await logger.Log(wbd.Email, DateTimeOffset.Now, updateTransaction);

                return;
            }
        }

        [TestMethod]
        public async Task AdWritebackCommitTest()
        {
            var cfg = ActiveDirectoryConfigFactory.Create();

            var services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);

            using var ctx = new PrincipalContext(ContextType.Domain, "unispace.com", cfg.Username, cfg.Password);

            var svc = services.BuildServiceProvider();
            var adWritebackDataClient = svc.GetRequiredService<AdWritebackDataClient>();
            var adUserWriteService = svc.GetRequiredService<AdUserWriteService>();
            var logger = svc.GetRequiredService<AdUserUpdateTransactionDatabaseLogger>();
            var dbContext = svc.GetRequiredService<ADDbContext>();
            await dbContext.Database.MigrateAsync();

            var writebackData = await adWritebackDataClient.Get();

            using var up = new UserPrincipal(ctx);
            using var searcher = new PrincipalSearcher(up);

            var bangalore = searcher.FindAll().Where(y => y.DistinguishedName.Contains("Bangalore")).ToList();

            foreach (var sa in bangalore)
            {
                var deUser = sa.GetUnderlyingObject() as DirectoryEntry;
                var wbd = writebackData.FirstOrDefault(y => y.Email == sa.UserPrincipalName);

                if (wbd is null)
                {
                    continue;
                }

                using var updateTransaction = adUserWriteService.StartUpdateTransaction(wbd);

                if (updateTransaction.HasChanges() == false)
                    continue;

                await logger.Log(wbd.Email, DateTimeOffset.Now, updateTransaction);
                updateTransaction.Commit();
            }
        }
    }
}