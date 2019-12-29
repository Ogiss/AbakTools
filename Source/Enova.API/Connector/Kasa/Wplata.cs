using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.Wplata, Soneta.Kasa", null, typeof(Enova.API.Connector.Kasa.Wplata))]

namespace Enova.API.Connector.Kasa
{
    internal class Wplata : Zaplata
    {
    }
}
