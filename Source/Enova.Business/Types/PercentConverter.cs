using System;
using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Types
{
    public class PercentConverter : TypeConverter
    {
        // Methods
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type type)
        {
            return ((type == typeof(string)) || ((type == typeof(double)) || ((type == typeof(decimal)) || ((type == typeof(DBNull)) || base.CanConvertFrom(context, type)))));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type type)
        {
            return ((type == typeof(string)) || ((type == typeof(double)) || ((type == typeof(decimal)) || base.CanConvertTo(context, type))));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object obj)
        {
            if (obj == null)
            {
                return Percent.Zero;
            }
            if (obj is string)
            {
                if (((string)obj) == "")
                {
                    return Percent.Zero;
                }
                return Percent.Parse((string)obj, culture);
            }
            if (obj is double)
            {
                return new Percent((decimal)((double)obj));
            }
            if (obj is decimal)
            {
                return new Percent((decimal)obj);
            }
            if (obj is DBNull)
            {
                return Percent.Zero;
            }
            return base.ConvertFrom(context, culture, obj);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object obj, Type type)
        {
            if (obj != null)
            {
                if (type == typeof(string))
                {
                    Percent percent = (Percent)obj;
                    return percent.ToString(null, culture);
                }
                if (type == typeof(double))
                {
                    return (double)((Percent)obj);
                }
                if (type == typeof(decimal))
                {
                    return (decimal)((Percent)obj);
                }
            }
            return base.ConvertFrom(context, culture, obj);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(Percent), attributes);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new TypeConverter.StandardValuesCollection(new Percent[] { Percent.Zero, Percent.Hundred });
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return true;
        }
    }

}
