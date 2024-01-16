using MAD.ActiveDirectory.Push.Models;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;
using System;
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

        public async Task<IEnumerable<AdWritebackData>> Get(IEnumerable<string> extraFilters = null)
        {
            var token = await this.aadAuthClient.GetAccessToken();
            var client = new PowerBIClient(new TokenCredentials(token));

            // Create filter statements from extraFilters and these base filters
            var finalFilterStatements = new[]
            {
                "'User Comparison'[HasChanged] = TRUE() && 'User Comparison'[HasNameyUser] = TRUE()",
                "'Namely User'[User status] in { \"Active Employee\", \"Pending Employee\" }"
            }.Union(extraFilters);

            // Build the DAX query using all the filters
            var baseQuery = @$"evaluate CALCULATETABLE('User Comparison', {string.Join(',' + Environment.NewLine, finalFilterStatements)} )";

            var response = client.Datasets.ExecuteQueries(this.activeDirectoryConfig.PowerBIDataSetId, new Microsoft.PowerBI.Api.Models.DatasetExecuteQueriesRequest
            {
                Queries = new[]
                {
                    new Microsoft.PowerBI.Api.Models.DatasetExecuteQueriesQuery (baseQuery)
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

            return deltas.Select(y => new AdWritebackData
            {
                Email = y.Key,
                Deltas = y.Value
            }).ToList();
        }
    }
}