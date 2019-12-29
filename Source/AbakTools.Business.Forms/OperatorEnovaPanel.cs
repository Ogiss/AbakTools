using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Forms;

//[assembly: DataPanel("Enova", typeof(AbakTools.Business.Operator), typeof(AbakTools.Business.Forms.OperatorEnovaPanel))]
[assembly: DataPanel("Enova", typeof(Enova.Business.Old.DB.Web.Operator), typeof(AbakTools.Business.Forms.OperatorEnovaPanel))]

namespace AbakTools.Business.Forms
{
    [Priority(40)]
    public partial class OperatorEnovaPanel : BAL.Forms.DataPanel
    {
        public OperatorEnovaPanel()
        {
            InitializeComponent();
        }
    }
}
