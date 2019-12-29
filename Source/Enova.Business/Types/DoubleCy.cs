using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Globalization;
using Enova.Old.Types.Design;

namespace Enova.Old.Types
{
    [StructLayout(LayoutKind.Sequential), CompoundNames(new string[] { "Value", "Symbol" }, new string[] { "", "Waluta" }), BinSerializable, TypeConverter(typeof(DoubleCyConverter)), DefaultWidth(15)]
    public struct DoubleCy : IComparable, IFormattable, ISumable, ICompoundType, IConditionFormattable
    {
        public static readonly DoubleCy Zero;
        private readonly double value;
        private readonly string symbol;

        public DoubleCy(decimal value)
        {
            this.value = (double)value;
            this.symbol = Currency.SystemSymbol;
            this.checkValue();
        }

        public DoubleCy(double value)
        {
            this.value = value;
            this.symbol = Currency.SystemSymbol;
            this.checkValue();
        }

        public DoubleCy(decimal value, string symbol)
        {
            if ((symbol == null) || (symbol == ""))
            {
                throw new ArgumentException("Parametr symbol nie może być pusty", "symbol");
            }
            this.value = (double)value;
            this.symbol = symbol.ToUpper();
            this.checkValue();
        }

        public DoubleCy(double value, string symbol)
        {
            if ((symbol == null) || (symbol == ""))
            {
                throw new ArgumentException("Parametr symbol nie może być pusty", "symbol");
            }
            this.value = value;
            this.symbol = symbol.ToUpper();
            this.checkValue();
        }

        public DoubleCy(Currency kwota)
        {
            this.value = (double)kwota.Value;
            this.symbol = kwota.Symbol;
            this.checkValue();
        }

        public DoubleCy(DoubleCy kwota)
        {
            this.value = kwota.Value;
            this.symbol = kwota.Symbol;
            this.checkValue();
        }

        private void checkValue()
        {
            Currency.CheckSymbol(this.Symbol);
            if (double.IsNaN(this.value) || double.IsInfinity(this.value))
            {
                throw new InvalidOperationException("Błędna inicjacja DoubleCy");
            }
        }

        public bool IsValid()
        {
            return ((this.value == 0.0) || ((this.symbol != null) && (this.symbol != "")));
        }

        public double Value
        {
            get
            {
                return this.value;
            }
        }

        [MaxLength(3)]
        public string Symbol
        {
            get
            {
                if (this.symbol == null)
                {
                    return Currency.SystemSymbol;
                }
                return this.symbol;
            }
        }

        public bool IsMaxPrecision(int nr)
        {
            decimal d = (decimal)this.Value;
            return (Math.Round(d, nr) == d);
        }

        public bool IsMaxPrecisionCy()
        {
            return this.IsMaxPrecision(2);
        }

        public override string ToString()
        {
            return this.ToString(null, null);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            switch (format)
            {
                case "d":
                    return string.Format(provider, "{0} {1}", new object[] { this.Value, this.Symbol });

                case "v":
                    if (this.IsMaxPrecisionCy())
                    {
                        return string.Format(provider, "{0:n}", new object[] { this.Value });
                    }
                    return this.Value.ToString(provider);

                case "s":
                    return this.Symbol;
            }
            if (this.IsMaxPrecisionCy())
            {
                return string.Format(provider, "{0:n} {1}", new object[] { this.Value, this.Symbol });
            }
            return string.Format(provider, "{0} {1}", new object[] { this.Value, this.Symbol });
        }

