using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.ComponentModel;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;
using Enova.Old.Types;
using Enova.Old.Magazyny;
using Enova.Old.Handel;
using Enova.Old.Core;
using Enova.Old;

namespace Enova.Business.Old.DB
{
    [DataEditForm("Enova.Handel.Forms.DokHandlowyEditForm, Enova.Forms")]
    public partial class DokumentHandlowy : IGuidedRow, IEquatable<DokumentHandlowy>, IDbContext, IDokument, IDaneKontrahentaHost, IRowInvoker
    {

        #region Fields

        private RelacjeHandlowe nadrzedneRelacje = null;
        private RelacjeHandlowe podrzedneRelacje = null;
        private DokumentySubTable.Nadrzedne nadrzedne = null;
        private DokumentySubTable.Podrzedne podrzedne = null;
        private PozRelHandlowej nadrzednePozycje = null;
        private PozRelHandlowej podrzednePozycje = null;
        private PozycjeDokHan pozycje = null;
        private bool bezKopiowania;

        private NumerDokumentu groupNumer;
        private DokumentObcy groupObcy;

        #endregion

        #region Properties

        public HandelModule Module
        {
            get { return HandelModule.GetInstance(this.DataContext); }
        }

        public NumerDokumentu Numer
        {
            get { return this.groupNumer; }
        }
        public DefDokHandlowego Definicja
        {
            get { return this.RelationDefinicja; }
            set
            {
                if ((value != null) && (bool)value.Blokada)
                {
                    //throw new RowException(value, "Definicja {0} jest zablokowana.", new object[] { value });
                    throw new Exception("Definicja " + value.ToString() + " jet zablokowana");
                }
                if (value != null)
                {
                    value.SprawdźRekurencje();
                }
                /*
                if ((((value != null) && ((base.Status & RowStatus.Cloned) == RowStatus.None)) && ((this.Definicja != null) && (value != this.Definicja))) && (this.Definicja.PozwalajMagazynBezPraw != value.PozwalajMagazynBezPraw))
                {
                    throw new RowException(value, "Definicja {0} jest niedozwolona.", new object[] { value });
                }
                 */
                this.SprawdzDefinicjaMagazyn(value, this.Magazyn);
                DefDokHandlowego definicja = this.RelationDefinicja;
                this.RelationDefinicja = value;
                if ((this.RelationDefinicja != definicja) && (this.State != RowState.Detached))
                {
                    this.przeliczDefinicja(definicja);
                }
            }
        }

        IDefinicjaDokumentu IDokument.Definicja
        {
            get { return this.RelationDefinicja; }
        }

        public RelacjeHandlowe NadrzedneRelacje
        {
            get
            {
                if (nadrzedneRelacje == null)
                    nadrzedneRelacje = new RelacjeHandlowe() { BaseQuery = RelacjeHandlowePodrzedny.CreateSourceQuery() };
                return nadrzedneRelacje;
            }
        }
        public RelacjeHandlowe PodrzedneRelacje
        {
            get
            {
                if (podrzedneRelacje == null)
                    podrzedneRelacje = new RelacjeHandlowe() { BaseQuery = RelacjeHandloweNadrzedny.CreateSourceQuery() };
                return podrzedneRelacje;
            }
        }
        public DokumentySubTable.Nadrzedne Nadrzędne
        {
            get
            {
                if (nadrzedne == null)
                    nadrzedne = new DokumentySubTable.Nadrzedne(NadrzedneRelacje, this);
                return nadrzedne;
            }
        }
        public DokumentySubTable.Podrzedne Podrzędne
        {
            get
            {
                if (podrzedne == null)
                    podrzedne = new DokumentySubTable.Podrzedne(PodrzedneRelacje, this);
                return podrzedne;
            }
        }
        public PozRelHandlowej NadrzednePozycje
        {
            get
            {
                if (this.nadrzednePozycje == null)
                    this.nadrzednePozycje = new PozRelHandlowej() { BaseQuery = this.RelationPozRelHandlowejPodrzedny.CreateSourceQuery() };
                return this.nadrzednePozycje;
            }
        }
        public PozRelHandlowej PodrzednePozycje
        {
            get
            {
                if (this.podrzednePozycje == null)
                    this.podrzednePozycje = new PozRelHandlowej() { BaseQuery = this.RelationPozRelHandlowejNadrzedny.CreateSourceQuery() };
                return this.podrzednePozycje;
            }
        }
        public PozycjeDokHan Pozycje
        {
            get
            {
                if (this.pozycje == null)
                    this.pozycje = new PozycjeDokHan() { BaseQuery = RelationPozycjeDokHan.CreateSourceQuery() };
                return this.pozycje;
            }
        }

