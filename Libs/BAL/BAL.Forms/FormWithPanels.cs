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
    public partial class FormWithPanels : BAL.Forms.FormWithMenu
    {
        #region Properties

        [Browsable(true), Category("Panels")]
        public bool PanelLeftCollapsed
        {
            get { return this.LeftPanel.Panel1Collapsed; }
            set { this.LeftPanel.Panel1Collapsed = value; }
        }

        [Browsable(true), Category("Panels")]
        public bool PanelTopCollapsed
        {
            get { return this.TopPanel.Panel1Collapsed; }
            set { this.TopPanel.Panel1Collapsed = value; }
        }

        [Browsable(true), Category("Panels")]
        public bool PanelRightCollapsed
        {
            get { return this.RightPanel.Panel2Collapsed; }
            set { this.RightPanel.Panel2Collapsed = value; }
        }

        [Browsable(true), Category("Panels")]
        public bool PanelBottomCollapsed
        {
            get { return this.CenterPanel.Panel2Collapsed; }
            set { this.CenterPanel.Panel2Collapsed = value; }
        }

        #endregion

        #region Methods

        public FormWithPanels()
        {
            InitializeComponent();
        }

        private void FormWithPanels_Load(object sender, EventArgs e)
        {

        }

        public override IList GetControlCollection()
        {
            return this.CenterPanel.Panel1.Controls;
        }

        public override Rectangle GetClientRectagle()
        {
            return this.CenterPanel.Panel1.ClientRectangle;
        }

        #endregion
    }
}
