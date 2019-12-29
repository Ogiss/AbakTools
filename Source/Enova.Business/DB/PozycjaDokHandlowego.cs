using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Old.Types;
using Enova.Old.Core;
using Enova.Old.Magazyny;
using Enova.Old.Towary;
using Enova.Old.Handel;

namespace Enova.Business.Old.DB
{
    public partial class PozycjaDokHandlowego : IRow, IDbContext, ISessionable , ISetSession
    {
        #region Fields

        private StawkaVat groupStawka;
        private Session session;

        #endregion

        #region properties

        Session ISetSession.Session
        {
            set { this.session = value; }
        }

        public Session Session
        {
            get { return this.session; }
        }

        ObjectContext IDbContext.DbContext { get; set; }
        public EnovaContext DataContext
        {
            get
            {
                return (EnovaContext)((IDbContext)this).DbContext;
            }
        }
        public HandelModule Module
        {
            get
            {
                return HandelModule.GetInstance(this.DataContext);
            }
        }

        public PozRelHandlowej NadrzędneRelacje
        {
            get
            {
                return this.Module.PozRelHandlowej.WgPodrzednyDok[this.Dokument, this.Ident];
            }
        }
        public PozRelHandlowej PodrzędneRelacje
        {
            get
            {
                return this.Module.PozRelHandlowej.WgNadrzednyDok[this.Dokument, this.Ident];
            }
        }
        public PozycjeSubTable.Podrzędne Nadrzędne
        {
            get
            {
                return new PozycjeSubTable.Podrzędne(this, this.NadrzędneRelacje);
            }
        }
        public PozycjeSubTable.Nadrzędne Podrzędne
        {
            get
            {
                return new PozycjeSubTable.Nadrzędne(this, this.PodrzędneRelacje);
            }
        }

        [Description("Określa, czy dana pozycja koryguje inną pozycję.")]
        public bool Korekta
        {
            get
            {
                return (this.Nadrzędne[TypRelacjiHandlowej.Korekta] != null);
            }
        }

        [Description("Pozycja korygowanego dokumentu handlowego przez tę pozycję korekty.")]
        public PozycjaDokHandlowego PozycjaKorygowana
        {
            get
            {
                if (!this.Korekta)
                {
                    return null;
                }
                return this.Nadrzędne[TypRelacjiHandlowej.Korekta];
            }
        }
        [Description("Pozycja pierwszego korygowanego dokumentu handlowego przez tę pozycję korekty.")]
        public PozycjaDokHandlowego PozycjaKorygowanaPierwsza
        {
            get
            {
                PozycjaDokHandlowego pozycjaKorygowana = this;
                while (pozycjaKorygowana.Korekta)
                {
                    pozycjaKorygowana = pozycjaKorygowana.PozycjaKorygowana;
                }
                return pozycjaKorygowana;
            }
        }
        [Description("Pozycja korygująca tę pozycję dokumentu handlowego.")]
        public PozycjaDokHandlowego PozycjaKorygująca
        {
            get
            {
                return this.Podrzędne[TypRelacjiHandlowej.Korekta];
            }
        }
        [Description("Pozycja ostatniego korygującego tę pozycję dokumentu handlowego.")]
        public PozycjaDokHandlowego PozycjaKorygującaOstatnia
        {
            get
            {
                PozycjaDokHandlowego handlowego = this;
                while (true)
                {
                    PozycjaDokHandlowego handlowego2 = handlowego.PozycjaKorygująca;
                    if (handlowego2 == null)
                    {
                        return handlowego;
                    }
                    handlowego = handlowego2;
                }
            }
        }

        public double IloscPoKorektach
        {
            get
            {
                if (this.PozycjaKorygującaOstatnia != null)
                    return (double)PozycjaKorygującaOstatnia.IloscValue;
                return (double)this.IloscValue;
            }
        }

        public Quantity Ilosc
        {
            get
            {
                return new Quantity(this.IloscValue == null ? 0 : this.IloscValue.Value, this.IloscSymbol);
            }
        }

        public Quantity IloscMagazynu
        {
            get
            {
                return new Quantity(this.IloscMagazynuValue == null ? 0 : this.IloscMagazynuValue.Value, this.IloscMagazynuSymbol);
            }
            set
            {
                throw new NotImplementedException("PozycjaDokHandlowego.IloscMagazynu.set");
            }
        }

