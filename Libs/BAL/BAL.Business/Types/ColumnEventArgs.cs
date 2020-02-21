using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public class ColumnEventArgs : EventArgs
    {
        private Column column;

        public Column Column
        {
            get { return this.column; }
        }

        public ColumnEventArgs(Column column)
        {
            this.column = column;
        }
    }
}
