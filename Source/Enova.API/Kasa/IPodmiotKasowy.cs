using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.IPodmiotKasowy, Soneta.Kasa", typeof(Enova.API.Kasa.IPodmiotKasowy), typeof(Enova.API.Connector.Kasa.IPodmiotKasowy))]

namespace Enova.API.Kasa
{
    public interface IPodmiotKasowy : /*IPodmiotOdsetkiKarne,*/ Core.IPodmiot, Core.INipHost, Core.IEuVatHost, Business.IRow, Business.ISessionable
    {
        // Properties
        Core.Adres Adres { get; }
        //SubTable DokumentyRozliczeniowe { get; }
        //RachunekBankowyPodmiotu DomyslnyRachunek { get; }
        string Kod { get; }
        int KontrolaDni { get; }
        //Currency LimitKredytu { get; }
        bool LimitNieograniczony { get; }
        string Nazwa { get; }
        string NIP { get; }
        IPodmiotKasowy Platnik { get; }
        //SubTable Platnosci { get; }
        //SubTable Przelewy { get; }
        bool PrzeterminowanieNieograniczone { get; }
        //SubTable Rachunki { get; }
        //FormaPlatnosci SposobZaplaty { get; }
        int Termin { get; }
        int TerminPlanowany { get; }

        //SubTable Zaplaty { get; }
    }




}
