using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Enova.Old.Types.Design
{
    public class TimeSecConverter : TypeConverter
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
                    return TimeSec.MinValue;
                }
                if (span == TimeSpan.MaxValue)
                {
                    return TimeSec.MaxValue;
                }
                if (span == TimeSpan.Zero)
                {
                    return TimeSec.Zero;
                }
                return new TimeSec((TimeSpan)value);
            }
            if (value is string)
            {
                return TimeSec.Parse((string)value);
            }
            if (value is int)
            {
                int num1 = (int)value;
                return new TimeSec((double)((int)value));
            }
            return base.ConvertFrom(context, culture, value);
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
