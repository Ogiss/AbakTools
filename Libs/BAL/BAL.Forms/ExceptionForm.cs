using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public partial class ExceptionForm : BAL.Forms.FormBase
    {
        #region Fields

        private Exception exception;

        #endregion

        public ExceptionForm()
        {
            InitializeComponent();
        }

        public ExceptionForm(Exception ex)
        {
            InitializeComponent();
            this.exception = ex;
            this.Text = FormManager.FormService.ApplicationName;
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {
            if (this.exception != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Wystąpił wyjątek: " + this.GetExceptionTypeName(this.exception));
                builder.AppendLine(this.GetExceptionMessage(this.exception));
                if (this.exception.InnerException != null)
                {
                    builder.AppendLine();
                    builder.AppendLine();
                    builder.Append("Wyjatek wewnętrzny: ").AppendLine(this.GetExceptionTypeName(this.exception.InnerException));
                    builder.Append(this.GetExceptionMessage(this.exception.InnerException));
                }
                this.textBox.Text = builder.ToString();
            }
        }

        private void detailButton_Click(object sender, EventArgs e)
        {
            new ExceptionDetailForm(this.exception).ShowDialog();
        }

        public static void Show(Exception ex)
        {
            new ExceptionForm(ex).ShowDialog();
        }

        protected virtual string GetExceptionMessage(Exception ex)
        {
            if (ex is IExceptionInfo)
            {
                return ((IExceptionInfo)ex).GetExeptionMessage();
            }
            return ex.Message;
        }

        protected virtual string GetExceptionTypeName(Exception ex)
        {
            if (ex is IExceptionInfo)
            {
                return ((IExceptionInfo)ex).GetExeprtionTypeName();
            }
            return ex.GetType().Name;
        }

    }
}
