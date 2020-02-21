using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public partial class FormBase : Form , IControlContainer, IControlKey
    {
        #region Fields

        private bool isLoaded;

        #endregion

        #region Properties

        [Browsable(false)]
        public bool IsLoaded
        {
            get { return this.isLoaded; }
        }

        [Browsable(false)]
        public virtual string Key
        {
            get { return this.Name; }
        }

        [Browsable(false)]
        public bool HideOnClose { get; set; }

        #endregion

        #region Methods

        public FormBase()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.isLoaded = true;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                this.Select();
                if (this.HideOnClose)
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.HideOnClose)
            {
                e.Cancel = true;
                if (this.DialogResult == System.Windows.Forms.DialogResult.None)
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Hide();
            }
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (this.DialogResult == System.Windows.Forms.DialogResult.None)
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public virtual IList GetControlCollection()
        {
            return this.Controls;
        }

        public virtual Rectangle GetClientRectagle()
        {
            return this.ClientRectangle;
        }

        #endregion

        #region Events
        #endregion
    }
}
