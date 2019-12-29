using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Enova.Old.Types;
using Enova.Old.Core;
using Enova.Old.Handel;
using Enova.Old.Towary;

namespace Enova.Business.Old.DB
{
    [Obsolete("Do zrobienia DefinicjaEwidencjiFeatureDefinition")]
    public partial class DefDokHandlowego : IDbContext, IDefinicjaDokumentu, IGuidedRow, ISessionable , ISetSession
    {

        #region Fields

        private DokumentKoncowyInfo groupDokumentKoncowyInfo;
        private InicjalizatorWalutyInfo groupInicjalizatorWalutyInfo;
        private InwentaryzacjaInfo groupInwentaryzacjaInfo;
        private KontrolerRelacjiInfo groupKontrolerRelacjiInfo;
        private KreatorDokumentu groupKreatorDokumentu;
        private LimitWartosciInfo groupLimitWartosciInfo;
        private DefinicjaNumeracji groupNumeracja;
        private OstrzezenieDlaEdycji groupOstrzezenieDlaEdycji;
        private PrecyzjaCeny groupPrecyzjaCeny;
        private ProdukcjaShortInfo groupProdukcjaInfo;
        private UstawieniaWskazaniePartii groupUstawieniaWskazaniePartii;
        private ZmianaParametrowZasobuShortInfo groupZmianaParametrowZasobuInfo;
 
        private DefRelHandlowych podrzedne;
        private DefRelHandlowych nadrzedne;

        private Session session;

        #endregion

        #region Properties

        Session ISetSession.Session
        {
            set { this.session = value; }
        }

        public Session Session
        {
            get { return this.session; }
        }

        public DefRelHandlowych Podrzedne
        {
            get
            {
                if (this.podrzedne == null)
                    this.podrzedne = new DefRelHandlowych() { BaseQuery = this.DefRelHandlowych_DefinicjaNadrzednego.CreateSourceQuery() };
                return this.podrzedne;
            }
        }

        public DefRelHandlowych Nadrzedne
        {
            get
            {
                throw new Exception("Brak w nowej bazie DefrelHandlowej.definicjaPodrzednego");
                /*
                //return new SubTable(this.Table.Module.tableDefRelacjiHandlowej.WgDefinicjaPodrzednego, this);
                if (this.nadrzedne == null)
                    this.nadrzedne = new DefRelHandlowych() { BaseQuery = this.DefRelHandlowych_DefinicjaPodrzednego.CreateSourceQuery() };
                 
                return this.nadrzedne;
                 */
            }
        }

        public ZrodloEwidencji DefinicjaEwidencjiZrodlo
        {
            get { return (ZrodloEwidencji)this.DefinicjaEwidencjiZrodloInt; }
            set { this.DefinicjaEwidencjiZrodloInt = (int)value; }
        }
        public bool EwidencjaZKorekty
        {
            get
            {
                return (this.DefinicjaEwidencjiZrodlo == ZrodloEwidencji.Korekta);
            }
            set
            {
                this.DefinicjaEwidencjiZrodlo = value ? ZrodloEwidencji.Korekta : ZrodloEwidencji.Default;
            }
        }
        internal bool EwidencjaZDefinicji
        {
            get
            {
                if ((this.DefinicjaEwidencjiZrodlo != ZrodloEwidencji.Default) && (this.DefinicjaEwidencjiZrodlo != ZrodloEwidencji.Definicja))
                {
                    return (this.DefinicjaEwidencjiZrodlo == ZrodloEwidencji.CechaDokumentuDefinicja);
                }
                return true;
            }
        }
        public DefinicjaDokumentu DefinicjaEwidencji
        {
            get
            {
                switch (this.DefinicjaEwidencjiZrodlo)
                {
                    case ZrodloEwidencji.Korekta:
                    case ZrodloEwidencji.CechaDokumentu:
                        return null;
                }
                return this.RelationDefinicjaEwidencji;
            }
            set
            {
                this.RelationDefinicjaEwidencji = value;
            }
        }
        internal bool EwidencjaZCechy
        {
            get
            {
                if (this.DefinicjaEwidencjiZrodlo != ZrodloEwidencji.CechaDokumentu)
                {
                    return (this.DefinicjaEwidencjiZrodlo == ZrodloEwidencji.CechaDokumentuDefinicja);
                }
                return true;
            }
        }
        
