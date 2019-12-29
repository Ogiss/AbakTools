using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Forms;
using AbakTools.Core;

//[assembly: DataPanel("Algorytmy", typeof(AbakTools.Core.StatusDokumentu), typeof(AbakTools.Core.Forms.StatusDokumentuAlgorytmPanel))]
[assembly: DataPanel("Algorytmy", typeof(Enova.Business.Old.DB.Web.StatusDokumentu), typeof(AbakTools.Core.Forms.StatusDokumentuAlgorytmPanel))]

namespace AbakTools.Core.Forms
{
    [Priority(30), ToolboxItem(false)]
    public partial class StatusDokumentuAlgorytmPanel : BAL.Forms.DataPanel
    {
        private bool fireCheck;
        private bool fireTextChanged;

        private Enova.Business.Old.DB.Web.StatusDokumentu Row
        {
            get { return DataContext != null ? (Enova.Business.Old.DB.Web.StatusDokumentu)DataContext.GetData() : null; }
        }

        public StatusDokumentuAlgorytmPanel()
        {
            InitializeComponent();
        }

        protected override void OnBindingComplete(EventArgs e)
        {
            var opcje = Row.Algorytm;
            if (opcje != AlgorytmStatusuDokumentu.Brak)
            {
                fireCheck = false;
                foreach (CheckBox box in opcjeGroupBox.Controls)
                {
                    AlgorytmStatusuDokumentu o = (AlgorytmStatusuDokumentu)int.Parse(box.Tag.ToString());
                    if ((o & opcje) == o)
                        box.Checked = true;

                }
                fireCheck = true;
            }

            if (Row.Code != null)
            {
                fireTextChanged = false;
                codeTextBox.Text = Row.Code.Replace("//", "\r\n");
                fireTextChanged = true;
            }

            base.OnBindingComplete(e);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fireCheck)
            {
                var box = (CheckBox)sender;
                AlgorytmStatusuDokumentu o = (AlgorytmStatusuDokumentu)int.Parse(box.Tag.ToString());
                if (box.Checked)
                {
                    Row.Algorytm |= o;
                }
                else
                {
                    Row.Algorytm &= ~o;
                }
            }
        }

        private void codeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (fireTextChanged)
            {
                Row.Code = codeTextBox.Text.Replace("\r\n", "//");
            }
        }
    }
}
