using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class Reklamacja : IDokumentZHistoria , Enova.Business.Old.IDbContext, Enova.Business.Old.IIsLive, 
        Core.IDeleteRecord, Core.ISaveChanges, Core.IUndoChanges, IDokument, INumerDokumentu
    {
        #region Fields

        private HistoriaDokumentuCollection historia;
        private static ReklamacjeTable table = new ReklamacjeTable();
        private System.Data.Objects.ObjectContext dbContext;

        #endregion

        #region Properties

        public System.Data.Objects.ObjectContext DbContext
        {
            get
            {
                return dbContext;
            }
            set
            {
                dbContext = value;
            }
        }
        public bool IsLive { get; set; }

        #endregion

        #region IDokumentZHistoria Implementation

        public ICollection<HistoriaDokumentu> Historia
        {
            get
            {
                if (historia == null)
                    historia = new HistoriaDokumentuCollection((WebContext)DbContext, this);
                return historia;
            }
        }


        public HistoriaDokumentu ZmienStatus(StatusDokumentu status, string opis = "")
        {
            var op = Operator.CurrentOperator.GetOperator((WebContext)this.DbContext);
            var h = new HistoriaDokumentu()
            {
                Data = DateTime.Now,
                Guid = Guid.NewGuid(),
                Host_ID = this.ID,
                Host_Type = "Reklamacje",
                Nazwa = status.Nazwa + "(" + Operator.CurrentOperator.Nazwa + ")",
                Operator = op,
                Status = status,
                Opis = opis
            };
            this.Historia.Add(h);
            this.OstatniaHistoriaDokumentu = h;
            this.OstatniaHistoriaOperator = op;
            this.OstatniStatusDokumentu = status;
            return h;
        }


        public IRow Parent
        {
            get { return null; }
        }

        public IRow Root
        {
            get { return null; }
        }

        public string Prefix
        {
            get { return null; }
        }

        public ITable Table
        {
            get { return table; }
        }

        public RowState State
        {
            get { return (RowState)(int)this.EntityState; }
        }

        public bool IsReadOnly()
        {
            return false;
        }

        public int Kolejnosc
        {
            get
            {
                if (OstatniStatusDokumentu != null)
                    return OstatniStatusDokumentu.Kolejnosc;
                return 0;
            }
        }

        public DateTime Data
        {
            get { return this.DataDodania; }
            set
            {
                this.DataDodania = value;
                this.DataModyfikacji = value;
            }
        }

        public DefinicjaDokumentu Definicja
        {
            get
            {
                Guid guid = DefinicjaDokumentu.ReklamacjaGuid;
                if (this.IsLive)
                    return ((WebContext)DbContext).DefinicjeDokumentow.Where(r => r.Guid == guid).FirstOrDefault();
                return null;
            }
        }


        public int GetMaxLp()
        {
            return this.Pozycje != null ? (this.Pozycje.Count() > 0 ? this.Pozycje.Max(r => r.Lp) : 0) : 0;
        }

        public bool SaveChanges()
        {
            var dc = this.DbContext as WebContext;
            if (dc != null)
            {
                if (this.EntityState == System.Data.EntityState.Detached)
                    dc.Reklamacje.AddObject(this);
                if (this.EntityState == System.Data.EntityState.Added)
                {
                }
                if (this.Numer == null)
                    this.Numer = new NumerDokumentu();
                if (string.IsNullOrEmpty(this.Numer.NumerPelny))
                {
                    this.Numer.NumerPelny = NumerDokumentu.LiczSymbol(this.Definicja, this);
                    this.Numer.Symbol = this.Numer.NumerPelny;
                }

                this.OnSaved((WebContext)dc);

                dc.OptimisticSaveChanges();
                dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);

                if (this.historia != null)
                {
                    foreach (var i in this.historia.added)
                        i.Host_ID = this.ID;
                }
                dc.OptimisticSaveChanges();
                dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
                if (this.historia != null)
                    this.historia.Refresh();
                return true;
            }
            return false;
        }

        protected void OnSaved(WebContext dc)
        {
            if (!this.Deleted)
            {
                var h = OstatniaHistoriaDokumentu;

                if (h != null)
                {
                    if (this.OstatniaHistoriaID == null || this.OstatniaHistoriaID != h.ID)
                    {
                        this.OstatniaHistoriaID = h.ID;
                        this.OstatniaHistoriaOperatorID = h.Operator != null ? h.Operator.ID : 0;
                        this.OstatniStatusID = h.Status != null ? h.Status.ID : 0;
                    }
                }

                if (this is INumerDokumentu)
                {
                    var n = (INumerDokumentu)this;
                    if (n.Numer.Numer == 0)
                    {
                        var symbol = this.Numer.Symbol;
                        if (!dc.Reklamacje.Any(r=>r.Numer.Symbol == symbol))
                        {
                            this.Numer.Numer = 1;
                        }
                        else
                        {
                            var last = dc.Reklamacje.Where(r => r.Numer.Symbol == symbol).Max(r => r.Numer.Numer);
                            this.Numer.Numer = last + 1;
                        }

                        n.Numer.NumerPelny = n.Numer.CalcNumerPelny();
                    }
                }
            }
        }

        public bool DeleteRecord()
        {
            this.Deleted = true;
            if (this.DbContext != null)
            {
                ((WebContext)this.DbContext).OptimisticSaveChanges();
                this.DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
                
            }
            return false;
        }

        public bool UndoChanges()
        {
            var dc = this.DbContext as WebContext;
            if (dc != null)
            {
                foreach (var h in this.Historia.ToList())
                {
                    if (h.EntityState == System.Data.EntityState.Added)
                        dc.DeleteObject(h);
                    else if (h.EntityState == System.Data.EntityState.Modified)
                        dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, h);
                }
                foreach (var p in this.Pozycje.ToList())
                {
                    if (p.EntityState == System.Data.EntityState.Added)
                        dc.DeleteObject(p);
                    else if (p.EntityState == System.Data.EntityState.Modified)
                        dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, p);
                }
                if (this.EntityState == System.Data.EntityState.Added)
                    dc.DeleteObject(this);
                else if (this.EntityState == System.Data.EntityState.Modified)
                    dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
                return true;
            }
            return false;
        }

        #endregion

    }
}
