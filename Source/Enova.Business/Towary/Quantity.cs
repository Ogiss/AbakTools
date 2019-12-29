using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Globalization;
using Enova.Old.Types;
using Enova.Business.Old;

namespace Enova.Old.Towary
{
    [Serializable, StructLayout(LayoutKind.Sequential), TypeConverter(typeof(Quantity.Converter)), DefaultWidth(15), CompoundNames(new string[] { "Value", "Symbol" }, new string[] { "", "Jednostka" }), BinSerializable]
    public struct Quantity : IComparable, IFormattable, ICompoundType, ISumable, INullable
    {
        private const double zero = 1E-09;
        private const int roundDigiths = 9;
        private static string emptySymbol;
        public static readonly Quantity Zero;
        public static readonly Quantity Empty;
        private readonly double value;
        private readonly string symbol;
        private static readonly Regex fs;
        private static readonly Regex pq;
        private static readonly object[] emptyValues;

        public Quantity(double value)
        {
            this.value = Math.Round(value, 9);
            this.symbol = "";
        }

        [DefaultConstructor]
        public Quantity(double value, string symbol)
            : this(value)
        {
            this.symbol = symbol;
        }

        public Quantity(Quantity qty)
        {
            this.value = qty.Value;
            this.symbol = qty.Symbol;
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
        public bool IsPlusOrZero
        {
            get
            {
                if (!this.IsPlus)
                {
                    return this.IsZero;
                }
                return true;
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

        public static Quantity Parse(string value)
        {
            return Parse(value, null);
        }

        public static Quantity Parse(string value, IFormatProvider provider)
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
                return new Quantity(double.Parse(match.Groups[1].Value, provider), match.Groups[2].Value);
            }
            int length = input.Length;
            for (int i = 0; i < length; i++)
            {
                if (char.IsLetter(input, i))
                {
                    return new Quantity(double.Parse(input.Substring(0, i), provider), input.Substring(i, length - i));
                }
            }
            return new Quantity(double.Parse(input, provider));
        }

        private static bool neutralSymbol(Quantity q1, Quantity q2)
        {
            return (q1.IsNeutralSymbol || q2.IsNeutralSymbol);
        }

        private static bool equalSymbol(Quantity q1, Quantity q2)
        {
            return (neutralSymbol(q1, q2) || (q1.Symbol == q2.Symbol));
        }

        private static bool equalValues(double d1, double d2)
        {
            return (((d1 - 1E-09) < d2) && (d2 < (d1 + 1E-09)));
        }

        public static bool operator ==(Quantity q1, Quantity q2)
        {
            return (equalValues(q1.Value, q2.Value) && equalSymbol(q1, q2));
        }

        public static bool operator !=(Quantity q1, Quantity q2)
        {
            if (equalValues(q1.Value, q2.Value))
            {
                return !equalSymbol(q1, q2);
            }
            return true;
        }

        private static int compare(Quantity q1, Quantity q2)
        {
            if (!neutralSymbol(q1, q2))
            {
                int num = string.Compare(q1.Symbol, q2.Symbol, true);
                if (num != 0)
                {
                    return num;
                }
            }
            if (equalValues(q1.Value, q2.Value))
            {
                return 0;
            }
            if (q1.Value < q2.Value)
            {
                return -1;
            }
            return 1;
        }

        public static bool operator <(Quantity q1, Quantity q2)
        {
            return (compare(q1, q2) < 0);
        }

        public static bool operator >(Quantity q1, Quantity q2)
        {
            return (compare(q1, q2) > 0);
        }

        public static bool operator <=(Quantity q1, Quantity q2)
        {
            return (compare(q1, q2) <= 0);
        }

        public static bool operator >=(Quantity q1, Quantity q2)
        {
            return (compare(q1, q2) >= 0);
        }

        public bool Equals(Quantity q)
        {
            return (this == q);
        }

        public override bool Equals(object o)
        {
            return ((o is Quantity) && (this == ((Quantity)o)));
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
            if (!(obj is Quantity))
            {
                throw new ArgumentException();
            }
            return compare(this, (Quantity)obj);
        }

        private static string commonSymbol(Quantity q1, Quantity q2)
        {
            if (q1.IsNeutralSymbol || (q1.Symbol == emptySymbol))
            {
                return q2.Symbol;
            }
            if ((!q2.IsNeutralSymbol && (q2.Symbol != emptySymbol)) && (q1.Symbol != q2.Symbol))
            {
                throw new ArgumentException(string.Format("Nie można operować (dodawanie, odejmowanie) ilości w r\x00f3żnych jednostkach: '{0}', '{1}'.", q1.Symbol, q2.Symbol));
            }
            return q1.Symbol;
        }

        public static Quantity operator +(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.value + q2.value, commonSymbol(q1, q2));
        }

