using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Business.App;

namespace BAL.Configuration
{
    public interface IDatabasesConfigurationService : IAppService
    {
        IDatabase this[string name] { get; }
        DatabasesConfigurationCollection DatabasesConfigurations { get; }
    }
}
