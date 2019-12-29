using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using Enova.Old.Tools;

namespace Enova.Old.Types
{
    [AttributeUsage(AttributeTargets.All)]
    public class CaptionAttribute : Attribute
    {
        // Fields
        public readonly string Text;

        // Methods
        public CaptionAttribute(string text)
        {
            this.Text = text;
        }

        private string checkLocalize(string text)
        {
            /*
            if (LangTranslator.IsActive && !string.IsNullOrEmpty(text))
            {
                return text;
            }
             */
            return this.Text;
        }

        /*
        [Conditional("DEBUG")]
        private static void checkNames(Type enumType)
        {
            Set<string> set = new Set<string>();
            foreach (Enum enum2 in Enum.GetValues(enumType))
            {
                string str2 = enumToString(enum2).ToLower();
                set += str2;
            }
        }
         */

        public static string ConvertName(string s)
        {
            if (s.EndsWith("Worker"))
            {
                s = s.Substring(0, s.Length - 6);
            }
            int length = 0;
            while ((length < (s.Length - 1)) && (!char.IsUpper(s[length]) || !char.IsLower(s[length + 1])))
            {
                length++;
            }
            if ((length > 0) && (length < (s.Length - 1)))
            {
                s = string.Concat(new object[] { s.Substring(0, length), ' ', char.ToLower(s[length]), s.Substring(length + 1) });
            }
            while (length < (s.Length - 1))
            {
                char c = s[length + 1];
                if (char.IsLower(s[length]) && (char.IsUpper(c) || char.IsDigit(c)))
                {
                    length++;
                    s = string.Concat(new object[] { s.Substring(0, length), ' ', char.ToLower(c), s.Substring(length + 1) });
                }
                length++;
            }
            return s;
        }

        
        private static string enumToString(Enum v)
        {
            string name = v.ToString();
            FieldInfo field = v.GetType().GetField(name);
            if (field == null)
            {
                return Convert.ToInt32(v).ToString();
            }
            CaptionAttribute customAttribute = (CaptionAttribute)Attribute.GetCustomAttribute(field, typeof(CaptionAttribute));
            string enumText = string.Empty;
            if (customAttribute != null)
            {
                enumText = customAttribute.GetEnumText(field);
            }
            else
            {
                enumText = LangTranslator.GetEnumText(field);
            }
            if (!string.IsNullOrEmpty(enumText))
            {
                return enumText;
            }
            if (customAttribute != null)
            {
                return customAttribute.Text;
            }
            return ConvertName(name);
        }

        public static string EnumToString(Enum v)
        {
            return enumToString(v);
        }

        public static string ForCustomAttribute(ICustomAttributeProvider cap)
        {
            CaptionAttribute[] customAttributes = (CaptionAttribute[])cap.GetCustomAttributes(typeof(CaptionAttribute), true);
            if ((customAttributes != null) && (customAttributes.Length != 0))
            {
                return customAttributes[0].Text;
            }
            return null;
        }

        public static string ForPropertyDescriptor(PropertyDescriptor pd)
        {
            return ForPropertyDescriptor(pd, true);
        }

        public static string ForPropertyDescriptor(PropertyDescriptor pd, bool useName)
        {
            CaptionAttribute customAttribute = (CaptionAttribute)pd.Attributes[typeof(CaptionAttribute)];
            if (customAttribute != null)
            {
                return customAttribute.GetText(pd);
            }
            customAttribute = (CaptionAttribute)Attribute.GetCustomAttribute(pd.PropertyType, typeof(CaptionAttribute));
            if (customAttribute != null)
            {
                return customAttribute.Text;
            }
            if (!useName)
            {
                return null;
            }
            return ConvertName(pd.Name);
        }

        public static string ForPropertyInfo(PropertyInfo pi)
        {
            CaptionAttribute customAttribute = (CaptionAttribute)Attribute.GetCustomAttribute(pi, typeof(CaptionAttribute));
            if (customAttribute != null)
            {
                return customAttribute.Text;
            }
            customAttribute = (CaptionAttribute)Attribute.GetCustomAttribute(pi.PropertyType, typeof(CaptionAttribute));
            if (customAttribute != null)
            {
                return customAttribute.Text;
            }
            return ConvertName(pi.Name);
        }

        public static string ForType(Type type)
        {
            return ForType(type, true);
        }

        public static string ForType(Type type, bool forceName)
        {
            CaptionAttribute customAttribute = (CaptionAttribute)Attribute.GetCustomAttribute(type, typeof(CaptionAttribute));
            if (customAttribute != null)
            {
                return customAttribute.Text;
            }
            if (!forceName)
            {
                return null;
            }
            return ConvertName(type.Name);
        }

        
        public string GetDescription(PropertyDescriptor pd)
        {
            return LangTranslator.GetDescription(pd);
        }
        

        public string GetDescription(string path, string name)
        {
            return LangTranslator.GetDescription(path, name);
        }

        public string GetEnumText(FieldInfo fi)
        {
            string enumText = LangTranslator.GetEnumText(fi);
            return this.checkLocalize(enumText);
        }

        public string GetText(PropertyDescriptor pd)
        {
            string text = LangTranslator.GetText(pd);
            return this.checkLocalize(text);
        }

        public string GetText(string path, string name)
        {
            string text = LangTranslator.GetText(path, name);
            return this.checkLocalize(text);
        }

        private static bool isAnyLower(string s)
        {
            for (int i = 1; i < s.Length; i++)
            {
                if (char.IsLower(s[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static Enum ParseEnum(Type enumType, string name)
        {
            return ParseEnum(enumType, name, true);
        }

        public static Enum ParseEnum(Type enumType, string name, bool throwError)
        {
            return ParseEnum(enumType, name, false, throwError);
        }

        public static Enum ParseEnum(Type enumType, string name, bool ignoreCase, bool throwError)
        {
            Array values = Enum.GetValues(enumType);
            foreach (Enum enum2 in values)
            {
                if (string.Compare(EnumToString(enum2), name, ignoreCase) == 0)
                {
                    return enum2;
                }
            }
            foreach (Enum enum3 in values)
            {
                if (string.Compare(enum3.ToString(), name, ignoreCase) == 0)
                {
                    return enum3;
                }
            }
            if (throwError)
            {
                throw new ArgumentException(string.Concat(new object[] { "Nieznaleziona wartość ", name, " w typie ", enumType, "." }));
            }
            return null;
        }
    }
}
