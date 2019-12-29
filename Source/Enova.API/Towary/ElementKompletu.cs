using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Towary.ElementKompletu, Soneta.Handel", typeof(Enova.API.Towary.ElementKompletu), typeof(Enova.API.Connector.Towary.ElementKompletu)),
           RowMap("ElementyKompletow", typeof(Enova.API.Towary.ElementKompletu), typeof(Enova.API.Towary.TowaryModule))]

namespace Enova.API.Towary
{
    public interface ElementKompletu : Business.Row
    {
        TypElementuKompletu Typ { get; }
        Magazyny.Magazyn Magazyn { get; }
        Towar Towar { get; }
        double Ilosc { get; }
    }
}
