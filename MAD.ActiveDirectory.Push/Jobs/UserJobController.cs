using EFCore.BulkExtensions;
using MAD.ActiveDirectory.Push.Models;
using MAD.ActiveDirectory.Push.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Jobs
{
    public class UserJobController
    {
        private readonly AdUserReadService enumerateUsers;
        private readonly ADDbContext dbContext;

        public UserJobController(AdUserReadService enumerateUsers, ADDbContext dbContext)
        {
            this.enumerateUsers = enumerateUsers;
            this.dbContext = dbContext;
        }

        public async Task ExtractAndLoad()
        {
            var users = this.enumerateUsers.GetUsers().ToList();
            await this.dbContext.BulkInsertOrUpdateAsync(users);
        }
    }
}
