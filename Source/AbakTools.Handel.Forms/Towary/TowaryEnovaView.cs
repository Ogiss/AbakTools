using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Towary.Forms
{
    public class TowaryEnovaView : AbakTools.Forms.GridViewBaseWithDbContext<Enova.Business.Old.DB.Web.Produkt>
    {
        #region Fields

        private AbakTools.Handel.Forms.KategorieTreeView kategorieTreeView;

        #endregion

        #region properties

        public override string Key
        {
            get
            {
                return "TowarEnovaView" + (this.SelectionMode ? "Select" : "");
            }
        }

        public override bool ExtraPanelAvailable
        {
            get
            {
                return true;
            }
        }

        public override bool ExtraPanelVisible
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        public TowaryEnovaView() { }

        public TowaryEnovaView(Enova.Business.Old.DB.Web.WebContext dbContext)
            : base(dbContext)
        {
        }

        protected override Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.Produkt> CreateTable()
        {
            Enova.Business.Old.DB.Web.KategoriaOld kategoria = null;
            if (kategorieTreeView != null && kategorieTreeView.WybranaKategoria != null && kategorieTreeView.WybranaKategoria.GUID != Enova.Business.Old.DB.Web.KategoriaOld.EnovaRootGuid)
                kategoria = kategorieTreeView.WybranaKategoria;
            return new Enova.Business.Old.Web.TowaryEnova(this.DbContext) { Kategoria = kategoria };

        }

        public override string GetDefaultXmlDefinition()
        {
            if (this.SelectionMode)
                return AbakTools.Handel.Forms.Properties.Resources.TowarEnovaViewSelect_grid;
            return AbakTools.Handel.Forms.Properties.Resources.TowarEnovaView_grid;
        }

        public override void InitExtraPanel(System.Windows.Forms.Panel panel)
        {
            this.kategorieTreeView = new Handel.Forms.KategorieTreeView();
            this.kategorieTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kategorieTreeView.KategorieEnova = true;
            this.kategorieTreeView.DataContext = this;
            this.kategorieTreeView.AfterSelect +=new System.Windows.Forms.TreeViewEventHandler(kategorieTreeView_AfterSelect);

            panel.Controls.Add(this.kategorieTreeView);
        }

        private void kategorieTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            this.Reload();
        }

        public override void Reload()
        {
            var t = this.Table as Enova.Business.Old.Web.TowaryEnova;
            if (t != null)
            {
                Enova.Business.Old.DB.Web.KategoriaOld kategoria = null;
                if (kategorieTreeView != null && kategorieTreeView.WybranaKategoria != null && kategorieTreeView.WybranaKategoria.GUID != Enova.Business.Old.DB.Web.KategoriaOld.EnovaRootGuid)
                    kategoria = kategorieTreeView.WybranaKategoria;
                t.Kategoria = kategoria;
                t.Reload();
            }

            base.Reload();
        }

        #endregion
    }
}
