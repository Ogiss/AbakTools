using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.Core;

[assembly: TypeMap("Soneta.Kasa.Kompensata, Soneta.Kasa", typeof(Enova.API.Kasa.Kompensata), typeof(Enova.API.Connector.Kasa.Kompensata))]

namespace Enova.API.Kasa
{
    [Caption("Kompensata")]
    public interface Kompensata : KompensataBase/*, IZmianaPłatnościInfo*/
    {
        KierunekPlatnosci Kierunek { get; }
        Currency Kwota { get; }
        Currency Kwota1 { get; set; }
        Currency Kwota2 { get; set; }
        Currency Należność { get; }
        //[Dictionary("Kompensata", ManualList = true), AttributeInheritance]
        string Opis { get; set; }
        bool PodlegaNocie { get; set; }
        string Słownie { get; }
        [Caption("Słownie")]
        string SłownieUpr { get; }
        Currency Wartosc { get; }
        Currency Zobowiązanie { get; }

    }

}