        /*
        public FeatureDefinition DefinicjaEwidencjiFeatureDefinition
        {
            get
            {
                if (!string.IsNullOrEmpty(base.DefinicjaEwidencjiCecha) && this.EwidencjaZCechy)
                {
                    return base.Module.DokHandlowe.FeatureDefinitions[base.DefinicjaEwidencjiCecha];
                }
                return null;
            }
            set
            {
                base.DefinicjaEwidencjiCecha = (value == null) ? string.Empty : value.Name;
            }
        }
         */

        [Description("Informuje czy jest określona definicja ewidencji księgowej dla tej definicji dokumentu handlowego.")]
        public bool JestDefinicjaEwidencji
        {
            get
            {
                return ((this.EwidencjaZKorekty || (this.EwidencjaZDefinicji && (this.DefinicjaEwidencji != null))) 
                    /*|| (this.EwidencjaZCechy && (this.DefinicjaEwidencjiFeatureDefinition != null))*/);
            }
        }
        public KategoriaHandlowa Kategoria
        {
            get { return (KategoriaHandlowa)(this.KategoriaInt == null ? 0 : this.KategoriaInt.Value); }
            set { this.KategoriaInt = (int)value; }
        }
        public Enova.Old.Magazyny.TypPartii TypPartiiMagazynowej
        {
            get { return (Enova.Old.Magazyny.TypPartii)(this.TypPartiiMagazynowejInt == null ? 0 : this.TypPartiiMagazynowejInt.Value); }
            set { this.TypPartiiMagazynowejInt = (int)value; }
        }
        public Enova.Old.Magazyny.KierunekPartii KierunekMagazynu
        {
            get { return (Enova.Old.Magazyny.KierunekPartii)(this.KierunekMagazynuInt == null ? 0 : this.KierunekMagazynuInt.Value); }
            set { this.KierunekMagazynuInt = (int)value; }
        }
        public SposobPrzenoszeniaZaliczki SposobPrzenoszeniaZaliczki
        {
            get { return (SposobPrzenoszeniaZaliczki)(this.SposobPrzenoszeniaZaliczkiInt == null ? 0 : this.SposobPrzenoszeniaZaliczkiInt.Value); }
        }
        public SposobRozliczaniaNadrzednego SposobRozliczaniaNadrzednego
        {
            get { return (SposobRozliczaniaNadrzednego)(this.SposobRozliczaniaNadrzednegoInt == null ? 0 : this.SposobRozliczaniaNadrzednegoInt.Value); }
        }
        public RodzajIntrastat Intrastat
        {
            get { return (RodzajIntrastat)this.IntrastatInt; }
        }

        [Category("Relacje")]
        public DokumentKoncowyInfo DokumentKoncowyInfo
        {
            get
            {
                return this.groupDokumentKoncowyInfo;
            }
        }

        [Description("Ustawienia sposobu doboru walut dla: wartości pozycji, wartości dokumentu lub płatności."), Caption("Reguły doboru walut.")]
        public InicjalizatorWalutyInfo InicjalizatorWalutyInfo
        {
            get
            {
                return this.groupInicjalizatorWalutyInfo;
            }
        }

        [Caption("Reguły dotyczące inwentaryzacji."), Description("Ustawienie sposobu obsługi inwentaryzacji.")]
        public InwentaryzacjaInfo InwentaryzacjaInfo
        {
            get
            {
                return this.groupInwentaryzacjaInfo;
            }
        }

        [Description("Kontroler relacji.")]
        public KontrolerRelacjiInfo KontrolerRelacjiInfo
        {
            get
            {
                return this.groupKontrolerRelacjiInfo;
            }
        }

        [Browsable(false), Description("Ustawienia kreatora dokumentu handlowego."), Category("Wygląd formularza")]
        public KreatorDokumentu KreatorDokumentu
        {
            get
            {
                return this.groupKreatorDokumentu;
            }
        }

        public LimitWartosciInfo LimitWartosciInfo
        {
            get
            {
                return this.groupLimitWartosciInfo;
            }
        }

