using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AbakTools.Framework;
using AbakTools.EnovaApi;
using AbakTools.EnovaApi.Domain.CommercialDocument;
using AbakTools.Web;
using EnovaApi.Models.CommercialDocument;
using System.Net.Mail;
using EnovaApi.Models.Customer;

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
            var document = DependencyProvider.Resolve<ICommercialDocumentRepository>().Get(DokHandlowy.Guid);

            var mailData = PrepareMailData(document);

            if (EmailSendForm.SendMail(mailData, out string mailTo) == DialogResult.OK)
            {
                UpdateCustomerEmailAddress(document.Customer, mailTo);
            }
        }

        private MailData PrepareMailData(CommercialDocument document)
        {
            var mailData = new MailData();

            mailData.MailTo = document.Customer.Email;
            mailData.MailToName = document.Customer.Code;
            mailData.MailSubject = "ABAK DOKUMENTY";
            mailData.Attachments.Add(CreateDocumentPdfAttachment(document));

            return mailData;
        }

        private AttachmentData CreateDocumentPdfAttachment(CommercialDocument document)
        {
            return new AttachmentData
            {
                Content = DependencyProvider.Resolve<IEnovaService>().ExportDocumentToPdf(document),
                FileName = document.DocumentNumber.Replace('/', '_') + ".pdf"
            };
        }

        private void UpdateCustomerEmailAddress(Customer customer, string mailTo)
        {
            /*
            bool flag = false;
            if (string.IsNullOrEmpty(kontrahent.EMAIL))
            {
                if (MessageBox.Show("Dodać adres email do kartoteki klienta",
                    "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    flag = true;
            }
            else if (form.MailTo.ToLower() != kontrahent.EMAIL.ToLower())
            {
                if (MessageBox.Show("Wprowadzony adres email jest różny od adresu w kartotece.\r\nCzy chcesz go zamienić?",
                    "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    flag = true;
            }

            if (flag)
            {
                using (var t = session.CreateTransaction())
                {
                    kontrahent.EMAIL = form.MailTo.ToLower();
                    session.EventsInvoke();
                    t.Commit();
                }
                session.Save();
            }
            */
        }

        private void progressForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
        }

        #endregion
    }
}
