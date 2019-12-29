using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;

namespace EnovaTools.Forms.Web
{
    public partial class StatusZamowieniaEditForm : Enova.Business.Old.Forms.DataEditForm
    {
        public StatusZamowieniaEditForm()
        {
            InitializeComponent();
        }

        private void kolorButton_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((StatusZamowienia)DataSource).Kolor = ColorTranslator.ToHtml(colorDialog.Color);
                kolorButton.BackColor = colorDialog.Color;
            }
        }

        private void StatusZamowieniaEditForm_Load(object sender, EventArgs e)
        {
            if (DataSource != null && !string.IsNullOrEmpty(((StatusZamowienia)DataSource).Kolor))
            {
                kolorButton.BackColor = ColorTranslator.FromHtml(((StatusZamowienia)DataSource).Kolor);
            }
        }
    }
}