        public Currency WartoscCy
        {
            get
            {
                var symbol = string.IsNullOrEmpty(this.WartoscCySymbol) ? Currency.SystemSymbol : this.WartoscCySymbol;
                return new Currency(this.WartoscCyValue == null ? 0 : this.WartoscCyValue.Value, symbol);
            }
        }

        public IlośćWartość IlośćWartość
        {
            get
            {
                return new IlośćWartość(this.Ilosc, this.WartoscCy);
            }
            /*
            set
            {
                if (value.Ilość.Value < 0.0)
                {
                    value = IlośćWartość.SystemZero;
                }
                IlośćWartość wartość = this.IlośćWartość;
                if (!Currency.EqualSymbols(value.Wartość, wartość.Wartość))
                {
                    wartość = new IlośćWartość(wartość.Ilość, this.Dokument.PrzeliczWgKursu(wartość.Wartość, value.Wartość.Symbol));
                }
                if (value != wartość)
                {
                    this.zmianaIlości(value.Ilość, WyliczenieCeny.ZmianaIlościPozycji);
                    this.UstawWartośćCy(value.Wartość, true, false);
                }
            }
             */
        }

        [Description("Rodzaj stawki VAT, kt\x00f3ra została naliczona do tej pozycji dokumentu."), Category("Księgowe")]
        public StawkaVat Stawka
        {
            get
            {
                return this.groupStawka;
            }
        }
 
        #endregion

        public DoubleCy Cena
        {
            get
            {
                if (this.CenaValue == null)
                    return DoubleCy.Zero;
                var symbol = string.IsNullOrEmpty(this.CenaSymbol) ? Currency.SystemSymbol : this.CenaSymbol;
                return new DoubleCy(this.CenaValue.Value, symbol);
            }
        }

        #region Methods

        public PozycjaDokHandlowego()
        {
            this.groupStawka = new StawkaVat(this, "Stawka");
        }

        public void UstawWartośćCy(Currency value, bool dokładnie, bool ręcznie)
        {
            throw new NotImplementedException("PozycjaDokHandlowego.UstawWartośćCy(...)");
            /*
            if ((((value != Currency.Zero) && (this.Cena != DoubleCy.Zero)) && (!DoubleCy.EqualSymbols(this.Cena, (DoubleCy)value) && 
                (this.Dokument.Definicja.ZmianaWartosciPozycji != ZmianaWartosciPozycji.PrzeliczyćCenę))) && 
                (this.Dokument.Definicja.ZmianaWartosciPozycji != ZmianaWartosciPozycji.ZakazZmiany))
            {
                throw new RowException(this, "Waluta wartości '{0}' nie może być r\x00f3żna od waluty ceny '{1}'. Pozycja dokumentu handlowego: {2}.", new object[] { this.Cena.Symbol, value.Symbol, this });
            }
            if ((bool)this.Dokument.Definicja.CenaWartosc0 && (value != Currency.Zero))
            {
                throw new RowException(this, "Zgodnie z definicją dokumentu cena i wartość pozycji musi być r\x00f3wna 0.");
            }
            this.Dokument.SprawdzWalute(value.Symbol);
            decimal v = this.Dokument.PrzeliczWgKursu(value, Currency.SystemSymbol).Value;
            if (this.ustawSuma(value, v))
            {
                if (dokładnie)
                {
                    this.wyliczCenę(ręcznie);
                }
                else
                {
                    switch (this.Dokument.Definicja.ZmianaWartosciPozycji)
                    {
                        case ZmianaWartosciPozycji.ZakazZmiany:
                        case ZmianaWartosciPozycji.PrzeliczyćCenę:
                            this.wyliczCenę(ręcznie);
                            break;

                        case ZmianaWartosciPozycji.PrzeliczyćIlość:
                            this.wyliczIlość();
                            break;

                        case ZmianaWartosciPozycji.PrzeliczyćRabatCeny:
                            this.wyliczRabatCeny();
                            break;

                        default:
                            this.wyliczRabat();
                            break;
                    }
                    if (!base.Dokument.Definicja.NiezgodnoscWartosci)
                    {
                        this.WyliczWartosc();
                        return;
                    }
                }
                this.RejestrujPrzeliczWartosc();
                this.RejestrujMagazyn();
                this.RejestrujPrzeliczDoRelacji();
                this.RejestrujKopiowanie();
            }
             */
        }

