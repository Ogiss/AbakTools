using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BAL.Business;

namespace BAL.Configuration
{
    public interface IAppConfigurationService : IAppService
    {
        System.Configuration.Configuration Configuration { get; }
        IDatabasesConfigurationService DatabasesConfiguration { get; }
        void Init();
    }
}
