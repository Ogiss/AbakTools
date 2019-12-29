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

//[assembly: BAL.Forms.MenuAction("Magazyn\\Proponowane dostępne/niedostępne STARE", typeof(AbakTools.Towary.Forms.MagazynAVForm), Priority = 825)]

namespace AbakTools.Towary.Forms
{
    public partial class MagazynAVForm
        : Enova.Business.Old.Forms.DataGridForm
    {
        private bool fireEvents = true;

        public Func<IList<ProduktAtrybut>> GetRowsFunc;

        public MagazynAVForm()
        {
            InitializeComponent();
            
        }

        protected override void LoadData()
        {
            fireEvents = false;

            if (GetRowsFunc != null)
            {
                this.DataGridBindingSource.DataSource = GetRowsFunc();
            }
            else
            {

                bool? active = null;
                bool? available = null;

                if (aktywnoscCcomboBox.SelectedIndex < 0)
                    aktywnoscCcomboBox.SelectedIndex = 0;

                if (dostepnoscComboBox.SelectedIndex < 0)
                    dostepnoscComboBox.SelectedIndex = 2;

                switch (aktywnoscCcomboBox.SelectedIndex)
                {
                    case 0:
                        active = true;
                        break;
                    case 1:
                        active = false;
                        break;
                }

                switch (dostepnoscComboBox.SelectedIndex)
                {
                    case 0:
                        available = true;
                        break;
                    case 1:
                        available = false;
                        break;
                }

                this.DataGridBindingSource.DataSource = new Enova.Business.Old.Web.ProduktyAtrybutyAV(null, ProduktySortOrder.Kod, SortOrder.Ascending, active, available);
            }
            fireEvents = true;
        }

        private void DataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (fireEvents && e.RowIndex >= 0 && e.RowIndex < DataGrid.Rows.Count)
            {
                ProduktAtrybut pa = (ProduktAtrybut)DataGrid.Rows[e.RowIndex].DataBoundItem;
                if (DataGrid.Columns[e.ColumnIndex].Name == "AktywnyColumn" || DataGrid.Columns[e.ColumnIndex].Name == "DostepnyColumn")
                {
                    var produkt = pa.Produkt;

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

                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, pa);

                }
                else if (DataGrid.Columns[e.ColumnIndex].Name == "AvailableMsgColumn" || DataGrid.Columns[e.ColumnIndex].Name == "AvailableDateColumn")
                {
                    Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, pa);
                }
                else if (DataGrid.Columns[e.ColumnIndex].Name == "VisibleColumn")
                {
                    Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, pa);
                    visibleChanged(e.RowIndex);
                }
            }

        }

        private void visibleChanged(int rowIndex)
        {
            DataGridBindingSource.RemoveAt(rowIndex);
        }

        private void DataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DataGrid.Rows.Count &&
                (DataGrid.Columns[e.ColumnIndex].Name == "AktywnyColumn" || DataGrid.Columns[e.ColumnIndex].Name == "DostepnyColumn" || DataGrid.Columns[e.ColumnIndex].Name == "VisibleColumn"))
                DataGrid.EndEdit();

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

        private void MagazynAVForm_Load(object sender, EventArgs e)
        {
            aktywnoscCcomboBox.SelectedIndex = 0;
            dostepnoscComboBox.SelectedIndex = 2;
        }

        private void aktywnoscCcomboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dostepnoscComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
