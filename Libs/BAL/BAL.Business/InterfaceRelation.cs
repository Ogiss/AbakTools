using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class InterfaceRelation<T> : Relation<T>
        where T : Row
    {
        #region Fields

        private Type interfaceType;

        #endregion

        #region Methods

        public InterfaceRelation(Table<T> table, Type interfaceType)
            : base(table)
        {
            this.interfaceType = interfaceType;
        }

        #endregion
    }
}
