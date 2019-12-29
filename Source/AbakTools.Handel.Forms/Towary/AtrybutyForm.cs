using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Local=Enova.Business.Old.DB.Web;
using System.IO;
using Enova.Business.Old.Types;

[assembly: BAL.Forms.MenuAction("WebTools\\Atrybuty", typeof(AbakTools.Towary.Forms.AtrybutyForm), MenuAction = BAL.Forms.MenuActionsType.OpenFormModal, Priority = 1030)]

namespace AbakTools.Towary.Forms
{
    public partial class AtrybutyForm : Form
    {
        List<Enova.Business.Old.DB.Web.GrupaAtrybutow> grupyAtrybutow = null;
        List<AtrybutyTreeNode> deletedNodes = new List<AtrybutyTreeNode>();
        List<AtrybutyTreeNode> addedNodes = new List<AtrybutyTreeNode>();
        System.Drawing.Color buttonColor;
        const string TEXTURE_DIR = @"Z:\AbakSoft\EnovaTools\img\co\";

        public AtrybutyForm()
        {
            InitializeComponent();
        }

        private void AtrybutyForm_Load(object sender, EventArgs e)
        {
            buttonColor = kolorButton.BackColor;
            loadAtrybuty();
        }

        private void loadAtrybuty()
        {

            treeView.Nodes.Clear();
            grupyAtrybutow = Enova.Business.Old.Core.ContextManager.WebContext.GrupyAtrybutow.Include("Atrybuty")
                .Where(a=>a.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).OrderBy(g => g.Nazwa).ToList();
            //Enova.Business.Old.Core.ContextManager.WebContext.Refresh(RefreshMode.StoreWins, grupyAtrybutow);
            foreach (var grupa in grupyAtrybutow)
            {
                AtrybutyTreeNode grupaNode = new AtrybutyTreeNode(grupa);
                foreach (var atrybut in grupa.Atrybuty.Where(a=>a.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).OrderBy(a=>a.Kolejnosc).ThenBy(a => a.Nazwa))
                {
                    AtrybutyTreeNode atrybutNode = new AtrybutyTreeNode(atrybut);
                    grupaNode.Nodes.Add(atrybutNode);
                }
                treeView.Nodes.Add(grupaNode);
            }
        }

        private Enova.Business.Old.DB.Web.GrupaAtrybutow WybranaGrupaAtrybutow
        {
            get
            {
                if (treeView.SelectedNode != null)
                {
                    return ((AtrybutyTreeNode)treeView.SelectedNode).GrupaAtrybutow;
                }
                return null;
            }
        }

