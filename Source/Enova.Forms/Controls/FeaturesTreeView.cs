using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Forms.Controls
{
    public partial class FeaturesTreeView : UserControl
    {
        #region Fields

        private Enova.API.Business.Table table;
        private bool isLoaded;

        #endregion

        public string TableName { get; set; }

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

        [Browsable(false)]
        public TreeNode SelectedNode
        {
            get
            {
                return this.treeView.SelectedNode;
            }
        }

        [Browsable(false)]
        public Enova.API.Business.FeatureDefinition SelectedFeatureDef
        {
            get
            {
                if (SelectedNode != null)
                {
                    if (typeof(FeatureDefTreeNode).IsAssignableFrom(SelectedNode.GetType()))
                        return ((FeatureDefTreeNode)SelectedNode).FeatureDef;
                    else if (typeof(DictionaryTreeNode).IsAssignableFrom(SelectedNode.GetType()))
                        return ((DictionaryTreeNode)SelectedNode).FeatureDef;
                }
                return null;
            }
        }

        [Browsable(false)]
        public Enova.API.Business.DictionaryItem SelectedDictionaryItem
        {
            get
            {
                if (typeof(DictionaryTreeNode).IsAssignableFrom(SelectedNode.GetType()))
                    return ((DictionaryTreeNode)SelectedNode).DictionaryItem;
                return null;
            }
        }

        #region Events

        public event EventHandler<FeatureTreeViewEventArgs> AfterSelect;

        #endregion

        #region Methods

        public FeaturesTreeView()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!isLoaded && !this.DesignMode)
                loadFeatures();
        }

        private void loadFeatures()
        {
            this.treeView.Nodes.Clear();
            var root = new TreeNode("Wszystko");
            this.treeView.Nodes.Add(root);

            if (this.table != null)
            {
            }
            else if(!string.IsNullOrEmpty(this.TableName))
            {
                using (var s = Enova.API.EnovaService.Instance.CreateSession())
                {
                    var table = s.GetTable(this.TableName);
                    foreach (var fd in table.FeatureDefinitions.ToList().OrderBy(r=>r.Name))
                    {
                        if (fd.Group && fd.StrictDictionary)
                            root.Nodes.Add(new FeatureDefTreeNode(fd));
                    }
                }
            }
            root.Expand();
            isLoaded = true;

        }

        protected virtual void OnAfterSelect(FeatureTreeViewEventArgs e)
        {
            if (this.AfterSelect != null)
                this.AfterSelect(this, e);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.OnAfterSelect(new FeatureTreeViewEventArgs(e.Node, e.Action));
        }

        #endregion

        #region Nested Types

        public class FeatureDefTreeNode : TreeNode
        {
            #region Fields

            private Enova.API.Business.FeatureDefinition featureDef;
            private SortedSet<Enova.API.Business.DictionaryItem> items;
            private Dictionary<int, DicItem> nodes;
            
            #endregion

            #region Properties

            public Enova.API.Business.FeatureDefinition FeatureDef
            {
                get { return this.featureDef; }
            }

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
                items = new SortedSet<API.Business.DictionaryItem>(new DictionaryItemComparer());
                nodes = new Dictionary<int, DicItem>();
                foreach (var d in this.featureDef.DictionaryList)
                    items.Add(d);
                foreach (var d in items)
                {
                    DictionaryTreeNode node = new DictionaryTreeNode(this.featureDef, d);
                    if (d.Parent == null)
                        this.Nodes.Add(node);
                    else if (this.nodes.ContainsKey(d.Parent.ID))
                        this.nodes[d.Parent.ID].Node.Nodes.Add(node);
                    else
                    {

                    }
                    this.nodes.Add(d.ID, new DicItem() { Node = node, Item = d });
                }

            }

            #endregion
        }

        public class DictionaryItemComparer : IComparer<Enova.API.Business.DictionaryItem>
        {

            public int Compare(API.Business.DictionaryItem x, API.Business.DictionaryItem y)
            {
                int px = x.Parent == null ? 0 : x.Parent.ID;
                int py = y.Parent == null ? 0 : y.Parent.ID;
                var cmp = px.CompareTo(py);
                if (cmp != 0)
                    return cmp;
                return x.Value.CompareTo(y.Value);
            }
        }

        public class DictionaryTreeNode : TreeNode
        {
            private Enova.API.Business.DictionaryItem dictionaryItem;
            private Enova.API.Business.FeatureDefinition featureDef;

            public Enova.API.Business.FeatureDefinition FeatureDef
            {
                get { return this.featureDef; }
            }

            public Enova.API.Business.DictionaryItem DictionaryItem
            {
                get { return this.dictionaryItem; }
            }

            public DictionaryTreeNode(Enova.API.Business.FeatureDefinition featureDef, Enova.API.Business.DictionaryItem item)
                : base(item.Value)
            {
                this.dictionaryItem = item;
                this.featureDef = featureDef;
            }
        }

        public class DicItem
        {
            public DictionaryTreeNode Node;
            public Enova.API.Business.DictionaryItem Item;
        }


        public class FeatureTreeViewEventArgs : EventArgs
        {
            private TreeNode node;
            private TreeViewAction action;

            public TreeNode Node
            {
                get { return node; }
            }

            public Enova.API.Business.FeatureDefinition FeatureDef
            {
                get
                {
                    if (typeof(FeatureDefTreeNode).IsAssignableFrom(node.GetType()))
                        return ((FeatureDefTreeNode)node).FeatureDef;
                    else if (typeof(DictionaryTreeNode).IsAssignableFrom(node.GetType()))
                        return ((DictionaryTreeNode)node).FeatureDef;
                    else
                        return null;
                }
            }

            public Enova.API.Business.DictionaryItem DictionaryItem
            {
                get
                {
                    if (typeof(DictionaryTreeNode).IsAssignableFrom(node.GetType()))
                        return ((DictionaryTreeNode)node).DictionaryItem;
                    return null;
                }
            }

            public TreeViewAction Action
            {
                get { return this.action; }
            }

            public FeatureTreeViewEventArgs(TreeNode node, TreeViewAction action)
            {
                this.node = node;
                this.action = action;
            }
        }

        #endregion

    }
}