        string IConditionFormattable.ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} {1}", new object[] { this.Value, this.Symbol });
        }

        public static DoubleCy Parse(string value)
        {
            return Parse(value, null);
        }

        public static DoubleCy Parse(string value, IFormatProvider provider)
        {
            string s = value.Trim();
            if (s == "")
            {
                return Zero;
            }
            int length = s.Length;
            for (int i = 0; i < length; i++)
            {
                if (char.IsLetter(s, i))
                {
                    string str2 = s.Substring(0, i);
                    if (str2.Trim() == "")
                    {
                        return new DoubleCy(0M, s.Substring(i, length - i));
                    }
                    return new DoubleCy(double.Parse(str2, provider), s.Substring(i, length - i));
                }
            }
            return new DoubleCy(double.Parse(s, provider));
        }

        private static void Equalable(DoubleCy c1, DoubleCy c2)
        {
            if (((c1.Value != 0.0) && (c2.Value != 0.0)) && (c1.Symbol != c2.Symbol))
            {
                throw new VariousCurrencyException(c1.Symbol, c2.Symbol);
            }
        }

        public static bool operator ==(DoubleCy c1, DoubleCy c2)
        {
            Equalable(c1, c2);
            return (c1.value == c2.value);
        }

        public static bool operator !=(DoubleCy c1, DoubleCy c2)
        {
            Equalable(c1, c2);
            return !(c1.value == c2.value);
        }

        public static bool operator <(DoubleCy c1, DoubleCy c2)
        {
            Equalable(c1, c2);
            return (c1.Value < c2.Value);
        }

        public static bool operator >(DoubleCy c1, DoubleCy c2)
        {
            Equalable(c1, c2);
            return (c1.Value > c2.Value);
        }

        public static bool operator <=(DoubleCy c1, DoubleCy c2)
        {
            Equalable(c1, c2);
            return (c1.Value <= c2.Value);
        }

        public static bool operator >=(DoubleCy c1, DoubleCy c2)
        {
            Equalable(c1, c2);
            return (c1.Value >= c2.Value);
        }

        public override bool Equals(object o)
        {
            return ((o is DoubleCy) && this.Equals((DoubleCy)o));
        }

        public bool Equals(DoubleCy dcy)
        {
            return ((this.value == dcy.value) && (this.Symbol == dcy.Symbol));
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
            if (!(obj is DoubleCy))
            {
                throw new ArgumentException();
            }
            DoubleCy cy = (DoubleCy)obj;
            int num = string.Compare(this.Symbol, cy.Symbol, true);
            if (num != 0)
            {
                return num;
            }
            return this.value.CompareTo(cy.value);
        }

        public static DoubleCy operator +(DoubleCy c1, DoubleCy c2)
        {
            if (c1 == Zero)
            {
                return c2;
            }
            if (c2 == Zero)
            {
                return c1;
            }
            if (c1.Symbol != c2.Symbol)
            {
                throw new VariousCurrencyException(c1.Symbol, c2.Symbol);
            }
            return new DoubleCy(c1.value + c2.value, c1.Symbol);
        }

        public static DoubleCy operator +(DoubleCy c1, long x)
        {
            return (c1 + ((DoubleCy)x));
        }

        public static DoubleCy operator +(DoubleCy c1, int x)
        {
            return (c1 + ((DoubleCy)x));
        }

        public static DoubleCy operator +(DoubleCy c1, double x)
        {
            return new DoubleCy(c1.value + x, c1.Symbol);
        }

        public static DoubleCy operator -(DoubleCy c1, int x)
        {
            return (c1 - ((DoubleCy)x));
        }

        public static DoubleCy operator -(DoubleCy c1, long x)
        {
            return (c1 - ((DoubleCy)x));
        }

        public static DoubleCy operator -(DoubleCy c1, double x)
        {
            return new DoubleCy(c1.value - x, c1.Symbol);
        }

        public static DoubleCy operator -(DoubleCy c1, DoubleCy c2)
        {
            if (c1.Symbol != c2.Symbol)
            {
                throw new VariousCurrencyException(c1.Symbol, c2.Symbol);
            }
            return new DoubleCy(c1.value - c2.value, c1.Symbol);
        }

        public static DoubleCy operator *(DoubleCy c1, double x)
        {
            return new DoubleCy(c1.value * x, c1.Symbol);
        }

        public static DoubleCy operator *(DoubleCy c1, decimal x)
        {
            return (DoubleCy)(c1 * ((double)x));
        }

        public static DoubleCy operator *(DoubleCy c1, int n)
        {
            return (c1 * n);
        }

        public static DoubleCy operator /(DoubleCy c1, int n)
        {
            if (n == 0)
            {
                throw new ArgumentException("Parametr musi przyjmować wartość r\x00f3żną od zera", "n");
            }
            return new DoubleCy(c1.value / ((double)n), c1.Symbol);
        }

        public static DoubleCy operator /(DoubleCy dcy, Time t)
        {
            if ((t == Time.Empty) || (t == Time.Zero))
            {
                throw new ArgumentException();
            }
            return new DoubleCy(dcy.Value / t.TotalHours, dcy.Symbol);
        }

        public static DoubleCy operator /(DoubleCy c1, double x)
        {
            if (x == 0.0)
            {
                throw new ArgumentException("Parametr musi przyjmować wartość r\x00f3żną od zera", "x");
            }
            return new DoubleCy(c1.value / x, c1.Symbol);
        }

        public static DoubleCy operator -(DoubleCy c1)
        {
            return new DoubleCy(-c1.value, c1.Symbol);
        }

        public static double operator /(DoubleCy c1, DoubleCy c2)
        {
            if (c1.Symbol != c2.Symbol)
            {
                throw new VariousCurrencyException(c1.Symbol, c2.Symbol);
            }
            return (c1.value / c2.value);
        }

        object ISumable.Sum(object sum)
        {
            if (sum is string)
            {
                return sum;
            }
            DoubleCy cy = (DoubleCy)sum;
            if (cy.Symbol != this.Symbol)
            {
                return "r\x00f3żne waluty";
            }
            return (((DoubleCy)sum) + this);
        }

        object ISumable.Div(int divider)
        {
            return new DoubleCy(this.Value / ((double)divider), this.Symbol);
        }

        public static implicit operator DoubleCy(decimal value)
        {
            return new DoubleCy(value);
        }

        public static implicit operator Currency(DoubleCy value)
        {
            return new Currency(value.value, value.Symbol);
        }

        public static DoubleCy FromObj(object obj)
        {
            if (obj == null)
            {
                return Zero;
            }
            if (obj is decimal)
            {
                return (decimal)obj;
            }
            if (obj is double)
            {
                return new DoubleCy((double)obj);
            }
            if (obj is int)
            {
                return (DoubleCy)((int)obj);
            }
            if (obj is Currency)
            {
                return (DoubleCy)((Currency)obj);
            }
            if (!(obj is DoubleCy))
            {
                throw new NotSupportedException("Konwersja z typu " + obj.GetType() + " do typu DoubleCy nie jest obsługiwana");
            }
            return (DoubleCy)obj;
        }

        object[] ICompoundType.Values
        {
            get
            {
                return new object[] { this.Value, this.symbol };
            }
        }

        public Currency RoundCy()
        {
            return new Currency(Math.Round(this.value, 2), this.Symbol);
        }

        public DoubleCy Round(int digits)
        {
            return new DoubleCy(Math.Round(this.value, digits), this.Symbol);
        }

        public DoubleCy Floor(int digits)
        {
            return new DoubleCy(Tools.Math.Floor(this.value, digits), this.Symbol);
        }

        public DoubleCy Ceiling(int digits)
        {
            return new DoubleCy(Tools.Math.Ceiling(this.value, digits), this.Symbol);
        }

        public DoubleCy Round100(RoundMethod method, double round, double delta, int baseRound)
        {
            if (method == RoundMethod.Progresywny)
            {
                return this.Round100(round, delta);
            }
            if (method == RoundMethod.Prosty)
            {
                //return new DoubleCy(this.Round((int)round) - ((DoubleCy)delta));
                return new DoubleCy(this.Round((int)round) - delta);
            }
            if (method != RoundMethod.WgCyfrZnaczacych)
            {
                return new DoubleCy(this.value, this.Symbol);
            }
            string numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string[] strArray = Convert.ToString(Math.Round(this.value, baseRound)).Split(new string[] { numberDecimalSeparator }, StringSplitOptions.RemoveEmptyEntries);
            string str2 = string.Empty;
            if (round <= strArray[0].Length)
            {
                int length = strArray[0].Length;
                string s = strArray[0].Substring(0, (int)round) + new string('0', wyliczDokładnośćDlaLiczbyCałkowitej(delta));
                delta = wyliczPomniejszenie(delta);
                double num2 = double.Parse(s) - delta;
                s = Convert.ToString(num2);
                str2 = s + new string('0', length - s.Length);
            }
            else
            {
                round -= strArray[0].Length;
                string str4 = strArray[1].Substring(0, (int)round);
                str4 = (Parse(str4 + new string('0', strArray[1].Length - str4.Length)).Value - wyliczPomniejszenie(delta)).ToString();
                str2 = strArray[0] + numberDecimalSeparator + str4;
            }
            return Parse(str2);
        }

        public DoubleCy Round100(double round, double delta)
        {
            double num = measure100(this.value);
            round *= num;
            double num2 = round * Math.Floor((double)((this.value / round) + 0.50000000001));
            return new DoubleCy(num2 - (delta * num), this.Symbol);
        }

        private static double measure100(double v)
        {
            if (v <= 0.000200000001)
            {
                return 1E-06;
            }
            if (v <= 0.00200000001)
            {
                return 1E-05;
            }
            if (v <= 0.0200000001)
            {
                return 0.0001;
            }
            if (v <= 0.200000001)
            {
                return 0.001;
            }
            if (v <= 2.00000001)
            {
                return 0.01;
            }
            if (v <= 20.00000001)
            {
                return 0.1;
            }
            if (v <= 200.00000001)
            {
                return 1.0;
            }
            if (v <= 2000.00000001)
            {
                return 10.0;
            }
            if (v <= 20000.00000001)
            {
                return 100.0;
            }
            if (v <= 200000.00000001)
            {
                return 1000.0;
            }
            if (v <= 2000000.00000001)
            {
                return 10000.0;
            }
            return 100000.0;
        }

        private static int wyliczDokładnośćDlaLiczbyCałkowitej(double pomniejszenie)
        {
            if ((pomniejszenie < 1.0) && (pomniejszenie >= 0.1))
            {
                return 1;
            }
            if ((pomniejszenie < 0.1) && (pomniejszenie >= 0.01))
            {
                return 2;
            }
            if ((pomniejszenie < 0.01) && (pomniejszenie >= 0.001))
            {
                return 3;
            }
            return 0;
        }

        private static double wyliczPomniejszenie(double pomniejszenie)
        {
            if ((pomniejszenie < 1.0) && (pomniejszenie >= 0.1))
            {
                return (pomniejszenie * 10.0);
            }
            if ((pomniejszenie < 0.1) && (pomniejszenie >= 0.01))
            {
                return (pomniejszenie * 100.0);
            }
            if ((pomniejszenie < 0.01) && (pomniejszenie >= 0.001))
            {
                return (pomniejszenie * 1000.0);
            }
            return pomniejszenie;
        }

        public static bool EqualSymbols(DoubleCy c1, DoubleCy c2)
        {
            return (c1.Symbol == c2.Symbol);
        }

        public static DoubleCy Max(DoubleCy c1, DoubleCy c2)
        {
            if (c1 >= c2)
            {
                return c1;
            }
            return c2;
        }

        public static DoubleCy Min(DoubleCy c1, DoubleCy c2)
        {
            if (c1 <= c2)
            {
                return c1;
            }
            return c2;
        }

        public static DoubleCy Round(DoubleCy c, int digits)
        {
            return new DoubleCy(Math.Round(c.Value, digits), c.Symbol);
        }

        private void Test()
        {
            DoubleCy cy = Zero + 0;
            cy += (DoubleCy)0L;
            cy += 0M;
            cy += 0.0;
            cy += 0.0;
            cy += 0;
            cy += 0;
            cy -= 0;
            cy -= (DoubleCy)0L;
            cy -= 0M;
            cy -= 0.0;
            cy -= 0.0;
            cy -= 0;
            cy += 0;
        }

        static DoubleCy()
        {
            Zero = new DoubleCy();
        }
        // Nested Types
        public enum RoundMethod
        {
            Brak = 0,
            Progresywny = 1,
            Prosty = 2,
            //[Caption("Wg cyfr znaczących")]
            WgCyfrZnaczacych = 3
        }
    }
}
