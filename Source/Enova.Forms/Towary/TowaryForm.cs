using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.MenuAction("Kartoteki\\Towary", typeof(Enova.Forms.Towary.TowaryForm), Priority = 520)]

namespace Enova.Forms.Towary
{
    public partial class TowaryForm : Enova.Business.Old.Forms.FeaturesDataGridForm
    {
        public TowaryForm()
        {
            InitializeComponent();
        }

        private void TowaryForm_Load(object sender, EventArgs e)
        {
            
        }

        protected override void LoadData()
        {
            if (!RowsIsLoaded)
            {
                if (!this.DesignMode)
                    this.DataSource = new Enova.Business.Old.Towary();
                this.DataGrid.Sort(DataGrid.Columns[0], ListSortDirection.Ascending);
                RowsIsLoaded = true;
            }
        }

        private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void szukajTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(szukajTextBox.Text))
            {
                int idx = this.DataGridBindingSource.Find((System.ComponentModel.PropertyDescriptor)null, (object)szukajTextBox.Text);
                if (idx != -1)
                {
                    fireSelectionChange = false;
                    DataGrid.CurrentCell = DataGrid.Rows[idx].Cells[0];
                    fireSelectionChange = true;
                }
            }
        }

        private bool fireSelectionChange = true;
        private void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (fireSelectionChange)
                szukajTextBox.Text = null;
        }

        private void DataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Enova.Business.Old.Core.Tools.IsPrintableChar(e.KeyChar))
            {
                string str = szukajTextBox.Text == null ? "" : szukajTextBox.Text;
                str += e.KeyChar;
                szukajTextBox.Text = str;
            }
        }

        private void DataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DataGrid.Rows.Count &&
                    (DataGrid.Columns[e.ColumnIndex].Name == "BlokadaColumn"))
                DataGrid.EndEdit();
        }
    }
}
