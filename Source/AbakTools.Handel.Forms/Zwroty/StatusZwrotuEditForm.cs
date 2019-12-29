using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;


namespace AbakTools.Zwroty.Forms
{
    public partial class StatusZwrotuEditForm : Enova.Business.Old.Forms.DataEditForm
    {
        bool fireCheckedChanged = true;

        public StatusZwrotuEditForm()
        {
            InitializeComponent();
        }

        private void kolorButton_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ((StatusZwrotu)this.DataSource).Kolor = ColorTranslator.ToHtml(colorDialog.Color);
                kolorButton.BackColor = colorDialog.Color;
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fireCheckedChanged)
            {
                RadioButton rb = (RadioButton)sender;
                if (rb.Checked)
                {
                    var nr = rb.Name.Replace("radioButton", "");
                    ((StatusZwrotu)this.DataSource).Typ = int.Parse(nr);
                }
            }
        }

        private void StatusZwrotuEditForm_Load(object sender, EventArgs e)
        {
            fireCheckedChanged = false;

            if (this.DataSource != null)
            {
                string name = "radioButton" + ((StatusZwrotu)this.DataSource).Typ.ToString();
                RadioButton rb = (RadioButton)this.groupBox1.Controls[name];
                rb.Checked = true;

                Color color = ColorTranslator.FromHtml(((StatusZwrotu)this.DataSource).Kolor);
                this.kolorButton.BackColor = color;
            }

            fireCheckedChanged = true;
        }
    }
}
