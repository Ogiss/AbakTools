using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using DBWeb = Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace AbakTools.Towary.Forms
{
    public class TowaryAtrybutyView : AbakTools.Forms.GridViewBaseWithDbContext<DBWeb.TowarAtrybut>
    {
        #region Fields

        private ViewConfig viewConfig;
        private Enova.Business.Old.Controls.KategorieTreeView kategorieTreeView;
        private System.Windows.Forms.ComboBox activeComboBox;
        private System.Windows.Forms.ComboBox availableComboBox;

        #endregion

        #region Properties

        public override bool AllowNew
        {
            get
            {
                return false;
            }
        }

        public override bool AllowEdit
        {
            get
            {
                return true;
            }
        }

        public override bool AllowRemove
        {
            get
            {
                return false;
            }
        }

        public override bool ExtraPanelAvailable
        {
            get
            {
                if (this.viewConfig != null)
                    return this.viewConfig.ExtraPanelAvailable;
                return base.ExtraPanelAvailable;
            }
        }

        public override bool ExtraPanelVisible
        {
            get
            {
                if (this.viewConfig != null)
                    return this.viewConfig.ExtraPanelVisible;
                return base.ExtraPanelVisible;
            }
        }

        public override string Key
        {
            get
            {
                if (this.viewConfig != null && !string.IsNullOrEmpty(this.viewConfig.Key))
                    return this.viewConfig.Key;
                return base.Key;
            }
        }

        #endregion

        #region Methods

        public TowaryAtrybutyView(BAL.Forms.IMenuItem menuItem)
        {
            if (menuItem != null && menuItem.ActionAttribute.Data != null)
                this.viewConfig = new ViewConfig(menuItem.ActionAttribute.Data.ToString());
        }

        public TowaryAtrybutyView() { }

        protected override Enova.Business.Old.Core.TableBase<DBWeb.TowarAtrybut> CreateTable()
        {
            
            int kategotiaID = this.kategorieTreeView==null || this.kategorieTreeView.Kategoria == null || this.kategorieTreeView.Kategoria.Wlasciciel == null ? 0 : this.kategorieTreeView.Kategoria.ID;
            bool? visibleAV = this.Key == "TowaryAtrybutyListaView" ? (bool?)null : true;
            return new Enova.Business.Old.Web.TowaryAtrybutyTable(this.DbContext, null, null, visibleAV, false, this.kategorieTreeView == null ? null : this.kategorieTreeView.Kategoria);
        }

        public override void InitExtraPanel(System.Windows.Forms.Panel panel)
        {
            this.kategorieTreeView = new Enova.Business.Old.Controls.KategorieTreeView(this.DbContext);
            this.kategorieTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kategorieTreeView.AfterSelect+=new System.Windows.Forms.TreeViewEventHandler(kategorieTreeView_AfterSelect);
            panel.Controls.Add(this.kategorieTreeView);
        }

        private void kategorieTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (this.Table != null)
            {
                ((Enova.Business.Old.Web.TowaryAtrybutyTable)this.Table).Kategoria = this.kategorieTreeView.Kategoria;
            }
            this.Reload();
        }

        public override string GetTitle()
        {
            if (this.viewConfig != null && !string.IsNullOrEmpty(this.viewConfig.Title))
                return this.viewConfig.Title;
            return base.GetTitle();
        }

        public override IEnumerable<DataContextParam> GetParams()
        {
            return new DataContextParam[] { 
                new DataContextParam("activeParam","Aktywny", null){ ControlType = typeof(System.Windows.Forms.ComboBox)},
                new DataContextParam("availableParam","Dostepny", null){ ControlType = typeof(System.Windows.Forms.ComboBox)}
            };
        }

        protected override void OnInitParam(DataContextParamEventArgs e)
        {
            base.OnInitParam(e);
            if (e.Param.Name == "activeParam")
            {
                this.activeComboBox = (System.Windows.Forms.ComboBox)e.Control;
                this.activeComboBox.Items.Add("Razem");
                this.activeComboBox.Items.Add("Aktywne");
                this.activeComboBox.Items.Add("Nieaktywne");
                this.activeComboBox.SelectedIndex = 0;
                this.activeComboBox.SelectionChangeCommitted += (sender, args) =>
                {
                    int idx = this.activeComboBox.SelectedIndex;
                    ((Enova.Business.Old.Web.TowaryAtrybutyTable)this.Table).Aktywne = idx == 1 ? true : (idx == 2 ? false : (bool?)null);
                    this.Reload();
                };
            }
            else if (e.Param.Name == "availableParam")
            {
                this.availableComboBox = (System.Windows.Forms.ComboBox)e.Control;
                this.availableComboBox.Items.Add("Razem");
                this.availableComboBox.Items.Add("Dostepne");
                this.availableComboBox.Items.Add("Niedostępne");
                this.availableComboBox.SelectedIndex = 0;
                this.availableComboBox.SelectionChangeCommitted += (sender, args) =>
                {
                    int idx = this.availableComboBox.SelectedIndex;
                    ((Enova.Business.Old.Web.TowaryAtrybutyTable)this.Table).Dostepne = idx == 1 ? true : (idx == 2 ? false : (bool?)null);
                    this.Reload();
                };
            }
        }

        public override string GetDefaultXmlDefinition()
        {
            if (this.Key == "TowaryAtrybutyListaView")
                return AbakTools.Handel.Forms.Properties.Resources.TowaryAtrybutyListaView_grid;
            else if (this.Key == "MagazynAVView")
                return AbakTools.Handel.Forms.Properties.Resources.MagazynAVView_grid;
            return base.GetDefaultXmlDefinition();
        }

        #endregion

        #region Nested Types

        public class ViewConfig
        {
            public bool ExtraPanelAvailable { get; set; }
            public bool ExtraPanelVisible { get; set; }
            public string Key { get; set; }
            public string Title { get; set; }

            public ViewConfig() { }
            public ViewConfig(string data)
            {
                var parts = data.Split(';');
                foreach (var part in parts)
                {
                    var kvp = part.Split('=');
                    switch (kvp[0])
                    {
                        case "ExtraPanelAvailable":
                            this.ExtraPanelAvailable = bool.Parse(kvp[1]);
                            break;
                        case "ExtraPanelVisible":
                            this.ExtraPanelVisible = bool.Parse(kvp[1]);
                            break;
                        case "Key":
                            this.Key = kvp[1];
                            break;
                        case "Title":
                            this.Title = kvp[1];
                            break;
                    }
                }
            }
        }

        #endregion
    }
}
