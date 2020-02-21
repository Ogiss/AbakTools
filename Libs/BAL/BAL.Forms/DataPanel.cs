using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;

namespace BAL.Forms
{
    [ToolboxItem(false)]
    public partial class DataPanel : UserControl, IDataContexable, IValidator, IEndEdit
    {
        #region Fields

        private DataContext context;

        #endregion

        #region Properties

        public virtual DataContext DataContext
        {
            get { return this.context; }
            set
            {
                if (!this.DesignMode)
                {
                    this.OnBeforeBinding(new DataContextEventArgs(value));
                    this.context = value;
                    if (this.context != null)
                    {
                        if (this.context.ReadOnly)
                            SetReadOnly(this, true);
                        //this.BindingSource.DataSource = this.context.GetData();
                        this.BindingSource.DataSource = this.context;
                    }
                    this.OnBindingComplete(new EventArgs());
                }
            }
        }

        #endregion

        #region Methods

        public DataPanel()
        {
            InitializeComponent();
        }

        public virtual void EndEdit()
        {
            this.BindingSource.EndEdit();
        }

        protected virtual void OnBeforeBinding(EventArgs e)
        {
            if (this.BeforeBinding != null)
                this.BeforeBinding(this, e);
        }

        protected virtual void OnBindingComplete(EventArgs e)
        {
            if (this.BindingComplete != null)
                this.BindingComplete(this, e);
        }

        public virtual bool IsValid(out string msg)
        {
            msg = null;
            return true;
        }

        public virtual void SetReadOnly(Control parent, bool readOnly)
        {
            foreach (Control ctr in parent.Controls)
            {
                var t = ctr.GetType();
                if (!typeof(Label).IsAssignableFrom(t) &&
                    !typeof(GroupBox).IsAssignableFrom(t))
                {
                    if (typeof(TextBoxBase).IsAssignableFrom(t))
                        ((TextBoxBase)ctr).ReadOnly = readOnly;
                    else if (typeof(BAL.Forms.Controls.BALControl).IsAssignableFrom(t))
                        ((BAL.Forms.Controls.BALControl)ctr).ReadOnly = readOnly;
                    else
                        ctr.Enabled = !readOnly;
                }
                ctr.TabStop = !readOnly;
                if (!typeof(BAL.Forms.Controls.BALControl).IsAssignableFrom(t))
                    SetReadOnly(ctr, readOnly);
            }
        }

        #endregion

        #region Events

        public event EventHandler BindingComplete;
        public event EventHandler BeforeBinding;

        #endregion

    }
}
