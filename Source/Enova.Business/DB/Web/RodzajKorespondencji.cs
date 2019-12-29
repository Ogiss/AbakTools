using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Core;

namespace Enova.Business.Old.DB.Web
{
    [DataEditForm("AbakTools.CRM.Forms.RodzajKorespondencjiEditForm, EnovaTools.Forms")]
    public partial class RodzajKorespondencji : ISaveChanges , IUndoChanges , IDeleteRecord, IComparable
    {

        /*
        public EntityCollection<Korespondencja> Korespondencje
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !RelationKorespondencje.IsLoaded)
                    RelationKorespondencje.Load();
                return RelationKorespondencje;
            }
        }
         */

        public string NazwaPelna
        {
            get
            {
                return Nazwa + (KosztZnaczkow == null || KosztZnaczkow == 0 ? "" : " - " + string.Format("{0:C}", KosztZnaczkow));
            }
        }

        public override string ToString()
        {
            return Nazwa + (KosztZnaczkow == null || KosztZnaczkow == 0 ? "" : " - " + string.Format("{0:C}", KosztZnaczkow));
        }



        #region Edit record implementation

        public bool SaveChanges()
        {
            WebContext dc = ContextManager.WebContext;
            if (EntityState == EntityState.Detached)
                dc.AddToRodzajeKorespondencji(this);

            dc.SaveChanges();
            
            return true;
        }

        public bool UndoChanges()
        {
            if (EntityState == EntityState.Added)
            {
                ContextManager.WebContext.DeleteObject(this);
            }
            else if (EntityState == EntityState.Modified)
            {
                ContextManager.WebContext.Refresh(RefreshMode.StoreWins, this);
            }

            return true;
        }

        public bool DeleteRecord()
        {
            if (Korespondencje.Count == 0)
            {
                ContextManager.WebContext.DeleteObject(this);
                ContextManager.WebContext.SaveChanges();
                return true;
            }

            System.Windows.Forms.MessageBox.Show("Nie można usunąć rekordu, istnieją powiązania", "EnovaTools", System.Windows.Forms.MessageBoxButtons.OK,
                 System.Windows.Forms.MessageBoxIcon.Error);

            return false;
        }

        #endregion


        public int CompareTo(object obj)
        {
            return this.ToString().CompareTo(obj.ToString());
        }
    }
}
