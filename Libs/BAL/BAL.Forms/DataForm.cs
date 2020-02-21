using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public partial class DataForm : BAL.Forms.FormBase, BAL.Business.IDataContexable
    {
        #region Fields

        private BAL.Business.DataContext dataContext;

        #endregion

        #region Properties

        [Browsable(false)]
        public virtual BAL.Business.DataContext DataContext
        {
            get { return this.dataContext; }
            set
            {
                this.dataContext = value;
                if (this.dataContext != null)
                {
                    if (typeof(GridViewContext).IsAssignableFrom(this.dataContext.GetType()))
                        ((GridViewContext)this.dataContext).ParentForm = this;
                    this.InitTitle();
                }
            }
        }

        public override string Key
        {
            get
            {
                if (this.dataContext != null)
                    return this.dataContext.Key;
                return base.Key;
            }
        }

        #endregion

        #region Methods

        public DataForm()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            /*
            if (this.DataContext != null)
                this.DataContext.Dispose();
             */
        }

        protected virtual void InitTitle()
        {
            this.Text = this.DataContext.GetTitle();
        }

        #endregion
    }
}
