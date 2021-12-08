using MAD.ActiveDirectory.Push.Models;
using System;
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
            var updateTargets = this.GetUpdateTargetDeltas(directoryEntry, adWritebackData).ToList();

            return new AdUserUpdateTransaction(ctx, userPrincipal, directoryEntry, updateTargets);
        }

        private IEnumerable<UpdateTarget> GetUpdateTargetDeltas(DirectoryEntry original, AdWritebackData update)
        {
            var propsToUpdate = this.GetPropertiesToUpdate();
            var updateType = typeof(AdWritebackData);

            foreach (var p in propsToUpdate)
            {
                var propToOverwrite = original.Properties[p];

                // Coalesque to string.Empty for easier comparison between "" and null
                var newValue = updateType.GetProperty(p).GetValue(update) ?? string.Empty;
                var oldValue = propToOverwrite.Value ?? string.Empty;

                if (newValue is string newValStr
                    && oldValue is string oldValStr)
                {
                    if (string.IsNullOrWhiteSpace(newValStr)
                        && string.IsNullOrWhiteSpace(oldValStr))
                        continue;

                    // Make sure to do a case insensitive equality check
                    if (string.Equals(newValStr, oldValStr, StringComparison.InvariantCultureIgnoreCase))
                        continue;
                }
                else
                {
                    if (newValue.Equals(oldValue))
                        continue;
                }

                // Return the properties to update
                yield return new UpdateTarget
                {
                    NewValue = newValue,
                    OldValue = oldValue,
                    PropertyToOverwrite = propToOverwrite
                };
            }
        }

        private IEnumerable<string> GetPropertiesToUpdate()
        {
            yield return "GivenName";


            yield return "Sn";


            yield return "DisplayName";


            yield return "Department";


            yield return "Title";


            yield return "Mobile";


            yield return "PhysicalDeliveryOfficeName";


            yield return "C";


            yield return "Co";


            yield return "ExtensionAttribute1";


            yield return "ExtensionAttribute2";


            yield return "ExtensionAttribute3";


            yield return "ExtensionAttribute4";


            yield return "ExtensionAttribute5";


            yield return "ExtensionAttribute6";


            yield return "ExtensionAttribute7";


            yield return "Manager";
        }

    }
}