        public Enova.Old.Handel.Helpers.Flags Flags
        {
            get { return (Enova.Old.Handel.Helpers.Flags)this.FlagsInt; }
            set { this.FlagsInt = (int)value; }
        }
        public KategoriaHandlowa Kategoria
        {
            get { return (KategoriaHandlowa)this.KategoriaInt; }
            set { this.KategoriaInt = (int)value; }
        }
        public Enova.Old.Magazyny.KierunekPartii KierunekMagazynu
        {
            get { return (Enova.Old.Magazyny.KierunekPartii)this.KierunekMagazynuInt; }
            set { this.KierunekMagazynuInt = (int)value; }
        }
        public StanDokumentuHandlowego Stan
        {
            get { return (StanDokumentuHandlowego)this.StanInt; }
            set { this.StanInt = (int)value; }
        }
        [Description("Określa, czy dokument został zatwierdzony lub zaksięgowany.")]
        public bool Zatwierdzony
        {
            get
            {
                return (((this.Stan == StanDokumentuHandlowego.Zatwierdzony) /*&& !this._ignorowanieStanuZatwierdzonego*/) || (this.Stan == StanDokumentuHandlowego.Zablokowany));
            }
        }
        [Description("Określa, czy dokument został anulowany.")]
        public bool Anulowany
        {
            get
            {
                return (this.StanInt  == (int)StanDokumentuHandlowego.Anulowany);
            }
        }
        [Description("Określa, czy dokument jest jeszcze w buforze.")]
        public bool Bufor
        {
            get
            {
                return (this.StanInt == (int)StanDokumentuHandlowego.Bufor);
            }
        }
        [Description("Dokument korygowany przez aktualny dokument.")]
        public DokumentHandlowy DokumentKorygowany
        {
            get
            {
                if (!(bool)this.Korekta)
                {
                    return null;
                }
                return this.Nadrzędne[TypRelacjiHandlowej.Korekta];
            }
        }
        [Description("Pierwszy dokument korygowany przez aktualny dokument. Jeżeli jest wiele korekt, to jest to faktura.")]
        public DokumentHandlowy DokumentKorygowanyPierwszy
        {
            get
            {
                bool flag = false;
                DokumentHandlowy handlowy = this;
                while (true)
                {
                    DokumentHandlowy handlowy2 = handlowy.Nadrzędne[TypRelacjiHandlowej.Korekta];
                    if (handlowy2 == null)
                    {
                        if (!flag)
                        {
                            return null;
                        }
                        return handlowy;
                    }
                    handlowy = handlowy2;
                    flag = true;
                }
            }
        }
        [Description("Dokument korygujący aktualny dokument.")]
        public DokumentHandlowy DokumentKorygujący
        {
            get
            {
                DokumentHandlowy handlowy = this.Podrzędne[TypRelacjiHandlowej.Korekta];
                if (this == handlowy)
                {
                    return null;
                }
                return handlowy;
            }
        }
        [Description("Ostani z 'listy' dokument korygujący aktualny dokument.")]
        public DokumentHandlowy DokumentKorygującyOstatni
        {
            get
            {
                DokumentHandlowy handlowy = this;
                while (true)
                {
                    DokumentHandlowy handlowy2 = handlowy.DokumentKorygujący;
                    if (handlowy2 == null)
                    {
                        return handlowy;
                    }
                    handlowy = handlowy2;
                }
            }
        }
        [Description("Wszystkie dokumenty korygowane przez ten dokument korygujący.")]
        public IEnumerable<DokumentHandlowy> DokumentyKorygowane
        {
            get
            {
                if (!(bool)this.Korekta)
                {
                    goto Label_0073;
                }
                DokumentHandlowy dokumentKorygowany = this.DokumentKorygowany;
            Label_PostSwitchInIterator: ;
                if (dokumentKorygowany != null)
                {
                    yield return dokumentKorygowany;
                    dokumentKorygowany = dokumentKorygowany.DokumentKorygowany;
                    goto Label_PostSwitchInIterator;
                }
            Label_0073: ;
            }
        }
        [Description("Wszystkie dokumenty korygujące ten dokument.")]
        public IEnumerable<DokumentHandlowy> DokumentyKorygujące
        {
            get
            {
                DokumentHandlowy iteratorVariable0 = this.DokumentKorygujący;
                while (true)
                {
                    if (iteratorVariable0 == null)
                    {
                        yield break;
                    }
                    yield return iteratorVariable0;
                    iteratorVariable0 = iteratorVariable0.DokumentKorygujący;
                }
            }
        }
        public Enova.Old.Magazyny.TypPartii TypPartii
        {
            get { return (Enova.Old.Magazyny.TypPartii)this.TypPartiiInt; }
            set { this.TypPartiiInt = (int)value; }
        }
        internal bool jestKWPZ
        {
            get
            {
                return (bool)this.RelationDefinicja.JestKWPZ;
            }
        }
        public SposobPrzenoszeniaZaliczki SposobPrzenoszeniaZaliczki
        {
            get { return (SposobPrzenoszeniaZaliczki)(this.SposobPrzenoszeniaZaliczkiInt == null ? 0 : this.SposobPrzenoszeniaZaliczkiInt.Value); }
            set { this.SposobPrzenoszeniaZaliczkiInt = (int)value; }
        }

        public SposobRozliczaniaNadrzednego SposobRozliczaniaNadrzednego
        {
            get { return (SposobRozliczaniaNadrzednego)(this.SposobPrzenoszeniaZaliczkiInt == null ? 0 : this.SposobPrzenoszeniaZaliczkiInt.Value); }
            set { this.SposobPrzenoszeniaZaliczkiInt = (int)value; }
        }

