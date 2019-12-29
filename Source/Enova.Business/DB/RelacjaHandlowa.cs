using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Enova.Old.Handel;
using Enova.Old.Magazyny;

namespace Enova.Business.Old.DB
{
    public partial class RelacjaHandlowa : IGuidedRow, IDbContext
    {
        #region Fields

        private BruttoNetto groupSuma;
        private PozRelHandlowej pozycje = null;

        #endregion

        #region Properties

        ObjectContext IDbContext.DbContext { get; set; }
        public EnovaContext DataContext
        {
            get { return (EnovaContext)((IDbContext)this).DbContext; }
        }
        public TypRelacjiHandlowej Typ
        {
            get { return (TypRelacjiHandlowej)this.TypInt; }
            set { this.TypInt = (int)value; }
        }
        public PozRelHandlowej Pozycje
        {
            get
            {
                if (this.pozycje == null)
                    this.pozycje = new PozRelHandlowej() { BaseQuery = this.PozRelHandlowej.CreateSourceQuery() };
                return this.pozycje;
            }
        }
        [Description("Określa spos\x00f3b przenoszenia podmiotu pomiędzy dokumentem nadrzędnym i podrzędnym.")]
        public virtual RelacjaKontrahenta RelacjaKontrahenta
        {
            get
            {
                return RelacjaKontrahenta.Kopiuj;
            }
        }
        [Description("Pewne ustawienia dokumentu nie zależą od definicji, tylko od dokumentu nadrzędnego.")]
        public virtual bool DziedziczyUstawienia
        {
            get
            {
                return false;
            }
        }

        [Description("Określa spos\x00f3b przenoszenia ilości i wartości do pozycji podrzednej.")]
        public virtual SposobPrzenoszeniaIlosci PrzenoszenieIlości
        {
            get
            {
                return SposobPrzenoszeniaIlosci.TylkoInicjuje;
            }
        }

        [Description("Wartość dokumentu nadrzędnego wpływa na wartość podrzędnego (dotyczy r\x00f3wnież sum VAT).")]
        public virtual bool KorektaWartości
        {
            get
            {
                return false;
            }
        }

        [Description("Określa, czy do jednej pozycji nadrzędnej może istnieć wiele pozycji podrzędnych.")]
        public virtual bool WielePozycji
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Methods

        public RelacjaHandlowa()
        {
            this.groupSuma = new BruttoNetto();
            this.initializer();
        }

        protected RelacjaHandlowa(DefRelacjiHandlowej definicja, TypRelacjiHandlowej typ, DokumentHandlowy podrzedny)
        {
            this.groupSuma = new BruttoNetto();
            if (definicja == null)
            {
                throw new RequiredException(this, "Definicja");
            }
            if (typ == ((TypRelacjiHandlowej)0))
            {
                throw new RequiredException(this, "Typ");
            }
            if (podrzedny == null)
            {
                throw new RequiredException(this, "Podrzedny");
            }
            //base.CheckAccessDenied(podrzedny);
            this.initializer();
            //base.GetRecord();
            //this.record.Definicja = definicja;
            //this.record.Typ = typ;
            //this.record.Podrzedny = podrzedny;

            this.Definicja = definicja;
            this.Typ = typ;
            this.Podrzedny = podrzedny;
        }

        protected RelacjaHandlowa(DefRelacjiHandlowej definicja, TypRelacjiHandlowej typ, DokumentHandlowy nadrzedny, DokumentHandlowy podrzedny)
            : this(calcDefinicja(definicja, typ, nadrzedny, podrzedny), typ, podrzedny)
        {
            this.Nadrzedny = nadrzedny;
        }

        private void initializer()
        {
            this.groupSuma.AssignParent(this, "Suma");
        }

