using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAD.ActiveDirectory.Push.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAD.ActiveDirectory.PushTests;
using Microsoft.EntityFrameworkCore;
using MAD.ActiveDirectory.Push.Models;
using EFCore.BulkExtensions;
using MAD.ActiveDirectory.Push.Services;

namespace MAD.ActiveDirectory.Push.Actions.Tests
{
    [TestClass()]
    public class EnumerateUsersTests
    {

        [TestMethod()]
        public async Task GetUsersTest()
        {
            var cfg = ActiveDirectoryConfigFactory.Create();
            using var dbContext = new ADDbContext(new DbContextOptionsBuilder().UseSqlServer(cfg.ConnectionString).Options);
            await dbContext.Database.MigrateAsync();

            var enumUsers = new AdUserReadService(new Services.PrincipalContextFactory(cfg));
            var users = enumUsers.GetUsers().ToList();

            await dbContext.BulkInsertOrUpdateAsync(users);
        }
    }
}