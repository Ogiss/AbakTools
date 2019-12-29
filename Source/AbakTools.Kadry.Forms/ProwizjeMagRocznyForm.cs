using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Kadry.Forms
{
    public partial class ProwizjeMagRocznyForm : Form
    {
        public ProwizjeMagRocznyForm()
        {
            InitializeComponent();
        }

        private void ProwizjeMagRocznyForm_Load(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;

            for (int y = year - 3; y <= year; y++)
                rokComboBox.Items.Add(y);

        }

        string[] miesiace = new string[]
        {
            "none",
            "Styczeń",
            "Luty",
            "Marzec",
            "Kwiecień",
            "Maj",
            "Czerwiec",
            "Lipiec",
            "Sierpień",
            "Wrzesień",
            "Październik",
            "Listopad",
            "Grudzień"
        };

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string pracownik = (string)pracownikComboBox.SelectedItem;
            int rok = (int)rokComboBox.SelectedItem;

            if (!string.IsNullOrEmpty(pracownik))
            {
                var rows = Enova.Business.Old.Core.ContextManager.WebContext.ProwizjePracownikow.Where(p => p.PracownikKod == pracownik && p.Rok == rok).OrderBy(p => p.Miesiac).ToList().
                    Select(p => new Enova.Business.Old.Types.ReportRow()
                    {
                        Int1 = p.Miesiac,
                        String1 = miesiace[p.Miesiac],
                        Decimal1 = p.ProwizjaSuma,
                        Decimal2 = p.Etat,
                        Decimal3 = p.ZusPlaconyFirma,
                        Decimal4 = p.ProwizjaNetto,
                        Decimal5 = p.Podatek19,
                        Decimal6 = p.ProwizjaBezPodatki,
                        Decimal7 = p.PodatekPlaconyPracownik,
                        Decimal8 = p.ZusPlaconyPracownik,
                        Decimal9 = p.EtatNetto,
                        Decimal10 = p.WyplataNetto
                    }).ToList();

                //this.reportViewer.LocalReport.ReportPath = "ProwizjaMagRocznyReport.rdlc";
                this.reportViewer.LocalReport.ReportEmbeddedResource = "AbakTools.Kadry.Forms.Reports.ProwizjaMagRocznyReport.rdlc";
                this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("title", "Roczny Raport Prowizji " + pracownik + "/" + rok.ToString()));
                this.reportViewer.LocalReport.DataSources.Clear();
                this.reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Rows", rows));
                this.reportViewer.RefreshReport();
            }

            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }
    }
}
