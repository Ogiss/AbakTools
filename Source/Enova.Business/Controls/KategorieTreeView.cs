using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Core;
using Enova.Business.Old.Types;
using Enova.Business.Old.DB.Web;

namespace Enova.Business.Old.Controls
{
    [DefaultEvent("AfterSelect")]
    [ToolboxItem(false)]
    public partial class KategorieTreeView : UserControl
    {
        #region Fields

        private WebContext dbContext;

        #endregion

        #region Properties

        [Browsable(false)]
        public KategoriaOld Kategoria
        {
            get
            {
                if (treeView.SelectedNode != null)
                    return ((KategoriaTreeNode)treeView.SelectedNode).Kategoria;
                return null;
            }
        }

        [Browsable(false)]
        public KategoriaTreeNode RootNode
        {
            get
            {
                return (KategoriaTreeNode)treeView.Nodes[0];
            }
        }


        [Browsable(false)]
        public TreeNode SelectedNode
        {
            get
            {
                return treeView.SelectedNode;
            }
            set
            {
                treeView.SelectedNode = value;
            }
        }

        private bool withEmtyRoot = false;
        [Browsable(true)]
        public bool WithEmptyRoot
        {
            get { return this.withEmtyRoot; }
            set { this.withEmtyRoot = value; }
        }

        public WebContext DbContext
        {
            get
            {
                if (this.dbContext == null && !this.DesignMode)
                    this.dbContext = Enova.Business.Old.Core.ContextManager.WebContext;
                return this.dbContext;
            }
        }

        #endregion

        public KategorieTreeView(WebContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
        }

        public KategorieTreeView()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                loadTree();
            }
        }

        private void loadTree()
        {
            //Kategoria root = Kategoria.Root;
            KategoriaOld root = KategoriaOld.GetRoot(this.DbContext);
            this.treeView.Nodes.Clear();
            if (withEmtyRoot)
                this.treeView.Nodes.Add(new KategoriaTreeNode(null));
            if (root != null)
            {
                if (withEmtyRoot)
                {
                    treeView.Nodes[0].Nodes.Add(new KategoriaTreeNode(root));
                    treeView.Nodes[0].Expand();
                    treeView.Nodes[0].Nodes[0].Expand();
                }
                else
                {
                    treeView.Nodes.Add(new KategoriaTreeNode(root));
                    treeView.Nodes[0].Expand();
                }
               
            }

        }

        public override void Refresh()
        {
            loadTree();
            base.Refresh();
        }

        public void Add()
        {
            Add(new KategoriaOld());
        }

        public void Add(KategoriaOld kategoria)
        {
            KategoriaTreeNode node = (KategoriaTreeNode)SelectedNode;
            if (node == null)
                node = RootNode;

            if (node != null)
            {
                kategoria.PoziomGlebokosci = (byte)(node.Kategoria.PoziomGlebokosci + 1);
                kategoria.Wlasciciel = node.Kategoria;
                KategoriaTreeNode newNode = new KategoriaTreeNode(kategoria);
                node.Nodes.Add(newNode);
                treeView.SelectedNode = newNode;
            }
        }

        public virtual void Remove()
        {
            KategoriaTreeNode node = (KategoriaTreeNode)SelectedNode;
            if (node != RootNode)
            {
                DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć kategorię?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    ((IDeleteRecord)node.Kategoria).DeleteRecord();
                    TreeNode parent = node.Parent;
                    parent.Nodes.Remove(node);
                    treeView.SelectedNode = parent;
                }
            }

        }

        public virtual void SelectKategorie(KategoriaOld kategoria)
        {
            if (kategoria != null)
            {
                TreeNode node = FindNodeByKategoria(kategoria);
                if (node != null)
                    treeView.SelectedNode = node;
            }
        }

        private TreeNode findNode(TreeNodeCollection nodes, KategoriaOld kategoria)
        {
            TreeNode node = null;
            if (kategoria != null)
            {
                foreach (TreeNode n in nodes)
                {
                    if (((KategoriaTreeNode)n).Kategoria.ID == kategoria.ID)
                    {
                        node = n;
                        break;
                    }
                    if (n.Nodes.Count > 0)
                    {
                        node = findNode(n.Nodes, kategoria);
                        if (node != null)
                            break;
                    }

                }
            }
            return node;
        }

        public virtual TreeNode FindNodeByKategoria(KategoriaOld kategoria)
        {
            return findNode(treeView.Nodes, kategoria);
        }

        #region Events

        [Browsable(true),Category("Behavior")]
        public event TreeViewEventHandler AfterSelect;

        [Browsable(true), Category("Behavior")]
        public event EventHandler EnterSelect;

        #endregion

        #region Events methods

        public virtual void OnAfterSelect(TreeViewEventArgs e)
        {
            if (AfterSelect != null)
                AfterSelect(this, e);
        }

        public virtual void OnEnterSelect(EventArgs e)
        {
            if (EnterSelect != null)
                EnterSelect(this, e);
        }

        #endregion

        #region Events Handlers

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnAfterSelect(e);
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

        #region Classes

        public class KategoriaTreeNode : TreeNode
        {
            #region Fields

            private KategoriaOld kategoria = null;

            #endregion

            #region Properties

            public KategoriaOld Kategoria
            {
                get { return kategoria; }
            }

            #endregion

            #region Constructors

            public KategoriaTreeNode(KategoriaOld kategoria)
                : base(kategoria != null ? kategoria.Nazwa : "Wszystkie")
            {
                this.kategoria = kategoria;
                if (kategoria != null)
                {
                    if (kategoria.Aktywna != null && !kategoria.Aktywna.Value)
                        this.ForeColor = Color.Red;

                    kategoria.PropertyChanged += new PropertyChangedEventHandler(kategoria_PropertyChanged);
                    loadChildren();
                }
            }

            #endregion

            #region Methosds

            private void kategoria_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Nazwa")
                {
                    this.Text = kategoria.Nazwa;
                }
            }

            private void loadChildren()
            {
                foreach (var k in kategoria.Podkategorie.Where(k => k.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && k.EnovaFeature == false )
                    .OrderBy(k => k.KolejnoscWyswietlania).ThenBy(r=>r.Nazwa))
                {
                    this.Nodes.Add(new KategoriaTreeNode(k));
                }
            }

            #endregion

        }

        #endregion

    }
}
