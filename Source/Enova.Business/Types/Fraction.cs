using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Old.Types.Design;

namespace Enova.Old.Types
{
    [StructLayout(LayoutKind.Sequential), CompoundNames(new string[] { "Num", "Den" }, new string[] { "L", "M" }), DefaultWidth(10), BinSerializable, TypeConverter(typeof(FractionConverter))]
    public struct Fraction : IFormattable, IComparable, ISumable, ICompoundType
    {
        private readonly int num;
        private readonly int den;
        public static readonly Fraction Zero;
        public static readonly Fraction One;
        public static readonly Fraction Half;
        public static readonly Fraction Quarter;

        public Fraction(int num, int den)
        {
            if ((num != 0) && (den == 0))
            {
                throw new ArgumentException("Mianownik nie może byś r\x00f3wny 0.");
            }
            this.num = num;
            this.den = den;
        }

        public Fraction(decimal value)
        {
            decimal num = value;
            this.den = 1;
            while (value != decimal.Truncate(value))
            {
                value *= 10M;
                this.den *= 10;
                if (this.den > 0x3b9aca00)
                {
                    throw new ArgumentException("Wartości " + num + " nie można przekształcić do ułamka (Fraction). Zbyt duża ilość cyfr po przecinku dziesiętnym.");
                }
            }
            this.num = (int)decimal.Truncate(value);
            int num2 = Tools.Math.NDW(this.num, this.den);
            this.num /= num2;
            this.den /= num2;
        }

        public int Num
        {
            get
            {
                return this.num;
            }
        }
        public int Den
        {
            get
            {
                if (this.den == 0)
                {
                    return 1;
                }
                return this.den;
            }
        }
        [Browsable(false)]
        public double Value
        {
            get
            {
                if (this.num == 0)
                {
                    return 0.0;
                }
                return (((double)this.num) / ((double)this.Den));
            }
        }
        [Browsable(false)]
        public Fraction Inverse
        {
            get
            {
                if (this.Value != 0.0)
                {
                    return new Fraction(this.Den, this.Num);
                }
                return Zero;
            }
        }
        public override string ToString()
        {
            return this.ToString(null, null);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString(null, provider);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return string.Format(provider, "{0}/{1}", new object[] { this.num, this.den });
        }

        public static Fraction Parse(string s)
        {
            int index = s.IndexOf('/');
            if (index < 0)
            {
                return new Fraction(decimal.Parse(s));
            }
            return new Fraction(int.Parse(s.Substring(0, index)), int.Parse(s.Substring(index + 1)));
        }

        public static Fraction Parse(string s, IFormatProvider provider)
        {
            int index = s.IndexOf('/');
            if (index < 0)
            {
                return new Fraction(decimal.Parse(s, provider));
            }
            return new Fraction(int.Parse(s.Substring(0, index), provider), int.Parse(s.Substring(index + 1), provider));
        }

        public static bool operator ==(Fraction fract1, Fraction fract2)
        {
            if (fract1.num != fract2.num)
            {
                return false;
            }
            if (fract1.num != 0)
            {
                return (fract1.den == fract2.den);
            }
            return true;
        }

        public static bool operator !=(Fraction fract1, Fraction fract2)
        {
            return !(fract1 == fract2);
        }

        public override bool Equals(object o)
        {
            return ((o is Fraction) && (this == ((Fraction)o)));
        }

        public override int GetHashCode()
        {
            return (this.num.GetHashCode() ^ (0x1d * this.den.GetHashCode()));
        }

        int IComparable.CompareTo(object obj)
        {
            Fraction fraction = (Fraction)obj;
            return this.Value.CompareTo(fraction.Value);
        }

        object[] ICompoundType.Values
        {
            get
            {
                return new object[] { this.Num, this.Den };
            }
        }
        public static explicit operator Percent(Fraction f)
        {
            return new Percent((decimal)f.Value);
        }

        public static implicit operator double(Fraction f)
        {
            return f.Value;
        }

        public static explicit operator decimal(Fraction f)
        {
            return (decimal)f.Value;
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            Fraction fraction = new Fraction((a.Num * b.Den) + (b.Num * a.Den), a.Den * b.Den);
            return fraction.Normalized;
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            Fraction fraction = new Fraction((a.Num * b.Den) - (b.Num * a.Den), a.Den * b.Den);
            return fraction.Normalized;
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            Fraction fraction = new Fraction(a.Num * b.Num, a.Den * b.Den);
            return fraction.Normalized;
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            Fraction fraction = new Fraction(a.Num * b.Den, a.Den * b.Num);
            return fraction.Normalized;
        }

        public static Fraction Max(Fraction a, Fraction b)
        {
            if (((double)a) < ((double)b))
            {
                return b;
            }
            return a;
        }

        public static Fraction Min(Fraction a, Fraction b)
        {
            if (((double)a) > ((double)b))
            {
                return b;
            }
            return a;
        }

        object ISumable.Sum(object sum)
        {
            return (this + ((Fraction)sum));
        }

        object ISumable.Div(int divider)
        {
            return (((double)this) / ((double)divider));
        }

        [Browsable(false)]
        public Fraction Normalized
        {
            get
            {
                int x = this.Num;
                int den = this.Den;
                if (((x == 0) || (x == 1)) || ((den == 0) || (den == 1)))
                {
                    return this;
                }
                int num3 = Tools.Math.NDW(x, den);
                if (num3 == 1)
                {
                    return this;
                }
                return new Fraction(x / num3, den / num3);
            }
        }
        static Fraction()
        {
            Zero = new Fraction(0, 1);
            One = new Fraction(1, 1);
            Half = new Fraction(1, 2);
            Quarter = new Fraction(1, 4);
        }
    }

}
