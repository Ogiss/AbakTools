using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.CRM.ZUSCentrala, Soneta.CRM", typeof(Enova.API.CRM.ZUSCentrala), typeof(Enova.API.Connector.CRM.ZUSCentrala))]

namespace Enova.API.CRM
{
    public interface ZUSCentrala : ZUS
    {
    }
}
