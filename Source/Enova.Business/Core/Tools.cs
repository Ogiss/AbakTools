using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Core
{
    public sealed class Tools
    {
        public static bool IsNumber(string ss)
        {
            if (ss.Length == 0)
            {
                return false;
            }
            foreach (char ch in ss)
            {
                if (!char.IsNumber(ch))
                {
                    return false;
                }
            }
            return true;
        }

        public static string Sub(string ss, int i)
        {
            if (i >= ss.Length)
            {
                return "";
            }
            return ss.Substring(i);
        }

        public static object Execute(object obj, string props)
        {
            foreach (string str in props.Split(new char[] { '.' }))
            {
                if (obj == null)
                {
                    return obj;
                }
                obj = TypeDescriptor.GetProperties(obj).Find(str, true).GetValue(obj);
            }
            return obj;
        }
    }
}
