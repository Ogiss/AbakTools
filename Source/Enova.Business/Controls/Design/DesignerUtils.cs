using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Controls.Design
{
    public class DesignerUtils
    {
        public static ICollection FilterGenericTypes(ICollection types)
        {
            if ((types == null) || (types.Count == 0))
            {
                return types;
            }
            ArrayList list = new ArrayList(types.Count);
            foreach (Type type in types)
            {
                if (!type.ContainsGenericParameters)
                {
                    list.Add(type);
                }
            }
            return list;
        }
    }
}
