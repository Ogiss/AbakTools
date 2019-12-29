using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Enova.Old.Types.Design
{
    public class DoubleCyConverter : TypeConverter
    {
        // Methods
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type type)
        {
            if (((!(type == typeof(string)) && !(type == typeof(Currency))) && (!(type == typeof(decimal)) && !(type == typeof(double)))) && (!(type == typeof(int)) && !(type == typeof(long))))
            {
                return base.CanConvertFrom(context, type);
            }
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type type)
        {
            return ((type == typeof(string)) || base.CanConvertTo(context, type));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object obj)
        {
            if (obj == null)
            {
                return DoubleCy.Zero;
            }
            if (obj is string)
            {
                return DoubleCy.Parse((string)obj, culture);
            }
            if (obj is Currency)
            {
                return (DoubleCy)((Currency)obj);
            }
            if (obj is decimal)
            {
                return (DoubleCy)((decimal)obj);
            }
            if (obj is double)
            {
                return new DoubleCy((double)obj);
            }
            if (obj is int)
            {
                return (DoubleCy)((int)obj);
            }
            if (obj is long)
            {
                return (DoubleCy)((long)obj);
            }
            return base.ConvertFrom(context, culture, obj);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object obj, Type type)
        {
            if ((obj != null) && (type == typeof(string)))
            {
                DoubleCy cy = (DoubleCy)obj;
                return cy.ToString(null, culture);
            }
            return base.ConvertFrom(context, culture, obj);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(DoubleCy), attributes);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new TypeConverter.StandardValuesCollection(new DoubleCy[] { DoubleCy.Zero });
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
            DoubleCy cy = (DoubleCy)value;
            return cy.IsValid();
        }
    }
}