        public DaneKontrahenta DaneKontrahenta
        {
            get
            {
                return this.Module.Core.DaneKontrahentow.WgHost[this, 0];
            }
        }

        public DaneKontrahenta DaneOdbiorcy
        {
            get
            {
                return this.Module.Core.DaneKontrahentow.WgHost[this, 2];
            }
        }

        [Description("Sposób liczenia wartości dokumentu: od ceny netto lub brutto."), Category("Cennik")]
        public SposobLiczeniaVAT LiczonaOd
        {
            get
            {
                return (SposobLiczeniaVAT)this.LiczonaOdInt;
            }
            set
            {
                this.LiczonaOdInt = (int)value;
            }
        }

        public double KursWaluty
        {
            get { return this.KursWalutyRecord == null || this.KursWalutyRecord.Value == 0 ? 1 : this.KursWalutyRecord.Value; }
        }

        [Obsolete("Brak obsługi Kontrahent.Platnik")]
        public Waluta WalutaKontrahenta
        {
            get
            {
                /*
                if (("Odbiorca Kontrahent".Equals(this.Definicja.Naglowek) && (this.Odbiorca != null)) && (this.Odbiorca.Platnik is Kontrahent))
                {
                    return (this.Odbiorca.Platnik as Kontrahent).Waluta;
                }
                 */
                if (this.Kontrahent == null)
                {
                    return null;
                }
                return this.Kontrahent.Waluta;
            }
        }

        public bool BezKopiowania
        {
            get
            {
                return this.bezKopiowania;
            }
            set
            {
                this.bezKopiowania = value;
            }
        }

        [Description("Inforamacja o dokumencie obcym, pochodzącym od kontrahenta.")]
        public DokumentObcy Obcy
        {
            get
            {
                return this.groupObcy;
            }
        }

        #endregion

        #region Methods

        public DokumentHandlowy()
        {
            this.groupNumer = new NumerDokumentu();
            this.groupObcy = new DokumentObcy();
        }

        private void initializer()
        {
            this.groupNumer.AssignParent(this, "Numer");
            //this.groupUmowaInfo.AssignParent(this, "UmowaInfo");
            this.groupObcy.AssignParent(this, "Obcy");
            //this.groupDostawa.AssignParent(this, "Dostawa");
            //this.groupPrecyzjaCeny.AssignParent(this, "PrecyzjaCeny");
            //this.groupSuma.AssignParent(this, "Suma");
            //this.groupInwentaryzacjaInfo.AssignParent(this, "InwentaryzacjaInfo");
            //this.groupProdukcjaInfo.AssignParent(this, "ProdukcjaInfo");
            //this.groupZmianaParametrowZasobuInfo.AssignParent(this, "ZmianaParametrowZasobuInfo");
        }

        public PozycjaDokHandlowego PozycjaWgIdent(int ident)
        {
            return this.Pozycje.WgIdent[this, ident];
        }

        internal void rejestrujKasujPusty()
        {
            throw new NotImplementedException("DokumentHandlowy.rejestrujKasujPusty()");
            //base.Session.Events.Add(new BusEventHandler(this.kasujPusty));
        }

        private void przeliczDefinicja(DefDokHandlowego save)
        {
            throw new NotImplementedException("DokumentHandlowy.przeliczDefinicja(DefDokHandlowego save)");
            /*
            if ((bool)this.Definicja.SeriaOperatora)
            {
                base.Seria = base.Session.Login.Operator.Name;
            }
            else if (!this.IsSeria)
            {
                base.Seria = "";
            }
             */
            /*
            this.UstawDefinicjaEwidencji();
            this.Kategoria = this.RelationDefinicja.Kategoria;
            this.TypPartii = this.RelationDefinicja.TypPartiiMagazynowej;
            this.KierunekMagazynu = this.RelationDefinicja.KierunekMagazynu;
            
            this.SposobPrzenoszeniaZaliczki = this.Definicja.SposobPrzenoszeniaZaliczki;
            this.SposobRozliczaniaNadrzednego = this.Definicja.SposobRozliczaniaNadrzednego;
            if (this.Definicja.Intrastat != RodzajIntrastat.NieUwzględniaj)
            {
                this.WarunkiDostawy = this.Definicja.WarunkiDostawy;
                this.RodzajTransakcji = this.Definicja.RodzajTransakcji;
                this.RodzajTransportu = this.Definicja.RodzajTransportu;
            }
            this.PrzeliczRelacje(save != null);
             */
            /*
            this.PrzeliczPłatności();
            foreach (RelacjaHandlowa handlowa in base.PodrzedneRelacje)
            {
                handlowa.Delete();
            }
            foreach (PozycjaDokHandlowego handlowego in this.Pozycje)
            {
                if (((save == null) || (save.Cena != this.Definicja.Cena)) || (save.LiczonaOd != this.Definicja.LiczonaOd))
                {
                    handlowego.PrzeliczCenę(WyliczenieCeny.ZmianaCenyPozycji, false);
                }
                handlowego.InicjalizujDefinicjaPowstaniaObowiazkuVAT();
            }
            foreach (DefRelacjiHandlowej handlowej in this.Definicja.Podrzedne)
            {
                if (!handlowej.Blokada)
                {
                    handlowej.PoZmianieDefinicji(this);
                }
            }
            base.Numer.PrzeliczSymbol();
            if (this.Definicja.WalutaPlatnosci == null)
            {
                throw new InvalidOperationException(string.Format("Na definicji {0} nie wskazano waluty płatności.", this.Definicja));
            }
            this.InicjalizujWalutaPlatnosci();
            this.PrzeliczKontrahent();
            if (this.Definicja.Buforowanie == SposobBuforowania.BuforNiedozwolony)
            {
                base.Session.ServerEvents.Add(new BusEventHandler(this.SprawdzDefinicjaBuforowanie));
            }
            base.UmowaInfo.Initialize(this.Definicja);
            base.InwentaryzacjaInfo.Initialize(this.Definicja);
            base.ZmianaParametrowZasobuInfo.Initialize(this.Definicja);
             */
        }

