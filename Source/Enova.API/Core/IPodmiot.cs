using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Core
{
    public interface IPodmiot : INipHost, IEuVatHost, Business.IRow, Business.ISessionable
    {
        // Properties
        Adres Adres { get; }
        bool Blokada { get; }
        string Kod { get; }
        string Nazwa { get; }
        string NazwaFormatowana { get; }
        string NazwaPierwszaLinia { get; }
        //Percent Rabat { get; }
        decimal Rabat { get; }
        RodzajPodmiotu RodzajPodmiotu { get; }
        StatusPodmiotu StatusPodmiotu { get; }
        TypPodmiotu Typ { get; }
    }




}