        internal IEnumerable<PozycjaDokHandlowego> wyszukajMagazynowe()
        {
            throw new NotImplementedException("PozycjaDokHandlowego.wyszukajMagazynowe()");
            /*
            if (this.Dokument.TypPartii == TypPartii.Magazynowy)
            {
                PozycjaDokHandlowego iteratorVariable0 = this.wyszukajMagazynową();
                if (iteratorVariable0 != null)
                {
                    yield return iteratorVariable0;
                }
            }
            else
            {
                bool iteratorVariable1 = false;
                IEnumerator enumerator = this.PodrzędneRelacje.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    PozycjaRelacjiHandlowej current = (PozycjaRelacjiHandlowej)enumerator.Current;
                    if (current.Relacja is RelacjaHandlowa.HandlowoMagazynowa)
                    {
                        PozycjaDokHandlowego iteratorVariable3 = current.Podrzedna.wyszukajMagazynową();
                        if (iteratorVariable3 != null)
                        {
                            yield return iteratorVariable3;
                            iteratorVariable1 = true;
                            for (iteratorVariable3 = iteratorVariable3.PozycjaKorygowana; (iteratorVariable3 != null) && !iteratorVariable3.jestRelacjaHandlowoMagazynowa(); iteratorVariable3 = iteratorVariable3.PozycjaKorygowana)
                            {
                                yield return iteratorVariable3;
                            }
                        }
                    }
                }
                if (!iteratorVariable1)
                {
                    IEnumerator iteratorVariable8 = this.NadrzędneRelacje.GetEnumerator();
                    while (iteratorVariable8.MoveNext())
                    {
                        PozycjaRelacjiHandlowej iteratorVariable4 = (PozycjaRelacjiHandlowej)iteratorVariable8.Current;
                        if (iteratorVariable4.Relacja is RelacjaHandlowa.HandlowoMagazynowa)
                        {
                            PozycjaDokHandlowego iteratorVariable5 = iteratorVariable4.Nadrzedna.wyszukajMagazynową();
                            if (iteratorVariable5 != null)
                            {
                                yield return iteratorVariable5;
                                for (iteratorVariable5 = iteratorVariable5.PozycjaKorygowana; (iteratorVariable5 != null) && !iteratorVariable5.jestRelacjaHandlowoMagazynowa(); iteratorVariable5 = iteratorVariable5.PozycjaKorygowana)
                                {
                                    yield return iteratorVariable5;
                                }
                            }
                        }
                    }
                }
            }
             */
        }

        internal void PrzeliczZRelacji(RelacjaHandlowa relacja, bool innerDelete, bool KasujPusty)
        {
            throw new NotImplementedException("PozycjaDokHandlowego.PrzeliczZRelacji(..)");
            /*
            IlośćWartość zero = IlośćWartość.Zero;
            foreach (PozycjaRelacjiHandlowej handlowej in this.NadrzędneRelacje)
            {
                if (((handlowej.Relacja.Definicja == relacja.Definicja) && (handlowej.Podrzedna == this)) && !RowHelper.CheckRowStatus(handlowej, new RowStatus[] { RowStatus.Deleting }))
                {
                    zero += handlowej.IlośćWartość;
                }
            }
            if (zero.IsZero)
            {
                if (KasujPusty && (!jestKorekta(base.Dokument.Kategoria) || innerDelete))
                {
                    if (!RowHelper.CheckRowStatus(this, new RowStatus[] { RowStatus.Deleting }))
                    {
                        base.Delete();
                    }
                    base.Dokument.rejestrujKasujPusty();
                }
                else if (relacja.PrzenoszenieIlości == SposobPrzenoszeniaIlosci.IlośćIWartość)
                {
                    this.IlośćWartość -= this.IlośćWartość;
                }
                else
                {
                    this.zmianaIlości(Quantity.Zero, WyliczenieCeny.ZmianaIlościPozycji);
                    this.WyliczWartosc();
                }
            }
            else if (!RowHelper.CheckRowStatus(this, new RowStatus[] { RowStatus.Deleting }))
            {
                if (((relacja.PrzenoszenieIlości == SposobPrzenoszeniaIlosci.IlośćIWartość) || (relacja.PrzenoszenieIlości == SposobPrzenoszeniaIlosci.UslugiWgWartosci)) || (relacja.VatWgPodrzednego && (relacja.PrzenoszenieIlości == SposobPrzenoszeniaIlosci.IlośćICena)))
                {
                    this.IlośćWartość = new IlośćWartość(zero.Ilość, this.PrzeliczWalutaPozycja(zero.Wartość, relacja.Nadrzedny, null));
                    this.KorektaCeny = relacja.Definicja.Zachowanie.InicjalizatorWalutyInfo.WalutaPozycji != ZrodloWaluty.ZKartyKontrahenta;
                }
                else if (relacja.PrzenoszenieIlości == SposobPrzenoszeniaIlosci.IlośćICena)
                {
                    this.zmianaIlości(zero.Ilość, WyliczenieCeny.ZmianaIlościPozycji);
                    this.KorektaCeny = true;
                    this.WyliczWartosc();
                }
                else
                {
                    this.Ilosc = zero.Ilość;
                }
            }
            base.Session.Events.Remove(new BusEventHandler(this.PrzeliczDoRelacji));
             */
        }

