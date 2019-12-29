using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.CRM.Kontrahenci, Soneta.CRM", typeof(Enova.API.CRM.Kontrahenci), typeof(Enova.API.Connector.CRM.Kontrahenci))]

namespace Enova.API.CRM
{
    public interface Kontrahenci : Business.GuidedTable<Kontrahent>
    {
    }
}
