using Newtonsoft.Json;
using System.Collections.Generic;

namespace MAD.ActiveDirectory.Push.Models
{
    public class AdWritebackDelta
    {
        [JsonProperty("User Comparison[Email]")]
        public string Email { get; set; }

        [JsonProperty("User Comparison[Attribute]")]
        public string Attribute { get; set; }

        [JsonProperty("User Comparison[AD]")]
        public string OldValue { get; set; }

        [JsonProperty("User Comparison[Namely]")]
        public string NewValue { get; set; }
    }

    public class AdWritebackData
    {
        public string Email { get; set; }
        public IEnumerable<AdWritebackDelta> Deltas { get; set; } = new List<AdWritebackDelta>();
    }
}