        internal void przeliczIlośćZasobu()
        {
            throw new NotImplementedException("PozycjaDokHandlowego.przeliczIlośćZasobu()");
            /*
            this.wywołajKopiowanie();
            Quantity iloscMagazynu = this.IloscMagazynu;
            this.sumujIlośćZasobu(ref iloscMagazynu);
            if (base.IloscZasobuValue != iloscMagazynu.Value)
            {
                base.IloscZasobuValue = iloscMagazynu.Value;
                this.RejestrujMagazyn();
            }
             */
        }

        internal void KopiujZ(RelacjaHandlowa relacja, PozycjaDokHandlowego nadrzędna, bool zIlością)
        {
            this.KopiujZ(relacja, nadrzędna, zIlością, true, SposobKopiowaniaPozycji.Standardowo, false, false);
        }

        internal void KopiujZ(RelacjaHandlowa relacja, PozycjaDokHandlowego nadrzędna, bool zIlością, bool zCeną, SposobKopiowaniaPozycji skp, bool zCenąPoRabacie, bool wymusKopiowanieDostawy)
        {
            throw new NotImplementedException("PozycjaDokHandlowego.KopiujZ...");
            /*
            if (nadrzędna.Towar != null)
            {
                decimal netto = base.Suma.Netto;
                if (base.Towar != nadrzędna.Towar)
                {
                    SprawdzTowarMagazyn(this, nadrzędna.Towar);
                    InwentaryzacjaInfo.InwentaryzacjaVerifier.Verify(this, nadrzędna.Towar);
                    base.Towar = nadrzędna.Towar;
                    this.InicjalizujDefinicjaPowstaniaObowiazkuVAT(nadrzędna);
                    netto = 79228162514264337593543950335M;
                    base.ProdukcjaInfo.UstawTechnologia(nadrzędna.Towar);
                }
                base.KrajowaStawkaVAT = nadrzędna.KrajowaStawkaVAT;
                base.KosztDodatkowy = nadrzędna.KosztDodatkowy;
                this.InicjalizujDefinicjaStawki(new InicjalizatorStawkiVatParams(this, relacja, nadrzędna));
                if (nadrzędna.UmowaInfo.IsUmowa() && nadrzędna.Dokument.CenaNaPodrzedny)
                {
                    base.Rabat = nadrzędna.Rabat;
                    if (base.Dokument.Korekta)
                    {
                        base.KorektaCeny = true;
                    }
                }
                if (base.Dokument.Definicja.ZmianaMarzy != ZmianaMarzy.Zerowa)
                {
                    base.Rabat = nadrzędna.Rabat;
                }
                if (zCeną)
                {
                    if (base.Dokument.Definicja.ZmianaMarzy != ZmianaMarzy.Zerowa)
                    {
                        base.RabatCeny = this.PrzeliczWalutaPozycja(nadrzędna.RabatCeny, nadrzędna.Dokument, null).Round(this.PrecyzjaCeny);
                    }
                    base.Cena = this.PrzeliczWalutaPozycja(nadrzędna.Cena, nadrzędna.Dokument, null).Round(this.PrecyzjaCeny);
                    if (zCenąPoRabacie)
                    {
                        this.CenaPoRabacie = this.PrzeliczWalutaPozycja(nadrzędna.CenaPoRabacie, nadrzędna.Dokument, null).Round(this.PrecyzjaCeny);
                    }
                }
                if ((!wymusKopiowanieDostawy && (nadrzędna.KierunekMagazynu == KierunekPartii.Rozchód)) && ((nadrzędna.Dokument.TypPartii == TypPartii.ZamówionyZasób) && nadrzędna.Dokument.Definicja.UstawieniaWskazaniePartii.DoZamowien))
                {
                    base.Dostawa = null;
                    RelacjeGrupDostw.Delete(base.RelacjePartii);
                }
                else if ((relacja != null) && relacja.Definicja.Zachowanie.ZasobyZNadrzednego)
                {
                    if (base.Dostawa == null)
                    {
                        base.Dostawa = nadrzędna;
                    }
                }
                else
                {
                    if (base.Dostawa != nadrzędna.Dostawa)
                    {
                        netto = 79228162514264337593543950335M;
                    }
                    base.Dostawa = nadrzędna.Dostawa;
                    RelacjeGrupDostw.Copy(nadrzędna.RelacjePartii, this, true);
                }
                if (!string.IsNullOrEmpty(nadrzędna.KodCN))
                {
                    base.KodCN = nadrzędna.KodCN;
                }
                else
                {
                    base.KodCN = nadrzędna.Towar.KodCN;
                }
                if (!string.IsNullOrEmpty(nadrzędna.KrajPrzeznaczenia))
                {
                    base.KrajPrzeznaczenia = nadrzędna.KrajPrzeznaczenia;
                }
                if (!string.IsNullOrEmpty(nadrzędna.KrajPochodzenia))
                {
                    base.KrajPochodzenia = nadrzędna.KrajPochodzenia;
                }
                base.RodzajTransakcji = nadrzędna.RodzajTransakcji;
                base.NumerArkusza = nadrzędna.NumerArkusza;
                base.NumerWArkuszu = nadrzędna.NumerWArkuszu;
                if (zIlością)
                {
                    bool flag = false;
                    Jednostka jednostka = nadrzędna.standardowaJednostka();
                    Jednostka jednostka2 = this.standardowaJednostka();
                    if (jednostka == jednostka2)
                    {
                        base.Ilosc = nadrzędna.Ilosc;
                        if (base.Dokument.Definicja.RezerwowacTowar)
                        {
                            base.IloscRezerwowana = nadrzędna.Ilosc;
                        }
                    }
                    else
                    {
                        Quantity quantity = this.Towar.PrzeliczIlość(jednostka2, nadrzędna.Ilosc, this, true);
                        base.Ilosc = quantity;
                        if (base.Dokument.Definicja.RezerwowacTowar)
                        {
                            base.IloscRezerwowana = quantity;
                        }
                        flag = true;
                    }
                    if (base.Dokument.Definicja.ProdukcjaInfo.ObslugaTechnologii)
                    {
                        this.ustawIlośćMagazynu(nadrzędna.IloscMagazynu);
                    }
                    else
                    {
                        base.IloscMagazynu = nadrzędna.IloscMagazynu;
                    }
                    switch (skp)
                    {
                        case SposobKopiowaniaPozycji.WartościStatystycznej:
                            if (this.ustawSuma(nadrzędna.WartośćStatystyczna, nadrzędna.WartośćStatystyczna))
                            {
                                flag = true;
                            }
                            break;

                        case SposobKopiowaniaPozycji.WartościFakturowej:
                            if (this.ustawSuma(nadrzędna.WartośćFakturowa, nadrzędna.WartośćFakturowa))
                            {
                                flag = true;
                            }
                            break;

                        case SposobKopiowaniaPozycji.WartościMagazynowej:
                            if (this.ustawSuma(nadrzędna.WartośćMagazynowa, nadrzędna.WartośćMagazynowa))
                            {
                                flag = true;
                            }
                            break;

                        default:
                            base.WartoscCy = this.PrzeliczWalutaPozycja(nadrzędna.WartoscCy, nadrzędna.Dokument, null);
                            base.KosztFakturowy = nadrzędna.KosztFakturowy;
                            base.KosztStatystyczny = nadrzędna.KosztStatystyczny;
                            base.KosztMagazynowy = nadrzędna.KosztMagazynowy;
                            break;
                    }
                    if (flag)
                    {
                        this.wyliczCenę(true);
                    }
                    if (base.IloscZasobuValue != nadrzędna.IloscZasobuValue)
                    {
                        base.IloscZasobuValue = nadrzędna.IloscZasobuValue;
                        netto = 79228162514264337593543950335M;
                    }
                    base.IloscUzupelniajaca = nadrzędna.IloscUzupelniajaca;
                    base.Wspolczynnik = nadrzędna.Wspolczynnik;
                }
                if (nadrzędna.MasaNetto.IsZero)
                {
                    base.MasaNetto = (Quantity)(this.Towar.MasaNetto * nadrzędna.IloscMagazynu.Value);
                }
                else
                {
                    base.MasaNetto = nadrzędna.MasaNetto;
                }
                if (nadrzędna.MasaBrutto.IsZero)
                {
                    base.MasaBrutto = (Quantity)(this.Towar.MasaBrutto * nadrzędna.IloscMagazynu.Value);
                }
                else
                {
                    base.MasaBrutto = nadrzędna.MasaBrutto;
                }
                if ((relacja != null) && (relacja.Definicja.Zachowanie.ZrodloNazwyTowaru == ZrodloDanychPrzyPrzeksztalcaniu.ZDefinicjiPodrzednego))
                {
                    this.InicjalizujNazwa();
                }
                else
                {
                    this.ustawPełnąNazwę(nadrzędna.PełnaNazwa);
                }
                if (relacja != null)
                {
                    if (relacja.Definicja.Zachowanie.KopiujCechyPozycji)
                    {
                        nadrzędna.Features.CopyTo(base.Features, false);
                    }
                }
                else
                {
                    nadrzędna.Features.CopyTo(base.Features, false);
                }
                if (zIlością)
                {
                    if (skp == SposobKopiowaniaPozycji.Standardowo)
                    {
                        base.Suma.KopiujZ(nadrzędna.Suma);
                        if (base.Dokument.KursWaluty != nadrzędna.Dokument.KursWaluty)
                        {
                            this.WyliczWartosc(true);
                        }
                    }
                    this.RejestrujPrzeliczWartosc();
                }
                if (this.DefinicjaStawki != nadrzędna.DefinicjaStawki)
                {
                    this.UstawWartośćCy(base.WartoscCy, true, false);
                }
                base.Urzadzenie = nadrzędna.Urzadzenie;
                base.SchematOpakowan = nadrzędna.SchematOpakowan;
                if ((base.Dokument.Definicja.DefinicjaEwidencji != null) && (base.Dokument.Definicja.DefinicjaEwidencji.Typ == TypDokumentu.FWUENabyciaNaliczonyEwidencja))
                {
                    base.NabywcaPodatnik = false;
                }
                else
                {
                    base.NabywcaPodatnik = nadrzędna.NabywcaPodatnik;
                }
                if (netto != base.Suma.Netto)
                {
                    this.RejestrujMagazyn();
                }
                if (base.Dokument.JestUnijny)
                {
                    base.RodzajTransakcji = ((base.Dokument.RodzajTransakcji != KodRodzajuTransakcji.Brak) && (base.Dokument.RodzajTransakcji != KodRodzajuTransakcji.Różne)) ? base.Dokument.RodzajTransakcji : nadrzędna.RodzajTransakcji;
                }
                this.RejestrujKopiowanie();
            }
            */
        }

