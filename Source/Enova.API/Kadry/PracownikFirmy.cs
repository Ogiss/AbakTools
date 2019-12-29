using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kadry.PracownikFirmy, Soneta.KadryPlace", typeof(Enova.API.Kadry.PracownikFirmy), typeof(Enova.API.Connector.Kadry.PracownikFirmy))]

namespace Enova.API.Kadry
{
    public interface PracownikFirmy : IPracownik
    {
    }
}
