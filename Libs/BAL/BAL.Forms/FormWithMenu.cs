using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public partial class FormWithMenu : BAL.Forms.FormBase, IFormWithMenu
    {
        #region Fields

        #endregion

        #region Properties

        public Controls.MenuBar MenuBar
        {
            get { return this.MainMenu; }
        }

        #endregion

        #region Methods

        public FormWithMenu()
        {
            InitializeComponent();
        }

        public IList GetMenuItemCollection()
        {
            return this.MainMenu.Items;
        }

        #endregion

    }
}
