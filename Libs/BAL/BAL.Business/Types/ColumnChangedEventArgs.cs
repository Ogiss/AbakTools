using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Types
{
    public class ColumnChangedEventArgs : EventArgs
    {
        #region Fields

        private Column column;
        private string propertyName;

        #endregion

        #region Properties

        public Column Column
        {
            get { return this.column; }
        }

        public string PropertyName
        {
            get { return this.propertyName; }
        }

        #endregion

        #region Methods

        public ColumnChangedEventArgs(Column column, string propertyName)
        {
            this.column = column;
            this.propertyName = propertyName;
        }

        #endregion
    }
}
