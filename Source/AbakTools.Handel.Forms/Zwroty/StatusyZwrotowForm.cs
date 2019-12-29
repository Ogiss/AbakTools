using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

[assembly: BAL.Forms.MenuAction("WebTools\\Statusy zwrotów", typeof(AbakTools.Zwroty.Forms.StatusyZwrotowForm), MenuAction = BAL.Forms.MenuActionsType.OpenFormModal, Priority = 1060)]

namespace AbakTools.Zwroty.Forms
{
    public partial class StatusyZwrotowForm : Enova.Business.Old.Forms.DataGridForm
    {
        public StatusyZwrotowForm()
        {
            InitializeComponent();
        }

        private void StatusyZwrotowForm_Load(object sender, EventArgs e)
        {
            
        }

        protected override void LoadData()
        {
            this.DataSource = new Enova.Business.Old.Web.StatusyZwrotow();
        }

        private void DataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < this.DataGrid.Rows.Count)
            {
                DataGridViewRow row = DataGrid.Rows[e.RowIndex];
                StatusZwrotu s = (StatusZwrotu)row.DataBoundItem;
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
