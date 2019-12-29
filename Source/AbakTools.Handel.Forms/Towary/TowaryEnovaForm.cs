using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Towary.Forms
{
    public partial class TowaryEnovaForm : Enova.Business.Old.Forms.DataGridForm
    {
        private bool kategorieIsLoaded = false;
        private bool towaryIsLoaded = false;
        private bool fireDataGridSelectionChanged = true;
        private Enova.Business.Old.DB.Web.KategoriaOld selectedCategory = null;
        private static Guid EnovaKatGuid = new Guid("00000000-0001-0001-0001-000000000000");

        public TowaryEnovaForm()
        {
            InitializeComponent();
        }

        protected override void LoadData()
        {
            if (!towaryIsLoaded)
            {
                var t = new Enova.Business.Old.Web.TowaryEnova(Enova.Business.Old.Core.ContextManager.WebContext);
                KategoriaTreeNode node = (KategoriaTreeNode)kategorieTreeView.SelectedNode;
                if (node != null && node.Kategoria.GUID != EnovaKatGuid)
                    t.Kategoria = node.Kategoria;
                //this.DataSource = new Enova.Business.Old.Web.TowaryEnova(node.Kategoria);
                else
                    t.Kategoria = null;
                    //this.DataSource = new Enova.Business.Old.Web.TowaryEnova(Enova.Business.Old.Core.ContextManager.WebContext);
                this.DataSource = t;
                this.DataGrid.Sort(DataGrid.Columns[0], ListSortDirection.Ascending);
                towaryIsLoaded = true;
            }
        }

        private void TowaryEnovaForm_Load(object sender, EventArgs e)
        {
            if (!kategorieIsLoaded)
            {
                loadKategorie();
                kategorieIsLoaded = true;
            }
            kategorieTreeView.Nodes[0].Expand();
            if (selectedCategory != null)
                selectCategory((KategoriaTreeNode)kategorieTreeView.Nodes[0], selectedCategory);
        }

        private bool selectCategory(KategoriaTreeNode node, Enova.Business.Old.DB.Web.KategoriaOld kategoria)
        {
            if (node.Kategoria.ID == kategoria.ID)
            {
                kategorieTreeView.SelectedNode = node;
                return true;
            }

            foreach (var child in node.Nodes)
            {
                if (selectCategory((KategoriaTreeNode)child, kategoria))
                    return true;
            }
            return false;
        }


        static List<Enova.Business.Old.DB.Web.KategoriaOld> kategorie = null;

        private void loadKategorie()
        {
            this.kategorieTreeView.Nodes.Clear();
            var dc = Enova.Business.Old.Core.ContextManager.WebContext;
            var guid = new Guid("00000000-0001-0001-0001-000000000000");

            if(kategorie == null)
                kategorie = dc.KategorieOld.Where(k => k.EnovaFeature == true).ToList();

            var root = kategorie.Where(k => k.GUID == guid).FirstOrDefault();
            kategorieTreeView.Nodes.Add(new KategoriaTreeNode(root, "Wszystko"));
            kategorie = null;

        }

        private void kategorieTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
            {
                towaryIsLoaded = false;
                Reload();
            }
        }


        public class KategoriaTreeNode : TreeNode
        {
            private Enova.Business.Old.DB.Web.KategoriaOld kategoria;

            public Enova.Business.Old.DB.Web.KategoriaOld Kategoria
            {
                get { return this.kategoria; }
            }

            public KategoriaTreeNode(Enova.Business.Old.DB.Web.KategoriaOld kategoria, string name)
                : base(name)
            {
                this.kategoria = kategoria;
                loadChildren();
            }

            public KategoriaTreeNode(Enova.Business.Old.DB.Web.KategoriaOld kategoria)
                : this(kategoria, kategoria.Nazwa) { }

            private void loadChildren()
            {
                foreach (var child in kategorie.Where(k=>k.WlascicielID == kategoria.ID).OrderBy(k=>k.Nazwa) )
                    this.Nodes.Add(new KategoriaTreeNode(child));
            }
        }

        private void TowaryEnovaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kategorieTreeView.SelectedNode != null)
                selectedCategory = ((KategoriaTreeNode)kategorieTreeView.SelectedNode).Kategoria;
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Tab)
                {
                    kategorieTreeView.Focus();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Back)
                {
                    if (!string.IsNullOrEmpty(szukajTextBox.Text))
                    {
                        szukajTextBox.Text = szukajTextBox.Text.Substring(0, szukajTextBox.Text.Length - 1);
                        szukajTextBox.SelectionStart = szukajTextBox.Text.Length;
                    }
                    e.Handled = true;
                }
            }
        }

        private void kategorieTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Tab)
                {
                    DataGrid.Focus();
                    e.Handled = true;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (kategorieTreeView.SelectedNode != null)
                    {
                        if (kategorieTreeView.SelectedNode.Nodes.Count == 0)
                        {
                            DataGrid.Focus();
                        }
                        else
                        {
                            if (!kategorieTreeView.SelectedNode.IsExpanded)
                                kategorieTreeView.SelectedNode.Expand();
                            else
                                kategorieTreeView.SelectedNode.Collapse();
                        }

                        e.Handled = true;
                    }
                }
            }
        }

        private void DataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Enova.Business.Old.Core.Tools.IsPrintableChar(e.KeyChar))
            {
                szukajTextBox.Text += e.KeyChar.ToString();
                szukajTextBox.SelectionStart = szukajTextBox.Text.Length;
            }
        }

        private void szukajTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(szukajTextBox.Text))
            {
                fireDataGridSelectionChanged = false;
                var idx = DataGridBindingSource.Find((System.ComponentModel.PropertyDescriptor) null, szukajTextBox.Text);
                if (idx > -1)
                {
                    DataGrid.CurrentCell = DataGrid.Rows[idx].Cells[0];
                }
                else
                {
                    szukajTextBox.Text = szukajTextBox.Text.Substring(0, szukajTextBox.Text.Length - 1);
                    szukajTextBox.SelectionStart = szukajTextBox.Text.Length;
                }
                fireDataGridSelectionChanged = true;
            }
        }

        private void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (fireDataGridSelectionChanged)
            {
                if (!string.IsNullOrEmpty(szukajTextBox.Text))
                    szukajTextBox.Text = string.Empty;
            }
        }

        private void TowaryEnovaForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                    e.Handled = true;
                }
            }
        }

    }

}