        internal void UstawDefinicjaEwidencji()
        {
            /*
            if (this.ReadOnly)
            {
                this.SprawdzDefinicja();
                try
                {
                    this.ReadOnly = false;
                    this.baseDefinicjaEwidencji = this.PoliczDefinicjaEwidencji();
                }
                finally
                {
                    this.ReadOnly = true;
                }
            }
            else
            {
             */
                //this.baseDefinicjaEwidencji = this.PoliczDefinicjaEwidencji();
            this.DefinicjaEwidencji = this.PoliczDefinicjaEwidencji();
            /*
            }
             */
        }

        private void SprawdzDefinicjaMagazyn(DefDokHandlowego definicja, Magazyn magazyn)
        {
            if (definicja != null)
            {
                definicja.SprawdzMagazyn(magazyn);
            }
        }

        [Obsolete("Dodać obsługę Ewidencji z cechy")]
        internal DefinicjaDokumentu PoliczDefinicjaEwidencji()
        {
            if (this.RelationDefinicja.EwidencjaZKorekty)
            {
                if (this.DokumentKorygowany != null)
                {
                    return this.DokumentKorygowany.DefinicjaEwidencji;
                }
                if (this.jestKWPZ)
                {
                    foreach (DokumentHandlowy handlowy in this.Nadrzędne)
                    {
                        if (handlowy.KierunekMagazynu == KierunekPartii.Rozchód)
                        {
                            return handlowy.DefinicjaEwidencji;
                        }
                    }
                }
                return null;
            }
            if (!this.RelationDefinicja.EwidencjaZCechy)
            {
                return this.RelationDefinicja.DefinicjaEwidencji;
            }
            
            DefinicjaDokumentu row = null;
            /*
            FeatureDefinition definicjaEwidencjiFeatureDefinition = this.Definicja.DefinicjaEwidencjiFeatureDefinition;
            if (definicjaEwidencjiFeatureDefinition != null)
            {
                row = base.Features[definicjaEwidencjiFeatureDefinition] as DefinicjaDokumentu;
            }
            if ((row != null) && !this.Definicja.GetListDefinicjaEwidencji().IsRowAcceptedByFilter(row))
            {
                throw new RowException(definicjaEwidencjiFeatureDefinition, "Ustalona przez cechę '{1}' definicja ewidencji '{0}' nie jest dozwolona do użycia w module enova Handel.", new object[] { row, definicjaEwidencjiFeatureDefinition });
            }
            if (((row != null) && this.Definicja.EwidencjaZDefinicji) && ((this.Definicja.DefinicjaEwidencji != null) && (row.Typ != this.Definicja.DefinicjaEwidencji.Typ)))
            {
                throw new RowException(definicjaEwidencjiFeatureDefinition, "Ustalona przez cechę '{0}' definicja ewidencji '{3}' powinna być tego samego typu, jak wybrana na definicji '{1}' definicja ewidencji '{2}'.", new object[] { definicjaEwidencjiFeatureDefinition, this.Definicja, this.Definicja.DefinicjaEwidencji, row });
            }
            if ((row == null) && this.Definicja.EwidencjaZDefinicji)
            {
                return this.Definicja.DefinicjaEwidencji;
            }
            if ((row == null) && (definicjaEwidencjiFeatureDefinition == null))
            {
                throw new RowException(this.Definicja, "Brak zdefiniowanej cechy wyliczającej definicję ewidencji dla definicji {0}.", new object[] { this.Definicja });
            }
             */
            return row;
        }

        private void PrzeliczRelacje(bool zmianaDefinicji)
        {
            foreach (RelacjaHandlowa handlowa in this.NadrzedneRelacje)
            {
                if ((handlowa.RelacjaKontrahenta != RelacjaKontrahenta.Brak) && (this.Kontrahent != handlowa.Nadrzedny.Kontrahent))
                {
                    this.Kontrahent = handlowa.Nadrzedny.Kontrahent;
                    this.KopiujDaneKontrahenta(this.DaneKontrahenta, handlowa.Nadrzedny.DaneKontrahenta);
                    this.KopiujDaneKontrahenta(this.DaneOdbiorcy, handlowa.Nadrzedny.DaneOdbiorcy);
                }
            }
            this.UstawLiczonaOd(zmianaDefinicji);
        }

