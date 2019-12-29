using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.Core;
using Enova.API.Kasa;

[assembly: TypeMap("Soneta.Kadry.Pracownik, Soneta.KadryPlace", typeof(Enova.API.Kadry.IPracownik), typeof(Enova.API.Connector.Kadry.Pracownik)),
           RowMap("Pracownicy",typeof(Enova.API.Kadry.IPracownik), typeof(Enova.API.Kadry.KadryModule))]

namespace Enova.API.Kadry
{
    [TableName("Pracownicy")]
    public interface IPracownik : GuidedRow, Core.IPodmiot, Kasa.IPodmiotRozrachunki
    {
        //[Required, Category("Og\x00f3lne"), MaxLength(0x16)]
        string Imie { get; set; }

        [Category("Og\x00f3lne")/*, Required, MaxLength(0x1f)*/]
        string Nazwisko { get; set; }


        //[Caption("Imię i nazwisko")]
        string ImięNazwisko { get; }
        string NazwiskoImię { get; }

        [Category("Og\x00f3lne")/*, MaxLength(12), Required*/]
        string Kod { get; set; }

        string Nazwa { get; }
        string NazwaFormatowana { get; }
        string NazwaPierwszaLinia { get; }

        [Category("Dodatkowe")]
        Currency Kwota { get; set; }


        [Category("Og\x00f3lne")/*, MaxLength(11)*/]
        string PESEL { get; }

        //[ChildTable("Platnosc", "Soneta.Kasa.Platnosc", "Podmiot")]
        SubTable Platnosci { get; }

        //[ChildTable("RozrachunekIdx", "Soneta.Kasa.RozrachunekIdx", "Podmiot")]
        SubTable Rozrachunki { get; }

        [Category("Kontrahent")]
        FormaPlatnosci SposobZaplaty { get; set; }

        [Category("Og\x00f3lne")/*, Required*/]
        TypPracownika Typ { get; }

        //[Category("Dodatkowe")]
        //MemoText Uwagi { get; set; }

        //[ChildTable("Wyplata", "Soneta.Place.Wyplata", "Pracownik")]
        SubTable Wyplaty { get; }

        //[ChildTable("Zaliczka", "Soneta.Place.Zaliczka", "Pracownik")]
        SubTable Zaliczki { get; }

        //[ChildTable("Zaplata", "Soneta.Kasa.Zaplata", "Podmiot")]
        SubTable Zaplaty { get; }

        Adres Adres { get; }

        string EMAIL { get; set; }

        #region Do ewentualnego zaimplementowania

