using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms.Controls
{
    public partial class Grid : BAL.Forms.Controls.BALControl
    {

        #region Methods

        public Grid()
        {
            InitializeComponent();
        }

        protected override void OnAfterBinding(EventArgs e)
        {
            base.OnAfterBinding(e);
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion
    }
}
