using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Enova.Old.Types.Design
{
    internal class CurrencyConverter : TypeConverter
    {
        // Methods
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type type)
        {
            return ((type == typeof(string)) || base.CanConvertFrom(context, type));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type type)
        {
            return ((type == typeof(string)) || base.CanConvertTo(context, type));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object obj)
        {
            if (obj == null)
            {
                return Currency.Zero;
            }
            if (obj is string)
            {
                return Currency.Parse((string)obj, culture);
            }
            return base.ConvertFrom(context, culture, obj);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object obj, Type type)
        {
            if ((obj != null) && (type == typeof(string)))
            {
                Currency currency = (Currency)obj;
                return currency.ToString(null, culture);
            }
            return base.ConvertFrom(context, culture, obj);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(Currency), attributes);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new TypeConverter.StandardValuesCollection(new Currency[] { Currency.Zero });
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
            Currency currency = (Currency)value;
            return currency.IsValid();
        }
    }

}
