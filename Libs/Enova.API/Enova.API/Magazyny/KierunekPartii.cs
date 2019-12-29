using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.API.Magazyny.KierunekPartii, Soneta.Handel", null, typeof(Enova.API.Magazyny.KierunekPartii))]

namespace Enova.API.Magazyny
{
    public enum KierunekPartii
    {
        Brak = 0,
        Przychód = 1,
        Rozchód = -1
    }
}
