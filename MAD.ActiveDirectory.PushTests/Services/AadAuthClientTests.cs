using MAD.ActiveDirectory.Push.Models;
using MAD.ActiveDirectory.PushTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

        // This test method has side effects and will alter AD
        [DataTestMethod]
        [DataRow("'AD OU'[OU_DistinguishedName] = \"Bangalore\"", false)]
        public async Task AdWritebackCommitTest(string filter, bool commit = false)
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

            var writebackData = await adWritebackDataClient.Get(new[] { filter });
            writebackData = writebackData.Take(10);

            foreach (var wbd in writebackData)
            {
                using var updateTransaction = adUserWriteService.StartUpdateTransaction(wbd);

                if (updateTransaction.HasChanges() == false)
                    continue;

                await logger.Log(wbd.Email, DateTimeOffset.Now, updateTransaction);

                if (commit)
                    updateTransaction.Commit();
            }
        }
    }
}