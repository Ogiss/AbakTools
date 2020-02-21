using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Configuration
{
    public interface IDatabaseConfiguration
    {
        string Name { get; }
        string ConnectionString { get; }
        string ProviderName { get; }
    }
}
