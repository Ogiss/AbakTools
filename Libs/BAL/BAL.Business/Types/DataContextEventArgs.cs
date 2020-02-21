using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Types
{
    public class DataContextEventArgs : EventArgs
    {
        private DataContext dataContext;

        public DataContext DataContext
        {
            get { return this.dataContext; }
        }

        public DataContextEventArgs(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
