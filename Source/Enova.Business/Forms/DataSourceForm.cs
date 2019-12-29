using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Business;

namespace Enova.Business.Old.Forms
{
    public partial class DataSourceForm : Enova.Business.Old.Forms.DataBaseForm, Enova.Business.Old.Core.IDataSource, IDataContexable
    {
        private bool isLoaded = false;
        private bool isReadOnly = false;
        private BAL.Business.DataContext __dataContext__;

        [Browsable(true)]
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { isReadOnly = value; }
        }

        private object dataSource = null;
        [Browsable(false)]
        public virtual object DataSource
        {
            get { return dataSource; }
            set
            {
                if (!this.DesignMode)
                {
                    dataSource = value;
                    OnBeforeBinding(new BindingEventArgs(this.dataSource));
                    if (dataSource != null && dataSource is Enova.Business.Old.Core.IReadOnly)
                        isReadOnly = ((Enova.Business.Old.Core.IReadOnly)DataSource).IsReadOnly;

                    if (isLoaded)
                        DataSourceBinding.DataSource = dataSource;
                }
            }
        }


        DataContext IDataContexable.DataContext
        {
            get { return this.__dataContext__; }
            set
            {
                if (!this.DesignMode)
                {
                    this.__dataContext__ = value;
                    if (this.__dataContext__ != null)
                        this.DataSource = this.__dataContext__.GetData(this.__dataContext__.GetDataType());
                }
            }
        }


        public DataSourceForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DataSource != null && !this.DesignMode)
            {
                if (isReadOnly)
                {
                    disableControls(Controls);
                }
                DataSourceBinding.DataSource = DataSource;
            }
            isLoaded = true;
        }

        private void disableControls(Control.ControlCollection controls)
        {
            foreach (var c in controls)
            {
                Type t = c.GetType();
                if (t.IsAssignableFrom(typeof(TabControl)))
                {
                    disableControls(((TabControl)c).Controls);
                    foreach (TabPage p in ((TabControl)c).TabPages)
                    {
                        disableControls(p.Controls);
                    }
                }
                else if (t.IsAssignableFrom(typeof(TextBox)) || t.IsAssignableFrom(typeof(ComboBox))
                            || t.IsAssignableFrom(typeof(CheckBox)) || t.IsAssignableFrom(typeof(RadioButton)))
                {
                    if (!((Control)c).Name.StartsWith("filter"))
                    {
                        ((Control)c).Enabled = false;
                        ((Control)c).BackColor = Color.White;
                        ((Control)c).ForeColor = Color.Black;
                    }
                }
            }
        }

        public event BindingEventHandler BeforeBinding;

        protected virtual void OnBeforeBinding(BindingEventArgs e)
        {
            if (BeforeBinding != null)
                BeforeBinding(this, e);
        }
    }

    public class BindingEventArgs : EventArgs
    {
        private object dataSource;

        public object DataSource
        {
            get { return dataSource; }
        }

        public BindingEventArgs(object dataSource)
            : base()
        {
            this.dataSource = dataSource;
        }
    }

    public delegate void BindingEventHandler(object sender,BindingEventArgs e);
}
