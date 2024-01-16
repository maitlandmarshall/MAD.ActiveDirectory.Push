using Hangfire;
using MAD.ActiveDirectory.Push.Models;
using MAD.ActiveDirectory.Push.Services;
using System;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Jobs
{
    public class AdWritebackJob
    {
        private readonly AdUserWriteService adUserWriteService;
        private readonly AdWritebackDataClient adWritebackDataClient;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly AdUserUpdateTransactionDatabaseLogger databaseLogger;

        public AdWritebackJob(AdUserWriteService adUserWriteService, AdWritebackDataClient adWritebackDataClient, IBackgroundJobClient backgroundJobClient, AdUserUpdateTransactionDatabaseLogger databaseLogger)
        {
            this.backgroundJobClient = backgroundJobClient;
            this.databaseLogger = databaseLogger;
            this.adUserWriteService = adUserWriteService;
            this.adWritebackDataClient = adWritebackDataClient;
        }

        public async Task EnqueueUsersToUpdate()
        {
            var writebackData = await this.adWritebackDataClient.Get();

            foreach (var wbd in writebackData)
            {
                this.backgroundJobClient.Enqueue<AdWritebackJob>(y => y.UpdateUser(wbd));
            }
        }

        public async Task UpdateUser(AdWritebackData adWritebackData)
        {
            using var updateTransaction = this.adUserWriteService.StartUpdateTransaction(adWritebackData);

            if (updateTransaction.HasChanges() == false)
                return;

            await this.databaseLogger.Log(adWritebackData.Email, DateTimeOffset.Now, updateTransaction);

            updateTransaction.Commit();
        }
    }
}
