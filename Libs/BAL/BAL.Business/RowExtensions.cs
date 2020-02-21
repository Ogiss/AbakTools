using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public static class RowExtensions
    {
        public static Host GetHost(this IRow row)
        {
            return new Host() { ID = row.ID, Type = row.Table.TableName };
        }

        public static long GetStamp(this IStampRow row)
        {
            return BitConverter.ToInt64(CoreTools.LE2BE(row.Stamp), 0);
        }

    }
}
