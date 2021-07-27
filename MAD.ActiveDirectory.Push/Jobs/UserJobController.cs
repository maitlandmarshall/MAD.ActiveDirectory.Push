using EFCore.BulkExtensions;
using MAD.ActiveDirectory.Push.Actions;
using MAD.ActiveDirectory.Push.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Jobs
{
    public class UserJobController
    {
        private readonly EnumerateUsers enumerateUsers;
        private readonly ADDbContext dbContext;

        public UserJobController(EnumerateUsers enumerateUsers, ADDbContext dbContext)
        {
            this.enumerateUsers = enumerateUsers;
            this.dbContext = dbContext;
        }

        public async Task ExtractAndLoad()
        {
            var users = this.enumerateUsers.GetUsers().ToList();
            await dbContext.BulkInsertOrUpdateAsync(users);
        }
    }
}
