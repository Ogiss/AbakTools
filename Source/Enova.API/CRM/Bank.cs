using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;
using Enova.API.Core;
using Enova.API.Kasa;
using Enova.API;

[assembly: TypeMap("Soneta.CRM.Bank, Soneta.CRM", typeof(Enova.API.CRM.Bank), typeof(Enova.API.Connector.CRM.Bank)),
            RowMap("Banki", typeof(Enova.API.CRM.Bank), typeof(Enova.API.CRM.CRMModule))]

namespace Enova.API.CRM
{
    public interface Bank : Business.GuidedRow, IPodmiotKasowy, /*IPodmiotOdsetkiKarne,*/ IBank, IKontrahent, IPodmiot, IPodmiotRozrachunki,
        /*INipHost, IEuVatHost, IAdresHost, IElementSlownika,*/ IComparable, /*IDbEuVat, IPoleAutonumerowane, IAdresyWWWHost, IDaneKontaktoweHost,*/ IRow, ISessionable
    {
        //[SqlResolving(RelationField = "Host", ParentTableSubRow = "Adresy", PrefixSubRow = "Adres", WhereEqual = new object[] { "HostType", "Banki", "Typ", 1 })]
        Adres Adres { get; }

        //[AttributeInheritance]
        string Kod { get; set; }

        //[AttributeInheritance]
        string Nazwa { get; set; }
        //[Required, MaxLength(0xff)]
        string NazwaFormatowana { get; set; }
        string NazwaPierwszaLinia { get; }

        #region Do ewentualnego zaimplementowania

        /*
         * BankRow
         * 
        [ChildTable("AdresWWW", "Soneta.Core.AdresWWW", "Zapis")]
        public SubTable AdresyWWW { get; }
        [Category("Kontrahent"), Description("Określa, czy dany bank ma być widoczny na listach.")]
        public bool Blokada { get; set; }
        [ChildTable("Deklaracja", "Soneta.Deklaracje.Deklaracja", "Podmiot")]
        public SubTable DeklaracjePodmiotu { get; }
        [ChildTable("DokEwidencji", "Soneta.Core.DokEwidencji", "Podmiot")]
        public SubTable DokumentyEwidencji { get; }
        [ChildTable("PreliminarzDokument", "Soneta.Kasa.PreliminarzDokument", "Podmiot")]
        public SubTable DokumentyPreliminarza { get; }
        [ChildTable("DokRozliczBase", "Soneta.Kasa.DokRozliczBase", "Podmiot")]
        public SubTable DokumentyRozliczeniowe { get; }
        [ChildTable("ElementPodzielnika", "Soneta.Core.ElementPodzielnika", "ElementPodzialowy")]
        public SubTable ElementyPodzielnika { get; }
        [Category("Og\x00f3lne"), MaxLength(0x10)]
        public virtual string EuVAT { get; set; }
        [ChildTable("IdentyfikacjaPlatnika", "Soneta.Kasa.IdentyfikacjaPlatnika", "Podmiot")]
        public SubTable Identyfikacje { get; }
        [MaxLength(8), Category("Bank")]
        public string Kierunek { get; set; }
        [Category("Og\x00f3lne"), Required, MaxLength(20)]
        public string Kod { get; set; }
        [Category("Kontrahent")]
        public Kontakt Kontakt { get; }
        [ChildTable("DaneKontaktowe", "Soneta.Core.DaneKontaktowe", "Host")]
        public SubTable Kontakty { get; }
        [Category("Kontrahent")]
        public Currency LimitKredytu { get; set; }
        [ChildTable("Lokalizacja", "Soneta.CRM.Lokalizacja", "Kontrahent")]
        public SubTable Lokalizacje { get; }
        [Browsable(false)]
        public CRMModule Module { get; }
        [Required, Category("Og\x00f3lne"), MaxLength(240)]
        public string Nazwa { get; set; }
        [MaxLength(13), Category("Og\x00f3lne")]
        public virtual string NIP { get; set; }
        [Category("Kontrahent")]
        public OdsetkiKarne OdsKarne { get; }
        [Category("Kontrahent")]
        public Osoba Osoba { get; }
        [ChildTable("KontaktOsoba", "Soneta.CRM.KontaktOsoba", "Kontrahent")]
        public SubTable Osoby { get; }
        [ChildTable("OsobaKontrahent", "Soneta.CRM.OsobaKontrahent", "Kontrahent")]
        public SubTable OsobyKontaktowe { get; }
        [ChildTable("Platnosc", "Soneta.Kasa.Platnosc", "Podmiot")]
        public SubTable Platnosci { get; }
        [ChildTable("Pojazd", "Soneta.Samochodowka.Pojazd", "Dysponent")]
        public SubTable Pojazdy { get; }
        [ChildTable("PowiazanieKontaBase", "Soneta.Ksiega.PowiazanieKontaBase", "Element")]
        public SubTable PowiazaniaKontElementu { get; }
        [ChildTable("Projekt", "Soneta.Zadania.Projekt", "Kontrahent")]
        public SubTable Projekty { get; }
        [ChildTable("PrzelewBase", "Soneta.Kasa.PrzelewBase", "Podmiot")]
        public SubTable Przelewy { get; }
        [Category("Kontrahent")]
        public Percent Rabat { get; set; }
        [ChildTable("RachunekBankowyPodmiotu", "Soneta.Kasa.RachunekBankowyPodmiotu", "Podmiot")]
        public SubTable Rachunki { get; }
        [ChildTable("RachunekWirtualny", "Soneta.Kasa.RachunekWirtualny", "Podmiot")]
        public SubTable RachunkiWirtualne { get; }
        [ChildTable("RozrachunekIdx", "Soneta.Kasa.RozrachunekIdx", "Podmiot")]
        public SubTable Rozrachunki { get; }
        [Category("Kontrahent"), Required]
        public FormaPlatnosci SposobZaplaty { get; set; }
        [MaxLength(0x10), Category("Bank")]
        public string SWIFT { get; set; }
        [Browsable(false)]
        public Banki Table { get; }
        [Category("Kontrahent")]
        public int Termin { get; set; }
        [ChildTable("Urzadzenie", "Soneta.Zadania.Urzadzenie", "Kontrahent")]
        public SubTable Urzadzenia { get; }
        [Category("Dodatkowe")]
        public MemoText Uwagi { get; set; }
        [ChildTable("Zadanie", "Soneta.Zadania.Zadanie", "Kontrahent")]
        public SubTable Zadania { get; }
        [ChildTable("PodmiotZadanie", "Soneta.Zadania.PodmiotZadanie", "Kontrahent")]
        public SubTable ZadaniaKontrahenta { get; }
        [ChildTable("Zaplata", "Soneta.Kasa.Zaplata", "Podmiot")]
        public SubTable Zaplaty { get; }
        [ChildTable("DokumentZdarzenia", "Soneta.CRM.DokumentZdarzenia", "Kontrahent")]
        public SubTable Zdarzenia { get; }
         */

        /*
         * Bank
         * 
        public bool AktualizacjaPrzyImporcie { get; }
        [Browsable(false)]
        public bool BlokadaSprzedaży { get; }
        public RachunekBankowyPodmiotu DomyslnyRachunek { get; }
        public string DomyślnyAdresWWW { get; }
        [Browsable(false)]
        public EFaktura EFaktura { get; }
        public string EMAIL { get; set; }
        [Caption("EU VAT"), AttributeInheritance]
        public override string EuVAT { get; set; }
        [AttributeInheritance]
        public string Kierunek { get; set; }
        [Browsable(false)]
        public int KontrolaDni { get; }
        [Browsable(false)]
        public Currency KontrolaKwota { get; }
        [AttributeInheritance, Browsable(false)]
        public Currency LimitKredytu { get; set; }
        public bool LimitNieograniczony { get; }
        [AttributeInheritance]
        public override string NIP { get; set; }
        public View OsobyZOsobyKontrahent { get; }
        public IPodmiotKasowy Platnik { get; }
        public bool PrzeterminowanieNieograniczone { get; }
        [Browsable(false)]
        public string Segment { get; }
        AdresExt IDaneKontaktoweHost.DomyslnyAdres { get; }
        string IPoleAutonumerowane.Pole { get; set; }
        string IDbEuVat.DbEuVat { get; }
        IPodmiotKasowy IPodmiotKasowy.Platnik { get; }
        IOdsetkiKarne IPodmiotOdsetkiKarne.OdsKarne { get; }
        public int TerminPlanowany { get; }
        [Browsable(false)]
        public TypLimituKredytowego TypLimituKredytowego { get; }
        [Browsable(false)]
        public TypLimituKredytowego TypPrzeterminowania { get; }
        public Waluta Waluta { get; }
        [AttributeInheritance, Browsable(false)]
        public Percent Rabat { get; set; }

         */

        #endregion
    }
}
