using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class SqlServer
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Instance { get; set; }
        public bool IsClustered { get; set; }
        public string Version { get; set; }
        public bool IsLocal { get; set; }
    }
}
