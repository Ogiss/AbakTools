using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Enova.Old.Handel;

namespace Enova.Business.Old.DB
{
    public partial class DefRelacjiHandlowej : IGuidedRow, IDbContext, ISetSession , ISessionable
    {
        #region Fields

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

        public HandelModule Module
        {
            get
            {
                return HandelModule.GetInstance(this);
            }
        }

        public virtual TypRelacjiHandlowej Typ
        {
            get { throw new NotImplementedException(); }
        }

        [Description("Typ obiektu (klasa) relacji handlowej utworzonego z tej definicji.")]
        private Type TypRelacji
        {
            get
            {
                return this.Module.RelacjeHandlowe.GetRowType((int)this.Typ);
            }
        }

        #endregion

        #region Methods

        /*
        public DefRelacjiHandlowej()
        {
            this.groupZNadrzednego = new DefRelacjiZ(this, "ZNadrzednego");
            this.groupZPodrzednego = new DefRelacjiZ(this, "ZPodrzednego");
            this.groupZachowanie = new ZachowanieRelacji(this, "Zachowanie");
        }
         */

        /*
        protected DefRelacjiHandlowej()
        {
            this.groupZNadrzednego = new DefRelacjiZ();
            this.groupZPodrzednego = new DefRelacjiZ();
            this.groupZachowanie = new ZachowanieRelacji();
            if (typ == ((TypRelacjiHandlowej)0))
            {
                //throw new RequiredException(this, "Typ");
                throw new Exception("RequiredException");
            }
            this.initializer();
            //this.TypInt = (int)typ;
        }

        private void initializer()
        {
            this.groupZNadrzednego.AssignParent(this, "ZNadrzednego");
            this.groupZPodrzednego.AssignParent(this, "ZPodrzednego");
            this.groupZachowanie.AssignParent(this, "Zachowanie");
        }
         */

        public RelacjaHandlowa UtwórzPodrzędny(DokumentHandlowy nadrzędny)
        {
            return this.UtwórzPodrzędny(nadrzędny, 1);
        }

        public RelacjaHandlowa UtwórzPodrzędny(DokumentHandlowy nadrzędny, int nr)
        {
            return this.UtwórzPodrzędny(nadrzędny, nr, this.TypRelacji);
        }


        public RelacjaHandlowa UtwórzPodrzędny(DokumentHandlowy nadrzędny, int nr, Type typRelacji)
        {
            throw new NotImplementedException("DefRelacjiHandlowej.UtwórzPodrzędny(DokumentHandlowy nadrzędny, int nr, Type typRelacji)");
            /*
            DokumentHandlowy dokument = new DokumentHandlowy();
            switch (nr)
            {
                case 1:
                    dokument.Definicja = this.DefinicjaPodrzednego;
                    break;

                case 2:
                    dokument.Definicja = this.DefinicjaPodrzednego2;
                    break;

                case 3:
                    dokument.Definicja = this.DefinicjaPodrzednego3;
                    break;

                case 4:
                    dokument.Definicja = this.DefinicjaPodrzednego4;
                    break;

                default:
                    throw new ArgumentException();
            }
            Date date = this.PoliczDatę(nadrzędny, base.Zachowanie.DataKursu, nadrzędny.DataKursu, Date.Empty);
            if (date != Date.Empty)
            {
                dokument.DataKursu = date;
            }
            InicjalizatorWaluty.Instance.Add(dokument, nadrzędny, this);
            nadrzędny.Table.AddRow(dokument);
            if (!base.Zachowanie.DowolnaData && (dokument.Data < nadrzędny.Data))
            {
                dokument.Data = nadrzędny.Data;
            }
            this.KopiujDaty(nadrzędny, dokument);
            if (nadrzędny.Definicja.Intrastat == RodzajIntrastat.NieUwzględniaj)
            {
                dokument.WarunkiDostawy = dokument.Definicja.WarunkiDostawy;
                dokument.RodzajTransakcji = dokument.Definicja.RodzajTransakcji;
                dokument.RodzajTransportu = dokument.Definicja.RodzajTransportu;
            }
            return this.UtwórzRelacje(nadrzędny, dokument, typRelacji, true);
             */
        }

        public PozycjaRelacjiHandlowej UtwórzPodrzędną(RelacjaHandlowa relacja, PozycjaDokHandlowego nadrzędna)
        {
            throw new NotImplementedException("DefRelacjiHandlowej.UtwórzPodrzędną(RelacjaHandlowa relacja, PozycjaDokHandlowego nadrzędna)");
            /*
            PozycjaDokHandlowego row = new PozycjaDokHandlowego(relacja.Podrzedny);
            base.Module.PozycjeDokHan.AddRow(row);
            if (relacja.Typ == TypRelacjiHandlowej.Korekta)
            {
                row.Wspolczynnik = nadrzędna.Wspolczynnik;
            }
            PozycjaRelacjiHandlowej handlowej = new PozycjaRelacjiHandlowej(relacja, nadrzędna, row, false);
            base.Module.PozRelHandlowej.AddRow(handlowej);
            relacja.Konfiguruj(handlowej);
            return handlowej;
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

        public DefRelHandlowych Table
        {
            get { return HandelModule.GetInstance(this.DataContext).DefRelHandlowych; }
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
    }
}
