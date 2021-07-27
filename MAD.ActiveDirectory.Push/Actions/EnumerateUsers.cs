using MAD.ActiveDirectory.Push.Models;
using MAD.ActiveDirectory.Push.Services;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Actions
{
    public class EnumerateUsers
    {
        private readonly PrincipalContextFactory principalContextFactory;

        public EnumerateUsers(PrincipalContextFactory principalContextFactory)
        {
            this.principalContextFactory = principalContextFactory;
        }

        public IEnumerable<User> GetUsers()
        {
            using var ctx = this.principalContextFactory.Create();
            using var up = new UserPrincipal(ctx);
            using var searcher = new PrincipalSearcher(up);

            var extractDate = DateTime.Now;
            var findAll = searcher.FindAll();

            foreach (var r in findAll)
            {
                var de = r.GetUnderlyingObject() as DirectoryEntry;

                var usr = new User
                {
                    Id = r.Guid?.ToString(),
                    DistinguishedName = r.DistinguishedName,
                    Name = r.Name,
                    UserPrincipalName = this.GetPropertyValue("userPrincipalName", de),
                    GivenName = this.GetPropertyValue("givenName", de),
                    Sn = this.GetPropertyValue("sn", de),
                    DisplayName = this.GetPropertyValue("displayName", de),
                    Department = this.GetPropertyValue("department", de),
                    Title = this.GetPropertyValue("title", de),
                    Mobile = this.GetPropertyValue("mobile", de),
                    PhysicalDeliveryOfficeName = this.GetPropertyValue("physicalDeliveryOfficeName", de),
                    C = this.GetPropertyValue("c", de),
                    Co = this.GetPropertyValue("co", de),
                    ExtensionAttribute1 = this.GetPropertyValue("extensionAttribute1", de),
                    ExtensionAttribute2 = this.GetPropertyValue("extensionAttribute2", de),
                    ExtensionAttribute3 = this.GetPropertyValue("extensionAttribute3", de),
                    ExtensionAttribute4 = this.GetPropertyValue("extensionAttribute4", de),
                    ExtensionAttribute5 = this.GetPropertyValue("extensionAttribute5", de),
                    Manager = this.GetPropertyValue("manager", de),
                    ExtractedDate = extractDate
                };

                yield return usr;
            }
        }

        private string GetPropertyValue(string propName, DirectoryEntry de)
        {
            var prop = de.Properties[propName];

            if (prop.Count == 0)
                return null;

            return prop[0].ToString();
        }
    }
}
