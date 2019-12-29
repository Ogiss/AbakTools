using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.Naleznosc, Soneta.Kasa", null, typeof(Enova.API.Connector.Kasa.Naleznosc))]

namespace Enova.API.Connector.Kasa
{
    internal class Naleznosc : Platnosc
    {
    }
}