        /*
 * PracownikRow
 * 
        [ChildTable("Akord", "Soneta.Kadry.Akord", "Pracownik")]
        ISubTable Akordy { get; }
        [ChildTable("BadanieLekarskie", "Soneta.Kadry.BadanieLekarskie", "Pracownik")]
        ISubTable BadaniaLekarskie { get; }
        [ChildTable("Deklaracja", "Soneta.Deklaracje.Deklaracja", "Podmiot")]
        ISubTable DeklaracjePodmiotu { get; }
        [ChildTable("DzienPracy", "Soneta.Kalend.DzienPracy", "Pracownik")]
        DateSubTable DniPracy { get; }
        [ChildTable("Dodatek", "Soneta.Kadry.Dodatek", "Pracownik")]
        ISubTable Dodatki { get; }
        [ChildTable("DodatekAutomatyczny", "Soneta.Place.DodatekAutomatyczny", "Pracownik")]
        ISubTable DodatkiAutomatyczne { get; }
        [ChildTable("DokEwidencji", "Soneta.Core.DokEwidencji", "Podmiot")]
        ISubTable DokumentyEwidencji { get; }
        [ChildTable("PreliminarzDokument", "Soneta.Kasa.PreliminarzDokument", "Podmiot")]
        ISubTable DokumentyPreliminarza { get; }
        [ChildTable("DokRozliczBase", "Soneta.Kasa.DokRozliczBase", "Podmiot")]
        ISubTable DokumentyRozliczeniowe { get; }
        [ChildTable("WypElement", "Soneta.Place.WypElement", "Pracownik")]
        ISubTable Elementy { get; }
        [ChildTable("ElementOcenyPracownika", "Soneta.HR.ElementOcenyPracownika", "Pracownik")]
        ISubTable ElementyOceny { get; }
        [ChildTable("ElementPodzielnika", "Soneta.Core.ElementPodzielnika", "ElementPodzialowy")]
        ISubTable ElementyPodzielnika { get; }
        [ChildTable("ElementPrzeszeregowania", "Soneta.Przeszeregowania.ElementPrzeszeregowania", "Pracownik")]
        ISubTable ElementyPrzeszeregowania { get; }
        [ChildTable("WyposazenieHistoria", "Soneta.SrodkiTrwale.WyposazenieHistoria", "Odpowiedzialny")]
        ISubTable EwidencjaWyposazeniaHistoria { get; }
        [ChildTable("FundPozyczkowy", "Soneta.Kadry.FundPozyczkowy", "Pracownik")]
        SubTable FunduszePozyczkowe { get; }
        [ChildTable("GrafikPracownika", "Soneta.Kalend.GrafikPracownika", "Pracownik")]
        ISubTable Grafiki { get; }
        [ChildTable("PracHistoria", "Soneta.Kadry.PracHistoria", "Pracownik")]
        HistorySubTable Historia { get; }
        [ChildTable("HistoriaZatrudnieniaBase", "Soneta.Kadry.HistoriaZatrudnieniaBase", "Pracownik")]
        ISubTable HistoriaZatrudnienia { get; }
        [Category("Wieloetatowość")]
        bool HistZatrWgGlownego { get; set; }
        [ChildTable("IdentyfikacjaPlatnika", "Soneta.Kasa.IdentyfikacjaPlatnika", "Podmiot")]
        ISubTable Identyfikacje { get; }
        [Category("Og\x00f3lne")]
        public bool IndywidualnaDRA { get; set; }
        [ChildTable("InnyDochod", "Soneta.Kadry.InnyDochod", "Pracownik")]
        public SubTable InneDochody { get; }
        [Category("Wieloetatowość")]
        public bool InneDochodyWgGlownego { get; set; }
        [ChildTable("ZnajomośćJęzykaObcego", "Soneta.Kadry.ZnajomośćJęzykaObcego", "Pracownik")]
        public SubTable JęzykiObce { get; }
        [ChildTable("KalendarzBase", "Soneta.Kalend.KalendarzBase", "Pracownik")]
        public SubTable Kalendarze { get; }
        [ChildTable("Rekrutacja", "Soneta.HR.Rekrutacja", "Pracownik")]
        public SubTable Kandydatury { get; }
        [ChildTable("KartaRCP", "Soneta.Kadry.KartaRCP", "Pracownik")]
        public SubTable KartyRCP { get; }
        [ChildTable("KosztAutorski", "Soneta.Place.KosztAutorski", "Pracownik")]
        public SubTable KosztyAutorskie { get; }
        [ChildTable("LimitNieobecnosci", "Soneta.Kalend.LimitNieobecnosci", "Pracownik")]
        public SubTable Limity { get; }
        [ChildTable("Lokalizacja", "Soneta.CRM.Lokalizacja", "Kontrahent")]
        public SubTable Lokalizacje { get; }
        [Category("Og\x00f3lne")]
        public bool NaDRAUmieszczajREGON { get; set; }
        [ChildTable("NagrodaKara", "Soneta.Kadry.NagrodaKara", "Pracownik")]
        public SubTable NagrodyKary { get; }
        [Category("enovaNet")]
        public PracownikNet Net { get; }
        [ChildTable("Nieobecnosc", "Soneta.Kalend.Nieobecnosc", "Zrodlo")]
        public FromToSubTable Nieobecnosci { get; }
        [ChildTable("OcenaOceniający", "Soneta.Oceny.OcenaOceniający", "Oceniajacy")]
        public SubTable Oceniający { get; }
        [ChildTable("OcenaOceniany", "Soneta.Oceny.OcenaOceniany", "Oceniany")]
        public SubTable Oceniani { get; }
        [ChildTable("OcenaPracownika", "Soneta.HR.OcenaPracownika", "Pracownik")]
        public SubTable Oceny { get; }
        [ChildTable("OświadczenieZusOpieka", "Soneta.Place.OświadczenieZusOpieka", "Pracownik")]
        public DateSubTable OpiekaPracownika { get; }
        [ChildTable("KontaktOsoba", "Soneta.CRM.KontaktOsoba", "Kontrahent")]
        public SubTable Osoby { get; }
        [ChildTable("OsobaKontrahent", "Soneta.CRM.OsobaKontrahent", "Kontrahent")]
        public SubTable OsobyKontaktowe { get; }
        [ChildTable("OświadczeniePracownika", "Soneta.Kadry.OświadczeniePracownika", "Pracownik")]
        public SubTable Oświadczenia { get; }
        [ChildTable("PlanowanyElementWypłaty", "Soneta.Place.PlanowanyElementWypłaty", "Pracownik")]
        public SubTable PlanowaneElementy { get; }
        [ChildTable("PlanowanaNieobecność", "Soneta.Kalend.PlanowanaNieobecność", "Pracownik")]
        public FromToSubTable PlanowaneNieobecności { get; }
        [ChildTable("PlanowanaWypłata", "Soneta.Place.PlanowanaWypłata", "Pracownik")]
        public SubTable PlanowaneWypłaty { get; }
        [ChildTable("PodstawaNieobecnosci", "Soneta.Place.PodstawaNieobecnosci", "Pracownik")]
        public SubTable PodstawyNieobecności { get; }
        [ChildTable("PodzielnikKosztow", "Soneta.Core.PodzielnikKosztow", "Zrodlo")]
        public SubTable Podzielniki { get; }
        [ChildTable("Pojazd", "Soneta.Samochodowka.Pojazd", "Dysponent")]
        public SubTable Pojazdy { get; }
        [ChildTable("GIODOZgodny", "Soneta.Core.GIODOZgodny", "Host")]
        public SubTable PotwierdzeniaGIODO { get; }
        [ChildTable("PowiazanieKontaBase", "Soneta.Ksiega.PowiazanieKontaBase", "Element")]
        public SubTable PowiazaniaKontElementu { get; }
        [ChildTable("PowiązanieStrukturyOrganizacyjnej", "Soneta.Core.PowiązanieStrukturyOrganizacyjnej", "Zrodlo")]
        public SubTable PowiązaniaStrOrg { get; }
        [ChildTable("Pracownik", "Soneta.Kadry.Pracownik", "Wlasciciel")]
        public SubTable Pracownicy { get; }
        [Browsable(false), Obsolete]
        public Pracownik Pracownik { get; }
        [Category("Dodatkowe")]
        public Percent Procent { get; set; }
        [ChildTable("Projekt", "Soneta.Zadania.Projekt", "Kontrahent")]
        public SubTable Projekty { get; }
        [ChildTable("PrzelewBase", "Soneta.Kasa.PrzelewBase", "Podmiot")]
        public SubTable Przelewy { get; }
        [ChildTable("RachunekBankowyPodmiotu", "Soneta.Kasa.RachunekBankowyPodmiotu", "Podmiot")]
        public SubTable Rachunki { get; }
        [ChildTable("RachunekWirtualny", "Soneta.Kasa.RachunekWirtualny", "Podmiot")]
        public SubTable RachunkiWirtualne { get; }
        [ChildTable("CzlonekRodziny", "Soneta.Kadry.CzlonekRodziny", "Pracownik")]
        public SubTable Rodzina { get; }
        [ChildTable("Schorzenie", "Soneta.Kadry.Schorzenie", "Pracownik")]
        public SubTable Schorzenia { get; }
        [ChildTable("SrodekTrwalyBaseHistoria", "Soneta.SrodkiTrwale.SrodekTrwalyBaseHistoria", "Odpowiedzialny")]
        public SubTable SrodkiTrwaleHistoria { get; }
        [ChildTable("StornoElementu", "Soneta.Place.StornoElementu", "Pracownik")]
        public SubTable StornaElementow { get; }
        [ChildTable("SwiadczSocjalne", "Soneta.Kadry.SwiadczSocjalne", "Pracownik")]
        public SubTable Swiadczenia { get; }
        [ChildTable("SzkolenieBHP", "Soneta.Kadry.SzkolenieBHP", "Pracownik")]
        public SubTable SzkoleniaBHP { get; }
        [ChildTable("UkończoneSzkolenie", "Soneta.HR.UkończoneSzkolenie", "Pracownik")]
        public SubTable UkończoneSzkolenia { get; }
        [ChildTable("Umowa", "Soneta.Kadry.Umowa", "Pracownik")]
        public SubTable Umowy { get; }
        [ChildTable("UprawnieniePracownika", "Soneta.HR.UprawnieniePracownika", "Pracownik")]
        public SubTable Uprawnienia { get; }
        [ChildTable("Urzadzenie", "Soneta.Zadania.Urzadzenie", "Kontrahent")]
        public SubTable Urzadzenia { get; }
        [ChildTable("WejscieWyjscieI", "Soneta.Kalend.WejscieWyjscieI", "Pracownik")]
        public SubTable WeWyI { get; }
        [ChildTable("WejscieWyjscieO", "Soneta.Kalend.WejscieWyjscieO", "Pracownik")]
        public SubTable WeWyO { get; }
        [Category("Wieloetatowość")]
        public TypyWieloetatowosci Wieloetatowosc { get; set; }
        [Category("Og\x00f3lne")]
        public virtual Pracownik Wlasciciel { get; set; }
        [ChildTable("WniosekUrlopowy", "Soneta.Kadry.WniosekUrlopowy", "Kierownik")]
        public SubTable WnioskiKierownika { get; }
        [ChildTable("WniosekOSzkolenie", "Soneta.HR.WniosekOSzkolenie", "Pracownik")]
        public SubTable WnioskiOSzkolenia { get; }
        [ChildTable("WniosekUrlopowy", "Soneta.Kadry.WniosekUrlopowy", "Pracownik")]
        public SubTable WnioskiUrlopowe { get; }
        [ChildTable("WniosekUrlopowy", "Soneta.Kadry.WniosekUrlopowy", "Zastepca")]
        public SubTable WnioskiZastępcy { get; }
        [Category("System")]
        public Wydzial Wydzial { get; set; }
        [ChildTable("Wypadek", "Soneta.Kadry.Wypadek", "Pracownik")]
        public SubTable Wypadki { get; }
        [ChildTable("Zadanie", "Soneta.Zadania.Zadanie", "Kontrahent")]
        public SubTable Zadania { get; }
        [ChildTable("PodmiotZadanie", "Soneta.Zadania.PodmiotZadanie", "Kontrahent")]
        public SubTable ZadaniaKontrahenta { get; }
        [ChildTable("ZajęcieKomornicze", "Soneta.Kadry.ZajęcieKomornicze", "Pracownik")]
        public SubTable ZajęciaKomornicze { get; }
        [ChildTable("ZaniechaniePodatkowe", "Soneta.Place.ZaniechaniePodatkowe", "Pracownik")]
        public DateSubTable ZaniechaniaPodatkowe { get; }
        [ChildTable("ZasobCRM", "Soneta.Zadania.ZasobCRM", "Zasob")]
        public SubTable ZasobyCRM { get; }
        [ChildTable("ZbiegPracyIRodzicielstwa", "Soneta.Kalend.ZbiegPracyIRodzicielstwa", "Pracownik")]
        public FromToSubTable ZbiegiPracyIRodzicielstwa { get; }
        [ChildTable("ZbiegUbezpieczenia", "Soneta.Kadry.ZbiegUbezpieczenia", "Pracownik")]
        public FromToSubTable ZbiegiUbezpieczen { get; }
        [Category("Wieloetatowość")]
        public bool ZbiegUbespWgGlownego { get; set; }
        [ChildTable("DokumentZdarzenia", "Soneta.CRM.DokumentZdarzenia", "Kontrahent")]
        public SubTable Zdarzenia { get; }
        [ChildTable("ZestawieniePracy", "Soneta.Kalend.ZestawieniePracy", "Pracownik")]
        public FromToSubTable Zestawienia { get; }
        [ChildTable("RataPozyczki", "Soneta.Kadry.RataPozyczki", "Zyrant")]
        public SubTable ZyrowaneRaty { get; }
        [ChildTable("ŻyrantPożyczki", "Soneta.Kadry.ŻyrantPożyczki", "Pracownik")]
        public SubTable ŻyrowanePożyczki { get; }
*/

