using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;

namespace AbakTools.CRM.Forms
{
    public partial class RodzajKorespondencjiEditForm : Enova.Business.Old.Forms.DataEditForm
    {
        public RodzajKorespondencji RodzajKorespondencji
        {
            get
            {
                if (DataSource == null)
                    DataSource = new RodzajKorespondencji();
                return (RodzajKorespondencji)DataSource;
            }
        }

        public RodzajKorespondencjiEditForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void RodzajKorespondencjiEditForm_Load(object sender, EventArgs e)
        {
            kosztTextBox.Text = string.Format("{0:C}", RodzajKorespondencji.KosztZnaczkow);
            nazwaTextBox.Focus();
        }

        protected override void OnBeforeSaveChanges(EventArgs e)
        {
            base.OnBeforeSaveChanges(e);

            decimal koszt = 0;

            if (decimal.TryParse(kosztTextBox.Text, out koszt))
            {
                RodzajKorespondencji.KosztZnaczkow = decimal.Round(koszt,2);
            }

        }
    }
}
