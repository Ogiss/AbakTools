using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Forms;

//[assembly: DataPanel("Kontakt", typeof(AbakTools.Business.Operator), typeof(AbakTools.Business.Forms.OperatorKontaktPanel))]
[assembly: DataPanel("Kontakt", typeof(Enova.Business.Old.DB.Web.Operator), typeof(AbakTools.Business.Forms.OperatorKontaktPanel))]

namespace AbakTools.Business.Forms
{
    [Priority(30)]
    public partial class OperatorKontaktPanel : BAL.Forms.DataPanel
    {
        public OperatorKontaktPanel()
        {
            InitializeComponent();
        }
    }
}
