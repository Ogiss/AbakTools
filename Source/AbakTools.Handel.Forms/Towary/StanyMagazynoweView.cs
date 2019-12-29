using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using DBWeb = Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

/*
[assembly: MenuAction(
    "Magazyn\\Stany magazynowe", 
    MenuAction = MenuActionsType.OpenView, 
    ViewType = typeof(AbakTools.Towary.Forms.StanyMagazynoweView), 
    Options = ActionOptions.WithoutSession, 
    Priority = 850)]
 */

/*
namespace AbakTools.Towary.Forms
{
    public class StanyMagazynoweView : GridViewContext
    {
        #region Fields

        private bool? isNewRow = null;
        private Enova.Business.Old.Web.StanyMagazynoweView table;
        private DBWeb.WebContext dbContext;
        private object reloadLock = new object();

        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return "StanyMagazynoweView";
            }
        }

        public override System.Collections.IList Rows
        {
            get
            {
                if (this.table == null)
                    this.initTable();
                return table;
            }
        }

        public DBWeb.WebContext DBContext
        {
            get
            {
                if (dbContext == null)
                    dbContext = new DBWeb.WebContext("name=WebContext");
                return dbContext;
            }
        }

        public override bool IsNewRow
        {
            get
            {
                if (this.isNewRow != null)
                    return this.isNewRow.Value;
                return base.IsNewRow;
            }
        }

        #endregion

        #region Methods

        public override Type GetDataType()
        {
            return typeof(DBWeb.StanMagazynowyView);
        }

        public override string GetTitle()
        {
            return "Stany magazynowe";
        }

        public override string GetDefaultXmlDefinition()
        {
            return AbakTools.Handel.Forms.Properties.Resources.StanyMagazynoweView_grid;
        }

        private void initTable()
        {
            this.table = new Enova.Business.Old.Web.StanyMagazynoweView(
                (System.Data.Objects.ObjectQuery<DBWeb.StanMagazynowyView>)this.DBContext.StanyMagazynoweView.Where(sm=>(sm.Usuniety == null || sm.Usuniety == false) && sm.KontrolaStanow == true)
                );
            if (this.SupportsSorting && IsSorted)
                this.table.Sort(this.GetSortComparer());

        }

        protected override void Dispose(bool userCall)
        {
            base.Dispose(userCall);
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
                this.dbContext = null;
            }
        }

        public override void Reload()
        {
            lock (reloadLock)
            {
                if (this.table != null)
                {
                    this.table.Reload();
                    if (SupportsSorting && IsSorted)
                        this.table.Sort(GetSortComparer());
                }
                base.Reload();
            }
        }

        public override int IndexOf(object obj)
        {
            var view = obj as DBWeb.StanMagazynowyView;

            if (view == null)
                view = DBContext.StanyMagazynoweView.Where(s => s.StanMagazynowyID == ((DBWeb.StanMagazynowy)obj).ID).FirstOrDefault();

            return base.IndexOf(view);
        }

        public override object AddNew()
        {
            var form = new WyborTowaruEnovaForm();
            form.ShowDialog();
            if (form.SelectedItem == null)
                return null;

            var guid = ((DBWeb.Produkt)form.SelectedItem).EnovaGuid.Value;
            var row = DBContext.StanyMagazynowe.Where(s => s.EnovaTowarGuid == guid).FirstOrDefault();
            if (row == null)
            {
                this.isNewRow = true;
                row = new DBWeb.StanMagazynowy()
                {
                    EnovaTowarGuid = ((DBWeb.Produkt)form.SelectedItem).EnovaGuid,
                    KontrolaStanowMag = true,
                    StanMag = 0,
                    Rezerwacja = 0,
                    DbContext = DBContext
                };

                DBContext.StanyMagazynowe.AddObject(row);
            }
            else
            {
                this.isNewRow = false;
                if (!row.KontrolaStanowMag)
                {
                    row.KontrolaStanowMag = true;
                    row.StanMag = 0;
                }
            }

            if (this.EditRow(row))
            {
                if (row is IRow && ((IRow)row).State == RowState.Unchanged)
                    return row;
            }
            return null;
        }

        public override int Add(object obj)
        {
            var view = obj as DBWeb.StanMagazynowyView;

            if (view == null)
                view = DBContext.StanyMagazynoweView.Where(s => s.StanMagazynowyID == ((DBWeb.StanMagazynowy)obj).ID).FirstOrDefault();

            return base.Add(view);
        }

        public override void Remove(object obj)
        {
            var row = ((DBWeb.StanMagazynowyView)obj).GetStanMagazynowy(DBContext);
            row.KontrolaStanowMag = false;
            DBContext.OptimisticSaveChanges();
            this.Reload();
        }

        public override void Sort(System.Collections.IComparer comparer)
        {
            if (this.table != null)
                this.table.Sort(comparer);
        }

        public override void EndEdit()
        {
            base.EndEdit();
            this.Reload();
            this.isNewRow = null;
        }
        

        #endregion
    }
}
*/