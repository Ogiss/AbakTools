using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class BusinessRowAttribute : Attribute
    {
        // Fields
        public readonly Type RowType;
        public readonly int Selector;

        // Methods
        public BusinessRowAttribute(Type rowType, object selector)
        {
            this.RowType = rowType;
            this.Selector = ((IConvertible)selector).ToInt32(null);
        }
    }
}
