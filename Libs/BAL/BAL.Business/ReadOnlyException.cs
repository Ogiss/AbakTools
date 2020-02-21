using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class ReadOnlyException : AccessException
    {
        // Methods
        public ReadOnlyException(IRow row)
            : this(row, "Pr\x00f3ba edycji zapisu tylko do odczytu.\n{0} ({1}).")
        {
        }

        public ReadOnlyException(IRow row, string desc)
            : base(row, string.Format(desc, row, (row.State != RowState.Detached) ? row.Table.TableName : ""))
        {
        }
    }
}
