using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public class DataContextKeyAttribute : Attribute
    {
        private string key;

        public string Key
        {
            get { return this.key; }
        }

        public DataContextKeyAttribute(string key)
        {
            this.key = key;
        }

        public static string GetKey(object obj, string defaultKey = "")
        {
            if (obj != null)
            {
                Type type = obj is Type ? (Type)obj : obj.GetType();
                var attrs = type.GetCustomAttributes(typeof(DataContextKeyAttribute), true);
                if (attrs.Length > 0)
                    return ((DataContextKeyAttribute)attrs[0]).Key;
            }
            return defaultKey;
        }
    }
}
