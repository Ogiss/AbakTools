using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Text;
using System.Drawing;
using Enova.Business.Old.Core;
using Enova.Business.Old.Types;
using System.Windows.Forms;

namespace Enova.Business.Old.DB.Web
{
    [DataEditForm("AbakTools.Zamowienia.Forms.StatusZamowieniaEditForm, AbakTools.Handel.Forms")]
    public partial class StatusZamowienia : ISaveChanges, IUndoChanges, IDeleteRecord
    {
        public StatusZamowienia()
        {
            this.PSID = null;
            this.Szablon = string.Empty;
            this.BlokadaUsuniecia = false;
            this.Faktura = true;
            this.Kolor = ColorTranslator.ToHtml(Color.White);
            this.Nazwa = string.Empty;
            this.Stamp = DateTime.Now;
            this.Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew;
            this.Ukryty = false;
            this.WyslacEmail = false;
        }

        public override string ToString()
        {
            return Nazwa;
        }

        public EntityCollection<HistoriaZamowienia> HistorieZamowien
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !RelationHistorieZamowien.IsLoaded)
                    RelationHistorieZamowien.Load();
                return RelationHistorieZamowien;
            }
        }

        #region Edit record functions

        public bool SaveChanges()
        {
            WebContext dc = ContextManager.WebContext;

            if (!string.IsNullOrEmpty(this.Nazwa))
            {
                if (EntityState == EntityState.Detached)
                    dc.AddToStatusyZamowien(this);

                if (EntityState == EntityState.Added || EntityState == EntityState.Modified)
                {
                    if(EntityState == EntityState.Added){
                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
                    }else{
                        switch ((RowSynchronizeOld)Synchronizacja)
                        {
                            case RowSynchronizeOld.NotsynchronizedDelete:
                            case RowSynchronizeOld.Synchronized:
                                Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                                break;
                        }
                    }
                    Stamp = DateTime.Now;
                    dc.SaveChanges();
                }

                return true;
            }
            return false;
        }

        public bool UndoChanges()
        {
            WebContext dc = ContextManager.WebContext;
            if (EntityState == EntityState.Added)
            {
                dc.DeleteObject(this);
            }
            else if(EntityState == EntityState.Modified)
            {
                dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
            }

            return true;
        }

        public bool DeleteRecord()
        {
            if (EntityState != EntityState.Deleted && EntityState!= EntityState.Detached)
            {
                if ((bool)BlokadaUsuniecia)
                {
                    MessageBox.Show("Nie można usunąć statusu, ponieważ posiada blokade usunięcia.", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (HistorieZamowien.Count > 0)
                {
                    MessageBox.Show("Nie można usunąć statusu, ponieważ isnieją powiązane recordy.", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    WebContext dc = ContextManager.WebContext;
                    if (EntityState == EntityState.Added)
                    {
                        dc.DeleteObject(this);
                    }
                    else
                    {
                        this.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                        dc.SaveChanges();
                    }
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