        #endregion

        #region IRow Implementation

        IRow IRow.Parent
        {
            get { return null; }
        }
        IRow IRow.Root
        {
            get { return this; }
        }
        string IRow.Prefix
        {
            get { return ""; }
        }
        RowState IRow.State
        {
            get { return this.GetRowState(); }
        }

        public PozycjeDokHan Table
        {
            get { return HandelModule.GetInstance(this.DataContext).PozycjeDokHan; }
        }

        ITable IRow.Table
        {
            get { return this.Table; }
        }

        public bool IsLive
        {
            get { return this.GetIsLive(); }
        }

        public bool IsReadOnly()
        {
            return false;
        }

        #endregion


        #region Do przeglądniecia i ewentualnie do przerobienia

        public IQueryable<Feature> Features
        {
            get
            {
                //Enova.Business.Old.DB.EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
                var dc = this.DataContext;
                if (this.EntityState == EntityState.Modified || this.EntityState == EntityState.Unchanged)
                {
                    return from f in dc.Features
                           where f.ParentType == "PozycjeDokHan" && f.Parent == this.ID
                           select f;

                }
                return null;
            }
        }

        public decimal? CenaAbak
        {
            get
            {
                var feature = Features.Where(f => f.Name == "Prowizja").FirstOrDefault();
                return feature == null ? null : (decimal?)decimal.Parse(feature.Data.Trim().Replace(".", ","));
            }
        }

