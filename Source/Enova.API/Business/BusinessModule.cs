using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface BusinessModule : Module
    {
        Dictionary Dictionary { get; }
        FeatureDefs FeatureDefs { get; }
    }
}
