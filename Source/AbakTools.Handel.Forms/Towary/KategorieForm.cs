using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Controls;

[assembly: BAL.Forms.MenuAction("WebTools\\Kategorie", typeof(AbakTools.Towary.Forms.KategorieForm), MenuAction = BAL.Forms.MenuActionsType.OpenFormModal, Priority = 1020)]

namespace AbakTools.Towary.Forms
{


    public partial class KategorieForm : Form
    {

        private bool nazwaChanged = false;

        public KategorieForm()
        {
            InitializeComponent();
        }

        private void SaveChanges(Enova.Business.Old.Controls.KategorieTreeView.KategoriaTreeNode node)
        {
            Enova.Business.Old.DB.Web.KategoriaOld kategoria = node.Kategoria;
            if (kategoria != null)
            {
                foreach (var n in node.Nodes)
                {
                    SaveChanges((Enova.Business.Old.Controls.KategorieTreeView.KategoriaTreeNode)n);
                }
                ((ISaveChanges)kategoria).SaveChanges();
            }
        }

        private void SaveChanges()
        {
            SaveChanges(kategorieTreeView.RootNode);
        }

        private void UndoChanges(Enova.Business.Old.Controls.KategorieTreeView.KategoriaTreeNode node)
        {
            Enova.Business.Old.DB.Web.KategoriaOld kategoria = node.Kategoria;
            if (kategoria != null)
            {
                foreach (var n in node.Nodes)
                {
                    UndoChanges((Enova.Business.Old.Controls.KategorieTreeView.KategoriaTreeNode)n);
                }
                ((IUndoChanges)kategoria).UndoChanges();
            }
        }

        private void UndoChanges()
        {
            UndoChanges(kategorieTreeView.RootNode);
        }

        private void DeleteRecord(Enova.Business.Old.Controls.KategorieTreeView.KategoriaTreeNode node)
        {
            Enova.Business.Old.DB.Web.KategoriaOld kategoria = node.Kategoria;
            if (kategoria != null)
            {
                if (kategoria.PoziomGlebokosci == 0)
                {
                    MessageBox.Show("Nie można usunąć głównej kategori", "EnovaTools");
                    return;
                }

                DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć kategorię?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (var n in node.Nodes)
                    {
                        DeleteRecord((Enova.Business.Old.Controls.KategorieTreeView.KategoriaTreeNode)n);
                    }
                    kategoria.DoUsuniecia = true;
                }
            }
        }

        private void kategorieTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            kategoriaBindingSource.DataSource = kategorieTreeView.Kategoria;
            nazwaChanged = false;
        }

        private void zatwierdzbbutton_Click(object sender, EventArgs e)
        {
            SaveChanges();
            kategorieTreeView.Refresh();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
            this.Close();
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            UndoChanges();
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            kategorieTreeView.Add();
            nazwaTextBox.Focus();
        }

        private void nazwaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
                opisTextBox.Focus();
        }

        private void opisTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
                przyjaznayLinkTextBox.Focus();
        }

        private void przyjaznayLinkTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
                metaTytulTextBox.Focus();
        }

        private void metaTytulTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
                metaOpisTextBox.Focus();
        }

        private void metaOpisTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
                metaSłowaTextBox.Focus();
        }

        private void metaSłowaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
                kolejnoscTextBox.Focus();
        }

        private void kolejnoscTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
                aktywnaTheckBox.Focus();
        }

        private void aktywnaCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
                addButton.Focus();
        }

        private void KategorieForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Insert)
                addButton_Click(null, null);
            else if (e.Control == true && e.KeyCode == Keys.Enter)
                okButton_Click(null, null);
        }

        private void nazwaTextBox_Leave(object sender, EventArgs e)
        {
            /*
            if (nazwaChanged)
            {
                //przyjaznayLinkTextBox.Text = Enova.Business.Core.Tools.LinkRewrite(nazwaTextBox.Text);
                ((Kategoria)kategoriaBindingSource.DataSource).PrzyjaznyLink = Enova.Business.Core.Tools.LinkRewrite(nazwaTextBox.Text);

            }
            */
        }

        private void nazwaTextBox_TextChanged(object sender, EventArgs e)
        {
            nazwaChanged = true;
        }

        private void przyjaznayLinkTextBox_Enter(object sender, EventArgs e)
        {
        }

        private void delButton_Click(object sender, EventArgs e)
        {

        }

        private void aktywnaTheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (aktywnaTheckBox.Checked)
            {
                kategorieTreeView.SelectedNode.ForeColor = Color.Black;
            }
            else
            {
                kategorieTreeView.SelectedNode.ForeColor = Color.Red;
            }
        }


    }
}
