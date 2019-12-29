using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Forms
{
    public partial class NavigationDataEditForm : Enova.Business.Old.Forms.DataEditForm
    {
        public NavigationDataEditForm()
        {
            InitializeComponent();
        }

        private void zatwierdzButton_Click(object sender, EventArgs e)
        {
            if (!IsReadOnly)
                SaveChanges();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (DataSourceBinding.Current != null && DataSourceBinding.Current is Enova.Business.Old.Core.IPrintable)
            {
               Enova.Business.Old.Core.IPrintable p = (Enova.Business.Old.Core.IPrintable)DataSourceBinding.Current;
                new AbakTools.Printer.ReportForm(p.ReportTitle, p.ReportPath, p.ReportDataSources).Show();
            }
        }
    }
}