        public static Quantity operator -(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.value - q2.value, commonSymbol(q1, q2));
        }

        public static Quantity operator *(Quantity q1, double x)
        {
            return new Quantity(q1.value * x, q1.Symbol);
        }

        public static Quantity operator *(Quantity q1, Percent x)
        {
            return new Quantity(q1.value * x, q1.Symbol);
        }

        public static Quantity operator *(Quantity q1, int n)
        {
            return (q1 * n);
        }

        public static Quantity operator /(Quantity q1, double n)
        {
            if (n == 0.0)
            {
                throw new ArgumentException("Dzielenie ilości przez 0.", "n");
            }
            return new Quantity(q1.value / n, q1.Symbol);
        }

        public static Quantity operator /(Quantity q1, int n)
        {
            return (Quantity)(q1 / ((double)n));
        }

        public static Quantity operator -(Quantity q1)
        {
            return new Quantity(-q1.value, q1.Symbol);
        }

        public static double operator /(Quantity q1, Quantity q2)
        {
            if (!equalSymbol(q1, q2))
            {
                throw new ArgumentException(string.Format("Nie można dzielić ilości w r\x00f3żnych jednostkach: '{0}', '{1}'.", q1.Symbol, q2.Symbol));
            }
            return (q1.value / q2.value);
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

        public Quantity Round(int digits)
        {
            return new Quantity(Math.Round(this.value, digits), this.Symbol);
        }

        public Quantity Floor(int digits)
        {
            return new Quantity(Tools.Math.Floor(this.value, digits), this.Symbol);
        }

        public Quantity Ceiling(int digits)
        {
            return new Quantity(Tools.Math.Ceiling(this.value, digits), this.Symbol);
        }

        public static Quantity Max(Quantity q1, Quantity q2)
        {
            if (q1 >= q2)
            {
                return q1;
            }
            return q2;
        }

        public static Quantity Min(Quantity q1, Quantity q2)
        {
            if (q1 <= q2)
            {
                return q1;
            }
            return q2;
        }

        public static Quantity Abs(Quantity q)
        {
            return new Quantity(Math.Abs(q.Value), q.Symbol);
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
            Quantity quantity = (Quantity)sum;
            if (((quantity.Value == 0.0) && quantity.IsNeutralSymbol) || (quantity.Symbol == emptySymbol))
            {
                return this;
            }
            if (quantity.Symbol != this.Symbol)
            {
                return "R\x00f3żne jednostki";
            }
            return (this + quantity);
        }

        object ISumable.Div(int divider)
        {
            return new Quantity(this.Value / ((double)divider), this.Symbol);
        }

        public static implicit operator Amount(Quantity q)
        {
            return new Amount(q.Value, q.Symbol);
        }

        public static implicit operator Quantity(Amount a)
        {
            return new Quantity(a.Value, a.Symbol);
        }

        static Quantity()
        {
            emptySymbol = "$$$";
            Zero = new Quantity(0.0, "");
            Empty = new Quantity(0.0, emptySymbol);
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
                    return Quantity.Empty;
                }
                if (obj is string)
                {
                    return Quantity.Parse((string)obj, culture);
                }
                return base.ConvertFrom(context, culture, obj);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object obj, Type type)
            {
                if ((obj != null) && (type == typeof(string)))
                {
                    Quantity quantity = (Quantity)obj;
                    return quantity.ToString(null, culture);
                }
                return base.ConvertFrom(context, culture, obj);
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties(typeof(Quantity), attributes);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new TypeConverter.StandardValuesCollection(new Quantity[] { Quantity.Zero });
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

        public sealed class Quantity4Query
        {
            // Properties
            public Quantity Quantity
            {
                get
                {
                    return new Quantity(this.Value, this.Symbol);
                }
            }

            public string Symbol { get; set; }

            public double Value { get; set; }
        }
    }

}
