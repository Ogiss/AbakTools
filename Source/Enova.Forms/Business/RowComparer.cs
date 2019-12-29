using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Business
{
    public class RowComparer<T> : Comparer<T>
        where T : Enova.API.Business.Row
    {
        private bool compareTableName;

        public RowComparer(bool compareTableName)
        {
            this.compareTableName = compareTableName;
        }

        public RowComparer() : this(false) { }

        public override int Compare(T x, T y)
        {
            var cmp = compareTableName ? x.Table.TableName.CompareTo(y.Table.TableName) : 0;
            if (cmp == 0)
                cmp = x.ID.CompareTo(y.ID);
            return cmp;
        }
    }
}
