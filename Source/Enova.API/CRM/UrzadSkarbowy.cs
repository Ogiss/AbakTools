using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Business;
using Enova.API.Core;

[assembly: 
    TypeMap("Soneta.CRM.UrzadSkarbowy, Soneta.CRM", typeof(Enova.API.CRM.UrzadSkarbowy), typeof(Enova.API.Connector.CRM.UrzadSkarbowy)),
    RowMap("UrzedySkarbowe", typeof(Enova.API.CRM.UrzadSkarbowy), typeof(Enova.API.CRM.CRMModule))]        

namespace Enova.API.CRM
{
    public interface UrzadSkarbowy: GuidedRow, IPodmiot
    {
        #region Do ewentualnego zaimplementowania

        /*
         * UrzadSkarbowyRow
         * 

        [ChildTable("AdresWWW", "Soneta.Core.AdresWWW", "Zapis")]
        public SubTable AdresyWWW { get; }
        [Description("Określa, czy dany urząd ma być widoczny na listach."), Category("Kontrahent")]
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
        [MaxLength(0x10), Category("Og\x00f3lne")]
        public virtual string EuVAT { get; set; }
        [ChildTable("IdentyfikacjaPlatnika", "Soneta.Kasa.IdentyfikacjaPlatnika", "Podmiot")]
        public SubTable Identyfikacje { get; }
        [MaxLength(20), Required, Category("Og\x00f3lne")]
        public string Kod { get; set; }
        [MaxLength(4), Category("Og\x00f3lne")]
        public string KodUrzeduSkarbowego { get; set; }
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
        [MaxLength(240), Required, Category("Og\x00f3lne")]
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
        [Browsable(false)]
        public UrzedySkarbowe Table { get; }
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
         * 
         * 
        
        
        public NumerRachunku AKC { get; }
        public bool AktualizacjaPrzyImporcie { get; }
        [Browsable(false)]
        public bool BlokadaSprzedaży { get; }
        public NumerRachunku CIT { get; }
        public RachunekBankowyPodmiotu DomyslnyRachunek { get; }
        public string DomyślnyAdresWWW { get; }
        [Browsable(false)]
        public EFaktura EFaktura { get; }
        public string EMAIL { get; set; }
        [Caption("EU VAT"), AttributeInheritance]
        public override string EuVAT { get; set; }
        private NumerRachunku this[TypRachunkuBankowego typ, bool throwException] { get; }
        [Browsable(false)]
        public int KontrolaDni { get; }
        [Browsable(false)]
        public Currency KontrolaKwota { get; }
        [Browsable(false), AttributeInheritance]
        public Currency LimitKredytu { get; set; }
        public bool LimitNieograniczony { get; }
        [AttributeInheritance]
        public override string NIP { get; set; }
        public View OsobyZOsobyKontrahent { get; }
        public NumerRachunku PIT { get; }
        public IPodmiotKasowy Platnik { get; }
        public bool PrzeterminowanieNieograniczone { get; }
        [AttributeInheritance, Browsable(false)]
        public Percent Rabat { get; set; }
        public RodzajPodmiotu RodzajPodmiotu { get; }
        AdresExt IDaneKontaktoweHost.DomyslnyAdres { get; }
        string IElementSlownika.Nazwa { get; }
        string IElementSlownika.Segment { get; }
        bool IPodmiot.Blokada { get; }
        string IPoleAutonumerowane.Pole { get; set; }
        string IDbEuVat.DbEuVat { get; }
        IPodmiotKasowy IPodmiotKasowy.Platnik { get; }
        IOdsetkiKarne IPodmiotOdsetkiKarne.OdsKarne { get; }
        public StatusPodmiotu StatusPodmiotu { get; }
        public int TerminPlanowany { get; }
        public TypPodmiotu Typ { get; }
        [Browsable(false)]
        public TypLimituKredytowego TypLimituKredytowego { get; }
        [Browsable(false)]
        public TypLimituKredytowego TypPrzeterminowania { get; }
        public NumerRachunku VAT { get; }
        public Waluta Waluta { get; }
        public NumerRachunku ZalPIT { get; }
        */

        #endregion
    }
}
