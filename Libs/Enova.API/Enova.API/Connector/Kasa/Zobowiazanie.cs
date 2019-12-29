using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.Zobowiazanie, Soneta.Kasa", null, typeof(Enova.API.Connector.Kasa.Zobowiazanie))]

namespace Enova.API.Connector.Kasa
{
    internal class Zobowiazanie : Platnosc
    {
    }
}
