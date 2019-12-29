using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Zamowienia.Forms
{
    public partial class AddMessageForm : Form
    {
        public Enova.Business.Old.DB.Web.Wiadomosc DataSource
        {
            get
            {
                return (Enova.Business.Old.DB.Web.Wiadomosc)bindingSource.DataSource;
            }
            set
            {
                bindingSource.DataSource = value;
            }
        }

        public AddMessageForm()
        {
            InitializeComponent();
        }

        private void AddMessageForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                bindingSource.EndEdit();
                if (!string.IsNullOrEmpty(((Enova.Business.Old.DB.Web.Wiadomosc)bindingSource.DataSource).Tekst))
                    this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
