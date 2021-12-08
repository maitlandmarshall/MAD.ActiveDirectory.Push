using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.ActiveDirectory.Push.Services
{
    public class AdWritebackData
    {
        [JsonProperty("_Writeback Data[Source]")]
        public string Source { get; set; }

        [JsonProperty("_Writeback Data[email]")]
        public string Email { get; set; }

        [JsonProperty("_Writeback Data[givenName]")]
        public string GivenName { get; set; }

        [JsonProperty("_Writeback Data[sn]")]
        public string Sn { get; set; }

        [JsonProperty("_Writeback Data[displayName]")]
        public string DisplayName { get; set; }

        [JsonProperty("_Writeback Data[department]")]
        public string Department { get; set; }

        [JsonProperty("_Writeback Data[title]")]
        public string Title { get; set; }

        [JsonProperty("_Writeback Data[mobile]")]
        public string Mobile { get; set; }

        [JsonProperty("_Writeback Data[physicalDeliveryOfficeName]")]
        public string PhysicalDeliveryOfficeName { get; set; }

        [JsonProperty("_Writeback Data[extensionAttribute1]")]
        public string ExtensionAttribute1 { get; set; }

        [JsonProperty("_Writeback Data[extensionAttribute2]")]
        public string ExtensionAttribute2 { get; set; }

        [JsonProperty("_Writeback Data[extensionAttribute3]")]
        public string ExtensionAttribute3 { get; set; }

        [JsonProperty("_Writeback Data[extensionAttribute4]")]
        public string ExtensionAttribute4 { get; set; }

        [JsonProperty("_Writeback Data[extensionAttribute5]")]
        public string ExtensionAttribute5 { get; set; }

        [JsonProperty("_Writeback Data[Has Update]")]
        public bool HasUpdate { get; set; }

        [JsonProperty("_Writeback Data[Manager Is Mapped]")]
        public bool ManagerIsMapped { get; set; }

        [JsonProperty("_Writeback Data[co]")]
        public string Co { get; set; }

        [JsonProperty("_Writeback Data[status]")]
        public string Status { get; set; }

        [JsonProperty("_Writeback Data[extensionAttribute6]")]
        public string ExtensionAttribute6 { get; set; }

        [JsonProperty("_Writeback Data[extensionAttribute7]")]
        public string ExtensionAttribute7 { get; set; }

        [JsonProperty("_Writeback Data[c]", NullValueHandling = NullValueHandling.Ignore)]
        public string C { get; set; }

        [JsonProperty("_Writeback Data[manager]", NullValueHandling = NullValueHandling.Ignore)]
        public string Manager { get; set; }
    }
}
