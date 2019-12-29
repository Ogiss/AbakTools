using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.CRM.PFRONCentrala, Soneta.CRM", typeof(Enova.API.CRM.PFRONCentrala), typeof(Enova.API.Connector.CRM.PFRONCentrala))]

namespace Enova.API.CRM
{
    public interface PFRONCentrala : ZUS
    {
    }
}
