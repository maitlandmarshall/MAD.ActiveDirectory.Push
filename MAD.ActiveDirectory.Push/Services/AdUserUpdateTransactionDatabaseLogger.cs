using MAD.ActiveDirectory.Push.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Services
{
    public class AdUserUpdateTransactionDatabaseLogger
    {
        private readonly ADDbContext dbContext;

        public AdUserUpdateTransactionDatabaseLogger(ADDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Log(string email, DateTimeOffset date, AdUserUpdateTransaction updateTransaction)
        {
            var log = new AdWritebackLog
            {
                Email = email,
                Date = date
            };

            var oldValues = updateTransaction.UpdateTargets.ToDictionary(
                keySelector: y => y.PropertyToOverwrite.PropertyName,
                elementSelector: y => y.OldValue);

            var newValues = updateTransaction.UpdateTargets.ToDictionary(
                keySelector: y => y.PropertyToOverwrite.PropertyName,
                elementSelector: y => y.NewValue);

            log.OldValues = oldValues;
            log.NewValues = newValues;

            this.dbContext.Add(log);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
