using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Forms;

[assembly: DataPanel("Ogólne", typeof(Enova.Business.Old.DB.Web.Kontakt), typeof(AbakTools.CRM.Forms.KontaktOgolnePanel))]

namespace AbakTools.CRM.Forms
{
    [Priority(10), ToolboxItem(false)]
    public partial class KontaktOgolnePanel : BAL.Forms.DataPanel
    {
        public KontaktOgolnePanel()
        {
            InitializeComponent();
        }
    }
}