        private static DefRelacjiHandlowej calcDefinicja(DefRelacjiHandlowej definicja, TypRelacjiHandlowej typ, DokumentHandlowy nadrzedny, DokumentHandlowy podrzedny)
        {
            if (definicja == null)
            {
                throw new NotSupportedException("BRAK IMPLEMENTACJI W NOWEJ BAZIE DefRelacjiHandlowej.DefinicjaPodrzednego 2 3 itd");
                /*
                DefDokHandlowego handlowego = podrzedny.Definicja;
                
                foreach (DefRelacjiHandlowej handlowej in nadrzedny.Definicja.Podrzedne)
                {
                    if (((handlowej.DefinicjaPodrzednego == handlowego) || (handlowej.DefinicjaPodrzednego2 == handlowego)) || ((handlowej.DefinicjaPodrzednego3 == handlowego) || (handlowej.DefinicjaPodrzednego4 == handlowego)))
                    {
                        definicja = handlowej;
                        break;
                    }
                }
                if (((definicja == null) && (podrzedny.DokumentKorygowanyPierwszy != null)) && (nadrzedny.DokumentKorygowanyPierwszy != null))
                {
                    handlowego = podrzedny.DokumentKorygowanyPierwszy.Definicja;
                    foreach (DefRelacjiHandlowej handlowej2 in nadrzedny.DokumentKorygowanyPierwszy.Definicja.Podrzedne)
                    {
                        if (((handlowej2.DefinicjaPodrzednego == handlowego) || (handlowej2.DefinicjaPodrzednego2 == handlowego)) || ((handlowej2.DefinicjaPodrzednego3 == handlowego) || (handlowej2.DefinicjaPodrzednego4 == handlowego)))
                        {
                            definicja = handlowej2;
                            break;
                        }
                    }
                }
                 */
            }
            if ((definicja == null) && podrzedny.jestKWPZ)
            {
                if (nadrzedny.KierunekMagazynu == KierunekPartii.Rozchód)
                {
                    foreach (DefRelacjiHandlowej handlowej3 in nadrzedny.Definicja.Podrzedne)
                    {
                        if (handlowej3.Typ == TypRelacjiHandlowej.Korekta)
                        {
                            definicja = handlowej3;
                            break;
                        }
                    }
                }
                else if (nadrzedny.KierunekMagazynu == KierunekPartii.Przychód)
                {
                    foreach (RelacjaHandlowa handlowa in podrzedny.NadrzedneRelacje)
                    {
                        if ((handlowa is Korekta) || (handlowa is KorektaPWZ))
                        {
                            definicja = handlowa.Definicja;
                            break;
                        }
                    }
                }
            }
            if (definicja == null)
            {
                foreach (DefRelacjiHandlowej handlowej4 in nadrzedny.Definicja.Podrzedne)
                {
                    if (handlowej4.Typ == typ)
                    {
                        definicja = handlowej4;
                        break;
                    }
                }
                if ((definicja == null) && (nadrzedny.DokumentKorygowanyPierwszy != null))
                {
                    foreach (DefRelacjiHandlowej handlowej5 in nadrzedny.DokumentKorygowanyPierwszy.Definicja.Podrzedne)
                    {
                        if (handlowej5.Typ == typ)
                        {
                            definicja = handlowej5;
                            break;
                        }
                    }
                }
            }
            if (definicja == null)
            {
                foreach (DefRelacjiHandlowej handlowej6 in podrzedny.Definicja.Nadrzedne)
                {
                    if (handlowej6.Typ == typ)
                    {
                        definicja = handlowej6;
                        break;
                    }
                }
                if ((definicja == null) && (podrzedny.DokumentKorygowanyPierwszy != null))
                {
                    foreach (DefRelacjiHandlowej handlowej7 in podrzedny.DokumentKorygowanyPierwszy.Definicja.Nadrzedne)
                    {
                        if (handlowej7.Typ == typ)
                        {
                            definicja = handlowej7;
                            break;
                        }
                    }
                }
            }
            if (definicja == null)
            {
                throw new RowException(nadrzedny, "Nieznaleziona definicja relacji z dokumentu nadrzędnego '{0}' do dokumentu podrzędnego '{1}'. Uzupełnij definicje relacji pomiędzy dokumentami.", new object[] { nadrzedny, podrzedny });
            }
            return definicja;
        }

