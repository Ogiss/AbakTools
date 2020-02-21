using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public class OwnerTypeDescriptor
    {
        #region Fields

        private Type ownerType;
        private Type propertyType;

        #endregion


        public OwnerTypeDescriptor(Type ownerType, Type propertyType)
        {
            this.ownerType = ownerType;
            this.propertyType = propertyType;
        }
    }
}
