using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Forms.Controls
{
    public class GridView : BAL.Forms.Controls.GridView , BAL.Business.IDataContexable
    {
        #region Fields

        private BAL.Business.DataContext dataContext;

        #endregion

        public BAL.Business.DataContext DataContext
        {
            get
            {
                return this.dataContext;
            }
            set
            {
                OnDataContextChanging(new BAL.Types.DataContextEventArgs(value));
                this.dataContext = value;
                //this.DataSource = this.dataContext;
                OnDataContextChanged(new BAL.Types.DataContextEventArgs(this.dataContext));
            }
        }

        public BAL.Business.View View
        {
            get { return (BAL.Business.View)this.DataContext; }
        }


        #region Methods

        protected virtual void OnDataContextChanging(BAL.Types.DataContextEventArgs e)
        {
            if (this.DataContextChanging != null)
                this.DataContextChanging(this, e);
        }

        protected virtual void OnDataContextChanged(BAL.Types.DataContextEventArgs e)
        {
            if (this.DataContextChanged != null)
                this.DataContextChanged(this, e);
        }

        #endregion


        #region Events

        public event EventHandler<BAL.Types.DataContextEventArgs> DataContextChanging;
        public event EventHandler<BAL.Types.DataContextEventArgs> DataContextChanged;

        #endregion
    }
}
