using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BAL.Types;
using BAL.Business;
using System.Windows.Forms;

[assembly: RowAction(typeof(Enova.Forms.Handel.DokumentyEmailAction), DataContextKey = "DokumentyEnovaView")]
[assembly: RowAction(typeof(Enova.Forms.Handel.DokumentyEmailAction), DataContextKey = "KontrahentEditDokumentyView")]

namespace Enova.Forms.Handel
{
    [Priority(10), Caption("Email")]
    public class DokumentyEmailAction
    {
        #region Fields

        private bool tensamKontrahent;

        #endregion

        #region Properties

        public ActionTarget Target
        {
            get { return ActionTarget.GridHeader; }
        }

        public Control Control { get; set; }

        public IList SelectedRows { get; set; }

        #endregion

        #region Methods

        public void OnSelectionChanged()
        {

            if (SelectedRows.Count > 0)
            {
                bool flag = false;
                int? ostatniKontrahent = null;
                foreach (Enova.API.Handel.DokumentHandlowy dok in this.SelectedRows)
                {
                    if (ostatniKontrahent == null)
                    {
                        flag = true;
                        ostatniKontrahent = dok.Kontrahent.ID;
                    }
                    else
                    {
                        if (ostatniKontrahent.Value != dok.Kontrahent.ID)
                        {
                            flag = false;
                            break;
                        }

                    }
                }
                tensamKontrahent = flag;
            }
            else
                tensamKontrahent = false;
        }

        /*
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
                                var kontrahent = this.DokHandlowy.Kontrahent;
                                if (!string.IsNullOrEmpty(kontrahent.KontaktEMAIL))
                                    form.MailTo = kontrahent.KontaktEMAIL;

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
                                        if (string.IsNullOrEmpty(kontrahent.KontaktEMAIL))
                                        {
                                            if (MessageBox.Show("Dodać adres email do kartoteki klienta",
                                                "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                                flag = true;
                                        }
                                        else if (form.MailTo.ToLower() != kontrahent.KontaktEMAIL.ToLower())
                                        {
                                            if (MessageBox.Show("Wprowadzony adres email jest różny od adresu w kartotece.\r\nCzy chcesz go zamienić?",
                                                "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                                flag = true;
                                        }

                                        if (flag)
                                        {
                                            using (var t = kontrahent.Session.CreateTransaction())
                                            {
                                                kontrahent.KontaktEMAIL = form.MailTo.ToLower();
                                                t.Commit();
                                            }
                                            kontrahent.Session.Save();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        throw new Exception("Nie skonfigurowano wzorców wydruku");

                }

         */

        public void OnAction()
        {
            var service = Enova.API.EnovaService.Instance;
            System.Windows.Forms.Form progressForm = new System.Windows.Forms.Form();
            if (service != null && service.IsLogined)
            {
                using (var session = service.CreateSession())
                {
                    var hm = session.GetModule<API.Handel.HandelModule>();

                    if (!this.tensamKontrahent)
                    {
                        if (MessageBox.Show("!!! UWAGA !!! Wybrane dokumenty należą do róznych kontrahentów.\r\nCzy chcesz kontynułować?",
                            "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                            return;
                    }

                    string[] files = new string[SelectedRows.Count];
                    string[] names = new string[SelectedRows.Count];
                    Enova.API.CRM.Kontrahent kontrahent = null;
                    string path = AppDomain.CurrentDomain.BaseDirectory + "tmp\\";

                    for (int i = 0; i < this.SelectedRows.Count; i++)
                    {
                        Enova.API.Handel.DokumentHandlowy dok = (Enova.API.Handel.DokumentHandlowy)this.SelectedRows[i];

                        if (kontrahent == null)
                            //kontrahent = dok.Kontrahent;
                            kontrahent = (API.CRM.Kontrahent)session.GetModule<API.CRM.CRMModule>().Kontrahenci[dok.Kontrahent.ID];

                        string template = null;
                        if (dok.Korekta)
                            template = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFKReport");
                        else
                            template = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFVReport");

                        if (string.IsNullOrEmpty(template))
                            throw new Exception("Nie skonfigurowano wzorców wydruku");

                        template = Path.IsPathRooted(template) ? template : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aspx", template);

                        files[i] = Guid.NewGuid() + ".pdf";
                        using (var wait = new BAL.Forms.FileWaitCursor(null).AddCreated(path, files[i]))
                        {
                            var fileName = Path.Combine(path, files[i]);

                            hm.DrukujDokument(
                                null,
                                dok.Guid,
                                template,
                                Enova.API.Printer.Destinations.PDF,
                                fileName);


                            files[i] = fileName;
                            names[i] = dok.NumerPelny.Replace('/', '_') + ".pdf";
                            wait.CreateEvent.WaitOne();
                        }

                    }

                    var form = new AbakTools.Web.EmailSendForm();
                    //if (kontrahent != null && !string.IsNullOrEmpty(kontrahent.KontaktEMAIL))
                    if (kontrahent != null && !string.IsNullOrEmpty(kontrahent.EMAIL))
                        form.MailTo = kontrahent.KontaktEMAIL;

                    form.MailSubject = "ABAK DOKUMENTY";

                    for (int i = 0; i < this.SelectedRows.Count; i++)
                    {
                        DateTime now = DateTime.Now;
                        while (true)
                        {
                            try
                            {
                                var attach = new System.Net.Mail.Attachment(files[i]);
                                attach.Name = names[i];
                                form.Attachments.Add(attach);
                                break;
                            }
                            catch (Exception ex)
                            {
                                var diff = DateTime.Now - now;
                                if (diff > TimeSpan.FromSeconds(30))
                                    throw new Exception("Przekroczono limit czasu dostepnego na wygenerowanie wydruku", ex);
                            }
                        }
                    }

                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(form.MailTo) && this.tensamKontrahent)
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
                }
            }


        }

        #endregion
    }
}
