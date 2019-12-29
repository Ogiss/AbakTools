using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;

[assembly: MenuAction("Kartoteki\\Kontrahenci", MenuAction = MenuActionsType.OpenView, ViewType = typeof(Enova.Forms.CRM.KontrahenciView),
    Options = ActionOptions.WithoutSession, Priority = 510)]


namespace Enova.Forms.CRM
{
    public class KontrahenciView : Enova.Forms.GridViewWithEnovaApi<Enova.API.CRM.Kontrahent>
    {
        #region Fields

        private Enova.Forms.Controls.FeaturesTreeView featureTreeView;

        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return "KontrahenciEnovaView" + (SelectionMode ? "Select" : "");
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

        protected override API.Business.Table<API.CRM.Kontrahent> CreateTable(API.Business.Session session)
        {
            var table = session.GetModule<Enova.API.CRM.CRMModule>().Kontrahenci;
            table.Filter = getFilter();
            return table;
        }

        private string getFilter()
        {
            string filter = null;
            if (this.featureTreeView!=null && this.featureTreeView.SelectedFeatureDef != null && this.featureTreeView.SelectedDictionaryItem != null)
            {
                var data = this.featureTreeView.SelectedDictionaryItem.Path;
                if (this.featureTreeView.SelectedFeatureDef.TypeNumber == API.Business.FeatureTypeNumber.ArrayOfTrees)
                    data = "\\" + data + "\\%";
                filter = string.Format("Features.[{0}] LIKE '{1}'", this.featureTreeView.SelectedFeatureDef.Name, data);
            }
            return filter;
        }

        public override string GetDefaultXmlDefinition()
        {
            return SelectionMode ? Properties.Resources.KontrahenciEnovaViewSelect_grid : Properties.Resources.KontrahenciEnovaView_grid;
        }

        public override void InitExtraPanel(System.Windows.Forms.Panel panel)
        {
            this.featureTreeView = new Enova.Forms.Controls.FeaturesTreeView();
            this.featureTreeView.TableName = "Kontrahenci";
            this.featureTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.featureTreeView.AfterSelect+=new EventHandler<Controls.FeaturesTreeView.FeatureTreeViewEventArgs>(featureTreeView_AfterSelect);
            panel.Controls.Add(this.featureTreeView);
        }

        private void featureTreeView_AfterSelect(object sender, Controls.FeaturesTreeView.FeatureTreeViewEventArgs e)
        {
            if (e.Action == System.Windows.Forms.TreeViewAction.ByMouse || e.Action == System.Windows.Forms.TreeViewAction.ByKeyboard)
            {
                this.Reload();
            }
        }

        public override int Find(System.ComponentModel.PropertyDescriptor property, object key)
        {
            if(key != null)
            {
                var s = ((string)key).ToLower();
                if(!string.IsNullOrEmpty(s))
                {
                    var l = s.Length;
                    for(var i=0; i<this.Rows.Count;i++)
                    {
                        var k = (Enova.API.CRM.Kontrahent)this.Rows[i];
                        if (k.Kod.Length >= l && k.Kod.ToLower().StartsWith(s))
                            return i;
                        else if (k.NIP != null && k.NIP.Length >= l && k.NIP.StartsWith(s))
                            return i;
                    }

                }
            }
            return -1;
        }

        #endregion

    }

}
