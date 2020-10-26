using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace AbakTools.Web
{
    public partial class EmailSendForm : Form
    {

        private List<Attachment> attachments;
        private string mailToName;
        private BAL.Business.Session session;

        public string MailTo
        {
            get { return this.mailToTextBox.Text; }
            set { this.mailToTextBox.Text = value; }
        }

        public string MailToName
        {
            get { return this.mailToName; }
            set { this.mailToName = value; }
        }

        public string MailSubject
        {
            get { return this.mailSubjectTextBox.Text; }
            set { this.mailSubjectTextBox.Text = value; }
        }

        public string MailBody
        {
            get { return this.mailBodyTextBox.Text; }
            set { this.mailBodyTextBox.Text = value; }
        }

        public List<Attachment> Attachments
        {
            get
            {
                if (this.attachments == null)
                    this.attachments = new List<Attachment>();
                return this.attachments;

            }
        }

        public BAL.Business.Session Session
        {
            get
            {
                if (this.session == null)
                {
                    this.session = BAL.Business.AppController.Instance.CurrentLogin.CreateSession(false, false, "EmailSendForm");
                }
                return this.session;
            }
        }

        public EmailSendForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Attachments.Count > 0)
                this.attachmentsButton.Text = "Załączniki: " + this.Attachments.Count.ToString();

            this.initAutoComplete();
        }

        private void initAutoComplete()
        {
            /*
            foreach (var kontakt in AbakTools.CRM.CRMModule.GetInstance(this.Session).Kontakty.WgEmail.NotNullOrEmpty)
                this.mailToTextBox.AutoCompleteCustomSource.Add(kontakt.EMAIL);
             */
            foreach (var kontakt in Enova.Business.Old.Core.ContextManager.WebContext.Kontakty.Where(r=>r.EMAIL != null && r.EMAIL!="").ToList())
                this.mailToTextBox.AutoCompleteCustomSource.Add(kontakt.EMAIL);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (this.session != null)
            {
                this.session.Dispose();
                this.session = null;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.Enabled = false;

                if (string.IsNullOrEmpty(this.mailToTextBox.Text))
                {
                    MessageBox.Show("!!! BRAK ADRESU ODBIORCY !!!");
                    return;
                }

                MailAddress from = new MailAddress("abak145@gmail.com", "ABAK");
                MailAddress to = new MailAddress(this.mailToTextBox.Text);
                MailMessage msg = new MailMessage(from, to);
                msg.Subject = this.mailSubjectTextBox.Text;
                msg.SubjectEncoding = Encoding.UTF8;
                if (!string.IsNullOrEmpty(this.mailBodyTextBox.Text))
                {
                    msg.Body = this.mailBodyTextBox.Text;
                    msg.BodyEncoding = Encoding.UTF8;
                }

                foreach (var attach in this.Attachments)
                    msg.Attachments.Add(attach);

                if(potwierdzenieCheckBox.Checked)
                {
                    msg.Headers.Add("Disposition-Notification-To", "<abak145@gmail.com>");
                    msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                }

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                // The server requires user's credentials
                // not the default credentials
                client.UseDefaultCredentials = false;
                // Provide your credentials
                client.Credentials = new System.Net.NetworkCredential("abak145@gmail.com", "pdljlgezqylesfot");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                // Use SendAsync to send the message asynchronously
                client.Send(msg);
                this.dodajEmailDoKontaktow(this.mailToTextBox.Text);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Enabled = true;
                this.Cursor = Cursors.Default;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void attachmentsButton_Click(object sender, EventArgs e)
        {
            new AttachmentsForm()
            {
                Attachments = this.Attachments
            }.ShowDialog();
        }

        private void mailToTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dodajEmailDoKontaktow(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                //var crm = AbakTools.CRM.CRMModule.GetInstance(this.Session);
                var dc = Enova.Business.Old.Core.ContextManager.WebContext;
                //var kontakt = crm.Kontakty.WgEmail[email];
                var kontakt = dc.Kontakty.Where(r => r.EMAIL == email).FirstOrDefault();
                if (kontakt == null)
                {
                    /*
                    kontakt = new CRM.Kontakt();
                    crm.Kontakty.AddRow(kontakt);
                    kontakt.EMAIL = email;
                    if (!string.IsNullOrEmpty(this.mailToName))
                        kontakt.Nazwa = this.mailToName;
                   crm.Kontakty.AddRow(kontakt);
                    this.Session.Save();
                     */
                    kontakt = new Enova.Business.Old.DB.Web.Kontakt()
                    {
                        EMAIL = email,
                        Nazwa = this.mailToName
                    };
                    dc.Kontakty.AddObject(kontakt);
                    dc.SaveChanges();
                }
            }
        }

        private void addressBookButton_Click(object sender, EventArgs e)
        {
            var view = new KontaktyView();
            view.SelectionMode = true;
            if (BAL.Forms.FormManager.Instance.OpenView(view, true) == System.Windows.Forms.DialogResult.OK)
            {
                var kontakt = view.GetData<Enova.Business.Old.DB.Web.Kontakt>();
                this.mailToTextBox.Text = kontakt.EMAIL;
            }

        }
    }
}
