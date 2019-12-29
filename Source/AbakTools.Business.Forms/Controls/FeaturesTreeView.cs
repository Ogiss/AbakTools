using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Business.Forms.Controls
{
    public partial class FeaturesTreeView : UserControl
    {
        #region Fields

        private Enova.API.Business.Table table;

        #endregion

        [Browsable(false)]
        public Enova.API.Business.Table Table
        {
            get
            {
                return this.table;
            }
            set
            {
                this.table = value;
                this.loadFeatures();
            }

        }

        #region Methods

        public FeaturesTreeView()
        {
            InitializeComponent();
        }

        private void loadFeatures()
        {
            if (this.table != null)
            {
                this.treeView.Nodes.Clear();
                var root = new TreeNode("Wszystko");
                this.treeView.Nodes.Add(root);
                foreach (var fd in this.table.FeatureDefinitions)
                {
                    if (fd.Group && fd.StrictDictionary)
                        root.Nodes.Add(new FeatureDefTreeNode(fd));
                }
                root.Expand();
            }
        }

        #endregion

        #region Nested Types

        public class FeatureDefTreeNode : TreeNode
        {
            #region Fields

            private Enova.API.Business.FeatureDefinition featureDef;

            #endregion

            #region Methods

            public FeatureDefTreeNode(Enova.API.Business.FeatureDefinition featureDef)
                : base(featureDef.Name)
            {
                this.featureDef = featureDef;
                this.loadDictionary();
            }

            private void loadDictionary()
            {
                foreach (var d in this.featureDef.DictionaryList)
                {
                    this.Nodes.Add(new DictionaryTreeNode(d));
                }
            }

            #endregion
        }

        public class DictionaryTreeNode : TreeNode
        {
            private Enova.API.Business.DictionaryItem item;

            public DictionaryTreeNode(Enova.API.Business.DictionaryItem item)
                : base(item.Value)
            {
                this.item = item;
            }
        }

        #endregion
    }
}
