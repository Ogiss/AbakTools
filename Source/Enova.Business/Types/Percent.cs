using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Enova.Old.Tools;

namespace Enova.Old.Types
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(PercentConverter)), DefaultWidth(10), BinSerializable]
    public struct Percent : IComparable, IFormattable, ISumable, INullable
    {
        public static readonly Percent Zero;
        public static readonly Percent Fifty;
        public static readonly Percent Hundred;
        public static readonly Percent Blank;
        private readonly decimal v;

        private Percent(bool temp)
        {
            this.v = -0.00000000000000001M;
        }

        public Percent(decimal value)
        {
            this.v = Tools.Math.Round(value, 4);
        }

        public Percent(long l, long m)
            : this((decimal)(l / m))
        {
        }

        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                return ((-100M < this.v) && (this.v < 100M));
            }
        }
        public static Percent Parse(string value)
        {
            return Parse(value, null);
        }

        public static Percent Parse(string s, IFormatProvider provider)
        {
            if (s.EndsWith("%"))
            {
                s = s.Substring(0, s.Length - 1);
            }
            if (s.Trim() == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
            {
                return Zero;
            }
            return new Percent(decimal.Parse(s, provider) / 100M);
        }

        public override string ToString()
        {
            return this.ToString(null, null);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (this == Blank)
            {
                return "";
            }
            decimal num = Tools.Math.RoundCy((decimal)(this.v * 100M));
            if (format == "n")
            {
                return num.ToString();
            }
            return string.Format(provider, "{0}%", new object[] { num });
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString(null, provider);
        }

        public static bool operator ==(Percent c1, Percent c2)
        {
            return (c1.v == c2.v);
        }

        public static bool operator !=(Percent c1, Percent c2)
        {
            return (c1.v != c2.v);
        }

        public static bool operator <(Percent c1, Percent c2)
        {
            return (c1.v < c2.v);
        }

        public static bool operator >(Percent c1, Percent c2)
        {
            return (c1.v > c2.v);
        }

        public static bool operator <=(Percent c1, Percent c2)
        {
            return (c1.v <= c2.v);
        }

        public static bool operator >=(Percent c1, Percent c2)
        {
            return (c1.v >= c2.v);
        }

        public override bool Equals(object o)
        {
            return ((o is Percent) && (this == ((Percent)o)));
        }

        public override int GetHashCode()
        {
            return this.v.GetHashCode();
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (!(obj is Percent))
            {
                throw new ArgumentException();
            }
            return this.v.CompareTo(((Percent)obj).v);
        }

        public static implicit operator decimal(Percent p)
        {
            return p.v;
        }

        public static explicit operator double(Percent p)
        {
            return (double)p.v;
        }

        public static explicit operator Fraction(Percent p)
        {
            if (p == Zero)
            {
                return Fraction.Zero;
            }
            int num = (int)Tools.Math.Round((decimal)(p.v * 10000M));
            int den = 0x2710;
            Fraction fraction = new Fraction(num, den);
            return fraction.Normalized;
        }

        public static Percent operator +(Percent p1, Percent p2)
        {
            return new Percent(p1.v + p2.v);
        }

        public static Percent operator -(Percent p1, Percent p2)
        {
            return new Percent(p1.v - p2.v);
        }

        public static double operator *(double v, Percent p)
        {
            return (v * ((double)p));
        }

        public static double operator /(double v, Percent p)
        {
            return (v / ((double)p));
        }

        public static decimal operator *(decimal v, Percent p)
        {
            return (v * p);
        }

        public static decimal operator /(decimal v, Percent p)
        {
            return (v / p);
        }

        public static Currency operator *(Currency v, Percent p)
        {
            return new Currency(v.Value * p, v.Symbol);
        }

        public static Currency operator /(Currency v, Percent p)
        {
            return new Currency(v.Value / p, v.Symbol);
        }

        public static DoubleCy operator *(DoubleCy v, Percent p)
        {
            return new DoubleCy(v.Value * p, v.Symbol);
        }

        public static DoubleCy operator /(DoubleCy v, Percent p)
        {
            return new DoubleCy(v.Value / p, v.Symbol);
        }

        object ISumable.Sum(object sum)
        {
            return new Percent(((Percent)sum).v + this.v);
        }

        object ISumable.Div(int divider)
        {
            return new Percent(this.v / divider);
        }

        [Browsable(false)]
        public bool IsNull
        {
            get
            {
                return (this == Blank);
            }
        }
        static Percent()
        {
            Zero = new Percent(0M);
            Fifty = new Percent(0.5M);
            Hundred = new Percent(1M);
            Blank = new Percent(false);
        }
    }


}
