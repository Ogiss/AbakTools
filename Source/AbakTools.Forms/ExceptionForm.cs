using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Forms
{
    public partial class ExceptionForm : BAL.Forms.ExceptionForm
    {
        public ExceptionForm()
        {
            InitializeComponent();
        }

        public ExceptionForm(Exception ex)
            : base(ex)
        {
            InitializeComponent();
        }

        protected override string GetExceptionTypeName(Exception ex)
        {
            if (ex is Enova.API.SonetaException)
                return ((Enova.API.SonetaException)ex).ExeptionType;
            return base.GetExceptionTypeName(ex);
        }
    }
}
