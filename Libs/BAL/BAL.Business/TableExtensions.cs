using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public static class TableExtensions
    {
        public static Table<T> SetParentTable<T>(this Table<T> table, Table<T> parentTable)
            where T : Row
        {
            table.parentTable = parentTable;
            return table;
        }

        public static Table<T> SetKey<T>(this Table<T> table, Key<T> key, params object[] data)
            where T: Row
        {
            key.values = data;
            table.key = key;
            if (key.Table != table)
                table.parentTable = key.Table;
            return table;
        }
    }
}