        private Enova.Business.Old.DB.Web.Atrybut WybranyAtrybut
        {
            get
            {
                if (treeView.SelectedNode != null)
                {
                    return ((AtrybutyTreeNode)treeView.SelectedNode).Atrybut;
                }
                return null;
            }
        }


        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            grupaAtrybutowBindingSource.DataSource = WybranaGrupaAtrybutow;
            if (WybranyAtrybut != null)
            {
                atrybutBindingSource.DataSource = WybranyAtrybut;
                atrybutGroupBox.Enabled = true;
                string texturePath = null;

                kolorButton.BackColor = buttonColor;
                texturePictureBox.Image = null;
                usunTekstureButton.Visible = false;

                if (WybranyAtrybut.SynchronizacjaTekstury != (int)Enova.Business.Old.Types.SynchronizeImage.Delete)
                {
                    if (File.Exists(TEXTURE_DIR + WybranyAtrybut.ID.ToString() + ".jpg"))
                        texturePath = TEXTURE_DIR + WybranyAtrybut.ID.ToString() + ".jpg";
                    else if (File.Exists(TEXTURE_DIR + "PS" + WybranyAtrybut.PSAtrybutID.ToString() + ".jpg"))
                        texturePath = TEXTURE_DIR + "PS" + WybranyAtrybut.PSAtrybutID.ToString() + ".jpg";
                }

                if (!string.IsNullOrEmpty(texturePath))
                {
                    Image texture = Bitmap.FromFile(texturePath);
                    texturePictureBox.Image = (Image)texture.Clone();
                    texture.Dispose();
                    usunTekstureButton.Visible = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(WybranyAtrybut.Kolor))
                    {
                        Color color = System.Drawing.ColorTranslator.FromHtml(WybranyAtrybut.Kolor);
                        kolorButton.BackColor = color;
                    }
                }
            }
            else
            {
                atrybutBindingSource.Clear();
                atrybutGroupBox.Enabled = false;
                kolorButton.BackColor = buttonColor;
                texturePictureBox.Image = null;
            }
        }

        public class AtrybutyTreeNode : TreeNode
        {
            private bool isGroup = false;
            Enova.Business.Old.DB.Web.GrupaAtrybutow grupa = null;
            Enova.Business.Old.DB.Web.Atrybut atrybut = null;

            public Enova.Business.Old.DB.Web.GrupaAtrybutow GrupaAtrybutow
            {
                get { return this.grupa; }
            }

            public Enova.Business.Old.DB.Web.Atrybut Atrybut
            {
                get { return atrybut; }
            }

            public bool IsGroup
            {
                get { return this.isGroup; }
            }

            public AtrybutyTreeNode(Enova.Business.Old.DB.Web.GrupaAtrybutow grupa)
                : base(grupa.Nazwa)
            {
                this.grupa = grupa;
                isGroup = true;
            }

            public AtrybutyTreeNode(Enova.Business.Old.DB.Web.Atrybut atrybut)
                : base(atrybut.Nazwa)
            {
                this.grupa = atrybut.GrupaAtrybutow;
                this.atrybut = atrybut;
                isGroup = false;
            }
        }

        private void kolorButton_Click(object sender, EventArgs e)
        {
            Color color = System.Drawing.Color.Black;
            if (!string.IsNullOrEmpty(WybranyAtrybut.Kolor))
                color = System.Drawing.ColorTranslator.FromHtml(WybranyAtrybut.Kolor);
            colorDialog.Color = color;
            DialogResult result = colorDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                WybranyAtrybut.Kolor = System.Drawing.ColorTranslator.ToHtml(colorDialog.Color);
                kolorButton.BackColor = colorDialog.Color;
            }
        }

        private void usunButton_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć " + (((AtrybutyTreeNode)treeView.SelectedNode).IsGroup ? "grupę" : "atrybut"), "EnovaTools",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    deletedNodes.Add((AtrybutyTreeNode)treeView.SelectedNode);
                    if (treeView.SelectedNode.Parent != null)
                        treeView.SelectedNode.Parent.Nodes.Remove(treeView.SelectedNode);
                    else
                        treeView.Nodes.Remove(treeView.SelectedNode);
                }
            }
        }

        private void nowaGrupaButton_Click(object sender, EventArgs e)
        {
            Enova.Business.Old.DB.Web.GrupaAtrybutow grupa = new Enova.Business.Old.DB.Web.GrupaAtrybutow()
            {
                GUID = Guid.NewGuid(),
                GrupaKolorow = false,
                Nazwa = string.Empty,
                NazwaPubliczna = string.Empty,
                PSID = 0,
                PSLangID = 3,
                Stamp = DateTime.Now,
                Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew
            };
            Enova.Business.Old.Core.ContextManager.WebContext.AddToGrupyAtrybutow(grupa);

            AtrybutyTreeNode node = new AtrybutyTreeNode(grupa);
            treeView.Nodes.Add(node);
            addedNodes.Add(node);

            treeView.SelectedNode = node;
            nazwaTextBox.Focus();
        }

        private void nazwaTextBox_TextChanged(object sender, EventArgs e)
        {
            AtrybutyTreeNode node = (AtrybutyTreeNode)treeView.SelectedNode;
            if (!node.IsGroup)
                node = (AtrybutyTreeNode)node.Parent;
            node.Text = nazwaTextBox.Text;
        }

        private void nazwaAtrybutuTextBox_TextChanged(object sender, EventArgs e)
        {
            AtrybutyTreeNode node = (AtrybutyTreeNode)treeView.SelectedNode;
            if (!node.IsGroup)
                node.Text = nazwaAtrybutuTextBox.Text;
        }

        private void nowyAtrybytButton_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                AtrybutyTreeNode groupNode = (AtrybutyTreeNode)treeView.SelectedNode;
                if (!groupNode.IsGroup)
                    groupNode = (AtrybutyTreeNode)groupNode.Parent;

                Enova.Business.Old.DB.Web.Atrybut atrybut = new Enova.Business.Old.DB.Web.Atrybut()
                {
                    GUID = Guid.NewGuid(),
                    GrupaAtrybutow = groupNode.GrupaAtrybutow,
                    Kolejnosc = 1000,
                    Kolor = string.Empty,
                    Nazwa = string.Empty,
                    PSAtrybutID = 0,
                    PSGrupaID = 0,
                    Stamp = DateTime.Now,
                    Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew,
                    SynchronizacjaTekstury = (int)Enova.Business.Old.Types.SynchronizeImage.None
                };
                Enova.Business.Old.Core.ContextManager.WebContext.AddToAtrybuty(atrybut);

                AtrybutyTreeNode node = new AtrybutyTreeNode(atrybut);
                groupNode.Nodes.Add(node);
                addedNodes.Add(node);
                treeView.SelectedNode = node;
                nazwaAtrybutuTextBox.Focus();
            }
        }


        private void SaveChanges()
        {
            DateTime stamp = DateTime.Now;
            foreach (AtrybutyTreeNode gnode in treeView.Nodes)
            {
                foreach (AtrybutyTreeNode anode in gnode.Nodes)
                {
                    Enova.Business.Old.DB.Web.Atrybut atrybut = anode.Atrybut;
                    if (atrybut.EntityState == System.Data.EntityState.Modified)
                    {
                        if (atrybut.Synchronizacja == (int)RowSynchronizeOld.Synchronized)
                        {
                            atrybut.Stamp = stamp;
                            atrybut.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                        }

                    }
                }
                Enova.Business.Old.DB.Web.GrupaAtrybutow grupa = gnode.GrupaAtrybutow;
                if (grupa.EntityState == System.Data.EntityState.Modified)
                {
                    if (grupa.Synchronizacja == (int)RowSynchronizeOld.Synchronized)
                    {
                        grupa.Stamp = stamp;
                        grupa.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                    }

                }
            }

            foreach (AtrybutyTreeNode node in deletedNodes)
            {
                if (node.IsGroup)
                {
                    node.GrupaAtrybutow.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                    node.GrupaAtrybutow.Stamp = stamp;
                    Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
                    Enova.Business.Old.Core.ContextManager.WebContext.Detach(node.GrupaAtrybutow);

                    foreach (AtrybutyTreeNode anode in node.Nodes)
                    {
                        anode.Atrybut.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                        anode.Atrybut.Stamp = stamp;
                        Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
                        Enova.Business.Old.Core.ContextManager.WebContext.Detach(anode.Atrybut);

                    }
                }
                else
                {
                    node.Atrybut.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                    node.Atrybut.Stamp = stamp;
                    Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
                    Enova.Business.Old.Core.ContextManager.WebContext.Detach(node.Atrybut);

                }

            }

            Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
        }

        private void UndoChanges()
        {
            Enova.Business.Old.DB.Web.WebContext lc = Enova.Business.Old.Core.ContextManager.WebContext;
            foreach (AtrybutyTreeNode gnode in treeView.Nodes)
            {
                foreach (AtrybutyTreeNode anode in gnode.Nodes)
                {
                    if (anode.Atrybut.EntityState == System.Data.EntityState.Added)
                    {
                        lc.DeleteObject(anode.Atrybut);
                    }
                    else if (anode.Atrybut.EntityState == System.Data.EntityState.Modified)
                    {
                        lc.Refresh(System.Data.Objects.RefreshMode.StoreWins, anode.Atrybut);
                    }
                }
                if (gnode.GrupaAtrybutow.EntityState == System.Data.EntityState.Added)
                {
                    lc.DeleteObject(gnode.GrupaAtrybutow);
                }
                else if (gnode.GrupaAtrybutow.EntityState == System.Data.EntityState.Modified)
                {
                    lc.Refresh(System.Data.Objects.RefreshMode.StoreWins, gnode.GrupaAtrybutow);
                }
            }
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            UndoChanges();
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
            this.Close();
        }

        private void AtrybutyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Shift && e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        grupaAtrybutowBindingSource.EndEdit();
                        atrybutBindingSource.EndEdit();
                        nowyAtrybytButton_Click(null, null);
                        e.Handled = true;
                        break;
                }
            }

        }

        private void AtrybutyForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void copyGrupaAttrGuidButton_Click(object sender, EventArgs e)
        {
            if(grupaAtrybutowBindingSource.Current != null){
                Clipboard.SetText(((Local.GrupaAtrybutow)grupaAtrybutowBindingSource.Current).GUID.ToString());
            }
        }

        private void copyAttrGuidButton_Click(object sender, EventArgs e)
        {
            if (atrybutBindingSource.Current != null)
                Clipboard.SetText(((Local.Atrybut)atrybutBindingSource.Current).GUID.ToString());
        }

    }
}
