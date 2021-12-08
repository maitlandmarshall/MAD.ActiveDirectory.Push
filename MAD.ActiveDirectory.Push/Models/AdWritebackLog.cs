using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.ActiveDirectory.Push.Models
{
    public class AdWritebackLog
    {
        public string Email { get; set; }
        public DateTimeOffset Date { get; set; }

        public IDictionary<string, object> OldValues { get; set; }
        public IDictionary<string, object> NewValues { get; set; }
    }
}
