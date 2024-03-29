﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.ActiveDirectory.Push
{
    public class ActiveDirectoryConfig
    {
        public string ConnectionString { get; set; }
        public string Domain { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string PowerBIUsername { get; set; }
        public string PowerBIPassword { get; set; }
        public string PowerBIClientId { get; set; }
        public string PowerBIWorkspaceId { get; set; }
        public string PowerBIDataSetId { get; set; }
    }
}
