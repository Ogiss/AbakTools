using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Forms;

//[assembly: DataPanel("Ogólne", typeof(AbakTools.Core.StatusDokumentu), typeof(AbakTools.Core.Forms.StatusDokumentuOgolnePanel))]
[assembly: DataPanel("Ogólne", typeof(Enova.Business.Old.DB.Web.StatusDokumentu), typeof(AbakTools.Core.Forms.StatusDokumentuOgolnePanel))]


namespace AbakTools.Core.Forms
{
    [Priority(10), ToolboxItem(false)]
    public partial class StatusDokumentuOgolnePanel : BAL.Forms.DataPanel
    {

        private bool fireCheckBoxChecked;

        private Enova.Business.Old.DB.Web.StatusDokumentu Row
        {
            get
            {
                return (Enova.Business.Old.DB.Web.StatusDokumentu)this.DataContext.GetData();
            }
        }

        public StatusDokumentuOgolnePanel()
        {
            InitializeComponent();
        }

        protected override void OnBeforeBinding(EventArgs e)
        {
            var dce = e as DataContextEventArgs;
            if (dce != null)
            {
                //kategoriaComboBox.DataSource = CoreModule.GetInstance(dce.DataContext).KategorieStatusowDokumentow;
                kategoriaComboBox.DataSource = new AbakTools.Core.KategorieStatusowDokumentow();
            }
            base.OnBeforeBinding(e);
        }

        protected override void OnBindingComplete(EventArgs e)
        {
            base.OnBindingComplete(e);
            this.initOptions();
            if (!string.IsNullOrEmpty(Row.Kolor))
            {
                Color c = ColorTranslator.FromHtml(Row.Kolor);
                kolorButton.BackColor = c;
                colorDialog.Color = c;
            }
        }

        private void initOptions()
        {
            foreach (CheckBox cb in opcjeGroupBox.Controls)
            {
                OpcjeStatusuDokumentu opt = (OpcjeStatusuDokumentu)int.Parse(cb.Tag.ToString());
                if ((Row.Opcje & opt) == opt)
                    cb.Checked = true;

            }

            fireCheckBoxChecked = true;
        }

        private void opcjeCheckedChanged(object sender, EventArgs e)
        {
            if (fireCheckBoxChecked)
            {
                CheckBox cb = (CheckBox)sender;
                OpcjeStatusuDokumentu opt = (OpcjeStatusuDokumentu)int.Parse(cb.Tag.ToString());
                if (cb.Checked)
                {
                    Row.Opcje |= opt;
                }
                else
                {
                    Row.Opcje &= ~opt;
                }
            }
        }

        private void kolorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Row.Kolor = ColorTranslator.ToHtml(colorDialog.Color);
                kolorButton.BackColor = colorDialog.Color;
            }
        }

    }
}
