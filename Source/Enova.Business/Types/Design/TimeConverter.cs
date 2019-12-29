using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Enova.Old.Types.Design
{
    public class TimeConverter : TypeConverter
    {
        // Methods
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof(TimeSpan)) || ((sourceType == typeof(string)) || ((sourceType == typeof(int)) || base.CanConvertFrom(context, sourceType))));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(TimeSpan)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is TimeSpan)
            {
                TimeSpan span = (TimeSpan)value;
                if (span == TimeSpan.MinValue)
                {
                    return Time.Empty;
                }
                if (span == TimeSpan.MaxValue)
                {
                    return Time.MaxValue;
                }
                if (span == TimeSpan.Zero)
                {
                    return Time.Zero;
                }
                return new Time((TimeSpan)value);
            }
            if (value is string)
            {
                return Time.Parse((string)value);
            }
            if (!(value is int))
            {
                return base.ConvertFrom(context, culture, value);
            }
            int num = (int)value;
            if (num == Time.EmptyMinutes)
            {
                return Time.Empty;
            }
            if (num == Time.MaxMinutes)
            {
                return Time.MaxMinutes;
            }
            return new Time((int)value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((value is TimeSpan) && (destinationType == typeof(TimeSpan)))
            {
                return (TimeSpan)((Time)value);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