        private void KopiujDaneKontrahenta(DaneKontrahenta target, DaneKontrahenta source)
        {
            throw new NotImplementedException("DokumentHandlowy.KopiujDaneKontrahenta(...)");
            /*
            target.Copy(source);
            if ((this.KierunekMagazynu == KierunekPartii.Rozchód) && (target.RodzajPodmiotu == RodzajPodmiotu.BezVAT))
            {
                target.RodzajPodmiotu = RodzajPodmiotu.Krajowy;
            }
             */
        }

        private void UstawLiczonaOd(bool zmianaDefinicji)
        {
            throw new NotImplementedException("DokumentHandlowy.UstawLiczonaOd(bool zmianaDefinicji)");
            /*
            SposobLiczeniaVAT? nullable = null;
            foreach (RelacjaHandlowa handlowa in this.NadrzedneRelacje)
            {
                if (handlowa.DziedziczyUstawienia && !handlowa.Definicja.Zachowanie.VatWgPodrzednego)
                {
                    nullable = new SposobLiczeniaVAT?(handlowa.Nadrzedny.LiczonaOd);
                }
                if (handlowa.Definicja.Zachowanie.InicjalizatorWalutyInfo.WalutaPlatnosci == ZrodloWaluty.ZNadrzednego)
                {
                    InicjalizatorWaluty.Instance.Add(this, handlowa.Nadrzedny, handlowa.Definicja);
                }
            }
            if (!nullable.HasValue && (this.Definicja != null))
            {
                if (this.Definicja.LiczonaOd != SposobLiczeniaVAT.ZależyOdKontrahenta)
                {
                    nullable = new SposobLiczeniaVAT?(this.Definicja.LiczonaOd);
                }
                else if (this.Kontrahent != null)
                {
                    nullable = new SposobLiczeniaVAT?((SposobLiczeniaVAT)this.Kontrahent.VATLiczonyOd);
                }
                else
                {
                    nullable = new SposobLiczeniaVAT?(SposobLiczeniaVAT.OdNetto);
                }
            }
            if (nullable.HasValue && (this.LiczonaOd != nullable.Value))
            {
                this.LiczonaOd = nullable.Value;
                bool bezKopiowania = this.BezKopiowania;
                try
                {
                    this.BezKopiowania = zmianaDefinicji || this.BezKopiowania;
                    foreach (PozycjaDokHandlowego handlowego in this.Pozycje)
                    {
                        IlośćWartość iw = handlowego.IlośćWartość;
                        DefRelacjiKopiowania.PrzeliczVat(nullable.Value, handlowego.Stawka, ref iw);
                        handlowego.UstawWartośćCy(iw.Wartość, true, false);
                    }
                }
                finally
                {
                    this.BezKopiowania = bezKopiowania;
                }
            }
            */
        }

        internal void SprawdzWalute(string symbol)
        {
            throw new NotImplementedException("DokumentHandlowy.SprawdzWalute()");
            /*
            bool flag;
            bool flag2;
            DokumentHandlowy handlowy;
            if (((this.IsLive && this.JestDokZaliczkowy(out flag2, out flag, out handlowy)) && (flag && (handlowy != null))) && 
                !Currency.EqualSymbols(handlowy.BruttoCy.Symbol, symbol))
            {
                throw new RowException(this, "Nie można zmienić waluty na dokumencie zaliczkowym podrzędnym '{0}' do innego dokumentu zaliczkowego '{1}'.", new object[] { this, handlowy });
            }
             */
        }

        internal bool JestDokZaliczkowy(out bool korekta, out bool nowyObieg, out DokumentHandlowy nadrzedny)
        {
            bool flag;
            nadrzedny = null;
            if (flag = !this.JestDokZaliczkowy(out korekta, out nowyObieg) ? false : nowyObieg)
            {
                foreach (RelacjaHandlowa handlowa in this.NadrzedneRelacje)
                {
                    bool flag2;
                    bool flag3;
                    if (((handlowa is RelacjaHandlowa.Kopiowania) && handlowa.Nadrzedny.Zatwierdzony) && (handlowa.Nadrzedny.JestDokZaliczkowy(out flag2, out flag3) && (nowyObieg == flag3)))
                    {
                        nadrzedny = handlowa.Nadrzedny;
                    }
                }
            }
            return flag;
        }

        internal bool JestDokZaliczkowy(out bool korekta, out bool nowyObieg)
        {
            nowyObieg = false;
            bool flag = this.JestDokZaliczkowy(out korekta);
            if (flag)
            {
                nowyObieg = this.SposobPrzenoszeniaZaliczki != SposobPrzenoszeniaZaliczki.NieDotyczy;
            }
            return flag;
        }

        private bool JestDokZaliczkowy(out bool korekta)
        {
//            bool flag;
//            if (this.AccessRight != AccessRights.Denied)
//            {
                korekta = (bool)this.Korekta;
                return (this.Definicja.EdycjaWartosci == EdycjaWartosciDokumentu.PozwalajNaMniejsząKwotę);
//            }
//            korekta = flag = false;
//            return flag;
        }

