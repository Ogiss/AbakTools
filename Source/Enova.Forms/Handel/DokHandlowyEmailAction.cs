using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

[assembly: BAL.Types.Action(
    ActionType = typeof(Enova.Forms.Handel.DokHandlowyEmailAction),
    DataType = typeof(Enova.API.Handel.DokumentHandlowy),
    Path = "Wyślij email",
    Description = "Wysłanie faktury email-em",
    Priority = 30,
    Target = BAL.Types.ActionTarget.FormMenu
    )]


namespace Enova.Forms.Handel
{
    public class DokHandlowyEmailAction : BAL.Business.IDataContexable
    {
        #region Fields

        private Exception exception;

        #endregion

        #region Properties

        public BAL.Business.DataContext DataContext { get; set; }

        public Enova.API.Handel.DokumentHandlowy DokHandlowy
        {
            get
            {
                if (this.DataContext != null)
                    return this.DataContext.GetData<Enova.API.Handel.DokumentHandlowy>();
                return null;
            }
        }

        #endregion

        #region Methods

        public void Action()
        {
            string template = null;
            if (DokHandlowy.Korekta)
                template = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFKReport");
            else
                template = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFVReport");

            if (!string.IsNullOrEmpty(template))
            {
                template = Path.IsPathRooted(template) ? template : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aspx", template);
                string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp\\" + Guid.NewGuid().ToString() + ".pdf");
                var service = Enova.API.EnovaService.Instance;
                if (service != null && service.IsLogined)
                {
                    using (var session = service.CreateSession())
                    {
                        System.Windows.Forms.Form progressForm = new System.Windows.Forms.Form();
                        progressForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(progressForm_FormClosing);
                        progressForm.Text = "Progress FORM";

                        session.GetModule<API.Handel.HandelModule>().DrukujDokument(
                            null,
                            DokHandlowy.Guid,
                            template,
                            Enova.API.Printer.Destinations.PDF,
                            fileName);

                        var form = new AbakTools.Web.EmailSendForm();
                        //var kontrahent = this.DokHandlowy.Kontrahent;
                        var kontrahent = (API.CRM.Kontrahent)session.GetModule<API.CRM.CRMModule>().Kontrahenci[this.DokHandlowy.Kontrahent.ID];
                        /*
                        if (!string.IsNullOrEmpty(kontrahent.KontaktEMAIL))
                            form.MailTo = kontrahent.KontaktEMAIL;
                        */

                        if (!string.IsNullOrEmpty(kontrahent.EMAIL))
                            form.MailTo = kontrahent.EMAIL;


                        form.MailToName = kontrahent.Kod;

                        form.MailSubject = "ABAK DOKUMENTY";

                        DateTime now = DateTime.Now;


                        while (true)
                        {
                            try
                            {
                                var attach = new System.Net.Mail.Attachment(fileName);
                                attach.Name = this.DokHandlowy.NumerPelny.Replace('/', '_') + ".pdf";
                                form.Attachments.Add(attach);
                                break;
                            }
                            catch (Exception ex)
                            {
                                var diff = DateTime.Now - now;
                                if (diff > TimeSpan.FromSeconds(15))
                                    throw new Exception("Przekroczono limit czasu dostepnego na wygenerowanie wydruku", ex);
                            }
                        }

                        if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (!string.IsNullOrEmpty(form.MailTo))
                            {
                                bool flag = false;
                                //if (string.IsNullOrEmpty(kontrahent.KontaktEMAIL))
                                if (string.IsNullOrEmpty(kontrahent.EMAIL))
                                {
                                    if (MessageBox.Show("Dodać adres email do kartoteki klienta",
                                        "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                        flag = true;
                                }
                                //else if (form.MailTo.ToLower() != kontrahent.KontaktEMAIL.ToLower())
                                else if (form.MailTo.ToLower() != kontrahent.EMAIL.ToLower())
                                {
                                    if (MessageBox.Show("Wprowadzony adres email jest różny od adresu w kartotece.\r\nCzy chcesz go zamienić?",
                                        "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        flag = true;
                                }

                                if (flag)
                                {
                                    //using (var t = kontrahent.Session.CreateTransaction())
                                    using (var t = session.CreateTransaction())
                                    {
                                        //kontrahent.KontaktEMAIL = form.MailTo.ToLower();
                                        kontrahent.EMAIL = form.MailTo.ToLower();
                                        session.EventsInvoke();
                                        t.Commit();
                                    }
                                    //kontrahent.Session.Save();
                                    session.Save();
                                }
                            }
                        }
                    } // End using session



                } //End serwice
            }
            else
                throw new Exception("Nie skonfigurowano wzorców wydruku");

        }

        private void progressForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
        }

        #endregion
    }
}
