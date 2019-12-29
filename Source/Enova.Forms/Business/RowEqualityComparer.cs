using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Business
{
    public class RowEqualityComparer : IEqualityComparer<Enova.API.Business.Row>
    {
        bool compareTableName;

        public RowEqualityComparer(bool compareTableName)
        {
            this.compareTableName = compareTableName;
        }

        public RowEqualityComparer() : this(false) { }

        public bool Equals(API.Business.Row x, API.Business.Row y)
        {
            bool comp = compareTableName ? string.Compare(x.Table.TableName, y.Table.TableName, true) == 0 : true;
            if (comp)
                comp = x.ID == y.ID;
            return comp;
        }

        public int GetHashCode(API.Business.Row obj)
        {
            return compareTableName ? (obj.Table.TableName + "_" + obj.ID.ToString()).GetHashCode() : obj.ID.GetHashCode();
        }
    }
}
