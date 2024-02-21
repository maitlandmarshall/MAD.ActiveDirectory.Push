using System.DirectoryServices.AccountManagement;

namespace MAD.ActiveDirectory.Push.Services
{
    public class PrincipalContextFactory
    {
        private readonly ActiveDirectoryConfig activeDirectoryConfig;

        public PrincipalContextFactory(ActiveDirectoryConfig activeDirectoryConfig)
        {
            this.activeDirectoryConfig = activeDirectoryConfig;
        }

        public PrincipalContext Create()
        {
            if (string.IsNullOrWhiteSpace(this.activeDirectoryConfig.Username))
            {
                return new PrincipalContext(ContextType.Domain, this.activeDirectoryConfig.Domain);
            }
            else
            {
                return new PrincipalContext(ContextType.Domain, this.activeDirectoryConfig.Domain, this.activeDirectoryConfig.Username, this.activeDirectoryConfig.Password);
            }
        }
    }
}
