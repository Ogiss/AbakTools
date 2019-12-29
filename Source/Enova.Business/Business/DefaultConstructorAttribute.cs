using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Enova.Business.Old
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class DefaultConstructorAttribute : NamedConstructorAttribute
    {
        // Methods
        public DefaultConstructorAttribute()
            : base("default")
        {
        }

        public static ConstructorInfo Get(Type type, bool allowNoDefault, bool throwException)
        {
            ConstructorInfo info = null;
            foreach (ConstructorInfo info2 in type.GetConstructors())
            {
                if (allowNoDefault && (info == null))
                {
                    info = info2;
                }
                if (info2.GetCustomAttributes(typeof(DefaultConstructorAttribute), false).Length > 0)
                {
                    return info2;
                }
            }
            if (throwException && (info == null))
            {
                throw new ArgumentException("Typ '" + type + "' nie posiada konstruktora.");
            }
            return info;
        }
    }

}
