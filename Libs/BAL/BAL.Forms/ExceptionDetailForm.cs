using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BAL.Forms
{
    public partial class ExceptionDetailForm : BAL.Forms.FormBase
    {

        #region Fields

        private Exception exception;

        #endregion


        public ExceptionDetailForm(Exception ex)
        {
            InitializeComponent();
            this.exception = ex;
        }

        private void ExceptionDetailForm_Load(object sender, EventArgs e)
        {
            if (this.exception != null)
            {
                this.textBox.Text = this.getExeptionText(this.exception);
            }
        }

        private string getExeptionText(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Wyjatek: " + ex.GetType().Name);
            sb.AppendLine("Wiadomość: " + ex.Message);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine();
            sb.AppendLine();
            if (ex.InnerException != null)
                sb.AppendLine(this.getExeptionText(ex.InnerException));

            return ex.ToString();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(this.textBox.Text);
        }
    }
}
