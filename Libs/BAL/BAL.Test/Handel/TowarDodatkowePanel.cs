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

[assembly: DataPanel("Dodatkowe", typeof(BAL.Test.Handel.Towar), typeof(BAL.Test.Handel.TowarDodatkowePanel))]


namespace BAL.Test.Handel
{
    public partial class TowarDodatkowePanel : BAL.Forms.DataPanel
    {
        public TowarDodatkowePanel()
        {
            InitializeComponent();
        }
    }
}