        [Description("Ustawienia określające spos\x00f3b numeracji dokument\x00f3w."), Category("Definicja")]
        public DefinicjaNumeracji Numeracja
        {
            get
            {
                return this.groupNumeracja;
            }
        }

        public OstrzezenieDlaEdycji OstrzezenieDlaEdycji
        {
            get
            {
                return this.groupOstrzezenieDlaEdycji;
            }
        }

        [Description("Konfiguracja określania precyzji ceny na pozycji dokumentu handlowego."), Category("Cennik")]
        public PrecyzjaCeny PrecyzjaCeny
        {
            get
            {
                return this.groupPrecyzjaCeny;
            }
        }

        public ProdukcjaShortInfo ProdukcjaInfo
        {
            get
            {
                return this.groupProdukcjaInfo;
            }
        }

        [Description("Grupa ustawień dotyczących wskazania partii."), Category("Magazyn")]
        public UstawieniaWskazaniePartii UstawieniaWskazaniePartii
        {
            get
            {
                return this.groupUstawieniaWskazaniePartii;
            }
        }

        public ZmianaParametrowZasobuShortInfo ZmianaParametrowZasobuInfo
        {
            get
            {
                return this.groupZmianaParametrowZasobuInfo;
            }
        }

        [Description("Określa spos\x00f3b liczenia wartości dokumentu (cenę): od brutto lub od netto."), Category("Cennik")]
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

        public ZmianaWartosciPozycji ZmianaWartosciPozycji
        {
            get { return (ZmianaWartosciPozycji)this.ZmianaWartosciPozycjiInt; }
            set { this.ZmianaWartosciPozycjiInt = (int)value; }
        }

        public EdycjaWartosciDokumentu EdycjaWartosci
        {
            get
            {
                return (EdycjaWartosciDokumentu)this.EdycjaWartosciInt;
            }
        }

        [Description("Definicja relacji, zawierająca szczeg\x00f3ły działania korekty.")]
        public DefRelacjiHandlowej RelacjaKorektyDefinicja
        {
            get
            {
                throw new NotImplementedException("DefDokHandlowego.RelacjaKorektyDefinicja");
                /*
                DefRelacjiHandlowej relacjaKorektyDefinicja = this.DefRel(TypRelacjiHandlowej.Korekta);
                if (relacjaKorektyDefinicja == null)
                {
                    relacjaKorektyDefinicja = this.DefRelNad(TypRelacjiHandlowej.Korekta);
                    if (relacjaKorektyDefinicja != null)
                    {
                        relacjaKorektyDefinicja = relacjaKorektyDefinicja.DefinicjaNadrzednego.RelacjaKorektyDefinicja;
                    }
                }
                return relacjaKorektyDefinicja;
                 */
            }
        }
 
        #endregion

        #region Methods

        public DefDokHandlowego()
        {
            this.groupNumeracja = new DefinicjaNumeracji();
            this.groupKreatorDokumentu = new KreatorDokumentu();
            this.groupUstawieniaWskazaniePartii = new UstawieniaWskazaniePartii();
            this.groupPrecyzjaCeny = new PrecyzjaCeny();
            this.groupKontrolerRelacjiInfo = new KontrolerRelacjiInfo();
            this.groupLimitWartosciInfo = new LimitWartosciInfo();
            this.groupOstrzezenieDlaEdycji = new OstrzezenieDlaEdycji();
            this.groupProdukcjaInfo = new ProdukcjaShortInfo();
            this.groupZmianaParametrowZasobuInfo = new ZmianaParametrowZasobuShortInfo();
            this.groupInicjalizatorWalutyInfo = new InicjalizatorWalutyInfo();
            this.groupInwentaryzacjaInfo = new InwentaryzacjaInfo();
            this.groupDokumentKoncowyInfo = new DokumentKoncowyInfo();
            this.initializer();

        }

