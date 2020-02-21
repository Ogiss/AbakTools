using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public partial class DataGridForm : BAL.Forms.DataForm
    {
        public GridViewContext View
        {
            get
            {
                return this.DataContext as GridViewContext;
            }
        }

        public DataGridForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.GridView.DataContext = this.DataContext;
        }
    }
}
