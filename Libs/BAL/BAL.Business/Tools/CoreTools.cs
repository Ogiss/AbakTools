using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public static partial class CoreTools
    {
        public static bool ParseBool(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim().ToLower();
                return str == "true" || str == "1" || str == "yes";
            }
            return false;
        }

        public static byte[] LE2BE(byte[] data)
        {
            byte[] arr = new byte[data.Length];
            for (int i = data.Length - 1, j = 0; i >= 0; i--, j++)
            {
                arr[j] = data[i];
            }
            return arr;
        }

        private static List<Type> standardTypes = new List<Type>()
        {
            typeof(string),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(byte),
            typeof(short),
            typeof(char)
        };

        public static bool IsStandard(this Type type)
        {
            var u = Nullable.GetUnderlyingType(type);
            var t = u ?? type;
            return standardTypes.Contains(t);
        }

        static bool IsNullable(Type type)
        {
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }

        public static Type GetObjectType(object obj)
        {
            Type type = obj is Type ? (Type)obj : obj.GetType();
            if (type.FullName.StartsWith("System.Data.Entity.DynamicProxies"))
                return type.BaseType;
            return type;
        }

        public static bool IsDigitChar(char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool IsAlphaChar(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == ' ' || c == '_';
        }

        public static bool IsPrintableChar(char c)
        {
            return !char.IsControl(c);
        }

        public static List<Type> GetBaseTypes(Type type, Type endType = null)
        {
            if (endType == null)
                endType = typeof(object);
            List<Type> types = new List<Type>();
            if (endType == type)
                return types;
            Type ct = type.BaseType;
            while (ct != endType)
            {
                types.Add(ct);
                ct = ct.BaseType;
            }
            return types;
        }

    }
}