        public bool JestDokumentemOpakowań()
        {
            if (this.Kategoria != KategoriaHandlowa.WydanieOpakowań)
            {
                return (this.Kategoria == KategoriaHandlowa.PrzyjęcieOpakowań);
            }
            return true;
        }

        [Obsolete("Brak implementacji Session.Events")]
        internal void rejestrujPrzeliczRozliczenieRelacji()
        {
            //base.Session.Events.Add(new BusEventHandler(base.Table.przeliczRozliczenieRelacji), new RowEventArgs<DokumentHandlowy>(this));
        }

        internal void rejestrujPrzeliczRozliczenieRelacjiZPodrzędnego()
        {
            throw new NotImplementedException("DokumentHandlowy.rejestrujPrzeliczRozliczenieRelacjiZPodrzędnego()");
            /*
            if (this.State == RowState.Added)
            {
                this.Session.ServerEvents.Add(new BusEventHandler(base.Table.przeliczRozliczenieRelacjiZPodrzędnego), new RowEventArgs<DokumentHandlowy>(this));
            }
            else
            {
                DokumentHandlowy row = null;
                foreach (DokumentHandlowy handlowy2 in this.Podrzędne)
                {
                    if (handlowy2.JestDokumentemOpakowań())
                    {
                        if (RowHelper.CheckRowStatus(handlowy2, new RowStatus[] { RowStatus.Deleting }) && (row != null))
                        {
                            base.Session.ServerEvents.Add(new BusEventHandler(base.Table.przeliczRozliczenieRelacjiZPodrzędnego), new RowEventArgs<DokumentHandlowy>(row));
                            break;
                        }
                        if (RowHelper.CheckRowStatus(handlowy2, new RowStatus[] { RowStatus.Added }) || RowHelper.CheckRowStatus(handlowy2, new RowStatus[] { RowStatus.IsLive }))
                        {
                            base.Session.ServerEvents.Add(new BusEventHandler(base.Table.przeliczRozliczenieRelacjiZPodrzędnego), new RowEventArgs<DokumentHandlowy>(handlowy2));
                        }
                        row = handlowy2;
                    }
                }
            }
             */
        }

        public override string ToString()
        {
            return this.NumerPelny;
        }

        public override bool Equals(object obj)
        {
            if (typeof(DokumentHandlowy).IsAssignableFrom(obj.GetType()))
                return ((DokumentHandlowy)obj).Guid == this.Guid;
            return false;
        }

        public bool Equals(DokumentHandlowy dh)
        {
            return this.Guid == dh.Guid;
        }

        public override int GetHashCode()
        {
            return this.Guid.GetHashCode();
        }

        #endregion

        #region Events Handlers

        public void OnAdded()
        {
            throw new NotImplementedException();
        }

        public void OnDeleted()
        {
            throw new NotImplementedException();
        }

        public void OnLoaded()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataContex

