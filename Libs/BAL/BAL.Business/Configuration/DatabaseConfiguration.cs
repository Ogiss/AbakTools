using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Configuration
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
    }
}
