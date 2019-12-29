using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Old.Types;
using Enova.Old.Core;
using Enova.Old.Handel;
using Enova.Old.Towary;

namespace Enova.Business.Old.DB
{
    public partial class PozycjaRelacjiHandlowej : IRow, IDbContext
    {
        #region Fields

        private StawkaVat groupStawka;
        private BruttoNetto groupSuma;
        internal bool KasujPustyPodrzędny;

        #endregion

        #region Properties

        ObjectContext IDbContext.DbContext { get; set; }
        public EnovaContext DataContext
        {
            get { return (EnovaContext)((IDbContext)this).DbContext; }
        }
        public HandelModule Module
        {
            get { return HandelModule.GetInstance(this.DataContext); }
        }

        public PozycjaDokHandlowego Nadrzedna
        {
            get { return this.NadrzednyDok.PozycjaWgIdent(this.NadrzednaIdent); }
        }
        public PozycjaDokHandlowego Podrzedna
        {
            get { return this.PodrzednyDok.PozycjaWgIdent(this.PodrzednaIdent); }
        }

        public Quantity Ilosc
        {
            get
            {
                return new Quantity(this.IloscValue == null ? 0 : this.IloscValue.Value, this.IloscSymbol);
            }
            set
            {
                this.IloscValue = value.Value;
                this.IloscSymbol = value.Symbol;
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
                this.IloscMagazynuValue = value.Value;
                this.IloscMagazynuSymbol = value.Symbol;
            }
        }

        public Currency Wartosc
        {
            get
            {
                if (this.WartoscValue == null)
                    return Currency.Empty;
                var symbol = string.IsNullOrEmpty(this.WartoscSymbol) ? Currency.SystemSymbol : this.WartoscSymbol;
                return new Currency(this.WartoscValue.Value, symbol);
            }
            set
            {
                this.WartoscValue = value.Value;
                this.WartoscSymbol = value.Symbol;
            }
        }

        public IlośćWartość IlośćWartość
        {
            get
            {
                return new IlośćWartość(this.Ilosc, this.Wartosc);
            }
            /*
            set
            {
                PozycjaDokHandlowego nadrzedna = this.Nadrzedna;
                this.Ustaw(value, nadrzedna.Towar.PrzeliczIlośćMagazynu(value.Ilość, nadrzedna, true, true));
                this.przelicz();
            }
             */
        }

        [Description("Rodzaj stawki VAT, kt\x00f3ra została naliczona do tej pozycji relacji."), Category("Zaliczka")]
        public StawkaVat Stawka
        {
            get
            {
                return this.groupStawka;
            }
        }

        [Category("Zaliczka"), Description("Przeliczone na PLN i odpowiednio policzone wartości Netto, Brutto i VAT dla tej pozycji relacji.")]
        public BruttoNetto Suma
        {
            get
            {
                return this.groupSuma;
            }
        }

        #endregion

        #region Methods

        public PozycjaRelacjiHandlowej()
        {
            this.groupStawka = new StawkaVat();
            this.groupSuma = new BruttoNetto();
            this.initializer();
        }

        protected PozycjaRelacjiHandlowej(RelacjaHandlowa relacja, int nadrzednaident, int podrzednaident, DokumentHandlowy podrzednydok, bool dodatkowa)
        {
            this.groupStawka = new StawkaVat();
            this.groupSuma = new BruttoNetto();
            if (relacja == null)
            {
                throw new RequiredException(this, "Relacja");
            }
            //base.CheckAccessDenied(relacja);
            if (nadrzednaident == 0)
            {
                throw new RequiredException(this, "NadrzednaIdent");
            }
            if (podrzednaident == 0)
            {
                throw new RequiredException(this, "PodrzednaIdent");
            }
            if (podrzednydok == null)
            {
                throw new RequiredException(this, "PodrzednyDok");
            }
            this.initializer();
            this.Relacja = relacja;
            this.NadrzednaIdent = nadrzednaident;
            this.PodrzednaIdent = podrzednaident;
            this.PodrzednyDok = podrzednydok;
            this.Dodatkowa = dodatkowa;
        }

        public PozycjaRelacjiHandlowej(RelacjaHandlowa relacja, PozycjaDokHandlowego nadrzędna, PozycjaDokHandlowego podrzędna, bool dodatkowa)
            : this(relacja, nadrzędna.Ident, podrzędna.Ident, relacja.Podrzedny, dodatkowa)
        {
            this.KasujPustyPodrzędny = true;
            //base.baseNadrzednyDok = nadrzędna.Dokument;
            this.NadrzednyDok = nadrzędna.Dokument;
        }

        private void initializer()
        {
            this.groupStawka.AssignParent(this, "Stawka");
            this.groupSuma.AssignParent(this, "Suma");
        }

        internal void Ustaw(IlośćWartość iw, Quantity im, bool zPrzeliczaniem, bool zPrzeliczaniemIlościZasobu)
        {
            this.Ilosc = iw.Ilość;
            this.Wartosc = iw.Wartość;
            this.IloscMagazynu = im;
            if (this.Relacja.PrzenoszenieIlości != SposobPrzenoszeniaIlosci.TylkoInicjuje)
            {
                if (zPrzeliczaniem)
                {
                    this.Podrzedna.PrzeliczZRelacji(this.Relacja, false, true);
                }
                if (zPrzeliczaniemIlościZasobu)
                {
                    this.przeliczajIlośćZasobu();
                }
            }
            if (!this.NadrzednyDok.JestDokumentemOpakowań())
            {
                this.NadrzednyDok.rejestrujPrzeliczRozliczenieRelacji();
            }
            else
            {
                this.NadrzednyDok.rejestrujPrzeliczRozliczenieRelacjiZPodrzędnego();
            }
        }

        internal void przeliczajIlośćZasobu()
        {
            if (/*(*/(this.Relacja.PrzenoszenieIlości != SposobPrzenoszeniaIlosci.TylkoInicjuje) && 
                //(this.Relacja.Nadrzedny.AccessRight != AccessRights.Denied)) && 
                (this.Relacja.Nadrzedny.KierunekMagazynu == this.Relacja.Nadrzedny.KierunekMagazynu))
            {
                PozycjaDokHandlowego nadrzedna = this.Nadrzedna;
                nadrzedna.przeliczIlośćZasobu();
                foreach (PozycjaRelacjiHandlowej handlowej in nadrzedna.NadrzędneRelacje)
                {
                    handlowej.przeliczajIlośćZasobu();
                }
            }
        }

        public void Ustaw(IlośćWartość iw, Quantity im, bool zPrzeliczaniem)
        {
            this.Ustaw(iw, im, zPrzeliczaniem, true);
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

        public PozRelHandlowej Table
        {
            get
            {
                return HandelModule.GetInstance(this.DataContext).PozRelHandlowej;
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


    }
}
