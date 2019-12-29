using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Enova.Old.Types
{
    [StructLayout(LayoutKind.Sequential), DefaultWidth(15), CompoundNames(new string[] { "Value", "Symbol" }, new string[] { "", "Jednostka" }), BinSerializable, TypeConverter(typeof(Amount.Converter))]
    public struct Amount : IComparable, IFormattable, ICompoundType, ISumable, INullable
    {
        private const double zero = 1E-09;
        private const int roundDigiths = 9;
        private static string emptySymbol;
        public static readonly Amount Zero;
        public static readonly Amount Empty;
        private readonly double value;
        private readonly string symbol;
        private static readonly Regex fs;
        private static readonly Regex pq;
        private static readonly object[] emptyValues;

        public Amount(double value)
        {
            this.value = Math.Round(value, 9);
            this.symbol = "";
        }

        public Amount(double value, string symbol)
            : this(value)
        {
            this.symbol = symbol;
        }

        public Amount(Amount amount)
        {
            this.value = amount.Value;
            this.symbol = amount.Symbol;
        }

        public double Value
        {
            get
            {
                return this.value;
            }
        }

        [MaxLength(10)]
        public string Symbol
        {
            get
            {
                if (this.symbol == null)
                {
                    return "";
                }
                return this.symbol;
            }
        }

        [Browsable(false)]
        public bool IsZero
        {
            get
            {
                return ((-1E-09 < this.Value) && (this.Value < 1E-09));
            }
        }

        [Browsable(false)]
        public bool IsNeutralSymbol
        {
            get
            {
                if (this.Symbol != null)
                {
                    return (this.Symbol == "");
                }
                return true;
            }
        }

        [Browsable(false)]
        public bool IsMinus
        {
            get
            {
                return (this.Value < -1E-09);
            }
        }

        [Browsable(false)]
        public bool IsPlus
        {
            get
            {
                return (this.Value > 1E-09);
            }
        }

        [Browsable(false)]
        public bool IsNull
        {
            get
            {
                return (this.Symbol == emptySymbol);
            }
        }

        public override string ToString()
        {
            return this.ToString(null, null);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if ((this.Value == 0.0) && this.IsNeutralSymbol)
            {
                return "0";
            }
            if (this.Symbol == emptySymbol)
            {
                return "";
            }
            if (format == null)
            {
                format = "0.".PadRight(0x11, '#');
            }
            return string.Format(provider, "{0:" + format + "} {1}", new object[] { this.Value, FormatSymbol(this.Symbol) });
        }

        private static string FormatSymbol(string symbol)
        {
            string input = symbol.Trim();
            if (!fs.IsMatch(input))
            {
                return input;
            }
            return string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[] { input });
        }

        public static Amount Parse(string value)
        {
            return Parse(value, null);
        }

        public static Amount Parse(string value, IFormatProvider provider)
        {
            string input = value.Trim();
            switch (input)
            {
                case "0":
                    return Zero;

                case "":
                    return Empty;
            }
            if (pq.IsMatch(input))
            {
                Match match = pq.Match(input);
                return new Amount(double.Parse(match.Groups[1].Value, provider), match.Groups[2].Value);
            }
            int length = input.Length;
            for (int i = 0; i < length; i++)
            {
                if (char.IsLetter(input, i))
                {
                    return new Amount(double.Parse(input.Substring(0, i), provider), input.Substring(i, length - i));
                }
            }
            return new Amount(double.Parse(input, provider));
        }

        private static bool neutralSymbol(Amount a1, Amount a2)
        {
            return (a1.IsNeutralSymbol || a2.IsNeutralSymbol);
        }

        private static bool equalSymbol(Amount a1, Amount a2)
        {
            return (neutralSymbol(a1, a2) || (a1.Symbol == a2.Symbol));
        }

        private static bool equalValues(double d1, double d2)
        {
            return (((d1 - 1E-09) < d2) && (d2 < (d1 + 1E-09)));
        }

        public static bool operator ==(Amount a1, Amount a2)
        {
            return (equalValues(a1.Value, a2.Value) && equalSymbol(a1, a2));
        }

        public static bool operator !=(Amount a1, Amount a2)
        {
            if (equalValues(a1.Value, a2.Value))
            {
                return !equalSymbol(a1, a2);
            }
            return true;
        }

        private static int compare(Amount a1, Amount a2)
        {
            if (!neutralSymbol(a1, a2))
            {
                int num = string.Compare(a1.Symbol, a2.Symbol, true);
                if (num != 0)
                {
                    return num;
                }
            }
            if (equalValues(a1.Value, a2.Value))
            {
                return 0;
            }
            if (a1.Value < a2.Value)
            {
                return -1;
            }
            return 1;
        }

        public static bool operator <(Amount a1, Amount a2)
        {
            return (compare(a1, a2) < 0);
        }

        public static bool operator >(Amount a1, Amount a2)
        {
            return (compare(a1, a2) > 0);
        }

        public static bool operator <=(Amount a1, Amount a2)
        {
            return (compare(a1, a2) <= 0);
        }

        public static bool operator >=(Amount a1, Amount a2)
        {
            return (compare(a1, a2) >= 0);
        }

        public bool Equals(Amount a)
        {
            return (this == a);
        }

        public override bool Equals(object o)
        {
            return ((o is Amount) && (this == ((Amount)o)));
        }

        public override int GetHashCode()
        {
            return (this.value.GetHashCode() ^ (0x1d * this.Symbol.GetHashCode()));
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (!(obj is Amount))
            {
                throw new ArgumentException();
            }
            return compare(this, (Amount)obj);
        }

        private static string commonSymbol(Amount a1, Amount a2)
        {
            if (a1.IsNeutralSymbol || (a1.Symbol == emptySymbol))
            {
                return a2.Symbol;
            }
            if ((!a2.IsNeutralSymbol && (a2.Symbol != emptySymbol)) && (a1.Symbol != a2.Symbol))
            {
                throw new ArgumentException(string.Format("Nie można operować (dodawanie, odejmowanie) ilości w r\x00f3żnych jednostkach: '{0}', '{1}'.", a1.Symbol, a2.Symbol));
            }
            return a1.Symbol;
        }

        public static Amount operator +(Amount a1, Amount a2)
        {
            return new Amount(a1.value + a2.value, commonSymbol(a1, a2));
        }

        public static Amount operator -(Amount a1, Amount a2)
        {
            return new Amount(a1.value - a2.value, commonSymbol(a1, a2));
        }

        public static Amount operator *(Amount a1, double x)
        {
            return new Amount(a1.value * x, a1.Symbol);
        }

        public static Amount operator *(Amount a1, Percent x)
        {
            return new Amount(a1.value * x, a1.Symbol);
        }

        public static Amount operator *(Amount a1, int n)
        {
            return (a1 * n);
        }

        public static Amount operator /(Amount a1, double n)
        {
            if (n == 0.0)
            {
                throw new ArgumentException("Dzielenie ilości przez 0.", "n");
            }
            return new Amount(a1.value / n, a1.Symbol);
        }

        public static Amount operator /(Amount a1, int n)
        {
            return (Amount)(a1 / ((double)n));
        }

        public static Amount operator -(Amount a1)
        {
            return new Amount(-a1.value, a1.Symbol);
        }

        public static double operator /(Amount a1, Amount a2)
        {
            if (!equalSymbol(a1, a2))
            {
                throw new ArgumentException(string.Format("Nie można dzielić ilości w r\x00f3żnych jednostkach: '{0}', '{1}'.", a1.Symbol, a2.Symbol));
            }
            return (a1.value / a2.value);
        }

        object[] ICompoundType.Values
        {
            get
            {
                if (this.IsNull)
                {
                    return emptyValues;
                }
                return new object[] { this.Value, this.symbol };
            }
        }

        public Amount Round(int digits)
        {
            return new Amount(Math.Round(this.value, digits), this.Symbol);
        }

        public Amount Floor(int digits)
        {
            return new Amount(Tools.Math.Floor(this.value, digits), this.Symbol);
        }

        public Amount Ceiling(int digits)
        {
            return new Amount(Tools.Math.Ceiling(this.value, digits), this.Symbol);
        }

        public static Amount Max(Amount a1, Amount a2)
        {
            if (a1 >= a2)
            {
                return a1;
            }
            return a2;
        }

        public static Amount Min(Amount a1, Amount a2)
        {
            if (a1 <= a2)
            {
                return a1;
            }
            return a2;
        }

        object ISumable.Sum(object sum)
        {
            if ((this.Value == 0.0) && this.IsNeutralSymbol)
            {
                return sum;
            }
            if (this.Symbol == emptySymbol)
            {
                return sum;
            }
            if (sum is string)
            {
                return sum;
            }
            Amount amount = (Amount)sum;
            if (((amount.Value == 0.0) && amount.IsNeutralSymbol) || (amount.Symbol == emptySymbol))
            {
                return this;
            }
            if (amount.Symbol != this.Symbol)
            {
                return "R\x00f3żne jednostki";
            }
            return (this + amount);
        }

        object ISumable.Div(int divider)
        {
            return new Amount(this.Value / ((double)divider), this.Symbol);
        }

        static Amount()
        {
            emptySymbol = "$$$";
            Zero = new Amount(0.0, "");
            Empty = new Amount(0.0, emptySymbol);
            fs = new Regex(@"^[0-9\.]", RegexOptions.Singleline);
            pq = new Regex(@"(.+)\[([^\]]*)\]?", RegexOptions.Singleline);
            emptyValues = new object[] { 0.0, "" };
        }

        // Nested Types
        public class Converter : TypeConverter
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
                    return Amount.Empty;
                }
                if (obj is string)
                {
                    return Amount.Parse((string)obj, culture);
                }
                return base.ConvertFrom(context, culture, obj);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object obj, Type type)
            {
                if ((obj != null) && (type == typeof(string)))
                {
                    Amount amount = (Amount)obj;
                    return amount.ToString(null, culture);
                }
                return base.ConvertFrom(context, culture, obj);
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties(typeof(Amount), attributes);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new TypeConverter.StandardValuesCollection(new Amount[] { Amount.Zero });
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }
    }


}
