using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Forms.CRM
{
    public partial class OsobaKontaktowaEditForm : Enova.Business.Old.Forms.DataEditForm
    {
        public Enova.Business.Old.DB.KontaktOsoba KontaktOsoba
        {
            get { return (Enova.Business.Old.DB.KontaktOsoba)DataSource; }
            set { DataSource = value; }
        }

        public OsobaKontaktowaEditForm()
        {
            InitializeComponent();
        }
    }
}
