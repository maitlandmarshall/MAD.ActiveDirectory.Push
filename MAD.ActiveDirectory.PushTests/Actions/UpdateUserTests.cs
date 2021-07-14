using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAD.ActiveDirectory.Push.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Jobs.Tests
{
    [TestClass()]
    public class UpdateUserTests
    {
        [TestMethod()]
        public async Task UpdateTest()
        {
            var updateUser = new UpdateUser();
            await updateUser.Update(null, null);
        }
    }
}