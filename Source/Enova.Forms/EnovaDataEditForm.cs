using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace Enova.Forms
{
    public partial class EnovaDataEditForm : BAL.Forms.DataEditForm
    {
        private ToolStripDropDownButton printButton;

        public EnovaDataEditForm()
        {
            InitializeComponent();
        }

        private void EnovaDataEditForm_Load(object sender, EventArgs e)
        {
            if (DataContext != null && DataContext.Current != null)
            {
                var attrs = API.Printer.RowPrintTemplateAttribute.GetAttributes(DataContext.Current);
                if (attrs.Count() > 0)
                {
                    var image = (System.Drawing.Image)BAL.Forms.Properties.Resources.Print;
                    printButton = new ToolStripDropDownButton("", image);
                    printButton.Click +=new EventHandler(printButton_Click);
                    printButton.ToolTipText = "Drukuj";
                    //printButton.Tag = attrs.FirstOrDefault();

                    foreach (var attr in attrs)
                    {
                        var item = new ToolStripMenuItem(attr.Name, null, printButtonDropDownItem_Click);
                        item.Tag = attr;
                        printButton.DropDownItems.Add(item);
                    }

                    this.ToolBar.Items.Add(printButton);

                    if (this.DataContext.Current is API.CRM.ISendEmail)
                        this.ToolBar.Items.Add(new ToolStripButton("", (Image)BAL.Forms.Properties.Resources.SendEmail, sendEmailButton_Click) { 
                            Tag = attrs.FirstOrDefault(), ToolTipText = "Wyślij" });
                }
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            var attr = printButton.Tag as API.Printer.RowPrintTemplateAttribute;
            if (attr != null)
                API.EnovaService.Instance.DrukujRow(this, (API.Business.Row) this.DataContext.Current, attr.Template);
        }

        private void printButtonDropDownItem_Click(object sender, EventArgs e)
        {
            var attr = ((ToolStripMenuItem)sender).Tag as API.Printer.RowPrintTemplateAttribute;
            if (attr != null)
                API.EnovaService.Instance.DrukujRow(this, (API.Business.Row)this.DataContext.Current, attr.Template);

        }

        private void sendEmailButton_Click(object sender, EventArgs e)
        {
            var attr = ((ToolStripButton)sender).Tag as API.Printer.RowPrintTemplateAttribute;
            var service = Enova.API.EnovaService.Instance;
            var sendInfo = ((API.CRM.ISendEmail)this.DataContext.Current).GetEmail();
            if (attr != null && sendInfo != null && service != null && service.IsLogined)
            {
                string fileName = "tmp\\" + Guid.NewGuid().ToString() + ".pdf";
                System.Windows.Forms.Form progressForm = new System.Windows.Forms.Form();
                progressForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler((owner, args) =>
                {
                });
                progressForm.Text = "Progress FORM";

                service.DrukujRow(progressForm, (API.Business.Row)DataContext.Current, attr.Template, API.Printer.Destinations.PDF, fileName);
                var form = new AbakTools.Web.EmailSendForm();
                form.MailTo = sendInfo.Email;
                form.MailToName = sendInfo.To;
                form.MailSubject = sendInfo.Subject;
                DateTime now = DateTime.Now;

                while (true)
                {
                    try
                    {
                        var attach = new System.Net.Mail.Attachment(fileName);
                        attach.Name = sendInfo.AttachName.Replace('/', '_') + ".pdf";
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

                form.ShowDialog();

            }
        }
    }
}
