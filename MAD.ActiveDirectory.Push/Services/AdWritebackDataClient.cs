using MAD.ActiveDirectory.Push.Models;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Services
{
    public class AdWritebackDataClient
    {
        private readonly AadAuthClient aadAuthClient;
        private readonly ActiveDirectoryConfig activeDirectoryConfig;

        public AdWritebackDataClient(AadAuthClient aadAuthClient, ActiveDirectoryConfig activeDirectoryConfig)
        {
            this.aadAuthClient = aadAuthClient;
            this.activeDirectoryConfig = activeDirectoryConfig;
        }

        public async Task<IEnumerable<AdWritebackData>> Get()
        {
            var token = await this.aadAuthClient.GetAccessToken();
            var client = new PowerBIClient(new TokenCredentials(token));

            var response = client.Datasets.ExecuteQueries(this.activeDirectoryConfig.PowerBIDataSetId, new Microsoft.PowerBI.Api.Models.DatasetExecuteQueriesRequest
            {
                Queries = new[]
                {
                    new Microsoft.PowerBI.Api.Models.DatasetExecuteQueriesQuery ("evaluate FILTER('User Comparison', 'User Comparison'[HasChanged] = TRUE())")
                }
            });

            var rows = response
                .Results
                .First()
                .Tables
                .First()
                .Rows
                .ToList();

            var deltas = rows
                .Cast<JObject>()
                .Select(y => y.ToObject<AdWritebackDelta>())
                .GroupBy(y => y.Email)
                .ToDictionary(y => y.Key, y => y.ToList());

            return rows.Cast<JObject>()
                .Select(y => y.ToObject<AdWritebackData>());
        }
    }
}