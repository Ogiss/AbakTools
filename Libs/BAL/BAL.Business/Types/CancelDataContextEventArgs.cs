using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Types
{
    public class CancelDataContextEventArgs : CancelEventArgs
    {
        #region Fields

        private DataContext dataContext;

        #endregion

        #region Properties

        public DataContext DataContext
        {
            get { return this.dataContext; }
        }

        #endregion

        #region Methods

        public CancelDataContextEventArgs(DataContext dataContext, bool cancel = false)
            : base(cancel)
        {
            this.dataContext = dataContext;
        }

        #endregion
    }
}
