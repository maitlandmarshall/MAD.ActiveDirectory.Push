using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace MAD.ActiveDirectory.Push.Services
{
    public class AdUserUpdateTransaction : IDisposable
    {
        private readonly PrincipalContext ctx;
        private readonly UserPrincipal userPrincipal;
        private readonly DirectoryEntry directoryEntry;

        public AdUserUpdateTransaction(PrincipalContext ctx, UserPrincipal userPrincipal, DirectoryEntry directoryEntry, IEnumerable<UpdateTarget> updateTargets)
        {
            this.ctx = ctx;
            this.userPrincipal = userPrincipal;
            this.directoryEntry = directoryEntry;

            this.UpdateTargets = updateTargets;
        }

        public IEnumerable<UpdateTarget> UpdateTargets { get; }

        public bool HasChanges()
        {
            return this.UpdateTargets.Any();
        }

        public void Commit()
        {
            // Finally set the AD property to the new value
            foreach (var ut in this.UpdateTargets)
            {
                ut.PropertyToOverwrite.Value = ut.NewValue;
            }

            // Commit to AD
            this.directoryEntry.CommitChanges();
        }

        public void Dispose()
        {
            this.ctx?.Dispose();
            this.userPrincipal?.Dispose();
            this.directoryEntry?.Dispose();
        }

        public class UpdateTarget
        {
            public object NewValue { get; set; }
            public object OldValue { get; set; }

            public PropertyValueCollection PropertyToOverwrite { get; set; }
        }
    }
}
