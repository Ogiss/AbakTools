using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;

[assembly: BAL.Forms.MenuAction("WebTools\\Statusy zamówień", typeof(EnovaTools.Forms.Web.StatusyZamowienForm), MenuAction = BAL.Forms.MenuActionsType.OpenFormModal, Priority = 1050)]

namespace EnovaTools.Forms.Web
{
    public partial class StatusyZamowienForm : Enova.Business.Old.Forms.DataGridForm
    {
        public StatusyZamowienForm()
        {
            InitializeComponent();
        }

        protected override void LoadData()
        {
            DataGridBindingSource.DataSource = new Enova.Business.Old.Web.StatusyZamowien();
        }

        private void DataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DataGrid.Rows.Count)
            {
                DataGridViewRow row = DataGrid.Rows[e.RowIndex];
                StatusZamowienia s = (StatusZamowienia)row.DataBoundItem;
                if (s != null)
                {
                    Color color = ColorTranslator.FromHtml(s.Kolor);
                    row.DefaultCellStyle.BackColor = color;
                    row.HeaderCell.Style.BackColor = color;
                    row.HeaderCell.Style.SelectionBackColor = color;
                    row.HeaderCell.Style.SelectionForeColor = Color.Black;
                }

            }

        }
    }
}
