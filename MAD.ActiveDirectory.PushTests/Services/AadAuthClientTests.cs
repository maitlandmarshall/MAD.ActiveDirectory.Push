using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAD.ActiveDirectory.Push.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAD.ActiveDirectory.PushTests;

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
    }
}