        public IQueryable<Obrot> Obroty
        {
            get
            {
                if (!this.DokumentReference.IsLoaded)
                    this.DokumentReference.Load();

                return from o in this.Dokument.Obroty
                       where (this.Dokument.KierunekMagazynuInt == (int)KierunekPartii.Przychód || o.RozchodPozycjaIdent == this.Ident)
                       && (this.Dokument.KierunekMagazynuInt == (int)KierunekPartii.Rozchód || o.PrzychodPozycjaIdent == this.Ident)
                       select o;
            }
        }

        public decimal? WartośćWCenieZakupu
        {
            get
            {
                return this.Obroty.Where(o => o.Korekta != (int)KorektaObrotu.StornoZasobu).Sum(o => o.PrzychodWartosc);
            }
        }

        public decimal? WartośćWCenieAbak
        {
            get
            {
                decimal? cenaAbak = this.CenaAbak;

                if (cenaAbak != null)
                {
                    double? ilosc = (bool)this.Dokument.Korekta ? this.Obroty.Where(o => o.Korekta != (int)KorektaObrotu.StornoZasobu).Sum(o => o.IloscValue) : this.IloscValue;
                    if (ilosc == null)
                        return null;
                    return (decimal)ilosc * cenaAbak;
                }

                return this.WartośćWCenieZakupu;
            }
        }

