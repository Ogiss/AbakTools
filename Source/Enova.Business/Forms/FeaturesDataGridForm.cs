using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Forms
{
    public partial class FeaturesDataGridForm : Enova.Business.Old.Forms.DataGridForm
    {
        private bool featuresIsLoaded = false;
        protected bool RowsIsLoaded = false;
        ToolStripButton groupToolStripButton;

        [Browsable(true)]
        public string TableName
        {
            get { return FeaturesTreeView.TableName; }
            set { FeaturesTreeView.TableName = value; }
        }

        public FeaturesDataGridForm()
        {
            InitializeComponent();

            ComponentResourceManager resources = new ComponentResourceManager(typeof(FeaturesDataGridForm));
            this.groupToolStripButton = new ToolStripButton();
            groupToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            groupToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("GroupImage")));
            groupToolStripButton.Size = new Size(23, 23);
            groupToolStripButton.CheckOnClick = true;
            groupToolStripButton.Checked = true;
            groupToolStripButton.Text = "Cechy";
            groupToolStripButton.ToolTipText = "Cechy";
            groupToolStripButton.CheckedChanged += new EventHandler(groupToolStripButton_CheckedChanged);
            DataGridBindingNavigator.Items.Add(groupToolStripButton);
        }

        private void groupToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (groupToolStripButton.Checked)
            {
                LeftPanelCollapsed = false;
            }
            else
            {
                LeftPanelCollapsed = true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                base.OnLoad(e);
                if (!string.IsNullOrEmpty(TableName) && !featuresIsLoaded)
                {
                    FeaturesTreeView.LoadFeatures();
                    featuresIsLoaded = true;
                }
            }
        }

        private void FeaturesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RowsIsLoaded = false;
            if (DataSource is Enova.Business.Old.Core.IFeatures)
            {
                if (e.Node is Enova.Business.Old.Controls.FeaturesTree.FeatureTreeNode)
                {
                    Enova.Business.Old.Controls.FeaturesTree.FeatureTreeNode node = (Enova.Business.Old.Controls.FeaturesTree.FeatureTreeNode)e.Node;
                    if (node.IsAll)
                        ((Enova.Business.Old.Core.IFeatures)DataSource).RemoveFeatureFilter();
                }
                else
                {
                    Enova.Business.Old.Controls.FeaturesTree.DictionaryTreeNode node = (Enova.Business.Old.Controls.FeaturesTree.DictionaryTreeNode)e.Node;
                    ((Enova.Business.Old.Core.IFeatures)DataSource).ApplyFeatureFilter(node.FeatureDef, node.Value);
                }
            }
        }

        private void FeaturesTreeView_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void FeaturesTreeView_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void FeaturesTreeView_DragLeave(object sender, EventArgs e)
        {

        }

        private void FeaturesTreeView_DragOver(object sender, DragEventArgs e)
        {

        }

        private void FeaturesTreeView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void FeaturesTreeView_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void DataGrid_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void FeaturesTreeView_Load(object sender, EventArgs e)
        {

        }
    }
}
