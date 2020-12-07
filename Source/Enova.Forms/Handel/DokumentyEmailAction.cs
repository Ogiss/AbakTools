using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BAL.Types;
using BAL.Business;
using System.Windows.Forms;
using AbakTools.Web;
using EnovaApi.Models.Customer;
using AbakTools.EnovaApi.Domain.CommercialDocument;
using AbakTools.Framework;
using Enova.API.Handel;
using EnovaApi.Models.CommercialDocument;
using AbakTools.EnovaApi;
using BAL.Forms;

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

        public void OnAction()
        {
            var form = Control.FindForm();

            using (new WaitCursor(form))
            {
                if (!this.tensamKontrahent)
                {
                    if (MessageBox.Show("!!! UWAGA !!! Wybrane dokumenty należą do róznych kontrahentów.\r\nCzy chcesz kontynułować?",
                        "AbakTools", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        return;
                }

                var mailData = PrepareMailData();

                if (mailData != null && EmailSendForm.SendMail(mailData, out string mailTo) == DialogResult.OK)
                {
                    //UpdateCustomerEmailAddress(document.Customer, mailTo); // TODO: Make common base class for email sending action
                }
            }
        }

        private MailData PrepareMailData()
        {
            var attachments = GetAttachments(DependencyProvider.Resolve<ICommercialDocumentRepository>(), out Customer customer);

            if (attachments.Any() && customer != null)
            {
                var mailData = new MailData
                {
                    MailTo = customer.Email,
                    MailToName = customer.Code,
                    MailSubject = "ABAK DOKUMENTY"
                };

                mailData.Attachments.AddRange(attachments);

                return mailData;
            }

            return null;
        }

        private IEnumerable<AttachmentData> GetAttachments(ICommercialDocumentRepository documentRepository, out Customer customer)
        {
            customer = null;
            List<AttachmentData> attachments = new List<AttachmentData>();

            foreach (var row in SelectedRows)
            {
                var document = documentRepository.Get((row as DokumentHandlowy).Guid);

                if (document != null)
                {
                    if (customer == null)
                    {
                        customer = document.Customer;
                    }

                    attachments.Add(CreateDocumentPdfAttachment(document));
                }
            }

            return attachments;
        }

        private AttachmentData CreateDocumentPdfAttachment(CommercialDocument document)
        {
            return new AttachmentData
            {
                Content = DependencyProvider.Resolve<IEnovaService>().ExportDocumentToPdf(document),
                FileName = document.DocumentNumber.Replace('/', '_') + ".pdf"
            };
        }

        #endregion
    }
}
