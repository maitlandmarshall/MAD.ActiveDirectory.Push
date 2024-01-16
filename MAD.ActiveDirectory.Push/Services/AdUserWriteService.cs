using MAD.ActiveDirectory.Push.Models;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using static MAD.ActiveDirectory.Push.Services.AdUserUpdateTransaction;

namespace MAD.ActiveDirectory.Push.Services
{
    public class AdUserWriteService
    {
        private readonly PrincipalContextFactory principalContextFactory;

        public AdUserWriteService(PrincipalContextFactory principalContextFactory)
        {
            this.principalContextFactory = principalContextFactory;
        }

        public AdUserUpdateTransaction StartUpdateTransaction(AdWritebackData adWritebackData)
        {
            var ctx = this.principalContextFactory.Create();
            var userPrincipal = UserPrincipal.FindByIdentity(ctx, IdentityType.UserPrincipalName, adWritebackData.Email);
            var directoryEntry = userPrincipal.GetUnderlyingObject() as DirectoryEntry;

            // Get the update targets, which is just deltas to avoid updating fields that have no change
            var updateTargets = this.GetUpdateTargets(directoryEntry, adWritebackData).ToList();

            return new AdUserUpdateTransaction(ctx, userPrincipal, directoryEntry, updateTargets);
        }

        private IEnumerable<UpdateTarget> GetUpdateTargets(DirectoryEntry original, AdWritebackData data)
        {
            foreach (var delta in data.Deltas)
            {
                var propToOverwrite = original.Properties[delta.Attribute];
                var newValue = delta.NewValue;
                var oldValue = propToOverwrite.Value;

                if (newValue is null
                    && oldValue is null)
                    continue;

                if (newValue?.Equals(oldValue) == true)
                    continue;

                // Return the properties to update
                yield return new UpdateTarget
                {
                    NewValue = newValue,
                    OldValue = oldValue,
                    PropertyToOverwrite = propToOverwrite
                };
            }
        }
    }
}
