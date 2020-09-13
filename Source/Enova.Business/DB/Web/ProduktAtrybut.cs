using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Windows.Forms;

namespace Enova.Business.Old.DB.Web
{
    public partial class ProduktAtrybut
    {
        public string AtrybutNazwaPelna
        {
            get
            {
                if (!string.IsNullOrEmpty(AtrybutNazwa) && !string.IsNullOrEmpty(GrupaAtrubutowNazwa))
                    return GrupaAtrubutowNazwa + ": " + (this.AtrybutPrefix != null ? this.AtrybutPrefix : "") + AtrybutNazwa + (this.AtrybutSuffix != null ? this.AtrybutSuffix : "");
                return null;
            }
        }

        public decimal? CenaBrutto
        {
            get
            {
                if (CenaNetto != null && StawkaVatValue != null)
                    return decimal.Round(CenaNetto.Value * (1M + StawkaVatValue.Value / 100M), 2);
                return null;
            }
        }

        private Produkt produkt = null;
        public bool ProduktIsLoaded = false;
        public void LoadProdukt()
        {
            produkt = Enova.Business.Old.Core.ContextManager.WebContext.Produkty.Where(p => p.ID == this.ID).FirstOrDefault();
            ProduktIsLoaded = true;
        }

        public Produkt Produkt
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !ProduktIsLoaded)
                    LoadProdukt();
                return produkt;
            }
        }

        private AtrybutProduktu atrybutProduktu = null;
        public bool AtrybutProduktuIsLoaded = false;
        public void LoadAtrybutProduktu()
        {
            if (AtrybutProduktuID != null)
                atrybutProduktu = Enova.Business.Old.Core.ContextManager.WebContext.AtrybutyProduktow.Where(a => a.ID == this.AtrybutProduktuID).FirstOrDefault();
            AtrybutProduktuIsLoaded = true;
        }
        public AtrybutProduktu AtrybutProduktu
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !AtrybutProduktuIsLoaded)
                    LoadAtrybutProduktu();
                return atrybutProduktu;
            }
        }

        private Zdjecie zdjecie = null;
        public Zdjecie Zdjecie
        {
            get
            {
                /*
                 var zdjecie = atrybutProduktu.ProduktyAtrybutyZdjecia
                    .Where(apz=>apz.Synchronizacja != (int)RowSynchronize.NotsynchronizedDelete && apz.Zdjecie.Deleted == false && apz.Zdjecie.Synchronizacja != (byte)RowSynchronize.NotsynchronizedDelete).ToList()
                    .Where(apz=>apz.IsDeleted == false).Select(apz => apz.Zdjecie).FirstOrDefault();
                 */

                if (AtrybutProduktu != null)
                {
                    ProduktAtrybutZdjecie paz = AtrybutProduktu.ProduktyAtrybutyZdjecia.Where(apz => apz.Synchronizacja != (int)Types.RowSynchronizeOld.NotsynchronizedDelete &&
                        apz.Zdjecie.Deleted == false).FirstOrDefault();
                    if (paz != null)
                        zdjecie = paz.Zdjecie;
                }
                else if (Produkt != null && zdjecie == null)
                {
                    zdjecie = Produkt.Zdjecia.Where(z => z.Okladka == true && z.Synchronizacja != (int)Types.RowSynchronizeOld.NotsynchronizedDelete && z.Deleted == false).FirstOrDefault();
                    if (zdjecie == null)
                        zdjecie = Produkt.Zdjecia.Where(z=>z.Synchronizacja != (int)Types.RowSynchronizeOld.NotsynchronizedDelete && z.Deleted == false).FirstOrDefault();
                }
                return zdjecie;
            }
        }

        private bool? aktywny = null;
        public bool Aktywny
        {
            get
            {
                if (aktywny == null)
                    aktywny = this.IsActive == null ? true : this.IsActive.Value;
                return this.aktywny.Value;
            }
            set
            {
                var produkt = this.Produkt;
                var atrybut = this.AtrybutProduktu;
                if (atrybut != null)
                {
                    atrybut.Synchronizacja = value ? (byte)Types.RowSynchronizeOld.NotsynchronizedNew : (byte)Types.RowSynchronizeOld.NotsynchronizedDelete;
                    if (value)
                    {
                        produkt.Aktywny = true;
                        atrybut.Deleted = false;
                    }
                }
                else
                {
                    produkt.Aktywny = value;
                }
                this.aktywny = value;
            }
        }


        public bool Dostepny
        {
            get {
                return this.IsAvailable == null ? true : this.IsAvailable.Value;
            }
            set
            {
                var produkt = this.Produkt;
                var atrybutProduktu = this.AtrybutProduktu;
                if (atrybutProduktu != null)
                {
                    atrybutProduktu.Dostepny = value;
                }
                else if (produkt != null)
                {
                    produkt.Dostepny = value;
                }
            }
        }

        public bool Visible
        {
            get
            {
                return this.AvailableVisible == null ? true : this.AvailableVisible.Value;
            }
            set
            {
                var produkt = this.Produkt;
                var atrybutProduktu = this.AtrybutProduktu;
                if (atrybutProduktu != null)
                    atrybutProduktu.VisibleAV = value;
                else if (produkt != null)
                    produkt.VisibleAV = value;

            }
        }

        public string TekstDostepnosci { get; set; }
        public string DataDostepnosci { get; set; }

        #region Auto synchronizacja dostepności

        private bool? autoSynchDostepnosc;
        private int autoSynchStanMin;

        private void loadAutoSynch(EnovaContext ec)
        {
            if (this.EnovaGUID != null)
            {
                var features = (from fa in ec.Features
                                join t in ec.Towary on fa.Parent equals this.ID
                                where fa.ParentType == "Towary" && t.Guid == this.EnovaGUID && fa.Lp == 0
                                && (fa.Name == "AUTO_SYNCH_DOSTEPNOSC" || fa.Name == "AUTO_SYNCH_STAN_MIN")
                                select new { Name = fa.Name, Data = fa.Data }).ToList();

                autoSynchDostepnosc = false;
                autoSynchStanMin = 0;
                var fe = features.Where(f=>f.Name == "AUTO_SYNCH_DOSTEPNOSC").FirstOrDefault();
                if (fe != null)
                    autoSynchDostepnosc = fe.Data.Trim() == "1" ? true : false;
                fe = features.Where(f => f.Name == "AUTO_SYNCH_STAN_MIN").FirstOrDefault();
                if (fe != null)
                    int.TryParse(fe.Data, out autoSynchStanMin);
            }
        }

        public bool AutoSynchDostepnosc
        {
            get
            {
                if (autoSynchDostepnosc == null)
                    loadAutoSynch(Enova.Business.Old.Core.ContextManager.DataContext);
                return autoSynchDostepnosc.Value;
            }
        }

        public int AutoSynchStanMin
        {
            get
            {
                if (autoSynchDostepnosc == null)
                    loadAutoSynch(Enova.Business.Old.Core.ContextManager.DataContext);
                return autoSynchStanMin;
            }
        }

        #endregion

    }
}
