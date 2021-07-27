using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push.Models
{
    public class User
    {
        [MaxLength(40)]
        public string Id { get; set; }

        public string DistinguishedName { get; set; }

        [MaxLength(256)]
        public string UserPrincipalName { get; set; }
        public string Name { get; set; }

        public string GivenName { get; set; }
        public string Sn { get; set; }
        public string DisplayName { get; set; }

        public string Department { get; set; }
        public string Title { get; set; }
        public string Mobile { get; set; }
        public string PhysicalDeliveryOfficeName { get; set; }
        public string C { get; set; }
        public string Co { get; set; }

        public string ExtensionAttribute1 { get; set; }
        public string ExtensionAttribute2 { get; set; }
        public string ExtensionAttribute3 { get; set; }
        public string ExtensionAttribute4 { get; set; }
        public string ExtensionAttribute5 { get; set; }

        public string Manager { get; set; }

        public DateTime ExtractedDate { get; set; }
    }
}
