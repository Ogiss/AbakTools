using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using Db = Enova.Business.Old.DB.Web;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Types;

[assembly: MenuAction(
    "Magazyn\\Stany magazynowe",
    MenuAction = MenuActionsType.OpenView,
    ViewType = typeof(AbakTools.Magazyn.Forms.StanyMagazynoweView),
    Options = ActionOptions.WithoutSession,
    Priority = 860)]

namespace AbakTools.Magazyn.Forms
{
    public class StanyMagazynoweView : AbakTools.Forms.GridViewBaseWithDbContext<Db.Magazyn_StanMagazynowy>
    {
        #region Fields

        private bool? isNewRow = null;

        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return "Magazyn.StanyMagazynoweView";
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

        protected override Enova.Business.Old.Core.TableBase<Db.Magazyn_StanMagazynowy> CreateTable()
        {
            return new Enova.Business.Old.Core.TableBase<Db.Magazyn_StanMagazynowy>((ObjectQuery<Db.Magazyn_StanMagazynowy>)DbContext.Magazyn_StanyMagazynowe.Where(r => r.Disable == false));
        }

        public override string GetTitle()
        {
            return "Stany magazynowe";
        }

        public override object AddNew()
        {
            var form = new AbakTools.Towary.Forms.WyborTowaruEnovaForm();
            form.ShowDialog();
            if (form.SelectedItem == null)
                return null;

            var towar = (Db.Produkt)form.SelectedItem;
            var row = DbContext.Magazyn_StanyMagazynowe.Where(r => r.TowarGuid == towar.EnovaGuid.Value).FirstOrDefault();
            if (row == null)
            {
                isNewRow = true;

                row = new Db.Magazyn_StanMagazynowy()
                {
                    TowarGuid = towar.EnovaGuid.Value,
                    TowarID = towar.ID,
                    TowarKod = towar.Kod,
                    TowarNazwa = towar.Nazwa
                };

                DbContext.Magazyn_StanyMagazynowe.AddObject(row);
            }
            else
            {
                isNewRow = false;
                row.Disable = false;
                row.StanMag = 0;
                row.Rezerwacje = 0;
            }

            if (this.EditRow(row))
            {
                if (row is IRow && ((IRow)row).State == RowState.Unchanged)
                    return row;
            }
            return null;

        }

        public override void Remove(object obj)
        {
            ((Db.Magazyn_StanMagazynowy)obj).Disable = true;
            DbContext.OptimisticSaveChanges();
            DbContext.Refresh(RefreshMode.StoreWins, obj);
            Reload();
        }

        public override string GetDefaultXmlDefinition()
        {
            return AbakTools.Handel.Forms.Properties.Resources.Magazyn_StanyMagazynoweView_grid;
        }

        #endregion

    }
}
