using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace AbakTools.Kadry.Forms
{
    public partial class RaporOkresowyProwizjiForm : Form
    {
        public string Przedstawiciel = null;
        public int? Rok = null;

        public RaporOkresowyProwizjiForm()
        {
            InitializeComponent();
        }

        private void RaporOkresowyProwizjiForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Przedstawiciel) && Rok != null)
            {
                przedstawicielSelect.Przedstawiciel = Przedstawiciel;
                rokTextBox.Text = Rok.ToString();
            }
        }

        private void createReport()
        {
            string przedst = przedstawicielSelect.Przedstawiciel;
            int rok = int.Parse(rokTextBox.Text);

            /*ProwizjaBindingSource.DataSource = Enova.Business.Old.Core.ContextManager.WebContext.Prowizje
                .Where(p => p.Przedstawiciel == przedst && p.Rok == rok).OrderBy(p=>p.Miesiac).ToList();
             */

            var ds = Enova.Business.Old.Core.ContextManager.WebContext.Prowizje
                .Where(p => p.Przedstawiciel == przedst && p.Rok == rok).OrderBy(p => p.Miesiac).ToList();

            reportViewer.LocalReport.ReportPath = "Reports\\RaportOkresowyProwizji.rdlc";

            reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("przedstawiciel", przedst));
            reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("rok",rok.ToString()));
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Prowizja", ds));

            reportViewer.RefreshReport();
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            createReport();
        }
    }
}
