using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;
using BAL.Forms;

[assembly: DataPanel("Ogólne",typeof(BAL.Test.Handel.Towar), typeof(BAL.Test.Handel.TowarPanel))]

namespace BAL.Test.Handel
{
    [Priority(10)]
    public partial class TowarPanel : BAL.Forms.DataPanel
    {

        public TowarPanel()
        {
            InitializeComponent();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            
        }

        public override bool IsValid(out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                error = "Wartość pola kod nie może być pusta";
                return false;
            }
            return true;
        }

    }
}
