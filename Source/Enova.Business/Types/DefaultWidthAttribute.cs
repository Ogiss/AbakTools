using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace Enova.Old.Types
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Class)]
    public class DefaultWidthAttribute : Attribute
    {
        // Fields
        public readonly bool InChars;
        public static readonly string k100 = new string('k', 100);
        public static readonly string O100 = new string('o', 100);
        public readonly int Width;
        private const double widthMul = 1.1;

        // Methods
        public DefaultWidthAttribute(int width)
        {
            this.Width = width;
            this.InChars = true;
        }

        public DefaultWidthAttribute(int width, bool inChars)
        {
            this.Width = width;
            this.InChars = inChars;
        }

        public static string ForPropertyDescriptor(PropertyDescriptor pd)
        {
            DefaultWidthAttribute attribute = (DefaultWidthAttribute)pd.Attributes[typeof(DefaultWidthAttribute)];
            if (attribute == null)
            {
                return ForTypeImpl(pd.PropertyType);
            }
            if (!attribute.InChars)
            {
                return (attribute.Width + "px");
            }
            int width = attribute.Width;
            MaxLengthAttribute attribute2 = (MaxLengthAttribute)pd.Attributes[typeof(MaxLengthAttribute)];
            if ((attribute2 != null) && (attribute2.MaxLength < width))
            {
                width = attribute2.MaxLength;
            }
            return width.ToString();
        }

        public static int ForPropertyDescriptor(PropertyDescriptor pd, double pxOnChar)
        {
            DefaultWidthAttribute attribute = (DefaultWidthAttribute)pd.Attributes[typeof(DefaultWidthAttribute)];
            if (attribute == null)
            {
                return ForType(pd.PropertyType, pxOnChar);
            }
            MaxLengthAttribute attribute2 = (MaxLengthAttribute)pd.Attributes[typeof(MaxLengthAttribute)];
            int num = (attribute2 != null) ? ((int)((attribute2.MaxLength * pxOnChar) * 1.1)) : 0x7fffffff;
            if (attribute.InChars)
            {
                return Math.Min((int)((attribute.Width * pxOnChar) * 1.1), num);
            }
            return Math.Min(attribute.Width, num);
        }

        public static int ForType(Type type)
        {
            if (type == typeof(decimal))
            {
                return 15;
            }
            if (type == typeof(Guid))
            {
                return 0x24;
            }
            if (type == typeof(bool))
            {
                return 5;
            }
            if (type == typeof(int))
            {
                return 10;
            }
            if (typeof(Image).IsAssignableFrom(type))
            {
                return 3;
            }
            if (typeof(Icon).IsAssignableFrom(type))
            {
                return 3;
            }
            if (!type.IsEnum)
            {
                return -1;
            }
            int num = -1;
            foreach (string str in Enum.GetNames(type))
            {
                num = Math.Max(num, str.Length);
            }
            return num;
        }

        public static int ForType(Type type, double pxOnChar)
        {
            int width;
            DefaultWidthAttribute customAttribute = (DefaultWidthAttribute)Attribute.GetCustomAttribute(type, typeof(DefaultWidthAttribute));
            if (customAttribute != null)
            {
                if (!customAttribute.InChars)
                {
                    return customAttribute.Width;
                }
                width = customAttribute.Width;
            }
            else
            {
                width = ForType(type);
            }
            return (int)((width * pxOnChar) * 1.1);
        }

        private static string ForTypeImpl(Type type)
        {
            int width;
            DefaultWidthAttribute customAttribute = (DefaultWidthAttribute)Attribute.GetCustomAttribute(type, typeof(DefaultWidthAttribute));
            if (customAttribute != null)
            {
                if (!customAttribute.InChars)
                {
                    return (customAttribute.Width + "px");
                }
                width = customAttribute.Width;
            }
            else
            {
                width = ForType(type);
            }
            if (width < 0)
            {
                return null;
            }
            return width.ToString();
        }
    }
}
