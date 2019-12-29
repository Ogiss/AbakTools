using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.DataPanel("Dane kontaktowe", typeof(Enova.API.CRM.Kontrahent), typeof(Enova.Forms.CRM.KontrahentKontaktyPanel))]

namespace Enova.Forms.CRM
{
    [BAL.Types.Priority(20)]
    public partial class KontrahentKontaktyPanel : BAL.Forms.DataPanel
    {
        public KontrahentKontaktyPanel()
        {
            InitializeComponent();
        }
    }
}
