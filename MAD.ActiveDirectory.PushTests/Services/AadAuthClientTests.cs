using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAD.ActiveDirectory.Push.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAD.ActiveDirectory.PushTests;
using MAD.ActiveDirectory.Push.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using MAD.ActiveDirectory.Push.Models;
using Microsoft.EntityFrameworkCore;

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

            await adWritebackDataClient.Get();
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
            await dbContext.Database.MigrateAsync();

            var writebackData = await adWritebackDataClient.Get();

            foreach (var wbd in writebackData)
            {
                if (wbd.Email != "abby.kesselman@unispace.com")
                    continue;

                using var updateTransaction = adUserWriteService.StartUpdateTransaction(wbd);
                await logger.Log(wbd.Email, DateTimeOffset.Now, updateTransaction);

                return;
            }
        }

        [TestMethod]
        public async Task AdWritebackCommitTest()
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);

            var svc = services.BuildServiceProvider();
            var adWritebackDataClient = svc.GetRequiredService<AdWritebackDataClient>();
            var adUserWriteService = svc.GetRequiredService<AdUserWriteService>();
            var logger = svc.GetRequiredService<AdUserUpdateTransactionDatabaseLogger>();
            var dbContext = svc.GetRequiredService<ADDbContext>();
            await dbContext.Database.MigrateAsync();

            var writebackData = await adWritebackDataClient.Get();

            foreach (var wbd in writebackData)
            {
                if (wbd.Email != "abby.kesselman@unispace.com")
                    continue;

                using var updateTransaction = adUserWriteService.StartUpdateTransaction(wbd);
                await logger.Log(wbd.Email, DateTimeOffset.Now, updateTransaction);
                updateTransaction.Commit();

                return;
            }
        }
    }
}