        private void initializer()
        {
            this.groupNumeracja.AssignParent(this, "Numeracja");
            this.groupKreatorDokumentu.AssignParent(this, "KreatorDokumentu");
            this.groupUstawieniaWskazaniePartii.AssignParent(this, "UstawieniaWskazaniePartii");
            this.groupPrecyzjaCeny.AssignParent(this, "PrecyzjaCeny");
            this.groupKontrolerRelacjiInfo.AssignParent(this, "KontrolerRelacjiInfo");
            this.groupLimitWartosciInfo.AssignParent(this, "LimitWartosciInfo");
            this.groupOstrzezenieDlaEdycji.AssignParent(this, "OstrzezenieDlaEdycji");
            this.groupProdukcjaInfo.AssignParent(this, "ProdukcjaInfo");
            this.groupZmianaParametrowZasobuInfo.AssignParent(this, "ZmianaParametrowZasobuInfo");
            this.groupInicjalizatorWalutyInfo.AssignParent(this, "InicjalizatorWalutyInfo");
            this.groupInwentaryzacjaInfo.AssignParent(this, "InwentaryzacjaInfo");
            this.groupDokumentKoncowyInfo.AssignParent(this, "DokumentKoncowyInfo");
        }

        public void SprawdźRekurencje()
        {
            this.SprawdźRekurencje(new Stack());
        }

        private void SprawdźRekurencje(Stack definicje)
        {
            if (definicje.Contains(this))
            {
                //throw new RowException(this, "Rekurencyjna (zapętlona) definicja dokumentu '{0}'. Popraw relacje definicji dokumentu handlowego.", new object[] { this });
                throw new Exception(string.Format("Rekurencyjna (zapętlona) definicja dokumentu '{0}'. Popraw relacje definicji dokumentu handlowego.", this));
            }
            definicje.Push(this);
            foreach (DefRelacjiHandlowej handlowej in this.Podrzedne)
            {
                if (handlowej.ZNadrzednego.AutomatycznieNowy)
                {
                    throw new Exception("Brak w nowej bazie definizji pól DefrelacjiHandlowej.DefinicjaPodrzednego 1,2,3,4");

                    /*
                    if (handlowej.DefinicjaPodrzednego != null)
                    {
                        handlowej.DefinicjaPodrzednego.SprawdźRekurencje(definicje);
                    }
                    if (handlowej.DefinicjaPodrzednego2 != null)
                    {
                        handlowej.DefinicjaPodrzednego2.SprawdźRekurencje(definicje);
                    }
                    if (handlowej.DefinicjaPodrzednego3 != null)
                    {
                        handlowej.DefinicjaPodrzednego3.SprawdźRekurencje(definicje);
                    }
                    if (handlowej.DefinicjaPodrzednego4 != null)
                    {
                        handlowej.DefinicjaPodrzednego4.SprawdźRekurencje(definicje);
                    }
                     */
                }
            }
            definicje.Pop();
        }

        internal bool SprawdzMagazyn(Magazyn m)
        {
            return this.SprawdzMagazyn(m, true);
        }

        [Obsolete("Niepełny kod")]
        internal bool SprawdzMagazyn(Magazyn m, bool throwEx)
        {
            /*
            if (!base.Module.Config.Ogólne.PrzypisanieDefinicjiDoMagazynu || MagDefDokRightsWorker.SprawdźPrzypisanie(this, m))
            {
             */
                return true;
            /*
            }
            if (throwEx)
            {
                throw new RowException(this, "Definicja {0} nie jest przypisana do magazynu {1}.", new object[] { this, m });
            }
            return false;
             */
        }



        #endregion

        #region IDataContext Implementation

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
        RowState IRow.State
        {
            get { return this.GetRowState(); }
        }

        public DefDokHandlowych Table
        {
            get { return HandelModule.GetInstance(this.DataContext).DefDokHandlowych; }
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

        #region IDefinicjaDokumentu Implementation

        Type IDefinicjaDokumentu.TypDokumentu
        {
            get
            {
                return typeof(DokumentHandlowy);
            }
        }

        TypDokumentu IDefinicjaDokumentu.Typ
        {
            get
            {
                if (this.JestDefinicjaEwidencji && (this.DefinicjaEwidencji != null))
                {
                    return this.DefinicjaEwidencji.Typ;
                }
                return TypDokumentu.Niezdefiniowany;
            }
        }

        string IDefinicjaDokumentu.DomyślnaNumeracja
        {
            get
            {
                return "Definicja.Symbol/Data.Year:4/Data.Month:2/*:4";
            }
        }


        #endregion
    }
}
