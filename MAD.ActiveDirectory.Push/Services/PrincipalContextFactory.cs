using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
