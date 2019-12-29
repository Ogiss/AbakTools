using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Business;
using BAL.Forms;
using AbakTools.SMS;

[assembly: BAL.Forms.MenuAction(
    "Narzędzia\\Wyślij SMS",
    typeof(AbakTools.CRM.Forms.SMSSendForm),
    MenuAction = MenuActionsType.OpenFormModal,
    Priority = 640
)]

namespace AbakTools.CRM.Forms
{
    public partial class SMSSendForm : Form
    {
        public SMSSendForm()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.toTextBox.Text) &&
                !string.IsNullOrEmpty(this.msgTextBox.Text))
            {
                var sms = new SMSClient();
                try
                {
                    this.Enabled = false;
                    this.Cursor = Cursors.WaitCursor;

                    if (!sms.Send(this.toTextBox.Text, this.msgTextBox.Text))
                        FormManager.Alert("Wystapił bład w trakcie wysyłania SMSa");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    this.Enabled = true;
                }
                
            }
        }

        private void SMSSendForm_Load(object sender, EventArgs e)
        {
            /*
            using (var session = AppController.CurrentLogin.CreateSession(true, false, "Kontakty"))
            {

            }
             */

        }

        private void kontaktyButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            /*
            using (var session = BAL.Business.AppController.Instance.CurrentLogin.CreateSession(false, false, ""))
            {
                var view = new KontaktyView(session) { TylkoTelefoniczne = true, SelectionMode = true };
                if (FormManager.Instance.OpenView(view, true) == System.Windows.Forms.DialogResult.OK)
                {
                    this.toTextBox.Text = ((Kontakt)view.Current).TelefonKomorkowy.Trim().Replace(" ", "").Replace("-", "");
                }
            }
             */
        }
    }
}
