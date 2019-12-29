using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.Core;

[assembly:
    TypeMap("Soneta.Kasa.DokRozliczBase, Soneta.Kasa", typeof(Enova.API.Kasa.DokRozliczBase), typeof(Enova.API.Connector.Kasa.DokRozliczBase)),
    RowMap("DokRozliczeniowe", typeof(Enova.API.Kasa.DokRozliczBase), typeof(Enova.API.Kasa.KasaModule))]


namespace Enova.API.Kasa
{
    public interface DokRozliczBase : Business.GuidedRow, /*IVirtualComponent,*/ IDokumentPodmiotu, /*IApprovable, IDokumentObcy,*/ IDokument, /*IOddzialHostUI, IOddzialHost, IDokumentCRM,*/ IRow, ISessionable/*, INumerProceduryISO*/
    {

        #region Properties

        [Category("Dokument")]
        bool Bufor { get; set; }
        [Required, Category("Og\x00f3lne")]
        Date Data { get; set; }
        [Category("Wielowalutowość"), Description("Data do przeliczeń z tabeli kursowej")]
        Date DataKursu { get; set; }
        [Required, Category("Og\x00f3lne")]
        DefinicjaDokumentu Definicja { get; set; }
        //[ChildTable("DokEwidencji", "Soneta.Core.DokEwidencji", "Dokument")]
        SubTable DokumentyEwidencji { get; }
        [Category("Og\x00f3lne")]
        DokRozliczBase Nadrzedny { get; }
        [Category("Og\x00f3lne")]
        NumerDokumentu Numer { get; }
        [MaxLength(40), Description("Numer druku na kt\x00f3rym wystawiony został dokument."), Category("Og\x00f3lne")]
        string NumerDruku { get; set; }
        [MaxLength(20), Category("ISO"), Description("Numer procedury ISO")]
        string NumerProceduryISO { get; }
        [Category("Dokument"), MaxLength(80)]
        string Opis { get; set; }
        //[ChildTable("Platnosc", "Soneta.Kasa.Platnosc", "Dokument")]
        SubTable Platnosci { get; }
        [Category("Og\x00f3lne"), Required]
        IPodmiotKasowy Podmiot { get; set; }
        //[ChildTable("PozycjaDokRozlicz", "Soneta.Kasa.PozycjaDokRozlicz", "Dokument")]
        SubTable Pozycje { get; }
        //[ChildTable("PozycjaEwidencjiZbiorczej", "Soneta.Core.PozycjaEwidencjiZbiorczej", "Dokument")]
        SubTable PozycjeEwidencji { get; }
        [MaxLength(12), Category("Og\x00f3lne")]
        string Seria { get; set; }
        [Category("Og\x00f3lne"), Required]
        TypDokumentu Typ { get; }
        [Description("Czy dokument jest wielowalutowy"), Category("Wielowalutowość")]
        bool Wielowalutowy { get; set; }
        //[ChildTable("Windykacja", "Soneta.Windykacja.Windykacja", "Zrodlo")]
        SubTable Windykacje { get; }
        //[ChildTable("DokumentCRM", "Soneta.Zadania.DokumentCRM", "Dokument")]
        SubTable ZadaniaCRM { get; }
        [Browsable(false)]
        bool JestZrodlemStanowWindykacji { get; }
        EwidencjaSP RachunekBankowy { get; }
        bool Wielooddzialowosc { get; }
        bool Zatwierdzony { get; set; }

        #endregion

        #region Methods

        View GetListDefinicja();
        object GetListOddzial();
        View GetListRachunekFirmy();

        #endregion

        #region Do ewentualnego zaimplementowania

        //[Description("Oddział firmy"), Caption("Oddział firmy")]
        //OddzialFirmy Oddzial { get; set; }
        //[Caption("Rachunek firmowy"), Description("Numer rachunku firmowego skojarzony z dokumentem rozliczeniowym.")]
        //RachunekBankowyFirmy RachunekFirmy { get; set; }
        //StanWindykacji Stan { get; set; }
        //[Description("Tabela kursowa do przeliczeń walutowych"), Category("Wielowalutowość")]
        //TabelaKursowa TabelaKursowa { get; set; }
        //[Context(Required = false), AttributeInheritance, NumeratorRef]
        //public OddzialFirmy Oddzial { get; set; }
        //RachunekBankowyFirmy RachunekFirmy { get; set; }
        //RachunekWirtualny RachunekWirtualny { get; }
        //[AttributeInheritance]
        //StanWindykacji Stan { get; set; }
        //TabelaKursowa TabelaKursowa { get; set; }

        #endregion
    }
}
