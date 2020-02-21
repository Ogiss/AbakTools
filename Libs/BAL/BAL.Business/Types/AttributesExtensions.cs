using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public static class AttributesExtensions
    {
        public static string GetDescription(this DescriptionAttribute attr, object obj, string defaultDescription = "")
        {
            var type = obj is Type ? (Type)obj : obj.GetType();
            var attrs = type.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
            return defaultDescription;
        }
    }
}
