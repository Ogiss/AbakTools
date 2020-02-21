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

namespace BAL.Forms.Controls
{
    public partial class BALControl : UserControl , IDataContexable, INotifyValueChanged, IValue
    {
        #region Fields

        private DataContext dataContext;
        private bool readOnly;

        #endregion

        #region Properties

        [Browsable(true)]
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                OnReadOnlyChanged(new EventArgs());
            }
        }

        [Browsable(false)]
        public DataContext DataContext
        {
            get { return this.dataContext; }
            set
            {
                this.OnBeforeBinding(new DataContextEventArgs(value));
                this.dataContext = value;
                this.OnAfterBinding(new DataContextEventArgs(this.dataContext));
            }
        }

        [Browsable(false)]
        public virtual object Value
        {
            get { return null; }
        }

        #endregion

        #region Methods

        public BALControl()
        {
            InitializeComponent();
        }

        public Session GetSession()
        {
            if (this.DataContext != null)
                return this.DataContext.Session;
            Control parent = this.Parent;

            while (parent != null)
            {
                if (parent is ISessionable)
                    return ((ISessionable)parent).Session;

                if (parent is IDataContexable)
                    return ((IDataContexable)parent).DataContext.Session;

                parent = parent.Parent;
            }

            return null;
        }

        protected virtual void OnBeforeBinding(EventArgs e)
        {
            if (this.BeforeBinding != null)
                this.BeforeBinding(this, e);
        }

        protected virtual void OnAfterBinding(EventArgs e)
        {
            if (this.AfterBinding != null)
                this.AfterBinding(this, e);
        }

        protected virtual void OnValueChanging(CancelWithDataEventArgs e)
        {
            if (this.ValueChanging != null)
                this.ValueChanging(this, e);
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, e);
        }

        protected virtual void OnReadOnlyChanged(EventArgs e)
        {
            if (this.ReadOnlyChanged != null)
                this.ReadOnlyChanged(this, e);
        }

        #endregion

        #region Events

        public event EventHandler BeforeBinding;
        public event EventHandler AfterBinding;
        public event EventHandler<CancelWithDataEventArgs> ValueChanging;
        public event EventHandler ValueChanged;
        public event EventHandler ReadOnlyChanged;

        #endregion
    }
}
