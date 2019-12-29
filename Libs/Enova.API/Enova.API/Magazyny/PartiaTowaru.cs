using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;
using Enova.API.Handel;

[assembly: TypeMap("Soneta.Magazyny.PartiaTowaru, Soneta.Handel", typeof(Enova.API.Magazyny.PartiaTowaru), typeof(Enova.API.Connector.Magazyny.PartiaTowaru))]

namespace Enova.API.Magazyny
{
    public interface PartiaTowaru : Business.SubRow
    {
        [Description("Czas powstania partii towaru, pobrany z dokumentu.")]
        Time Czas { get; }

        [/*Required,*/ Description("Data powstania partii towaru, pobrana z dokumentu.")]
        Date Data { get; }

        [Description("Data wprowadzenia partii towaru do magazynu (FIFO, LIFO) nie korygowana przez korekty.")/*, Required*/]
        Date DataZasobu { get; }

        [Description("Dokument, na podstawie kt\x00f3rego powstała ta partia towaru.")/*, Required*/]
        DokumentHandlowy Dokument { get; set; }

        [Description("Partia towaru.")]
        Dostawy.IGrupaDostaw PartiaTowaru { get; }

        [Description("Identyfikuje pozycje dokumentu handlowego, na wskutek kt\x00f3rego powstała partia towaru.")/*, Required*/]
        int PozycjaIdent { get; set; }

        [Description("Typ partii towaru, określający czy jest to rzeczywista partia towaru, czy jedynie zam\x00f3wiona.")/*, Required*/]
        TypPartii Typ { get; set; }

        [/*TypeConverter(typeof(CurrencyConverter)), Caption("Wartość"),*/ Description("Wartość tej partii towaru.")]
        decimal Wartosc { get; set; }

        //public Relation WgDokument { get; }
        //public Relation WgPartiaTowaru { get; }

        double Cena { get; }

        IDanePartiiTowaru Dane { get; }

        IlośćWartość IlośćWartość { get; }

        PozycjaDokHandlowego Pozycja { get; }

    }
}
