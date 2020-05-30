using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Enova.Business.Old.Types;
using System.Transactions;

namespace Enova.Business.Old.DB.Web
{
    public partial class TowarAtrybut : IDbContext, Enova.Business.Old.Core.IContextSaveChanges
    {
        #region Methods

        private WeakReference enovaContext;

        #endregion

        #region Properties

        public System.Data.Objects.ObjectContext DbContext { get; set; }

        public EnovaContext EnovaContext
        {
            get { return enovaContext != null ? (EnovaContext)enovaContext.Target : null; }
            set { enovaContext = value == null ? null : new WeakReference(value); }
        }
        

        public bool Aktywny
        {
            get { return this.ROAktywny; }
            set { this.setAktywny(DbContext, value, false); }
        }

        public bool AktywnyWithSave
        {
            get { return this.Aktywny; }
            set { this.setAktywny(DbContext, value, true); }
        }

        public bool Dostepny
        {
            get { return this.RODostepny; }
            set { this.setDostepny(DbContext, value, false); }
        }

        public bool DostepnyWithSave
        {
            get { return this.Dostepny; }
            set { this.setDostepny(DbContext, value, true); }
        }

        public bool DisableAV
        {
            get { return this.RODisableAV == null ? false : this.RODisableAV.Value; }
            set { this.setDisableAV(DbContext, value, false); }
        }

        public bool DisableAVWithSave
        {
            get { return this.DisableAV; }
            set { this.setDisableAV(DbContext, value, true); }
        }

        public bool VisibleAV
        {
            get { return this.ROVisibleAV; }
            set { this.setVisibleAV(DbContext, value, false); }
        }

        public bool VisibleAVWidthSave
        {
            get { return this.VisibleAV; }
            set { this.setVisibleAV(DbContext, value, true); }
        }

        public string TowarKodNazwa
        {
            get
            {
                return "(" + this.TowarKod + ") " + this.TowarNazwa;
            }
        }

        public string AtrybutNazwaPelna
        {
            get
            {
                if (this.AtrybutTowaru != null)
                    return this.GrupaAtrNazwaPubliczna + ": " + this.AtrybutNazwa;
                return null;
            }
        }

        public bool? AutoSynchEnabled
        {
            get
            {
                if (this.EnovaContext != null)
                    return this.GetAutoSynchEnabled(this.EnovaContext);
                return null;
            }
            set
            {
                if (this.EnovaContext != null && value != null)
                {
                    this.SetAutoSynchEnabled(this.EnovaContext, value.Value, false);
                }
            }
        }

        public int? AutoSynchStanMin
        {
            get
            {
                if (this.EnovaContext != null)
                    return this.GetAutoSynchStanMin(this.EnovaContext);
                return null;
            }
            set
            {
                if (this.EnovaContext != null && value != null)
                {
                    this.SetAutoSynchStanMin(this.EnovaContext, value.Value.ToString(), false);
                }
            }
        }

        private Zdjecie zdjecie = null;
        public Zdjecie Zdjecie
        {
            get
            {
                if (this.AtrybutTowaru != null)
                {
                    ProduktAtrybutZdjecie paz = this.AtrybutTowaru.ProduktyAtrybutyZdjecia.Where(apz => apz.Synchronizacja != (int)Types.RowSynchronizeOld.NotsynchronizedDelete &&
                        apz.Zdjecie.Deleted == false).FirstOrDefault();
                    if (paz != null)
                        zdjecie = paz.Zdjecie;
                }
                else if (this.Towar != null && zdjecie == null)
                {
                    zdjecie = this.Towar.Zdjecia.Where(z => z.Okladka == true && z.Synchronizacja != (int)Types.RowSynchronizeOld.NotsynchronizedDelete && z.Deleted == false).FirstOrDefault();
                    if (zdjecie == null)
                        zdjecie = this.Towar.Zdjecia.Where(z => z.Synchronizacja != (int)Types.RowSynchronizeOld.NotsynchronizedDelete && z.Deleted == false).FirstOrDefault();
                }
                return zdjecie;
            }
        }

