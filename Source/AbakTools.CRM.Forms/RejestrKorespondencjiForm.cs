using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//[assembly: BAL.Forms.MenuAction("Narzędzia\\Korespondencja seryjna", typeof(AbakTools.CRM.Forms.RejestrKorespondencjiForm), Priority = 610)]

namespace AbakTools.CRM.Forms
{
    public partial class RejestrKorespondencjiForm : Enova.Business.Old.Forms.DataGridForm
    {
        public RejestrKorespondencjiForm()
        {
            InitializeComponent();
        }

        protected override void LoadData()
        {

            DataSource = new Enova.Business.Old.Web.RejestrKorespondencji();

            if (DataGrid.SortedColumn == null)
            {
                DataGrid.Sort(DataGrid.Columns["DataWysylki"], ListSortDirection.Descending);
            }

        }

        private void RejestrKorespondencjiForm_Load(object sender, EventArgs e)
        {
            //LoadData();
        }
    }
}
