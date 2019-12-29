using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Entity;
using System.Data.Objects;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Enova.Forms.Services;

[assembly: BAL.Forms.MenuAction("Dokumenty\\Niepodpisane korekty", typeof(AbakTools.Ksiegowosc.Forms.NiepodpisaneKorektyForm), Priority = 430)]

namespace AbakTools.Ksiegowosc.Forms
{
    public partial class NiepodpisaneKorektyForm : Enova.Forms.FormWithEnovaAPI
    {
        public NiepodpisaneKorektyForm()
        {
            InitializeComponent();
        }

        private void NiepodpisaneKorektyForm_Load(object sender, EventArgs e)
        {
            przedstawicielComboBox.Items.Add("(Wszyscy)");
            przedstawicielComboBox.Items.AddRange(BusinessService.Dictionary.GetByFeatureName(Session, "Kontrahenci", "przedstawiciel").OrderBy(r => r.Value).Select(r => r.Value).ToArray());
            przedstawicielComboBox.SelectedIndex = 0;
            dataOdDateTimePicker.Value = new DateTime(2009, 01, 01);
            dataDoDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            createReport();
        }

        private void createReport()
        {
            using (new BAL.Forms.WaitCursor(this))
            {
                var pr = przedstawicielComboBox.SelectedIndex > 0 ? (string)przedstawicielComboBox.SelectedItem : null;
                var reportSource = new List<DokumentEwidencjiViewRow>();
                DateTime dataDo = dataDoDateTimePicker.Value.Date; ;
                DateTime dataOd = dataOdDateTimePicker.Value.Date.AddDays(1).AddMilliseconds(-1);

                /*
                var cm = Session.GetModule<Enova.API.Core.CoreModule>();
                var kontrahenci = CRMService.Kontrahenci.ByPrzedstawiciel(Session, pr).ToList();

                foreach (var k in kontrahenci)
                {
                    var filter =
                        "NumerDokumentu LIKE 'FK/%'" +
                        " AND DataDokumentu >= '" + dataOd.ToShortDateString() + "' AND DataDokumentu <= '" + dataDo.ToShortDateString() + "'" +
                        " AND Typ = " + (int)Enova.API.Core.TypDokumentu.SprzedażEwidencja;
                    var dokumenty = cm.DokEwidencja.WgPodmiot(k).CreateView().SetFilter(filter).Cast<Enova.API.Core.DokEwidencji>().ToList();
                    foreach (var de in dokumenty)
                    {
                        if (de.Stan == Enova.API.Core.StanEwidencji.Bufor)
                        {
                            var dh = de.Dokument as Enova.API.Handel.DokumentHandlowy;
                            reportSource.Add(new DokumentEwidencjiViewRow()
                            {
                                ID = de.ID,
                                Stan = de.Stan,
                                KodKontrahenta = k.Kod,
                                NazwaKontrahenta = k.Nazwa,
                                NumerDokumentu = de.NumerDokumentu,
                                DataDokumentu = de.DataDokumentu,
                                WartośćDokumentu = de.Wartosc.Value,
                                WartośćNetto = dh!=null ? dh.WartoscNetto.Value : (decimal?)null
                            });
                        }
                    }
                }
                */

                var sql = string.Format(
                    "SELECT k.Kod,k.Nazwa,de.Stan,de.DataDokumentu,de.NumerDokumentu,de.WartoscValue AS Wartosc,fp.Data Przewoznik,t0.DataRozliczenia, dh.SumaNetto WartoscNetto " +
                    "FROM dbo.DokEwidencja de INNER JOIN dbo.DokHandlowe dh ON (dh.ID=de.Dokument) " +
                    "INNER JOIN dbo.Kontrahenci k ON (k.ID=de.Podmiot) " +
                    (pr != null ? "INNER JOIN dbo.Features f ON (f.Parent = k.ID AND f.ParentType='Kontrahenci' AND f.Lp=0 AND f.Name='przedstawiciel') " : "") +
                    "LEFT JOIN dbo.Features fp ON (fp.Parent = dh.ID AND fp.ParentType='DokHandlowe' AND fp.Lp=0 AND fp.Name='PRZEWOŻNIK') " +
                    "LEFT JOIN (SELECT pl.Dokument, MAX(pl.DataRozliczenia) DataRozliczenia FROM dbo.Platnosci pl WHERE pl.DokumentType='DokHandlowe' " +
                    "GROUP BY pl.Dokument)t0 ON t0.Dokument=dh.ID " +
                    "WHERE de.Stan={0} AND de.Typ={1} AND de.PodmiotType='Kontrahenci' AND de.DokumentType='DokHandlowe' " +
                    (pr != null ? "AND f.Data='{2}'" : "") + " AND dh.Definicja = 2 " +
                    "AND de.DataDokumentu >= '{3}' AND de.DataDokumentu <= '{4}'",
                    (int)Enova.API.Core.StanEwidencji.Bufor, (int)Enova.API.Core.TypDokumentu.SprzedażEwidencja,
                    pr, dataOd.ToShortDateString(), dataDo.ToShortDateString());

                using (var dc = new Enova.Business.Old.DB.EnovaContext())
                {
                    reportSource = (from r in dc.ExecuteStoreQuery<DokumentEwidencjiInfo>(sql)
                                    select new DokumentEwidencjiViewRow()
                                    {
                                        KodKontrahenta = r.Kod,
                                        NazwaKontrahenta = r.Nazwa,
                                        NumerDokumentu = r.NumerDokumentu,
                                        DataDokumentu = r.DataDokumentu,
                                        WartośćDokumentu = r.Wartosc,
                                        WartośćNetto = r.WartoscNetto,
                                        Przewoźnik = r.Przewoznik,
                                        DataRozliczenia = r.DataRozliczenia == null ? DateTime.MaxValue : r.DataRozliczenia.Value
                                    }).ToList();


                }


                this.reportViewer.LocalReport.ReportEmbeddedResource = "AbakTools.Ksiegowosc.Forms.Reports.NiepodpisaneKorektyReport.rdlc";
                this.reportViewer.LocalReport.DataSources.Clear();
                this.reportViewer.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("przedstawiciel", pr == null ? "Wszyscy" : pr) });
                this.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DokumentEwidencjiViewRow", reportSource));
                this.reportViewer.RefreshReport();
            }
        }

        [Obsolete("Do usunięcia")]
        public class DokumentEwidencjiInfo
        {
            public string Kod { get; set; }
            public string Nazwa { get; set; }
            public string NumerDokumentu { get; set; }
            public DateTime DataDokumentu { get; set; }
            public decimal Wartosc { get; set; }
            public decimal WartoscNetto { get; set; }
            public string Przewoznik { get; set; }
            public DateTime? DataRozliczenia { get; set; }
        }

        [Obsolete("Do usunięcia")]
        public class DokumentEwidencjiViewRow
        {
            public int ID { get; set; }
            public Enova.API.Core.StanEwidencji Stan { get; set; }
            public string KodKontrahenta { get; set; }
            public string NazwaKontrahenta { get; set; }
            public Enova.API.Core.IDokumentKsiegowalny DokHandlowy { get; set; }
            public string NumerDokumentu { get; set; }
            public DateTime DataDokumentu { get; set; }
            public decimal? WartośćDokumentu { get; set; }
            public decimal? WartośćNetto { get; set; }
            public DateTime DataRozliczenia { get; set; }
            public string Przewoźnik { get; set; }
        }

    }
}
