using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Web;

//[assembly: BAL.Forms.MenuAction("Magazyn\\Lista towarów OLD", typeof(AbakTools.Towary.Forms.MagazynForm), Priority = 810)]

namespace AbakTools.Towary.Forms
{
    public partial class MagazynForm : Enova.Business.Old.Forms.DataGridForm
    {
        bool fireEvents = true;

        public MagazynForm()
        {
            InitializeComponent();
        }

        protected override void LoadData()
        {
            fireEvents = false;
            loadProdukty();
            fireEvents = true;
        }

        private void loadProdukty()
        {
            var sortedColumn = DataGrid.SortedColumn;
            if (sortedColumn == null)
                sortedColumn = DataGrid.Columns[0];

            var sortOrder = DataGrid.SortOrder;
            if (sortOrder == SortOrder.None)
                sortOrder = SortOrder.Ascending;

            bool? aktywnosc = null;
            bool? dostepnosc = null;

            if (aktywnoscComboBox.SelectedIndex > 0)
                aktywnosc = aktywnoscComboBox.SelectedIndex == 1 ? true : false;

            if (dostepnoscComboBox.SelectedIndex > 0)
                dostepnosc = (aktywnosc == null || aktywnosc == true) ? (dostepnoscComboBox.SelectedIndex == 1 ? true : false) : (bool?)null;

            DataGridBindingSource.DataSource = new ProduktyAtrybuty(kategorieTreeView.Kategoria, ProduktySortOrder.None, SortOrder.None, aktywnosc , dostepnosc);

            DataGrid.Sort(sortedColumn, sortOrder == SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending);
        }



        private void kategorieTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            loadProdukty();
        }

        private void DataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DataGrid.Rows.Count)
            {
                var row = DataGrid.Rows[e.RowIndex];
                if (row != null && row.DataBoundItem != null)
                {
                    ProduktAtrybut pa = (ProduktAtrybut)row.DataBoundItem;
                    if (!pa.Aktywny)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                        row.DefaultCellStyle.SelectionForeColor = Color.Gray;
                    } 
                    else if (!pa.Dostepny)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.SelectionForeColor = Color.Red;
                    }

                    if (pa.Aktywny && pa.Dostepny)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                }
            }
        }

        private void MagazynForm_Load(object sender, EventArgs e)
        {
            aktywnoscComboBox.SelectedIndex = 1;
            dostepnoscComboBox.SelectedIndex = 0;
        }

        private void aktywnoscComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadProdukty();
        }

        private void dostepnoscComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadProdukty();
        }

        private void DataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (fireEvents && e.RowIndex >= 0 && e.RowIndex < DataGrid.Rows.Count)
            {
                ProduktAtrybut pa = (ProduktAtrybut)DataGrid.Rows[e.RowIndex].DataBoundItem;
                if (DataGrid.Columns[e.ColumnIndex].Name == "AktywnyColumn" || DataGrid.Columns[e.ColumnIndex].Name == "DostepnyColumn")
                {
                    Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
                    try
                    {
                        Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, pa);
                    }
                    catch { }
                    var produkt = pa.Produkt;
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, produkt);

                    produkt.Gotowy = 
                        (produkt.Synchronizacja == (byte)RowSynchronizeOld.Synchronized || produkt.Synchronizacja == (byte)RowSynchronizeOld.NotsynchronizedEdit) ? true : produkt.Gotowy;
                    produkt.Stamp = DateTime.Now;
                    switch ((RowSynchronizeOld)produkt.Synchronizacja)
                    {
                        case RowSynchronizeOld.Synchronized:
                            produkt.Synchronizacja = (byte)RowSynchronizeOld.NotsynchronizedEdit;
                            break;
                        case RowSynchronizeOld.NotsynchronizedDelete:
                            produkt.Synchronizacja = (byte)RowSynchronizeOld.NotsynchronizedNew;
                            break;
                    }

                    Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();

                    try
                    {
                        Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, pa);
                    }
                    catch { }

                }
            }
        }

        private void DataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DataGrid.Rows.Count &&
                (DataGrid.Columns[e.ColumnIndex].Name == "AktywnyColumn" || DataGrid.Columns[e.ColumnIndex].Name == "DostepnyColumn"))
                DataGrid.EndEdit();
        }

        private void DataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private bool fireFindTextBoxChanged = true;
        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(findTextBox.Text) && fireFindTextBoxChanged)
            {
                fireDataGridSelectionChanged = false;
                var idx = DataGridBindingSource.Find("Kod", findTextBox.Text);
                if (idx > -1)
                {
                    DataGrid.CurrentCell = DataGrid.Rows[idx].Cells[0];
                }
                else
                {
                    findTextBox.Text = findTextBox.Text.Substring(0, findTextBox.Text.Length - 1);
                    findTextBox.SelectionStart = findTextBox.Text.Length;
                }
                fireDataGridSelectionChanged = true;
            }
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        protected bool fireDataGridSelectionChanged = true;
        private void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (fireDataGridSelectionChanged)
                findTextBox.Text = "";
        }

        private void MagazynForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Enova.Business.Old.Core.Tools.IsPrintableChar(e.KeyChar))
            {
                findTextBox.Text += e.KeyChar.ToString();
                findTextBox.SelectionStart = findTextBox.Text.Length;
            }
            e.Handled = true;
        }

        private void MagazynForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && !string.IsNullOrEmpty(findTextBox.Text))
            {
                findTextBox.Text = findTextBox.Text.Substring(0, findTextBox.Text.Length - 1);
                findTextBox.SelectionStart = findTextBox.Text.Length;
                e.Handled = true;
            }
        }
    }
}