        public string RabatProcent
        {
            get
            {
                return Decimal.Round(Rabat.Value * 100M, 2).ToString() + "%";
            }
        }

        public decimal? CenaPoRabacie
        {
            get
            {
                return decimal.Round((decimal)this.CenaValue.Value - ((decimal)this.CenaValue.Value * Rabat.Value), 2);
            }
        }

        public decimal? CenaBruttoPoRabacie
        {
            get
            {
                decimal netto = CenaPoRabacie.Value;
                return decimal.Round(netto + netto * this.StawkaProcent.Value, 2);
            }
        }

        
        public PozycjaRelacjiHandlowej PozycjaRelHandNadrzednyDok
        {
            get
            {
                return this.Dokument.PozRelHandlowejNadrzedny.Where(p => p.NadrzednaIdent == this.Ident).FirstOrDefault();
            }
        }

        public PozycjaRelacjiHandlowej PozycjaRelHandPodrzednyDok
        {
            get
            {
                return this.Dokument.PozRelHandlowejPodrzedny.Where(p => p.PodrzednaIdent == this.Ident).FirstOrDefault();
            }
        }

       
        public PozycjaDokHandlowego NadrzednaPozycja
        {
            get
            {
                PozycjaRelacjiHandlowej prhp = this.PozycjaRelHandPodrzednyDok;
                if (prhp != null)
                {
                    return prhp.NadrzednyDok.PozycjeDokHan.Where(p => p.Ident == prhp.NadrzednaIdent).FirstOrDefault();
                }
                return null;
            }
        }

        public double? NadrzednaPozycjaIlosc
        {
            get
            {
                PozycjaDokHandlowego p = NadrzednaPozycja;
                if (p != null)
                    return p.IloscValue;
                return null;
            }
        }

        public double? NadrzednaPozycjaCena
        {
            get
            {
                if (NadrzednaPozycja != null)
                    return NadrzednaPozycja.CenaValue;
                return null;  
            }
        }

        public decimal? NadrzednaPozycjaRabat
        {
            get
            {
                if (NadrzednaPozycja != null)
                    return NadrzednaPozycja.Rabat;
                return null;
            }
        }
        #endregion

    }
}
