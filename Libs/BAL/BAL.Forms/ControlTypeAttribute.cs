using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Forms
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
    public class ControlTypeAttribute : AttributeBase
    {
        private Type type;

        public Type Type
        {
            get { return this.type; }
        }

        public ControlTypeAttribute(Type type)
        {
            this.type = type;
        }

        public static Type GetControlType(object obj, Type defaultType = null)
        {
            var t = obj is Type ? (Type)obj : obj.GetType();
            var attrs = t.GetCustomAttributes(typeof(ControlTypeAttribute), true);
            if (attrs != null && attrs.Length > 0)
                return ((ControlTypeAttribute)attrs[0]).Type;
            return defaultType;
        }
    }
}
