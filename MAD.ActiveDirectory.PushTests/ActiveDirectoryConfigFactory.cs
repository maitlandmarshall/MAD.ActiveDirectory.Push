using MAD.ActiveDirectory.Push;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.PushTests
{
    public static class ActiveDirectoryConfigFactory
    {
        public static ActiveDirectoryConfig Create()
        {
            var config = File.ReadAllText("settings.default.json");
            return JsonConvert.DeserializeObject<ActiveDirectoryConfig>(config);
        }
    }
}
