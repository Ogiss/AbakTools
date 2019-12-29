using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Core;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.DB.Web
{
    public partial class KategoriaOld : ISaveChanges ,IUndoChanges, IDeleteRecord, IDbContext
    {
        #region Fields

        private static Guid enovaRootGuid = new Guid("00000000-0001-0001-0001-000000000000");

        #endregion

        #region Properties

        public static Guid EnovaRootGuid
        {
            get { return KategoriaOld.enovaRootGuid; }
        }

        public ObjectContext DbContext { get; set; }

        public bool DoUsuniecia = false;

        #endregion

        public KategoriaOld() : this(null) { }

        public KategoriaOld(KategoriaOld wlasciciel)
        {
            PSID = 0;
            PSWlascielID = 0;
            PoziomGlebokosci = wlasciciel != null ? (byte)(wlasciciel.PoziomGlebokosci + 1) : (byte)0;
            Aktywna = true;
            DataDodania = DateTime.Now;
            DataAktualizacji = DataDodania;
            GrupaID = 0;
            LangID = 3;
            Nazwa = string.Empty;
            Opis = string.Empty;
            PrzyjaznyLink = string.Empty;
            MetaTytul = string.Empty;
            MetaOpis = string.Empty;
            MetaSlowa = string.Empty;
            Stan = 0;
            Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
            Stamp = DataDodania;
            GUID = Guid.NewGuid();
            KolejnoscWyswietlania = 1000;
            Wlasciciel = wlasciciel;
            EnovaFeature = false;
        }

        #region Properties

        public EntityCollection<KategoriaProdukt> KategorieProduktow
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !RelationKategorieProduktow.IsLoaded)
                    RelationKategorieProduktow.Load();
                return RelationKategorieProduktow;
            }
        }


        public static KategoriaOld Root
        {
            get
            {
                return GetRoot(Enova.Business.Old.Core.ContextManager.WebContext);
            }
        }

        public static KategoriaOld EnovaRoot
        {
            get
            {
                return GetEnovaRoot(Enova.Business.Old.Core.ContextManager.WebContext);
            }
        }

        partial void OnNazwaChanged()
        {
            PrzyjaznyLink = Enova.Business.Old.Core.Tools.LinkRewrite(Nazwa);
        }

        public static KategoriaOld GetRoot(WebContext dc)
        {
            return dc.KategorieOld.Where(k => k.PoziomGlebokosci == 0).FirstOrDefault();
        }

        public static KategoriaOld GetEnovaRoot(WebContext dc)
        {
            return dc.KategorieOld.Where(k => k.GUID == enovaRootGuid).FirstOrDefault();
        }

        #endregion


        #region Edit record implementation

        bool ISaveChanges.SaveChanges()
        {
            if (this.DoUsuniecia)
            {
                return ((IDeleteRecord)this).DeleteRecord();
            }

            if (Wlasciciel != null && (Wlasciciel.EntityState == EntityState.Added || Wlasciciel.EntityState == EntityState.Detached))
            {
                if (EntityState == EntityState.Added)
                    ContextManager.WebContext.Detach(this);
                if (!((ISaveChanges)Wlasciciel).SaveChanges())
                    return false;
            }

            if (EntityState == EntityState.Detached)
                ContextManager.WebContext.AddToKategorieOld(this);


            if (EntityState != EntityState.Unchanged)
            {
                Stamp = DateTime.Now;
                DataAktualizacji = Stamp;
                if (EntityState == EntityState.Added)
                {
                    DataDodania = Stamp;
                    Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
                }
                else if (EntityState == EntityState.Modified)
                {
                    if (Synchronizacja == (int)RowSynchronizeOld.Synchronized)
                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                }
                ContextManager.WebContext.SaveChanges();
            }

            return true;
        }

        bool IUndoChanges.UndoChanges()
        {
            if (EntityState == EntityState.Added)
                ContextManager.WebContext.DeleteObject(this);
            else if (EntityState == EntityState.Modified)
                ContextManager.WebContext.Refresh(RefreshMode.StoreWins, this);
            if (DoUsuniecia)
                DoUsuniecia = false;
            return true;
        }

        bool IDeleteRecord.DeleteRecord()
        {
            if (this.EntityState == EntityState.Added)
            {
                ContextManager.WebContext.DeleteObject(this);
            }
            else
            {
                Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                this.Stamp = DateTime.Now;
                ContextManager.WebContext.SaveChanges();
            }
            return true;
        }

        #endregion

    }
}
