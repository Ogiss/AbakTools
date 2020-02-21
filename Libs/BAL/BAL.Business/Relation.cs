using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class Relation<T> : Key<T>
        where T : Row
    {
        #region Methods

        public Relation(Table<T> table) : base(table) { }

        #endregion
    }
}