        /*  
         * Pracownik
         * 
                public bool AktualizacjaPrzyImporcie { get; }
                internal string BaseKod { get; }
                [Browsable(false)]
                public bool BuforujCzasy { get; set; }
                public KalkulatorPracownika Czasy { get; }
                public DateSubTable DniPlanu { get; }
                public RachunekBankowyPodmiotu DomyslnyRachunek { get; }
                protected internal abstract int DomyślnyKodUbezpieczenia { get; }
                public ItElementy Elementy { get; }
                [Caption("Elementy wynagrodzenia")]
                public SubTable ElementyEtatu { get; }
                public Pracownik EtatGłówny { get; }
                [AttributeInheritance]
                public SubTable HistoriaZatrudnienia { get; }
                [AttributeInheritance]
                public bool HistZatrWgGlownego { get; set; }
                [AttributeInheritance]
                public SubTable InneDochody { get; }
                [AttributeInheritance]
                public bool InneDochodyWgGlownego { get; set; }
                public PracHistoria this[Date data] { get; }
                internal KalendarzPracownika Kalendarz { get; }
                [NumeratorInfo(NumeratorInfoType.str, "J23"), AttributeInheritance, NumeratorItem]
                public string KodZasobu { get; }
                public Kontakt Kontakt { get; }
                public bool KontrolaAktywna { get; }
                [AttributeInheritance]
                public Currency Kwota { get; set; }
                public PracHistoria Last { get; }
                [Browsable(false)]
                public bool LimitNieograniczony { get; }
                public string NazwaZasobu { get; }
                [AttributeInheritance]
                public string Nazwisko { get; }
                [Caption("Nazwisko i imię")]
                public View OsobyZOsobyKontrahent { get; }
                 Pracownik[] PracownicyPowiązani { get; }
                [Browsable(false)]
                 bool PrzeterminowanieNieograniczone { get; }
                SubTable ITaskUser.TaskList { get; }
                string IWebOperator.EMAIL { get; }
                Type IWebOperator.HostType { get; }
                string IWebOperator.Name { get; }
                string IElementSlownika.Nazwa { get; }
                string IElementSlownika.Segment { get; }
                string IEuVatHost.EuVAT { get; }
                string IKontrahent.EMAIL { get; set; }
                string IKontrahent.EuVAT { get; }
                Kontakt IKontrahent.Kontakt { get; }
                string IKontrahent.NIP { get; }
                Osoba IKontrahent.Osoba { get; }
                string INipHost.NIP { get; }

                string IPoleAutonumerowane.Pole { get; set; }
                bool IZrodloPodzielnikaKosztow.Historyczny { get; }
                FromTo IZrodloNieobecnosci.Okres { get; }
                Pracownik IZrodloNieobecnosci.Pracownik { get; }
                Adres IPodmiotKasowy.Adres { get; }
                int IPodmiotKasowy.KontrolaDni { get; }
                Currency IPodmiotKasowy.LimitKredytu { get; }
                string IPodmiotKasowy.NIP { get; }
                IPodmiotKasowy IPodmiotKasowy.Platnik { get; }
                FormaPlatnosci IPodmiotKasowy.SposobZaplaty { get; }
                int IPodmiotKasowy.Termin { get; }
                int IPodmiotKasowy.TerminPlanowany { get; }
                IOdsetkiKarne IPodmiotOdsetkiKarne.OdsKarne { get; }
                 string TypZasobu { get; }
                 TypyWieloetatowosci Wieloetatowosc { get; }
                [AttributeInheritance]
                 Wydzial Wydzial { get; set; }
                [AttributeInheritance]
                 SubTable ZbiegiUbezpieczen { get; }
                [AttributeInheritance]
                 bool ZbiegUbespWgGlownego { get; set; }
                 bool ZgodnoscGIODOPotwierdzona { get; set; }
         */

        #endregion

    }
}
