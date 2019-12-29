using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Magazyny.StanMagazynuWorker, Soneta.Handel", typeof(Enova.API.Magazyny.StanMagazynuWorker), typeof(Enova.API.Connector.Magazyny.StanMagazynuWorker))]

namespace Enova.API.Magazyny
{
    public interface StanMagazynuWorker : Types.IObjectBase
    {
        Towary.Towar Towar { get; set; }
        Towary.Quantity Stan { get; }
        Towary.Quantity StanMagazynu { get; }
        Magazyn Magazyn { get; set; }
        Magazyn[] Magazyny { get; set; }
    }
}
