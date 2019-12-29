using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;
using System.Data.Entity;
using System.Data.Objects;

[assembly: BAL.Forms.MenuAction("Prowizje\\Prowizje z faktur", typeof(AbakTools.Kadry.Forms.ProwizjaZFakturForm), Priority = 30)]

namespace AbakTools.Kadry.Forms
{
    public partial class ProwizjaZFakturForm : Enova.Forms.FormWithEnovaAPI
    {
        public ProwizjaZFakturForm()
        {
            InitializeComponent();
        }

        private void ProwizjaZFakturForm_Load(object sender, EventArgs e)
        {

           
        }

        private void CreateReport()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            DateTime dataOd = okresDateSpan.DateFrom.Date;
            DateTime dataDo = okresDateSpan.DateTo.Date.AddDays(1).AddMilliseconds(-1);
            Enova.API.CRM.Kontrahent kontrahent = kontrahentSelect.Kontrahent;

            if (kontrahent != null)
            {
                var dc = Enova.Business.Old.Core.ContextManager.DataContext;

                var dokumenty = dc.DokHandlowe.Where(d => d.Data >= dataOd && d.Data <= dataDo && (d.RelationDefinicja.ID == 1 || d.RelationDefinicja.ID == 2)
                    && d.Kontrahent.ID == kontrahent.ID).OrderBy(d=>d.Data).ThenBy(d=>d.NumerPelny).ToList();
                var rows = new List<ReportRow>();

                foreach (var dokument in dokumenty)
                {
                    DateTime? dataRoz = dokument.GetDataRozliczenia(dc);
                    rows.Add(new ReportRow()
                    {
                        Int1 = dokument.ID,
                        Int2 = dokument.Kontrahent.ID,
                        String1 = dokument.NumerPelny,
                        DateTime1 = dokument.Data,
                        String2 = dataRoz==null ? string.Empty : dataRoz.Value.ToShortDateString(),
                        Decimal1 = dokument.SumaBrutto,
                        Decimal2 = dokument.DoRozliczenia,
                        Decimal3 = dokument.GetProwizja(dc, 0.5M),
                        Decimal4 = dokument.GetPotracenia(dc,0.5M)
                    });
                }

                reportViewer.LocalReport.ReportPath = "Reports\\ProwizjaZFakturReport.rdlc";
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("text", "Kontrahent: " + kontrahent.Kod + "  Okres od: " + dataOd.ToShortDateString()
                + " do " + dataDo.ToShortDateString()));
                reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Rows", rows));
                reportViewer.RefreshReport();
            }
            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            CreateReport();
        }
    }
}
