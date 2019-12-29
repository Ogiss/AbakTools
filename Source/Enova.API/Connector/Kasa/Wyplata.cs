using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.Wyplata, Soneta.Kasa", null,typeof(Enova.API.Connector.Kasa.Wyplata))]

namespace Enova.API.Connector.Kasa
{
    internal class Wyplata : Zaplata
    {
    }
}
