using Microsoft.Identity.Client;
using System.Security;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Services
{
    public class AadAuthClient
    {
        private readonly ActiveDirectoryConfig activeDirectoryConfig;

        public AadAuthClient(ActiveDirectoryConfig activeDirectoryConfig)
        {
            this.activeDirectoryConfig = activeDirectoryConfig;
        }

        public async Task<string> GetAccessToken()
        {
            var clientApp = PublicClientApplicationBuilder.Create(this.activeDirectoryConfig.PowerBIClientId).WithAuthority("https://login.microsoftonline.com/organizations/").Build();
            var authResult = await clientApp.AcquireTokenByUsernamePassword(new[] { "https://analysis.windows.net/powerbi/api/.default" }, this.activeDirectoryConfig.PowerBIUsername, this.GetSecureString()).ExecuteAsync();

            return authResult.AccessToken;
        }

        private SecureString GetSecureString()
        {
            var secureString = new SecureString();

            foreach (var k in this.activeDirectoryConfig.PowerBIPassword)
            {
                secureString.AppendChar(k);
            }

            return secureString;
        }
    }
}
