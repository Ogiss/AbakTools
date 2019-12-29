using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kadry.TypPracownika, Soneta.KadryPlace", null, typeof(Enova.API.Kadry.TypPracownika))]

namespace Enova.API.Kadry
{
    public enum TypPracownika
    {
        OsobaWspółpracująca = 3,
        Pracownik = 1,
        Właściciel = 2
    }
}