        protected static bool Exists(TypRelacjiHandlowej typ, PozycjaDokHandlowego nadrzedna, PozycjaDokHandlowego podrzedna)
        {
            foreach (PozycjaRelacjiHandlowej handlowej in nadrzedna.PodrzędneRelacje)
            {
                if (handlowej.Relacja.Typ == typ)
                {
                    foreach (PozycjaDokHandlowego handlowego in handlowej.Podrzedna.wyszukajMagazynowe())
                    {
                        if (podrzedna == handlowego)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        internal virtual void Konfiguruj()
        {
        }

        internal virtual void Konfiguruj(PozycjaRelacjiHandlowej pozRel)
        {
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

        public RelacjeHandlowe Table
        {
            get
            {
                return HandelModule.GetInstance(this.DataContext).RelacjeHandlowe;
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


        #region Nested Types

        public class Kopiowania : RelacjaHandlowa
        {
            // Methods

            public Kopiowania(DefRelacjiHandlowej definicja, DokumentHandlowy nadrzedny, DokumentHandlowy podrzedny)
                : base(definicja, TypRelacjiHandlowej.Kopiowania, nadrzedny, podrzedny)
            {
            }

            internal static bool Exists(PozycjaDokHandlowego nadrzedna, PozycjaDokHandlowego podrzedna)
            {
                return RelacjaHandlowa.Exists(TypRelacjiHandlowej.Kopiowania, nadrzedna, podrzedna);
            }

            internal override void Konfiguruj()
            {
                //base.Nadrzedny.SetEdit();
                if (this.Definicja.Zachowanie.UsuwajAutomatycznie)
                {
                    KontrolerRelacji.KontrolujDynamicznie(base.Podrzedny);
                }
            }

            // Properties
            public override bool DziedziczyUstawienia
            {
                get
                {
                    return true;
                }
            }

            public override bool KorektaWartości
            {
                get
                {
                    throw new NotImplementedException("RelacjaHandlowa+Kopiowania.KorektaWartości");
                    /*
                    SposobLiczeniaSumVAT sumyVAT = base.Definicja.DefinicjaNadrzednego.SumyVAT;
                    if ((sumyVAT != SposobLiczeniaSumVAT.MożliwośćKorekty) && (sumyVAT != SposobLiczeniaSumVAT.LiczyćProporcjonalnie))
                    {
                        return (sumyVAT == SposobLiczeniaSumVAT.KorygowanyProporcjonalnie);
                    }
                    return true;
                     */
                }
            }

            public override SposobPrzenoszeniaIlosci PrzenoszenieIlości
            {
                get
                {
                    throw new NotImplementedException("RelacjaHandlowa+Kopiowania.PrzenoszenieIlości");
                    //return base.Definicja.Zachowanie.PrzenoszenieIlosci;
                }
            }

            public override RelacjaKontrahenta RelacjaKontrahenta
            {
                get
                {
                    return RelacjaKontrahenta.Kopiuj;
                }
            }

            public override bool WielePozycji
            {
                get
                {
                    return true;
                }
            }
        }

        public class Korekta : RelacjaHandlowa
        {
            // Methods

            public Korekta(DefRelacjiHandlowej definicja, DokumentHandlowy nadrzedny, DokumentHandlowy podrzedny)
                : base(definicja, TypRelacjiHandlowej.Korekta, nadrzedny, podrzedny)
            {
            }

            internal override void Konfiguruj()
            {
                throw new NotImplementedException("RelacjaHandlowa+Korekta.Konfiguruj()");
                /*
                base.Podrzedny.UmowaInfo.Initialize(base.Nadrzedny);
                base.Podrzedny.Korekta = true;
                base.Nadrzedny.PrecyzjaCeny.CopyTo(base.Podrzedny.PrecyzjaCeny);
                base.Nadrzedny.SetEdit();
                 */
            }

            internal override void Konfiguruj(PozycjaRelacjiHandlowej pozRel)
            {
                throw new NotImplementedException("RelacjaHandlowa+Korekta.Konfiguruj(...)");
                //pozRel.Podrzedna.UmowaInfo.Initialize(pozRel.Nadrzedna);
            }

            // Properties
            public override bool DziedziczyUstawienia
            {
                get
                {
                    return true;
                }
            }

            public override bool KorektaWartości
            {
                get
                {
                    return true;
                }
            }
        }

        public class KorektaPWZ : RelacjaHandlowa
        {
            // Methods

            public KorektaPWZ(DefRelacjiHandlowej definicja, DokumentHandlowy nadrzedny, DokumentHandlowy podrzedny)
                : base(definicja, TypRelacjiHandlowej.KorektaPWZ, nadrzedny, podrzedny)
            {
                this.Konfiguruj();
            }

            internal override void Konfiguruj()
            {
                throw new NotImplementedException("RelacjaHandlowa+KorektaPWZ.Konfiguruj()");
                /*
                base.Podrzedny.Korekta = true;
                base.Nadrzedny.PrecyzjaCeny.CopyTo(base.Podrzedny.PrecyzjaCeny);
                base.Nadrzedny.SetEdit();
                 */
            }

            // Properties
            public override bool DziedziczyUstawienia
            {
                get
                {
                    return false;
                }
            }

            public override bool KorektaWartości
            {
                get
                {
                    return false;
                }
            }
        }



        #endregion

    }
}
