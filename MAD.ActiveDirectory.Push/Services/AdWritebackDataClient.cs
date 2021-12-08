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
                    new Microsoft.PowerBI.Api.Models.DatasetExecuteQueriesQuery ("evaluate FILTER('_Writeback Data', [Has Update] = TRUE() && [Manager Is Mapped] = TRUE() && [Source] = \"Update\")")
                }
            });

            var rows = response
                .Results
                .First()
                .Tables
                .First()
                .Rows
                .Cast<JObject>()
                .Select(y => y.ToObject<AdWritebackData>());

            return rows.ToList();
        }
    }
}