        #endregion

        #region Methods

        private void setAktywny(System.Data.Objects.ObjectContext dc, bool value, bool save)
        {
             setBlokada(dc, !value);

        }

        private void setBlokada(System.Data.Objects.ObjectContext dc, bool value)
        {
            try
            {
                if (dc != null)
                {
                    dc.ExecuteStoreCommand("EXEC dbo.SetZasobyBlokada {0},{1},{2}", this.TowarID, this.AtrybutTowaruID, value);
                    dc.Refresh(RefreshMode.StoreWins, this);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void setDostepny(System.Data.Objects.ObjectContext dc, bool value, bool save)
        {
            try
            {
                dc.ExecuteStoreCommand("EXEC dbo.SetZasobyDostepny {0},{1},{2}", this.TowarID, this.AtrybutTowaruID, value);
                dc.Refresh(RefreshMode.StoreWins, this);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void setVisibleAV(System.Data.Objects.ObjectContext dc, bool value, bool save)
        {
            try
            {
                dc.ExecuteStoreCommand("EXEC dbo.SetZasobyDoSprawdzenia {0},{1},{2}", this.TowarID, this.AtrybutTowaruID, value);
                dc.Refresh(RefreshMode.StoreWins, this);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void setDisableAV(System.Data.Objects.ObjectContext dc, bool value, bool save)
        {
            using (var t = new TransactionScope())
            {
                bool changed = false;
                if (this.AtrybutTowaru != null)
                {
                    if (this.AtrybutTowaru.DisableAVList != value)
                    {
                        this.AtrybutTowaru.DisableAVList = value;
                        changed = true;
                    }
                    if (!value && this.Towar.DisableAVList)
                    {
                        this.Towar.DisableAVList = false;
                        changed = true;
                    }
                }
                else if (this.Towar.DisableAVList != value)
                {
                    this.Towar.DisableAVList = value;
                    changed = true;
                }

                if (changed && save)
                {
                    dc.SaveChanges();
                    dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
                }
                t.Complete();
            }
        }

        public void UpdateSynchronize()
        {
            if ((RowSynchronizeOld)this.Towar.Synchronizacja == RowSynchronizeOld.Synchronized)
                this.Towar.Synchronizacja = (byte)RowSynchronizeOld.NotsynchronizedEdit;
            if ((RowSynchronizeOld)this.Towar.Synchronizacja == RowSynchronizeOld.NotsynchronizedEdit)
                this.Towar.Gotowy = true;
        }

        public bool SaveChanges(System.Data.Objects.ObjectContext dataContext)
        {
            using (var t = new System.Transactions.TransactionScope())
            {
                dataContext.SaveChanges();
                dataContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
                t.Complete();
            }

            return true;
        }

        public bool GetAutoSynchEnabled(EnovaContext dc)
        {
            var feature = this.Towar.GetFeature(dc, "AUTO_SYNCH_DOSTEPNOSC");
            if (feature != null)
                return feature.Data.Trim() == "1" ? true : false;
            return false;
        }

        public void SetAutoSynchEnabled(EnovaContext dc, bool enabled, bool save)
        {
            using (var t = new TransactionScope())
            {
                this.Towar.SetFeature(dc, "AUTO_SYNCH_DOSTEPNOSC", enabled ? "1" : "0");
                if (save)
                    dc.OptimisticSaveChanges();
                t.Complete();
            }
        }

        public int GetAutoSynchStanMin(EnovaContext dc)
        {
            try
            {
                var feature = this.Towar.GetFeature(dc, "AUTO_SYNCH_STAN_MIN");
                if (feature != null)
                    return int.Parse(feature.Data);
            }
            catch { }
            return 0;
        }

        public void SetAutoSynchStanMin(EnovaContext dc, string stanMin, bool save)
        {
            using (var t = new TransactionScope())
            {
                this.Towar.SetFeature(dc, "AUTO_SYNCH_STAN_MIN", stanMin);
                if (save)
                    dc.OptimisticSaveChanges();
                t.Complete();
            }
        }

        #endregion

    }
}
