using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.MenuAction("WebTools\\Kontrahenci", typeof(AbakTools.CRM.Forms.KontrahenciForm), Priority = 1010)]

namespace AbakTools.CRM.Forms
{
    public partial class KontrahenciForm : Enova.Business.Old.Forms.DataGridForm
    {
        public KontrahenciForm()
        {
            InitializeComponent();
        }

        private void KontrahenciForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        protected override void LoadData()
        {
            this.DataSource = new Enova.Business.Old.Web.Kontrahenci();
        }

    }
}
