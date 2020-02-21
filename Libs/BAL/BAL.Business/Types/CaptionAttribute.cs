using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace BAL.Types
{
    public class CaptionAttribute : Attribute
    {
        private string caption;

        public string Caption
        {
            get { return this.caption; }
        }

        public CaptionAttribute(string caption)
        {
            this.caption = caption;
        }

        public static string GetCaption(object obj, string defaultCaption = "")
        {
            object[] attrs = null;
            if (typeof(MemberInfo).IsAssignableFrom(obj.GetType()))
            {
                attrs = ((MemberInfo)obj).GetCustomAttributes(typeof(CaptionAttribute), true);
            }
            else
            {
                var type = obj is Type ? (Type)obj : obj.GetType();
                attrs = type.GetCustomAttributes(typeof(CaptionAttribute), true);
            }
            if (attrs.Length > 0)
                return ((CaptionAttribute)attrs[0]).Caption;
            return defaultCaption;
        }
    }
}
