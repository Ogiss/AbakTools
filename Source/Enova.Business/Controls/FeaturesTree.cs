using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class FeaturesTree : UserControl
    {
        #region Properties

        public string TableName { get; set; }

        public Color LineColor
        {
            get { return treeView.LineColor; }
            set { treeView.LineColor = value; }
        }

        #endregion

        #region Constructors

        public FeaturesTree()
        {
            InitializeComponent();
        }

        public FeaturesTree(string tableName)
            : this()
        {
            this.TableName = tableName;
        }

        #endregion

        #region Methods

        public void LoadFeatures()
        {
            this.Invoke(new MethodInvoker(loadData));
        }

        private void loadData()
        {
            Enova.Business.Old.DB.EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
            if (dc != null && !string.IsNullOrEmpty(TableName))
            {
                this.treeView.Nodes.Clear();
                this.treeView.Nodes.Add(new FeatureTreeNode(new FeatureDef() { Name = "Wszystko", ID = 0 }));
                var features = dc.FeatureDefs.Where(f => f.TableName == TableName && f.StrictDictionary == true && f.Group == true)
                    .OrderBy(f => f.Name).ToList();

                foreach (var f in features)
                {
                    this.treeView.Nodes.Add(new FeatureTreeNode(f));
                }
                treeView.SelectedNode = treeView.Nodes[0];

            }

        }

        private void addGroup(bool isSubGroup, FeatureTypeNumber? type)
        {
            Enova.Business.Old.Forms.FeatureEditForm form = new Enova.Business.Old.Forms.FeatureEditForm(isSubGroup);

            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrEmpty(form.Value))
            {
                EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;

                if (treeView.SelectedNode is FeatureTreeNode && ((FeatureTreeNode)treeView.SelectedNode).FeatureDef.ID == 0)
                {
                    if (dc.FeatureDefs.Any(f => f.Name == form.Value))
                    {
                        MessageBox.Show("Istnieje już grupa o takiej nazwie.", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FeatureTreeNode node = (FeatureTreeNode)treeView.SelectedNode;
                    FeatureDef featureDef = new FeatureDef(TableName, form.Value, type.Value);
                    ((Enova.Business.Old.Core.IContextSaveChanges)featureDef).SaveChanges(dc);

                    FeatureTreeNode newNode = new FeatureTreeNode(featureDef);
                    treeView.Nodes.Add(newNode);
                    treeView.SelectedNode = newNode;
                }
                else
                {
                    Dictionary parent = null;
                    FeatureDef featureDef = null;
                    TreeNode parentNode = null;
                    if (treeView.SelectedNode is FeatureTreeNode)
                    {
                        featureDef = ((FeatureTreeNode)treeView.SelectedNode).FeatureDef;
                        parentNode = treeView.SelectedNode;
                    }
                    else
                    {
                        featureDef = ((DictionaryTreeNode)treeView.SelectedNode).FeatureDef;
                        if (featureDef.IsTree)
                        {
                            parent = ((DictionaryTreeNode)treeView.SelectedNode).Dictionary;
                            parentNode = treeView.SelectedNode;
                        }
                        else
                        {
                            parentNode = treeView.SelectedNode;
                            if (parentNode.Parent != null)
                                parentNode = parentNode.Parent;
                        }
                    }

                    Dictionary dictionary = new Dictionary(parent, "F." + featureDef.Dictionary, form.Value);
                    ((Enova.Business.Old.Core.ISaveChanges)dictionary).SaveChanges();

                    DictionaryTreeNode newNode = new DictionaryTreeNode(dictionary, featureDef);
                    parentNode.Nodes.Add(newNode);
                    treeView.SelectedNode = newNode;
                }

            }
        }

        protected virtual void OnEnterSelect(EventArgs e)
        {
            if (EnterSelect != null)
                EnterSelect(this, e);
        }

        #endregion

        #region Events

        [Browsable(true),Category("Behavior")]
        public event TreeViewEventHandler AfterSelect
        {
            add
            {
                treeView.AfterSelect += value;
            }
            remove
            {
                treeView.AfterSelect -= value;
            }
        }

        [Browsable(true), Category("Behavior")]
        public event EventHandler EnterSelect;

        #endregion

        #region Events handlers

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is FeatureTreeNode && ((FeatureTreeNode)e.Node).FeatureDef.ID == 0)
            {
                newGroupButton.Visible = true;
                newSubgroupButton.Visible = false;
                deleteGroupButton.Enabled = false;
            }
            else
            {
                newGroupButton.Visible = false;
                newSubgroupButton.Visible = true;
                deleteGroupButton.Enabled = true;
            }
        }

        private void newGroupButton_ButtonClick(object sender, EventArgs e)
        {
            addGroup(false, FeatureTypeNumber.String);
        }

        private void jednopoziomowajednowartościowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addGroup(false, FeatureTypeNumber.String );
        }

        private void jednopoziomowawielowartościowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addGroup(false, FeatureTypeNumber.Array);
        }

        private void hierarchicznajednowartościowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addGroup(false, FeatureTypeNumber.Tree);
        }

        private void hierarhicznawielowartościowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addGroup(false, FeatureTypeNumber.ArrayOfTrees);
        }

        private void newSubgroupButton_Click(object sender, EventArgs e)
        {
            addGroup(true, null);
        }

        private void deleteGroupButton_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode is FeatureTreeNode)
            {
                FeatureTreeNode node = (FeatureTreeNode)treeView.SelectedNode;

                if (node.FeatureDef.ID != 0)
                {

                    DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć grupę", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        ((Enova.Business.Old.Core.IDeleteRecord)node.FeatureDef).DeleteRecord();
                    }
                    //LoadFeatures(Enova.Business.Core.ContextManager.DataContext);
                    if (node.Parent == null)
                        treeView.Nodes.Remove(node);
                    else
                        node.Parent.Nodes.Remove(node);
                    this.treeView.SelectedNode = treeView.Nodes[0];
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Chy napewno chcesz usunąć podgrupę", "EnovaTools", MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    DictionaryTreeNode node = (DictionaryTreeNode)treeView.SelectedNode;
                    TreeNode parent = node.Parent;
                    ((Enova.Business.Old.Core.IDeleteRecord)node.Dictionary).DeleteRecord();
                    //LoadFeatures(Enova.Business.Core.ContextManager.DataContext);
                    parent.Nodes.Remove(node);
                    treeView.SelectedNode = parent;
                }

            }
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.Enter)
            {
                TreeNode node = treeView.SelectedNode;
                if (!node.IsExpanded && node.Nodes.Count > 0)
                    node.Expand();
                else
                    OnEnterSelect(new EventArgs());
            }
        }

        #endregion

        #region Internal classes

        public class FeatureTreeNode : TreeNode
        {
            private FeatureDef featureDef;

            public FeatureTreeNode(FeatureDef featureDef)
                : base(featureDef.Name)
            {
                this.featureDef = featureDef;
                loadDictionary();
            }

            private void loadDictionary()
            {
                if (featureDef != null)
                {
                    var dictionarySet = featureDef.DictionarySet.OrderBy(d => d.Value).ToList();
                    foreach (var d in dictionarySet)
                    {
                        this.Nodes.Add(new DictionaryTreeNode(d, featureDef));
                    }
                }
            }

            public FeatureDef FeatureDef
            {
                get { return featureDef; }
            }

            public bool IsAll
            {
                get { return featureDef.ID == 0; }
            }
        }

        public class DictionaryTreeNode : TreeNode
        {
            private Dictionary dictionary;
            private FeatureDef featureDef;

            public DictionaryTreeNode(Dictionary dic, FeatureDef fd)
                : base(dic.Value)
            {
                this.dictionary = dic;
                this.featureDef = fd;

                if (fd.IsTree)
                    loadSubDictionary();

            }

            private void loadSubDictionary()
            {
                var sd = dictionary.SubDictionary.OrderBy(d => d.Value).ToList();
                foreach (var d in sd)
                {
                    this.Nodes.Add(new DictionaryTreeNode(d, featureDef));
                }
            }

            public Dictionary Dictionary
            {
                get { return dictionary; }
            }

            public FeatureDef FeatureDef
            {
                get { return featureDef; }
            }

            public string Value
            {
                get
                {
                    if (featureDef.IsTree)
                    {
                        return dictionary.Path;
                    }
                    return dictionary.Value;
                }
            }

        }


        #endregion

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void treeView_DragLeave(object sender, EventArgs e)
        {

        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {

        }

        private void treeView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void treeView_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

 





    }
}
