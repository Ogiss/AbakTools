using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    public class RowEventArgs : EventArgs
    {
        #region Fields

        private object row;

        #endregion

        #region Properties

        public object Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        #endregion

        #region Methods

        public RowEventArgs(object row)
        {
            this.row = row;
        }

        #endregion
    }

    public delegate void RowEventHandler(object sender, RowEventArgs e);
}