        ObjectContext IDbContext.DbContext { get; set; }
        public EnovaContext DataContext
        {
            get { return (EnovaContext)((IDbContext)this).DbContext; }
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

        public DokHandlowe Table
        {
            get
            {
                return HandelModule.GetInstance(this.DataContext).DokHandlowe;
            }
        }

        ITable IRow.Table
        {
            get
            {
                return this.Table;
            }
        }
        public RowState State
        {
            get { return this.GetRowState(); }
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

        #region IDaneKontrahentaHost Implementation

        bool IDaneKontrahentaHost.IsReadOnlyDaneKontrahenta(int typ)
        {
            throw new NotImplementedException();
        }

        void IDaneKontrahentaHost.Update(int typ, DaneKontrahentaUpdate pola)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Nested Types

        public class EqualityComparer : IEqualityComparer<DokumentHandlowy>
        {
            public bool Equals(DokumentHandlowy dh1, DokumentHandlowy dh2)
            {
                return dh1.Guid == dh2.Guid;
            }

            public int GetHashCode(DokumentHandlowy dh)
            {
                return dh.Guid.GetHashCode();
            }
        }

        public class DataComparer : IComparer<DokumentHandlowy>
        {
            public int Compare(DokumentHandlowy dh1, DokumentHandlowy dh2)
            {
                if (dh1.Equals(dh2))
                    return 0;
                else if (dh1.Data <= dh2.Data)
                    return -1;
                else
                    return 1;
            }
        }

        public class DataComparerDescending : IComparer<DokumentHandlowy>
        {
            public int Compare(DokumentHandlowy dh1, DokumentHandlowy dh2)
            {
                if (dh1.Equals(dh2))
                    return 0;
                else if (dh1.Data < dh2.Data)
                    return 1;
                else
                    return -1;
            }
        }

        #endregion

        #region Properties do sprawdzenia

        public decimal? WartośćWCeniaZakupu
        {
            get
            {
                return this.PozycjeQuery.ToList().Sum(p => p.WartośćWCenieZakupu);
            }
        }

        public decimal? WartośćWCenieAbak
        {
            get
            {
                return this.PozycjeQuery.ToList().Sum(p => p.WartośćWCenieAbak);
            }
        }

        public decimal? DochódWGCenyAbak
        {
            get
            {
                return this.SumaNetto - this.WartośćWCenieAbak;
            }
        }

        public decimal? Prowizja
        {
            get
            {
                return decimal.Round((decimal)((this.SumaNetto - this.WartośćWCenieAbak) / 2M) * ((bool)this.Korekta ? 1.25M : 1), 2);
            }
        }

        public ObjectQuery<Feature> Features
        {
            get
            {
                Enova.Business.Old.DB.EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
                return (ObjectQuery<Feature>)dc.Features.Where(f => f.ParentType == "DokHandlowe" && f.Parent == this.ID);
            }
        }

        public string KontrahentKod
        {
            get
            {
                if (!KontrahentReference.IsLoaded)
                    KontrahentReference.Load();

                if (Kontrahent != null)
                    return Kontrahent.Kod;
                return null;
            }
        }

        public string KontrahentNazwa
        {
            get
            {
                if (!KontrahentReference.IsLoaded)
                    KontrahentReference.Load();

                if (Kontrahent != null)
                    return Kontrahent.Nazwa;
                return null;
            }
        }

        public string MagazynNazwa
        {
            get
            {
                if (!MagazynReference.IsLoaded)
                    MagazynReference.Load();

                if (Magazyn != null)
                    return Magazyn.Nazwa;

                return null;
            }
        }

        public string Przedstawiciel
        {
            get
            {
                var feature = Features.Where(f => f.Name == "PRZEDSTAWICIEL").FirstOrDefault();
                if (feature != null)
                {
                    return feature.Data;
                }
                return null;
            }
        }

        public string Przewoźnik
        {
            get
            {
                var feature = Features.Where(f => f.Name == "PRZEWOŻNIK").FirstOrDefault();
                if (feature != null)
                {
                    return feature.Data;
                }
                return null;
            }
        }

        public ObjectQuery<Platnosc> PłatnościQuery
        {
            get
            {
                if (EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                    return (ObjectQuery<Platnosc>)Enova.Business.Old.Core.ContextManager.DataContext.Platnosci
                        .Where(p => p.DokumentType == "DokHandlowe" && p.DokumentID == ID);
                return null;
            }
        }

        public decimal? DoRozliczenia
        {
            get
            {
                /*
                if (!this.DefinicjaReference.IsLoaded)
                    this.DefinicjaReference.Load();
                 */
                return PłatnościQuery.Sum(p => p.KwotaValue - p.KwotaRozliczonaValue) * (this.RelationDefinicja.Symbol == "FK" ? -1 : 1);
            }
        }

        public ObjectQuery<Platnosc> GetPlatnosci(EnovaContext ec)
        {
            return (ObjectQuery<Platnosc>)ec.Platnosci.Where(p => p.DokumentType == "DokHandlowe" && p.DokumentID == this.ID);
        }

        public DateTime? GetTermin(EnovaContext ec)
        {
            return this.GetPlatnosci(ec).Max(p => p.Termin);
        }


        public decimal? GetWartoscZakupuAbak(EnovaContext ec)
        {
            var podrzedny = this.RelacjeHandloweNadrzedny.Where(r => r.Definicja.ID == 3).Select(r => r.Podrzedny).FirstOrDefault();
            if (podrzedny != null)
            {
                decimal? zakup = 0;
                foreach (var pozycja in podrzedny.PozycjeDokHan.ToList())
                {
                    var tmp = ec.Features.Where(f => f.ParentType == "PozycjeDokHan" && f.Parent == pozycja.ID && f.Name == "Prowizja").Select(f => f.Data).FirstOrDefault();
                    if (tmp == null || string.IsNullOrEmpty(tmp.Trim()))
                    {
                        zakup += ec.Obroty.Where(o => o.RozchodDokument.ID == podrzedny.ID && o.RozchodPozycjaIdent == pozycja.Ident).Sum(o => o.PrzychodWartosc);
                    }
                    else
                    {
                        double? ilosc = ec.Obroty.Where(o => o.RozchodDokument.ID == podrzedny.ID && o.RozchodPozycjaIdent == pozycja.Ident).Sum(p => p.IloscValue);
                        if (ilosc != null)
                            zakup += decimal.Parse(tmp.Trim().Replace('.', ',')) * (decimal)ilosc;
                    }
                }
                return zakup;
            }
            return null;
        }

        public decimal? GetDochodAbak(EnovaContext ec)
        {
            decimal? zakup = GetWartoscZakupuAbak(ec);
            if (zakup != null)
            {
                return this.SumaNetto - zakup;
            }
            return null;
        }

        public decimal? GetProwizja(EnovaContext ec, decimal procent)
        {
            decimal? dochod = GetDochodAbak(ec);
            if (dochod != null)
                return decimal.Round(dochod.Value * procent, 2);
            return null;
        }

        public int MiesiacRozliczeniowy
        {
            get
            {
                if (this.Data.Month > 0 && this.Data.Month < 5)
                    return 4;
                else if (this.Data.Month > 4 && this.Data.Month < 9)
                    return 8;
                else
                    return 12;
            }
        }

        public DateTime TerminGraniczny
        {
            get
            {
                int miesiacRozlicz = MiesiacRozliczeniowy;
                return new DateTime(this.Data.Year, miesiacRozlicz, 1, 0, 0, 0).AddMonths(5).AddMilliseconds(-1);
            }
        }

        public decimal? GetPotracenia(EnovaContext ec, decimal procent)
        {
            DateTime termin = TerminGraniczny;
            if (DateTime.Now < termin)
                return null;
            var feature = GetFeatures(ec).Where(f => f.Name == "NIE LICZ POTRACEN").FirstOrDefault();
            if (feature == null || feature.Data.Trim() == "0")
            {
                decimal? prowizja = GetProwizja(ec, procent);
                if (prowizja != null)
                {
                    decimal rozliczono = 0;
                    foreach (var platnosc in GetPlatnosci(ec).ToList())
                    {
                        if (platnosc.Rozliczenia.Count() > 0)
                        {
                            decimal? zaplata = platnosc.Rozliczenia.Where(r => r.Data <= termin).Sum(r => r.KwotaZaplatyValue);
                            if (zaplata != null)
                                rozliczono += zaplata.Value;
                        }
                    }
                    if (rozliczono < SumaBrutto)
                    {
                        decimal pozostalo = SumaBrutto.Value - rozliczono;
                        decimal pr = pozostalo / SumaBrutto.Value;
                        decimal dopotracenia = decimal.Round(prowizja.Value * pr, 2);
                        int rm = DateTime.Now.Month - termin.Month;
                        if (rm > 3)
                            return dopotracenia;
                        return decimal.Round(dopotracenia * rm * 25 / 100, 2);
                    }
                }
            }
            return null;
        }

        public ObjectQuery<Feature> GetFeatures(EnovaContext ec)
        {
            return ec.Features.Where(f => f.ParentType == "DokHandlowe" && f.Parent == this.ID) as ObjectQuery<Feature>;
        }

        public DateTime? GetDataRozliczenia(EnovaContext ec)
        {
            decimal? rozliczono = this.GetPlatnosci(ec).Sum(p => p.KwotaRozliczonaValue);
            if (rozliczono == this.SumaBrutto)
            {
                return this.GetPlatnosci(ec).Max(p => p.DataRozliczenia);
            }
            return null;
        }

        #endregion

        #region Do usunięcia

        [Obsolete("Do usuniecia")]
        public EntityCollection<PozycjaRelacjiHandlowej> PozRelHandlowejNadrzedny
        {
            get
            {
                if (EntityState != System.Data.EntityState.Added && EntityState != EntityState.Detached && !RelationPozRelHandlowejNadrzedny.IsLoaded)
                    RelationPozRelHandlowejNadrzedny.Load();
                return RelationPozRelHandlowejNadrzedny;
            }
        }

        [Obsolete("Do usuniecia")]
        public EntityCollection<PozycjaRelacjiHandlowej> PozRelHandlowejPodrzedny
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !RelationPozRelHandlowejPodrzedny.IsLoaded)
                    RelationPozRelHandlowejPodrzedny.Load();
                return RelationPozRelHandlowejPodrzedny;
            }
        }

