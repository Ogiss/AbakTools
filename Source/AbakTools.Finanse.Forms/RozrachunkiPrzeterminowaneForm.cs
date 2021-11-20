using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using AbakTools.Printer;

[assembly: BAL.Forms.MenuAction("Finanse\\Rozrachunki przeterminowane", typeof(AbakTools.Finanse.Forms.RozrachunkiPrzeterminowaneForm), Priority = 320)]

namespace AbakTools.Finanse.Forms
{
    public partial class RozrachunkiPrzeterminowaneForm : Enova.Business.Old.Forms.FeaturesDataGridForm
    {
        private bool isLoaded = false;

        public RozrachunkiPrzeterminowaneForm()
        {
            InitializeComponent();
        }

        private void RozrachunkiPrzeterminowaneForm_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            int m = now.Month;
            if (m == 12) m = 0;
            int y = now.Year;
            int pm = m < 4 ? 0 : (m > 3 && m < 8 ? 4 : 8);
            DateTime okresOd = new DateTime(pm == 0 && m != 0 ? y - 1 : y, pm == 0 ? 12 : pm, 1).AddMonths(-7);
            DateTime termin = okresOd.AddMonths(4).AddDays(-1);

            maxDataWystDateTimePicker.Value = termin;

            loadRozrachunki();

            isLoaded = true;
        }

        private void loadRozrachunki()
        {
            DateTime data = maxDataWystDateTimePicker.Value;
            data = new DateTime(data.Year, data.Month, data.Day, 23, 59, 59);

            DataSource = new Enova.Business.Old.KontrahenciPrzeterRozrach(data);   
        }

        private void RefreshData()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            DateTime data = maxDataWystDateTimePicker.Value;
            data = new DateTime(data.Year, data.Month, data.Day, 23, 59, 59);

            ((Enova.Business.Old.KontrahenciPrzeterRozrach)DataSource).InitQuery(data);
            ((Enova.Business.Old.KontrahenciPrzeterRozrach)DataSource).Reload();

            this.Cursor = Cursors.Default;
            this.Enabled = true;

        }

        protected override DialogResult EditRecord(object record)
        {
            DialogResult result = base.EditRecord(record);

            if (result == DialogResult.OK)
            {
                Enova.Business.Old.Types.RozrachunekRow rozrach = (Enova.Business.Old.Types.RozrachunekRow)record;
                if (rozrach.IDKontrahenta != null)
                {
                    var kontrahent = DataContext.Kontrahenci.Where(k => k.ID == rozrach.IDKontrahenta).FirstOrDefault();
                    if (kontrahent != null)
                    {
                        rozrach.BlokadaSprzedaży = kontrahent.BlokadaSprzedazy;
                    }
                }
                ((Enova.Business.Old.KontrahenciPrzeterRozrach)DataSource).Refresh();
                //RefreshData();
            }

            return result;
        }

        private void RozrachunkiPrzeterminowaneForm_PrintItemClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy wydrukować tylko zaznaczone pozycje ?", "EnovaTools", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            List<Enova.Business.Old.Types.RozrachunekRow> reportSource = null;
            if (result == DialogResult.Yes)
            {
                reportSource = new List<Enova.Business.Old.Types.RozrachunekRow>();
                foreach (DataGridViewRow row in DataGrid.SelectedRows)
                {
                    reportSource.Add((Enova.Business.Old.Types.RozrachunekRow)row.DataBoundItem);
                }
            }
            else if (result == DialogResult.No)
            {
                reportSource = new List<Enova.Business.Old.Types.RozrachunekRow>();
                foreach (DataGridViewRow row in DataGrid.Rows)
                {
                    reportSource.Add((Enova.Business.Old.Types.RozrachunekRow)row.DataBoundItem);
                }
            }

            if (reportSource != null)
            {
                ReportForm form = new ReportForm();
                form.ReportViewer.LocalReport.ReportPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports\\RozrachunkiPrzeterminowaneReport.rdlc");
                form.ReportViewer.LocalReport.DataSources.Clear();
                form.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("RozrachunekRow", reportSource));

                form.ShowDialog();
            }
            
        }

        private void maxDataWystDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
        }

        private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void zatwierdźDatęButton_Click(object sender, EventArgs e)
        {
            if (isLoaded)
            {
                //loadRozrachunki();
                RefreshData();
            }

        }

        private void DataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (DataGrid.Rows.Count > 0 && e.RowIndex >= 0 && e.RowIndex < DataGrid.Rows.Count)
            {
                Enova.Business.Old.Types.RozrachunekRow rozrach = (Enova.Business.Old.Types.RozrachunekRow)DataGrid.Rows[e.RowIndex].DataBoundItem;
                if (rozrach.BlokadaSprzedaży != null && rozrach.BlokadaSprzedaży.Value)
                {
                    DataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
                else if (rozrach.Blokada != null && rozrach.Blokada.Value)
                {
                    DataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Violet;
                }
            }
        }
    }
}
