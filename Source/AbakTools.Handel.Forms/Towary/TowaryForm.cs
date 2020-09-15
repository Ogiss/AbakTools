using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DbWeb = Enova.Business.Old.DB.Web;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

[assembly: BAL.Forms.MenuAction("WebTools\\Towary", typeof(AbakTools.Towary.Forms.TowaryForm), Priority = 1040)]

namespace AbakTools.Towary.Forms
{
    public partial class TowaryForm : Enova.Business.Old.Forms.DataGridForm
    {
        private bool disableOnSelectionChanged;
        private bool disableDostawcaChanged;
        private CheckBox readyHeaderCheckBox;
        private CheckBox activeHeaderCheckBox;
        private CheckBox availableHeaderCheckBox;

        public TowaryForm()
        {
            InitializeComponent();
        }

        protected override void LoadData()
        {
            try
            {
                DataGridViewColumn sortedColumn = DataGrid.SortedColumn;
                SortOrder sortOrder = DataGrid.SortOrder;
                bool f = false;

                if (sortedColumn == null)
                    sortedColumn = Kod;
                if (sortOrder == SortOrder.None)
                    sortOrder = SortOrder.Ascending;

                Enova.Business.Old.DB.Web.KategoriaOld kategoria = kategorieTreeView.Kategoria;
                int? dostawcaID = dostawcaComboBox.SelectedIndex > 0 ? ((DostawcaInfo)dostawcaComboBox.SelectedItem).ID : (int?)null;

                this.DataSource = new Enova.Business.Old.Web.Produkty(kategoria, (nieaktywneCheckBox.Checked ? (bool?)null : true), null, null, null, true,
                    (towaryEnovaCheckBox.Checked ? (bool?)null : false), dostawcaID);

                DataGrid.Sort(sortedColumn, (sortOrder == SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending));

                activeHeaderCheckBox = AddCheckboxToColumnHeader("Aktywny", ChangeActivForSelectedRows, treeState: true);
                readyHeaderCheckBox = AddCheckboxToColumnHeader("Gotowy", ChangeReadyForSelectedRows, treeState: true);
                availableHeaderCheckBox = AddCheckboxToColumnHeader("Dostepny", ChangeAvailableForSelectedRows, treeState: true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message, "EnovaTools");
            }
        }

        private void ChangeActivForSelectedRows(CheckBox checkBox)
        {
            if (checkBox.CheckState != CheckState.Indeterminate && DataGrid.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in DataGrid.SelectedRows)
                {
                    var product = (Produkt)row.DataBoundItem;
                    product.IsActive = checkBox.CheckState == CheckState.Checked ? true : false;

                    if (product.EntityState == EntityState.Modified)
                    {
                        product.Synchronizacja = (byte)RowSynchronizeOld.NotsynchronizedEdit;
                    }
                }

                WebContext.SaveChanges();
                DataGrid.Refresh();
            }
        }

        private void ChangeReadyForSelectedRows(CheckBox checkBox)
        {
            if(checkBox.CheckState != CheckState.Indeterminate && DataGrid.SelectedRows.Count > 0)
            {
                foreach(DataGridViewRow row in DataGrid.SelectedRows)
                {
                    var product = ( Produkt)row.DataBoundItem;
                    product.Gotowy = checkBox.CheckState == CheckState.Checked ? true : false;

                    if(product.EntityState == EntityState.Modified)
                    {
                        product.Synchronizacja = (byte)RowSynchronizeOld.NotsynchronizedEdit;
                    }
                }

                WebContext.SaveChanges();
                DataGrid.Refresh();
            }
        }

        private void ChangeAvailableForSelectedRows(CheckBox checkBox)
        {
            if (checkBox.CheckState != CheckState.Indeterminate && DataGrid.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in DataGrid.SelectedRows)
                {
                    var product = (Produkt)row.DataBoundItem;
                    product.Dostepny = checkBox.CheckState == CheckState.Checked ? true : false;

                    if (product.EntityState == EntityState.Modified)
                    {
                        product.Synchronizacja = (byte)RowSynchronizeOld.NotsynchronizedEdit;
                    }
                }

                WebContext.SaveChanges();
                DataGrid.Refresh();
            }
        }

        private void kategorieTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadData();
        }

        private void nieaktywneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void towaryEnovaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void TowaryForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.F8:
                        CloneCurrentRow();
                        break;
                }
            }
        }

        private void DataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Enova.Business.Old.Core.Tools.IsPrintableChar(e.KeyChar))
            {
                findTextBox.Text += e.KeyChar.ToString();
                findTextBox.SelectionStart = findTextBox.Text.Length;
            }
        }

        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(findTextBox.Text))
            {
                try
                {
                    disableOnSelectionChanged = true;
                    int idx = DataGridBindingSource.Find("Kod", findTextBox.Text);
                    if (idx != -1)
                    {
                        this.DataGrid.CurrentCell = this.DataGrid.Rows[idx].Cells[0];
                    }
                    else
                    {
                        findTextBox.Text = findTextBox.Text.Substring(0, findTextBox.Text.Length - 1);
                        findTextBox.SelectionStart = findTextBox.Text.Length;
                    }
                }
                finally
                {
                    disableOnSelectionChanged = false;
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (!disableOnSelectionChanged)
            {
                findTextBox.Text = "";
            }
        }

        private void TowaryForm_Load(object sender, EventArgs e)
        {
            var dostawcy = new List<DostawcaInfo>();
            dostawcy.Add(new DostawcaInfo() { ID = 0, Nazwa = "Wszyscy" });
            if (!this.DesignMode)
            {
                using (var dc = new Enova.Business.Old.DB.Web.WebContext())
                {
                    dostawcy.AddRange(from d in dc.Dostawcy orderby d.Nazwa select new DostawcaInfo() { ID = d.ID, Nazwa = d.Nazwa });
                }
            }
            disableDostawcaChanged = true;
            dostawcaBindingSource.DataSource = dostawcy;
            dostawcaComboBox.SelectedIndex = 0;
            disableDostawcaChanged = false;
        }

        private void dostawcaComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!disableDostawcaChanged)
            {
                LoadData();
            }
        }

        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DataGrid.Rows.Count)
            {
                var row = DataGrid.Rows[e.RowIndex];
                var product = row?.DataBoundItem as DbWeb.Produkt;

                if (product != null)
                {
                    //row.Cells[7].Style.SelectionBackColor = DataGrid.DefaultCellStyle.BackColor;
                    //row.Cells[7].Style.SelectionForeColor = DataGrid.DefaultCellStyle.ForeColor;

                    var pink = Color.FromArgb(204, 67, 136);
                    Color? backColor = product.NotWebAvailable ? pink :
                        (product.IsNew ? Color.LightBlue : (product.IsEdited ? Color.LightGreen : (Color?)null));

                    if (backColor.HasValue)
                    {
                        row.DefaultCellStyle.BackColor = backColor.Value;
                        row.DefaultCellStyle.SelectionBackColor = backColor.Value;
                        /*
                        row.Cells[7].Style.SelectionBackColor = backColor.Value;
                        if (product.IsActive)
                        {
                            row.Cells[7].Style.SelectionForeColor = Color.Black;
                        }
                        */
                    }


                    if (!product.IsActive)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.SelectionForeColor = Color.Red;
                        //row.Cells[7].Style.SelectionForeColor = Color.Red;
                    }
                }
            };
        }
    }
}