        [Obsolete("Do usunięcia")]
        public IQueryable<Platnosc> Platnosci
        {
            get
            {
                Enova.Business.Old.DB.EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
                if (this.EntityState == EntityState.Modified || this.EntityState == EntityState.Unchanged)
                {
                    return from pl in dc.Platnosci
                           where pl.Rozliczana == true && pl.DokumentType == "DokHandlowe" && pl.DokumentID == this.ID
                           select pl;
                }
                return null;
            }
        }

        [Obsolete("Do usuniecia")]
        public DokumentHandlowy Podrzedny
        {
            get
            {
                Enova.Business.Old.DB.EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
                if (this.EntityState == EntityState.Modified || this.EntityState == EntityState.Unchanged)
                {
                    EntityKeyMember key = this.RelationDefinicja.EntityKey.EntityKeyValues.Where(k => k.Key == "ID").FirstOrDefault();
                    if (key != null && ((int)(key.Value) == 1 || (int)(key.Value) == 2))
                    {
                        return (from rh in this.RelacjeHandloweNadrzedny.CreateSourceQuery()
                                where rh.Definicja.TypInt == (int)TypRelacjiHandlowej.HandlowoMagazynowa
                                select rh.Podrzedny).FirstOrDefault();
                    }
                }
                return null;
            }
        }

        [Obsolete("Do usunięcia")]
        public IQueryable<Obrot> Obroty
        {
            get
            {
                if (this.KierunekMagazynuInt == (int)KierunekPartii.Przychód)
                {
                    return this.ObrotyWgPrzychod.CreateSourceQuery();
                }
                return this.ObrotyWgRozchod.CreateSourceQuery();

            }
        }

        [Obsolete("Do usuniecia, używać DokumentHandlowy.Pozycje")]
        public IQueryable<PozycjaDokHandlowego> PozycjeQuery
        {
            get
            {
                return this.RelationPozycjeDokHan.CreateSourceQuery();
            }
        }

        [Obsolete("Do usuniecia, używać DokumentHandlowy.Pozycje")]
        public EntityCollection<PozycjaDokHandlowego> PozycjeDokHan
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !RelationPozycjeDokHan.IsLoaded)
                    RelationPozycjeDokHan.Load();
                return RelationPozycjeDokHan;
            }
        }

        #endregion


    }
}
