using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.Core;

namespace Enova.API.Kasa
{
    public interface IDokumentPlatny2 : IDokumentPlatny, IDokumentPodmiotu, IDokument, IRow, ISessionable
    {
        // Properties
        bool Anulowany { get; }
        KierunekPlatnosci Kierunek { get; }
        string NazwaPola { get; }
        bool Ostrzeżenie { get; }
        bool PlanSplat { get; }
        Currency Wartosc { get; }
        bool Wielowalutowy { get; }
    }
}
