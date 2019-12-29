using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Enova.Business.Old.DB;
//using Enova.Business.Old.DB.Services;
using Enova.Business.Old.Types;

[assembly: BAL.Forms.MenuAction("Finanse\\Rozrachunki", typeof(AbakTools.Finanse.Forms.RozrachunkiForm), Priority = 310)]

namespace AbakTools.Finanse.Forms
{
    public partial class RozrachunkiForm : Enova.Forms.FormWithEnovaAPI
    {
        public RozrachunkiForm()
        {
            InitializeComponent();
        }

        private void RozrachunkiForm_Load(object sender, EventArgs e)
        {
        }


        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            createReport();
        }

        private bool drukowanoRaport;

        private void createReport()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            var przedstawiciel = kontrahentSelect.Przedstawiciel;
            var trasa = kontrahentSelect.Trasa;
            var kontrahent = kontrahentSelect.Kontrahent;
            ArrayList kontrahenciChecked = kontrahentSelect.KontrahenciIDChecked;
            int kontrahentID = (kontrahent == null || (kontrahenciChecked != null && kontrahenciChecked.Count > 0)) ? 0 : kontrahent.ID;
            

            DateTime min = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime max = new DateTime(2099, 12, 31, 0, 0, 0);

            var dc = Enova.Business.Old.Core.ContextManager.DataContext;

            List<RozrachunkiViewRow> reportSource = null;

