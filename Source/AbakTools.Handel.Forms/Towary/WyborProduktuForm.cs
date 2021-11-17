using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Towary.Forms
{
    public partial class WyborProduktuForm : Enova.Business.Old.Forms.DataBaseForm, Enova.Business.Old.Controls.ISelectForm
    {
        private bool fireWebSelectionChanged = true;
        private bool fireEnovaSelectionChanged = true;

        Enova.Business.Old.DB.Web.KategoriaOld kategorieSelected = null;
        //DataGridViewColumn webSortedColumn = null;
        DataGridViewColumn enovaSortedColumn = null;
        SortOrder webSortOrder = SortOrder.Ascending;
        SortOrder enovaSortOrder = SortOrder.None;
        //Enova.Business.Old.Web.ProduktySortOrder webSortType = Enova.Business.Old.Web.ProduktySortOrder.Kod;
        Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType webSortType = Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType.Kod;

        private bool enovaProduktyIsLoaded = false;

        public WyborProduktuForm()
        {
            InitializeComponent();
            produktyWebDataGridView.AutoGenerateColumns = false;
        }

        #region ISelectForm Implementation

        private object selectedItem = null;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
            }
        }

        private bool selectMode = true;
        public bool SelectMode
        {
            get { return selectMode; }
            set { selectMode = value; }
        }

        public bool HideOnSelect
        {
            get { return false; }
        }

        #endregion

        #region Methods

        private void loadData()
        {
            //produktyBindingSource.DataSource = new Enova.Business.Old.Web.ProduktyAtrybuty(kategorieTreeView.Kategoria, webSortType, webSortOrder);
            var dc = new Enova.Business.Old.Web.TowaryAtrybutyTable(Enova.Business.Old.Core.ContextManager.WebContext, true, null, null, null, kategorieTreeView.Kategoria);
            dc.SortType = this.webSortType;
            dc.SortOrder = this.webSortOrder;
            produktyBindingSource.DataSource = dc;
            setWebSortedColumn();
        }

        private void setWebSortedColumn()
        {
            kodWebColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            nazwaWebColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            atrybutWebColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            cenaWebColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            if (webSortOrder != SortOrder.None)
            {
                switch (webSortType)
                {
                    case Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType.Kod:
                        kodWebColumn.HeaderCell.SortGlyphDirection = webSortOrder;
                        atrybutWebColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        break;
                    case Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType.Nazwa:
                        nazwaWebColumn.HeaderCell.SortGlyphDirection = webSortOrder;
                        atrybutWebColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        break;
                    case Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType.Atrybut:
                        atrybutWebColumn.HeaderCell.SortGlyphDirection = webSortOrder;
                        break;
                    case Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType.Cena:
                        cenaWebColumn.HeaderCell.SortGlyphDirection = webSortOrder;
                        break;
                }
            }
        }


        private void loadEnovaProdukty()
        {
            /*
            DataGridViewColumn sortedColumn = produktyEnovaDataGridView.SortedColumn;
            SortOrder sortOrder = produktyEnovaDataGridView.SortOrder;
             */

            if (enovaSortedColumn == null)
            {
                enovaSortedColumn = kodEnovaDataGridViewTextBoxColumn;
                enovaSortOrder = SortOrder.Ascending;
            }

            if (enovaSortOrder == SortOrder.None)
                enovaSortOrder = SortOrder.Ascending;

            produktyEnovaBindingSource.DataSource = new Enova.Business.Old.Towary();

            produktyEnovaDataGridView.Sort(enovaSortedColumn, enovaSortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }

        #endregion


        #region Event Handlers

        private void WyborProduktuForm_Load(object sender, EventArgs e)
        {
            loadData();
            //produktyWebDataGridView.Sort(produktyWebDataGridView.Columns[0], ListSortDirection.Ascending);
        }

        #endregion

        private void produktyWebDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectWebProdukt();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (findTextBox.Text.Length > 0)
                {
                    findTextBox.Text = findTextBox.Text.Substring(0, findTextBox.Text.Length - 1);
                    findTextBox.SelectionStart = findTextBox.Text.Length;
                }
            }
            else if (e.Control == true && e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
            {
            }
        }

        private void produktyEnovaDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectEnovaProdukt();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (findEnovaTextBox.Text.Length > 0)
                {
                    findEnovaTextBox.Text = findEnovaTextBox.Text.Substring(0, findEnovaTextBox.Text.Length - 1);
                    findEnovaTextBox.SelectionStart = findEnovaTextBox.Text.Length;
                }
            }

        }

        private void selectWebProdukt()
        {
            if (produktyWebDataGridView.CurrentRow != null && produktyWebDataGridView.CurrentRow.DataBoundItem != null)
            {
                this.selectedItem = produktyWebDataGridView.CurrentRow.DataBoundItem;
                if (this.selectedItem != null)
                {
                    /*if (((Enova.Business.Old.DB.Web.ProduktAtrybut)this.selectedItem).Dostepny == false)*/
                    if (((Enova.Business.Old.DB.Web.TowarAtrybut)this.selectedItem).Dostepny == false)
                    {
                        DialogResult result = MessageBox.Show("Wybrany towar chwilowo jest niedostepny. Czy mimo to chcesz go dodać do zamówienia?", "EnovaTools",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (result == System.Windows.Forms.DialogResult.No)
                            return;
                    }
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.Cancel;
                //this.Close();
                this.Hide();
            }
        }

        private void selectEnovaProdukt()
        {
            if (produktyEnovaDataGridView.CurrentRow != null)
            {
                Enova.Business.Old.DB.TowarRow towarRow = (Enova.Business.Old.DB.TowarRow)produktyEnovaDataGridView.CurrentRow.DataBoundItem;
                if (towarRow != null)
                {
                    Enova.Business.Old.DB.Towar towar = (Enova.Business.Old.DB.Towar)towarRow;
                    Enova.Business.Old.DB.Web.Produkt produkt = (Enova.Business.Old.DB.Web.Produkt)towar;
                    if (produkt == null)
                    {
                        DateTime stamp = DateTime.Now;
                        if (!towar.DefinicjaStawkiReference.IsLoaded)
                            towar.DefinicjaStawkiReference.Load();
                        Enova.Business.Old.DB.Web.StawkaVat stawkaVat = (Enova.Business.Old.DB.Web.StawkaVat)towar.DefinicjaStawki;
                        if (stawkaVat != null)
                        {
                            produkt = new Enova.Business.Old.DB.Web.Produkt()
                            {
                                EnovaGuid = towar.Guid,
                                GUID = Guid.NewGuid(),
                                Kod = towar.Kod,
                                Nazwa = towar.Nazwa,
                                Aktywny = true,
                                Cena = (decimal)towar.CenaHurtowaNetto,
                                DataAktualizacji = stamp,
                                DataDodania = stamp,
                                Gotowy = false,
                                Indexed = true,
                                KrotkiOpis = "",
                                LangID = 3,
                                LinkRewrite = Enova.Business.Old.Core.Tools.LinkRewrite(towar.Nazwa),
                                MetaOpis = "",
                                MetaTytul = "",
                                Opis = "",
                                Podprodukt = false,
                                ProduktGrupujacy = false,
                                PSID = 0,
                                Stamp = stamp,
                                Stan = 0,
                                Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew,
                                Widoczny = true,
                                WlascicielID = 0,
                                StawkaVat = stawkaVat,
                                TowarEnova = true,
                                EnovaStamp = towar.Stamp,
                                Dostepny = true
                            };

                            Enova.Business.Old.Core.ContextManager.WebContext.AddToProdukty(produkt);

                            var grupy = Enova.Business.Old.DB.FeatureDef.GrupyRabatowe;
                            var wc = Enova.Business.Old.Core.ContextManager.WebContext;
                            foreach (var gr in grupy)
                            {
                                var features = towar.Features.Where(f => f.Name == gr.Name).ToList();
                                foreach (var feature in features)
                                {
                                    var dictionary = gr.DictionarySet.Where(d => d.Value == feature.Data).FirstOrDefault();
                                    if (dictionary != null)
                                    {
                                        var grupaRabatowa = wc.GrupyRabatowe.Where(r => r.GUID == dictionary.Guid).FirstOrDefault();
                                        if (grupaRabatowa != null)
                                        {
                                            var towarGrupa = wc.TowarGrupyRabatowe.Where(tgr => tgr.TowarID == produkt.ID && tgr.GrupaRabatowaID == grupaRabatowa.ID).FirstOrDefault();
                                            if (towarGrupa == null)
                                            {
                                                towarGrupa = new Enova.Business.Old.DB.Web.TowarGrupaRabatowa()
                                                {
                                                    Towar = produkt,
                                                    GrupaRabatowa = grupaRabatowa
                                                };
                                                wc.AddToTowarGrupyRabatowe(towarGrupa);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    this.selectedItem = produkt;
                    if (this.selectedItem != null)
                        this.DialogResult = DialogResult.OK;
                    else
                        this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }

            }
        }


        private void produktyWebDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Enova.Business.Old.Core.Tools.IsPrintableChar(e.KeyChar))
            {
                findTextBox.Text += e.KeyChar.ToString();
                findTextBox.SelectionStart = findTextBox.Text.Length;
            }
        }

        private void produktyEnovaDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Enova.Business.Old.Core.Tools.IsPrintableChar(e.KeyChar))
            {
                findEnovaTextBox.Text += e.KeyChar.ToString();
                findEnovaTextBox.SelectionStart = findEnovaTextBox.Text.Length;
            }

        }


        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(findTextBox.Text))
            {
                fireWebSelectionChanged = false;
                int idx = produktyBindingSource.Find("TowarKod", findTextBox.Text);
                if (idx != -1)
                {
                    produktyWebDataGridView.CurrentCell = produktyWebDataGridView.Rows[idx].Cells[0];
                }
                else
                {
                    findTextBox.Text = findTextBox.Text.Substring(0, findTextBox.Text.Length - 1);
                    findTextBox.SelectionStart = findTextBox.Text.Length;
                }
                fireWebSelectionChanged = true;
            }

        }

        private void findEnovaTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(findEnovaTextBox.Text))
            {
                fireEnovaSelectionChanged = false;
                int idx = produktyEnovaBindingSource.Find("Kod", findEnovaTextBox.Text);
                if (idx != -1)
                {
                    produktyEnovaDataGridView.CurrentCell = produktyEnovaDataGridView.Rows[idx].Cells[0];
                }
                else
                {
                    findEnovaTextBox.Text = findEnovaTextBox.Text.Substring(0, findEnovaTextBox.Text.Length - 1);
                    findEnovaTextBox.SelectionStart = findEnovaTextBox.Text.Length;
                }
                fireEnovaSelectionChanged = true;
            }

        }


        private void WyborProduktuForm_Shown(object sender, EventArgs e)
        {
            produktyWebDataGridView.Focus();
        }

        private void WyborProduktuForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void WyborProduktuForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == false && e.Control == false && e.Shift == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.DialogResult = DialogResult.Cancel;
                        this.selectedItem = null;
                        this.Close();
                        break;
                    case Keys.Tab:
                        if (tabControl.SelectedIndex == 0)
                        {
                            if (kategorieTreeView.Focused)
                            {
                                produktyWebDataGridView.Focus();
                            }
                            else
                            {
                                kategorieTreeView.Focus();
                            }
                        }
                        else
                        {
                            if (featuresTree.Focused)
                            {
                                produktyEnovaDataGridView.Focus();
                            }
                            else
                            {
                                featuresTree.Focus();
                            }
                        }
                        e.Handled = true;
                        break;
                }

            }
            else if (e.Alt == false && e.Control == true && e.Shift == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.E:
                        if (tabControl.SelectedTab.Name != "enovaTabPage")
                            tabControl.SelectTab(1);
                        break;
                    case Keys.W:
                        if (tabControl.SelectedTab.Name == "enovaTabPage")
                            tabControl.SelectTab(0);
                        break;
                    case Keys.P:
                        if (tabControl.SelectedIndex == 0)
                        {
                            podgladCheckBox.Checked = !podgladCheckBox.Checked;
                        }
                        break;
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Name == "enovaTabPage")
            {
                if (previewForm != null && previewForm.Visible)
                    setPreviewVisible(false);
                if (!enovaProduktyIsLoaded)
                {
                    featuresTree.LoadFeatures();
                    loadEnovaProdukty();
                    enovaProduktyIsLoaded = true;
                }
                this.Focus();
                produktyEnovaDataGridView.Focus();
            }
            else
            {
                if (previewForm != null && !previewForm.Visible && podgladCheckBox.Checked)
                    setPreviewVisible(true);
                produktyWebDataGridView.Focus();
            }
        }

        private void featuresTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (produktyEnovaBindingSource.DataSource is Enova.Business.Old.Core.IFeatures)
            {
                if (e.Node is Enova.Business.Old.Controls.FeaturesTree.FeatureTreeNode)
                {
                    Enova.Business.Old.Controls.FeaturesTree.FeatureTreeNode node = (Enova.Business.Old.Controls.FeaturesTree.FeatureTreeNode)e.Node;
                    if (node.IsAll)
                        ((Enova.Business.Old.Core.IFeatures)produktyEnovaBindingSource.DataSource).RemoveFeatureFilter();
                }
                else
                {
                    Enova.Business.Old.Controls.FeaturesTree.DictionaryTreeNode node = (Enova.Business.Old.Controls.FeaturesTree.DictionaryTreeNode)e.Node;
                    ((Enova.Business.Old.Core.IFeatures)produktyEnovaBindingSource.DataSource).ApplyFeatureFilter(node.FeatureDef, node.Value);
                }
            }

        }

        private void WyborProduktuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            e.Cancel = true;
            this.Hide();
             */
        }

        private void kategorieTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            loadData();
        }

        private void produktyWebDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (fireWebSelectionChanged)
                findTextBox.Text = "";
            if (podgladCheckBox.Checked && previewForm != null)
            {
                //previewForm.Product = (Enova.Business.Old.DB.Web.Produkt)produktyWebDataGridView.CurrentRow.DataBoundItem;
                setPreviewProduct();
            }
        }

        private void setPreviewProduct()
        {
            if (previewForm.InvokeRequired)
            {
                var d = new MethodInvoker(setPreviewProduct);
                previewForm.Invoke(d);
            }
            else
            {
                if (produktyWebDataGridView.CurrentRow != null && produktyWebDataGridView.CurrentRow.DataBoundItem != null)
                {
                    //previewForm.Product = (Enova.Business.Old.DB.Web.ProduktAtrybut)produktyWebDataGridView.CurrentRow.DataBoundItem;
                    previewForm.TowarAtrybut = (Enova.Business.Old.DB.Web.TowarAtrybut)produktyWebDataGridView.CurrentRow.DataBoundItem;
                }
            }
        }

        private void produktyEnovaDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (fireEnovaSelectionChanged)
                findEnovaTextBox.Text = "";
        }

        private AbakTools.Towary.Forms.PodgladProduktuForm previewForm = null;

        private void podgladCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (podgladCheckBox.Checked)
            {
                if (previewForm == null)
                    previewForm = new PodgladProduktuForm();

                previewForm.Show();

                setPreviewProduct();
                this.Activate();
                produktyWebDataGridView.Focus();

                /*previewForm = new PodgladProduktuForm();
                var t = new System.Threading.Thread(() => Application.Run(previewForm));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();
                setPreviewProduct();
                this.Focus();
                this.Activate();
                produktyWebDataGridView.Focus();
                 */
            }
            else
            {
                if (previewForm != null)
                {
                    //closePreviewForm();
                    setPreviewVisible(false);
                }
            }
        }

        private void closePreviewForm()
        {
            if (previewForm != null)
            {
                if (previewForm.InvokeRequired)
                {
                    var d = new MethodInvoker(closePreviewForm);
                    previewForm.Invoke(d);
                }
                else
                {
                    previewForm.Close();
                    previewForm = null;
                }
            }
        }

        private void WyborProduktuForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (previewForm != null && !previewForm.Visible && podgladCheckBox.Checked)
                    setPreviewVisible(true);
                this.Focus();
                kategorieTreeView.SelectKategorie(this.kategorieSelected);
                if (tabControl.SelectedIndex == 0)
                {
                    if (produktyWebDataGridView.CurrentRow != null)
                    {
                        int first = produktyWebDataGridView.CurrentRow.Index - produktyWebDataGridView.DisplayedRowCount(false) / 2;
                        if (first < 0) first = 0;
                        produktyWebDataGridView.FirstDisplayedScrollingRowIndex = first;
                    }
                }
                else
                {
                    if (produktyEnovaDataGridView.CurrentRow != null)
                    {
                        int first = produktyEnovaDataGridView.CurrentRow.Index - produktyEnovaDataGridView.DisplayedRowCount(false) / 2;
                        if (first < 0) first = 0;
                        produktyEnovaDataGridView.FirstDisplayedScrollingRowIndex = first;
                    }
                }
            }
            else
            {
                this.kategorieSelected = kategorieTreeView.Kategoria;
                findTextBox.Text = "";
                findEnovaTextBox.Text = "";
                //webSortedColumn = produktyWebDataGridView.SortedColumn;
                //webSortOrder = produktyWebDataGridView.SortOrder;
                enovaSortedColumn = produktyEnovaDataGridView.SortedColumn;
                enovaSortOrder = produktyEnovaDataGridView.SortOrder;

                if (previewForm != null && previewForm.Visible)
                    setPreviewVisible(false);
            }
        }

        private delegate void setPreviewVisibleDelegate(bool visible);
        private void setPreviewVisible(bool visible)
        {
            if (previewForm.InvokeRequired)
            {
                var d = new setPreviewVisibleDelegate(setPreviewVisible);
                previewForm.Invoke(d, new object[] { visible });
            }
            else
            {
                previewForm.Visible = visible;
            }
        }

        private void kategorieTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.Alt == false && e.Shift == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Tab:
                        produktyWebDataGridView.Focus();
                        e.Handled = true;
                        break;
                }
            }
        }

        private void kategorieTreeView_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void produktyWebDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectWebProdukt();
        }

        private void produktyEnovaDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectEnovaProdukt();

        }

        private void produktyWebDataGridView_Sorted(object sender, EventArgs e)
        {
            loadData();
        }

        private void produktyWebDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //SortOrder sort = produktyWebDataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection;
            SortOrder sort = webSortOrder;
            if (e.ColumnIndex != 4)
                webSortOrder = sort == SortOrder.None ? SortOrder.Ascending : (sort == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
            switch (e.ColumnIndex)
            {
                case 0:
                    webSortType = Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType.Kod;
                    break;
                case 1:
                    webSortType = Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType.Nazwa;
                    break;
                case 2:
                    webSortType = Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType.Atrybut;
                    break;
                case 3:
                    webSortType = Enova.Business.Old.Web.TowaryAtrybutyTable.TowarySortType.Cena;
                    break;
            }

            loadData();
        }

        private void kategorieTreeView_EnterSelect(object sender, EventArgs e)
        {
            produktyWebDataGridView.Focus();
        }

        private void featuresTree_EnterSelect(object sender, EventArgs e)
        {
            produktyEnovaDataGridView.Focus();
        }

        private void produktyWebDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void produktyEnovaDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void produktyWebDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < produktyWebDataGridView.Rows.Count)
            {
                var row = produktyWebDataGridView.Rows[e.RowIndex];
                if (row.DataBoundItem != null)
                {
                    Enova.Business.Old.DB.Web.TowarAtrybut ta = (Enova.Business.Old.DB.Web.TowarAtrybut)row.DataBoundItem;
                    if (!string.IsNullOrEmpty(ta.SelectListBackColor))
                    {
                        try
                        {
                            var parts = ta.SelectListBackColor.Split(';');
                            if (parts.Length > 0)
                            {
                                var backColor = ColorTranslator.FromHtml(parts[0]);
                                row.DefaultCellStyle.BackColor = backColor;
                                row.DefaultCellStyle.SelectionBackColor = backColor;
                            }

                            if (parts.Length > 1)
                            {
                                var foreColor = ColorTranslator.FromHtml(parts[1]);
                                row.DefaultCellStyle.ForeColor = foreColor;
                                row.DefaultCellStyle.SelectionForeColor = foreColor;
                            }
                        }
                        catch { }

                    }
                    else if (!ta.Dostepny)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.SelectionForeColor = Color.Red;
                    }
                }
            }
        }

        private void refreshWebButton_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
