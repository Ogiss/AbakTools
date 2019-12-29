using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Core.Adres, Soneta.Core", typeof(Enova.API.Core.Adres), typeof(Enova.API.Connector.Core.Adres))]

namespace Enova.API.Core
{
    public interface Adres
    {
        // Properties
        string Faks { get; set;  }
        string Gmina { get; }
//        string KodKraju { get; }
        string KodPocztowy { get; }
//        string Kraj { get; }
//        string Linia1 { get; }
//        string Linia2 { get; }
        string Miejscowosc { get; set; }
        string NrDomu { get; set; }
        string NrLokalu { get; set; }
        string Poczta { get; set; }
//        string Powiat { get; }
        string Telefon { get; set; }
        string Ulica { get; set; }
//        Wojewodztwa Wojewodztwo { get; }
//        string ZagranicznyKodPocztowy { get; }
    }




}
