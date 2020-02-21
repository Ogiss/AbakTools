using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Forms;

[assembly: DataPanel("Ogólne", typeof(BAL.Test.Handel.Dokument), typeof(BAL.Test.Handel.DokumentOgolnePanel))]

namespace BAL.Test.Handel
{
    public partial class DokumentOgolnePanel : BAL.Forms.DataPanel
    {
        public DokumentOgolnePanel()
        {
            InitializeComponent();
        }

        protected override void OnBeforeBinding(EventArgs e)
        {
            base.OnBeforeBinding(e);
        }

    }
}