            if (trasa == null || kontrahent != null)
            {
                reportSource =
                    (from r in dc.RozrachunkiIdx
                     join k in dc.Kontrahenci on new { PodmiotType = r.PodmiotType, Podmiot = r.Podmiot.Value } equals new { PodmiotType = "Kontrahenci", Podmiot = k.ID }
                     join fp in dc.Features on
                     new { ParentType = "Kontrahenci", Parent = k.ID, Name = "przedstawiciel" } equals
                     new { ParentType = fp.ParentType, Parent = fp.Parent, Name = fp.Name }
                     /*join ft in dc.Features on
                     new { ParentType = "Kontrahenci", Parent = k.ID, Name = "TRASY" } equals
                     new { ParentType = ft.ParentType, Parent = ft.Parent, Name = ft.Name }*/
                     where r.DataRozliczenia == max && (przedstawiciel == null || fp.Data == przedstawiciel)
                     /*&& (trasa == null || ft.Data == @"\" + przedstawiciel + @"\" + trasa + @"\")*/
                     && (kontrahentID == 0 || r.Podmiot == kontrahentID)
                     select new RozrachunkiViewRow()
                     {
                         IDRozrachunku = r.ID,
                         DataDokumentu = r.Data,
                         NumerDokumentu = r.Numer,
                         IDKontrahenta = k.ID,
                         KodKontrahenta = k.Kod,
                         NazwaKontrahenta = k.NazwaStr,
                         Kwota = (r.Typ == 10 || r.Typ == 21) ? r.KwotaValue : -r.KwotaValue,
                         KwotaRozliczona = (r.Typ == 10 || r.Typ == 21) ? r.KwotaRozliczonaValue : -r.KwotaRozliczonaValue,
                         DataRozliczenia = r.DataRozliczenia,
                         Termin = r.Termin < r.Data ? r.Data : r.Termin,
                         PrzedstawicielDokumentu = fp.Data,
                         Zapłata = r.DokumentType == "Zaplaty"
                     } into rv
                     group rv by rv.IDRozrachunku into urv
                     select urv.FirstOrDefault()).ToList();
            }
            else
            {
                reportSource =
                    (from r in dc.RozrachunkiIdx
                     join k in dc.Kontrahenci on new { PodmiotType = r.PodmiotType, Podmiot = r.Podmiot.Value } equals new { PodmiotType = "Kontrahenci", Podmiot = k.ID }
                     join fp in dc.Features on
                     new { ParentType = "Kontrahenci", Parent = k.ID, Name = "przedstawiciel" } equals
                     new { ParentType = fp.ParentType, Parent = fp.Parent, Name = fp.Name }
                     join ft in dc.Features on
                     new { ParentType = "Kontrahenci", Parent = k.ID, Name = "TRASY" } equals
                     new { ParentType = ft.ParentType, Parent = ft.Parent, Name = ft.Name }
                     where r.DataRozliczenia == max && (przedstawiciel == null || fp.Data == przedstawiciel)
                     && (trasa == null || ft.Data == @"\" + przedstawiciel + @"\" + trasa + @"\")
                     && (kontrahentID == 0 || r.Podmiot == kontrahentID)
                     select new RozrachunkiViewRow()
                     {
                         IDRozrachunku = r.ID,
                         DataDokumentu = r.Data,
                         NumerDokumentu = r.Numer,
                         IDKontrahenta = k.ID,
                         KodKontrahenta = k.Kod,
                         NazwaKontrahenta = k.NazwaStr,
                         Kwota = (r.Typ == 10 || r.Typ == 21) ? r.KwotaValue : -r.KwotaValue,
                         KwotaRozliczona = (r.Typ == 10 || r.Typ == 21) ? r.KwotaRozliczonaValue : -r.KwotaRozliczonaValue,
                         DataRozliczenia = r.DataRozliczenia,
                         Termin = r.Termin < r.Data ? r.Data : r.Termin,
                         PrzedstawicielDokumentu = fp.Data,
                         Zapłata = r.DokumentType == "Zaplaty"
                     } into rv
                     group rv by rv.IDRozrachunku into urv
                     select urv.FirstOrDefault()).ToList();
            }

            if (kontrahenciChecked != null && kontrahenciChecked.Count > 0)
            {
                reportSource = reportSource.Where(r => kontrahenciChecked.Contains(r.IDKontrahenta)).ToList();
            }

            this.reportViewer.LocalReport.ReportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports\\RozrachunkiReport.rdlc");
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.LocalReport.SetParameters(new ReportParameter[] { 
                new ReportParameter("przedstawiciel", string.IsNullOrEmpty(przedstawiciel) ? "  " : przedstawiciel) ,
                new ReportParameter("trasa", trasa == null ? "Wszystkie" : trasa)
            });
            this.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RozrachunkiViewRow", reportSource));
            this.reportViewer.RefreshReport();

            this.drukowanoRaport = true;
            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        private void sendEmailButton_Click(object sender, EventArgs e)
        {
            if (!drukowanoRaport)
            {
                MessageBox.Show("Musisz najpierw wygenerować raport");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;

            Enova.API.CRM.Kontrahent kontrahent = null;
            var service = Enova.API.EnovaService.Instance;
            using (var session = service.CreateSession())
            {

                if (kontrahentSelect.Kontrahent != null)
                {
                    try
                    {
                        kontrahent = session.GetModule<Enova.API.CRM.CRMModule>().Kontrahenci[kontrahentSelect.Kontrahent.Guid];
                    }
                    catch { }
                }

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                string fileName = "tmp\\" + Guid.NewGuid().ToString() + ".pdf";

                byte[] bytes = this.reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                var form = new AbakTools.Web.EmailSendForm();

                if (kontrahent != null)
                {
                    
                    if (!string.IsNullOrEmpty(kontrahent.KontaktEMAIL))
                        form.MailTo = kontrahent.KontaktEMAIL;
                    form.MailToName = kontrahent.Kod;
                }


                form.MailSubject = "ABAK - PŁATNOŚCI";

                form.Attachments.Add(new System.Net.Mail.Attachment(fileName)
                {
                    Name = "Abak_Platnosci_" + DateTime.Now.ToShortDateString() + ".pdf"
                });

                this.Enabled = true;
                this.Cursor = Cursors.Default;


                var result = form.ShowDialog();
                if (kontrahent != null && result == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(form.MailTo))
                    {
                        bool flag = false;
                        if (string.IsNullOrEmpty(kontrahent.KontaktEMAIL))
                        {
                            if (MessageBox.Show("Dodać adres email do kartoteki klienta",
                                "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                flag = true;
                        }
                        else if (form.MailTo.ToLower() != kontrahent.KontaktEMAIL.ToLower())
                        {
                            if (MessageBox.Show("Wprowadzony adres email jest różny od adresu w kartotece.\r\nCzy chcesz go podmienić?",
                                "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                flag = true;
                        }

                        if (flag)
                        {
                            using (var t = session.CreateTransaction())
                            {
                                kontrahent.KontaktEMAIL = form.MailTo.ToLower();
                                t.Commit();
                            }
                            session.Save();

                        }
                    }

                }
            }
        }

    }
}
