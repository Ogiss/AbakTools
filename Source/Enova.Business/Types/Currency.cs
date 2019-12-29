using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using Enova.Old.Types.Design;

namespace Enova.Old.Types
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(CurrencyConverter)), CompoundNames(new string[] { "Value", "Symbol" }, new string[] { "", "Waluta" }), BinSerializable, DefaultWidth(15)]
    public struct Currency : IComparable, IFormattable, ISumable, ICompoundType, INullable, IConditionFormattable
    {
        public static readonly string SystemSymbol;
        private static readonly string EmptySymbol;
        private static readonly string MultiSymbol;
        public static readonly string Całkowity;
        public static readonly string Dziesiętny;
        public static readonly Currency Zero;
        public static readonly Currency Multi;
        public static readonly Currency Empty;
        private readonly decimal value;
        private readonly string symbol;
        private static readonly string[] digits;
        private static string[] slownie_1n;
        private static readonly string[] numbers;
        private static readonly string[] numbers2;
        private static readonly string[] numbers3;
        private static readonly object[] emptyValues;
        private static readonly object[] multiValues;

        public Currency(decimal value)
        {
            this.value = Tools.Math.RoundCy(value);
            this.symbol = SystemSymbol;
        }

        public Currency(double value)
        {
            this.value = Tools.Math.RoundCy(value);
            this.symbol = SystemSymbol;
        }

        public Currency(decimal value, string symbol)
        {
            CheckSymbol(symbol);
            this.value = Tools.Math.RoundCy(value);
            this.symbol = symbol.ToUpper();
        }

        public Currency(double value, string symbol)
        {
            CheckSymbol(symbol);
            this.value = Tools.Math.RoundCy(value);
            this.symbol = symbol.ToUpper();
        }

        public Currency(Currency kwota)
        {
            this.value = kwota.Value;
            this.symbol = kwota.Symbol;
        }

        private Currency(string symbol)
        {
            this.value = 0M;
            this.symbol = symbol.ToUpper();
            CheckSymbol(this.symbol);
        }

        public static void CheckSymbol(string symbol)
        {
            if ((symbol == null) || (symbol == ""))
            {
                throw new ArgumentException("Parametr symbol nie może być pusty", "symbol");
            }
            if (symbol.Length > 3)
            {
                throw new CurrencySymbolTooLongException();
            }
        }

        public bool IsSystemSymbol()
        {
            return (this.Symbol == SystemSymbol);
        }

        public bool IsValid()
        {
            return ((this.value == 0M) || ((this.symbol != null) && (this.symbol != "")));
        }

        public decimal Value
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
                    return SystemSymbol;
                }
                return this.symbol;
            }
        }

        [Browsable(false)]
        public string SłowniePZL
        {
            get
            {
                decimal num = this.Value;
                int num2 = (int)num;
                int num3 = (int)((num - num2) * 100M);
                string str = "";
                if (num2 != 0)
                {
                    while (num2 > 0)
                    {
                        str = digits[num2 % 10] + "*" + str;
                        num2 /= 10;
                    }
                }
                else
                {
                    str = digits[0];
                }
                return (str + num3.ToString("00") + "/100*");
            }
        }

        [Browsable(false)]
        public string SłownieUpr
        {
            get
            {
                return SłownieWalutaUpr(this.Value, this.Symbol, "");
            }
        }

        public static string SłownieWalutaUpr(decimal value)
        {
            return SłownieWalutaUpr(value, SystemSymbol, "");
        }

        public static string SłownieWalutaUpr(decimal value, string całkowity, string dziesiętny)
        {
            string str;
            int num;
            int num2;
            if (całkowity == SystemSymbol)
            {
                całkowity = Całkowity;
                dziesiętny = Dziesiętny;
            }
            if (value < 0M)
            {
                str = "-";
                value = -value;
            }
            else
            {
                str = "";
            }
            PodzielKwotę(value, out num, out num2);
            return (str + (num.ToString("n0") + " " + całkowity + " " + num2.ToString("00") + " " + dziesiętny).Trim());
        }

        private static void PodzielKwotę(decimal value, out int c, out int d)
        {
            c = (int)Math.Floor(value);
            d = (int)Math.Round((decimal)(100M * (value - c)));
        }

        [Browsable(false)]
        public string Słownie
        {
            get
            {
                return SłownieWaluta(this.Value, this.Symbol, "");
            }
        }

        public static string SłownieWaluta(decimal value)
        {
            return SłownieWaluta(value, SystemSymbol, "");
        }

        public static string SłownieWaluta(decimal value, string całkowity, string dziesiętny)
        {
            string str;
            int num;
            int num2;
            string str2;
            if (całkowity == SystemSymbol)
            {
                całkowity = Całkowity;
                dziesiętny = "";
            }
            if (value == 0M)
            {
                return "zero";
            }
            if (value < 0M)
            {
                str = "-";
                value = -value;
            }
            else
            {
                str = "";
            }
            PodzielKwotę(value, out num, out num2);
            if (num == 0)
            {
                str2 = "";
            }
            else if (num < 100)
            {
                str2 = Slownie_2N(num);
            }
            else if (num < 0x3e8)
            {
                str2 = Slownie_3N(num);
            }
            else if (num < 0xf4240)
            {
                str2 = Slownie_6N(num);
            }
            else if (num < 0x3b9aca00)
            {
                str2 = Slownie_9N(num);
            }
            else
            {
                str2 = Slownie_12N(num);
            }
            return (str + str2 + " " + całkowity + " " + num2.ToString("00") + "/100 ").Trim();
        }

        private static string Slownie_1N(int i)
        {
            return slownie_1n[i];
        }

        private static string Slownie_2N(int i)
        {
            string str;
            if (i == 0)
            {
                return "";
            }
            if (i <= 10)
            {
                return Slownie_1N(i);
            }
            if (i >= 20)
            {
                if (i < 30)
                {
                    str = "dwadzieścia";
                }
                else if (i < 40)
                {
                    str = "trzydzieści";
                }
                else if (i < 50)
                {
                    str = "czterdzieści";
                }
                else
                {
                    str = Slownie_1N(i / 10) + "dziesiąt";
                }
                return (str + AddPost(Slownie_1N(i % 10)));
            }
            str = Slownie_1N(i - 10);
            switch (i)
            {
                case 11:
                case 14:
                    str = str.Substring(0, str.Length - 1);
                    break;

                case 15:
                case 0x13:
                    str = str.Substring(0, str.Length - 1) + "t";
                    break;

                case 0x10:
                    str = "szes";
                    break;
            }
            return (str + "naście");
        }

        private static string Slownie_3N(int i)
        {
            string str;
            int num = i / 100;
            switch (num)
            {
                case 0:
                    str = "";
                    break;

                case 1:
                    str = "sto";
                    break;

                case 2:
                    str = "dwieście";
                    break;

                case 3:
                case 4:
                    str = Slownie_1N(num) + "sta";
                    break;

                default:
                    str = Slownie_1N(num) + "set";
                    break;
            }
            return (str + AddPost(Slownie_2N(i % 100)));
        }

        private static string Slownie_6N(int i)
        {
            string str;
            if (i < 0x3e8)
            {
                return Slownie_3N(i);
            }
            int num = i / 0x3e8;
            if (num == 1)
            {
                str = "jeden tysiąc";
            }
            else if (((num < 10) || (num > 20)) && (((num % 10) >= 2) && ((num % 10) <= 4)))
            {
                str = Slownie_3N(num) + " tysiące";
            }
            else
            {
                str = Slownie_3N(num) + " tysięcy";
            }
            return (str + AddPost(Slownie_3N(i % 0x3e8)));
        }

        private static string Slownie_9N(int i)
        {
            if (i < 0xf4240)
            {
                return Slownie_6N(i);
            }
            int num = i / 0xf4240;
            string str = " milion";
            if (num > 1)
            {
                str = Slownie_6N(num) + str;
                if (((num < 10) || (num > 20)) && (((num % 10) >= 2) && ((num % 10) <= 4)))
                {
                    str = str + "y";
                }
                else
                {
                    str = str + "\x00f3w";
                }
            }
            else
            {
                str = "jeden" + str;
            }
            return (str + AddPost(Slownie_6N(i % 0xf4240)));
        }

        private static string Slownie_12N(int i)
        {
            int num = i / 0x3b9aca00;
            string str = " miliard";
            if (num > 1)
            {
                str = Slownie_9N(num) + str;
                if (((num < 10) || (num > 20)) && (((num % 10) >= 2) && ((num % 10) <= 4)))
                {
                    str = str + "y";
                }
                else
                {
                    str = str + "\x00f3w";
                }
            }
            else
            {
                str = "jeden" + str;
            }
            return (str + AddPost(Slownie_9N(i % 0x3b9aca00)));
        }

        private static string AddPost(string s)
        {
            if (s != "")
            {
                return (" " + s);
            }
            return "";
        }

        public static string SłownieWalutaEng(decimal value, string symbol)
        {
            string str;
            bool flag;
            if (flag = value < 0M)
            {
                value = -value;
            }
            if (value > 999999999.99M)
            {
                str = value.ToString("n") + " " + symbol;
            }
            else
            {
                long num = (long)Math.Floor((double)value);
                long num2 = (long)Math.Round((decimal)(100M * (value - num)));
                if (num == 0L)
                {
                    str = "zero";
                }
                else
                {
                    str = "";
                    for (int i = 0; num > 0L; i++)
                    {
                        string str2 = SłownieEng999(num % 0x3e8L);
                        if (str2 != "")
                        {
                            if (i > 0)
                            {
                                str2 = str2 + " " + numbers3[i];
                            }
                            if (str == "")
                            {
                                str = str2;
                            }
                            else
                            {
                                str = str2 + " " + str;
                            }
                        }
                        num /= 0x3e8L;
                    }
                }
                if (symbol != "")
                {
                    str = str + " " + symbol;
                }
                str = str + " " + num2.ToString("00") + "/100";
            }
            if (flag)
            {
                str = "- " + str;
            }
            return str;
        }

        private static string SłownieEng999(long v)
        {
            long num = v / 100L;
            long num2 = v % 100L;
            string str = (num > 0L) ? (numbers[(int)((IntPtr)num)] + " " + numbers2[10]) : "";
            if (num2 > 0L)
            {
                if (str != "")
                {
                    str = str + " ";
                }
                if (num2 < 20L)
                {
                    return (str + numbers[(int)((IntPtr)num2)]);
                }
                str = str + numbers2[(int)((IntPtr)(num2 / 10L))];
                num2 = num2 % 10L;
                if (num2 > 0L)
                {
                    str = str + " " + numbers[(int)((IntPtr)num2)];
                }
            }
            return str;
        }

        [Browsable(false)]
        public string SłownieEng
        {
            get
            {
                return SłownieWalutaEng(this.Value, this.Symbol);
            }
        }

        public static string SlownieWalutaGer(decimal value, string symbol1, string symbol2)
        {
            if (value == 0M)
            {
                return "null";
            }
            string str = (value < 0M) ? "minus " : "";
            if (value < 0M)
            {
                value = -value;
            }
            long number = (long)Math.Floor(value);
            int num2 = (int)(100M * (value - number));
            string str2 = IntToGerman(number, false) + ((symbol1 != "") ? (" " + symbol1 + " ") : " ");
            return (str + str2 + num2.ToString("00") + "/100");
        }

        public static string IntToGerman(long number, bool nulltostring)
        {
            if (number == 0L)
            {
                if (nulltostring)
                {
                    return "null";
                }
                return "";
            }
            if (number > 0xe8d4a50fffL)
            {
                throw new Exception(string.Format("Zbyt duża wartość liczby: {0}", number));
            }
            return string.Format("{0}{1}{2}{3}", new object[] { toGerman999(toNum999(number, 0x3b9aca00), " Milliarden ", "eine Milliarde "), toGerman999(toNum999(number, 0xf4240), " Millionen ", "eine Million "), toGerman999(toNum999(number, 0x3e8), "tausend", "eintausend"), toGerman999(toNum999(number, 1), "", "eins") }).Trim();
        }

        private static int toNum999(long number, int level)
        {
            decimal num = Math.Floor((decimal)(number / ((long)level)));
            decimal num2 = Math.Floor((decimal)(num / 1000M)) * 1000M;
            return (int)(num - num2);
        }

        private static string toGerman999(int number, string symbol, string ein)
        {
            string str = "";
            if (ein == "")
            {
                ein = "eins";
            }
            if (number == 0)
            {
                return "";
            }
            if (number == 1)
            {
                return ein;
            }
            int num = number % 100;
            int num2 = (number - num) / 100;
            if (num2 != 0)
            {
                str = toGerman99(num2) + "hundert";
            }
            if (num <= 20)
            {
                str = str + toGerman99(num);
            }
            else
            {
                int num3 = num % 10;
                int num4 = num - num3;
                str = str + ((num3 != 0) ? (toGerman99(num3) + "und") : "") + toGerman99(num4);
            }
            return (str + symbol);
        }

        private static string toGerman99(int number)
        {
            switch (number)
            {
                case 0:
                    return "";

                case 1:
                    return "ein";

                case 2:
                    return "zwei";

                case 3:
                    return "drei";

                case 4:
                    return "vier";

                case 5:
                    return "f\x00fcnf";

                case 6:
                    return "sechs";

                case 7:
                    return "sieben";

                case 8:
                    return "acht";

                case 9:
                    return "neun";

                case 10:
                    return "zehn";

                case 11:
                    return "elf";

                case 12:
                    return "zw\x00f6lf";

                case 13:
                    return "dreizehn";

                case 14:
                    return "vierzehn";

                case 15:
                    return "f\x00fcnfzehn";

                case 0x10:
                    return "sechzehn";

                case 0x11:
                    return "siebzehn";

                case 0x12:
                    return "achtzehn";

                case 0x13:
                    return "neunzehn";

                case 20:
                    return "zwanzig";

                case 30:
                    return "drei\x00dfig";

                case 40:
                    return "vierzig";

                case 50:
                    return "f\x00fcnfzig";

                case 60:
                    return "sechzig";

                case 70:
                    return "siebzig";

                case 80:
                    return "achtzig";

                case 90:
                    return "neunzig";
            }
            throw new Exception(string.Format("Błędne wywołanie funkcji toGerman99({0})", number));
        }

        [Browsable(false)]
        public string SłownieGer
        {
            get
            {
                return SlownieWalutaGer(this.Value, this.Symbol, string.Empty);
            }
        }

        public static Currency Parse(string value)
        {
            return Parse(value, null);
        }

        public static Currency Parse(string value, IFormatProvider provider)
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
                        return new Currency(0M, s.Substring(i, length - i));
                    }
                    return new Currency(decimal.Parse(str2, provider), s.Substring(i, length - i));
                }
            }
            return new Currency(decimal.Parse(s, provider));
        }

        public override string ToString()
        {
            return this.ToString(null, null);
        }

        public Currency AddExt(Currency cy)
        {
            if ((this.Symbol == MultiSymbol) || (cy.Symbol == MultiSymbol))
            {
                return Multi;
            }
            if ((this.Symbol == EmptySymbol) || (this.Value == 0M))
            {
                return cy;
            }
            if ((cy.Symbol == EmptySymbol) || (cy.Value == 0M))
            {
                return this;
            }
            if (this.Symbol != cy.Symbol)
            {
                return Multi;
            }
            return (this + cy);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (this.Symbol == EmptySymbol)
            {
                return "";
            }
            if (this.Symbol == MultiSymbol)
            {
                return "{r\x00f3żne waluty}";
            }
            switch (format)
            {
                case "t":
                    return this.Słownie;

                case "+t":
                    return this.Słownie.Replace("-", "");

                case "e":
                    return this.SłownieEng;

                case "+e":
                    return this.SłownieEng.Replace("-", "");

                case "p":
                    return this.SłowniePZL;

                case "+p":
                    return this.SłowniePZL.Replace("-", "");

                case "T":
                case "u":
                    if (!(this.Symbol == SystemSymbol))
                    {
                        format = "{0:n} {1}";
                        break;
                    }
                    return this.SłownieUpr;

                case "+T":
                case "+u":
                    return this.SłownieUpr.Replace("-", "");

                case "n":
                    format = "{0:n}";
                    break;

                case "s":
                    format = "{1}";
                    break;

                case "v":
                    format = "{0:n}";
                    break;

                case "g":
                case "gr":
                    {
                        int num2 = (int)(this.Value * 100M);
                        return num2.ToString();
                    }
                case ".":
                    return this.Value.ToString(CultureInfo.InvariantCulture);

                case ",":
                    return this.Value.ToString(CultureInfo.InvariantCulture).Replace('.', ',');

                case "d":
                    format = "{0} {1}";
                    break;

                default:
                    format = "{0:n} {1}";
                    break;
            }
            return string.Format(provider, format, new object[] { this.Value, this.Symbol });
        }

        string IConditionFormattable.ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} {1}", new object[] { this.Value, this.Symbol });
        }

        private static void Equalable(Currency c1, Currency c2)
        {
            if (((c1.Value != 0.0M) && (c2.Value != 0.0M)) && (c1.Symbol != c2.Symbol))
            {
                throw new VariousCurrencyException(c1.Symbol, c2.Symbol);
            }
        }

        public static bool operator ==(Currency c1, Currency c2)
        {
            Equalable(c1, c2);
            return (c1.value == c2.value);
        }

        public static bool operator !=(Currency c1, Currency c2)
        {
            Equalable(c1, c2);
            return (c1.value != c2.value);
        }

        public static bool operator <(Currency c1, Currency c2)
        {
            Equalable(c1, c2);
            return (c1.Value < c2.Value);
        }

        public static bool operator >(Currency c1, Currency c2)
        {
            Equalable(c1, c2);
            return (c1.Value > c2.Value);
        }

        public static bool operator <=(Currency c1, Currency c2)
        {
            Equalable(c1, c2);
            return (c1.Value <= c2.Value);
        }

        public static bool operator >=(Currency c1, Currency c2)
        {
            Equalable(c1, c2);
            return (c1.Value >= c2.Value);
        }

        public override bool Equals(object o)
        {
            return ((o is Currency) && this.Equals((Currency)o));
        }

        public bool Equals(Currency cy)
        {
            return ((this.value == cy.value) && (this.Symbol == cy.Symbol));
        }

        public override int GetHashCode()
        {
            return (this.value.GetHashCode() ^ (0x1d * this.Symbol.GetHashCode()));
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (!(obj is Currency))
            {
                throw new ArgumentException();
            }
            Currency currency = (Currency)obj;
            int num = this.Symbol.CompareTo(currency.Symbol);
            if (num == 0)
            {
                num = this.value.CompareTo(currency.value);
            }
            return num;
        }

        public Currency Add(Currency c)
        {
            if (this.Symbol == EmptySymbol)
            {
                return c;
            }
            if (c.Symbol == EmptySymbol)
            {
                return this;
            }
            return (c + this);
        }

        public static Currency operator +(Currency c1, Currency c2)
        {
            if (c1.Symbol != c2.Symbol)
            {
                throw new VariousCurrencyException(c1.Symbol, c2.Symbol);
            }
            return new Currency(c1.value + c2.value, c1.Symbol);
        }

        public static Currency operator -(Currency c1, Currency c2)
        {
            if (c1.Symbol != c2.Symbol)
            {
                throw new VariousCurrencyException(c1.Symbol, c2.Symbol);
            }
            return new Currency(c1.value - c2.value, c1.Symbol);
        }

        public static DoubleCy operator *(Currency c1, double procent)
        {
            return new DoubleCy(((double)c1.value) * procent, c1.Symbol);
        }

        public static DoubleCy operator *(double procent, Currency c1)
        {
            return new DoubleCy(((double)c1.value) * procent, c1.Symbol);
        }

        public static DoubleCy operator *(Currency c1, decimal procent)
        {
            return (DoubleCy)(c1 * ((double)procent));
        }

        public static DoubleCy operator *(decimal procent, Currency c1)
        {
            return (DoubleCy)(c1 * ((double)procent));
        }

        public static DoubleCy operator *(Currency c1, int n)
        {
            return (DoubleCy)(c1 * n);
        }

        public static DoubleCy operator *(int n, Currency c1)
        {
            return (DoubleCy)(c1 * n);
        }

        public static DoubleCy operator /(Currency c1, int n)
        {
            if (n == 0)
            {
                throw new ArgumentException("Parametr musi przyjmować wartość r\x00f3żną od zera", "n");
            }
            return new DoubleCy(((double)c1.value) / ((double)n), c1.Symbol);
        }

        public static double operator /(Currency c1, Currency c2)
        {
            if (c1.Symbol != c2.Symbol)
            {
                throw new VariousCurrencyException(c1.Symbol, c2.Symbol);
            }
            return (((double)c1.value) / ((double)c2.value));
        }

        public static DoubleCy operator /(Currency cy, Time t)
        {
            if ((t == Time.Empty) || (t == Time.Zero))
            {
                throw new ArgumentException();
            }
            return new DoubleCy(((double)cy.Value) / t.TotalHours, cy.Symbol);
        }

        public static Currency operator -(Currency c1)
        {
            return new Currency(-c1.value, c1.Symbol);
        }

        object ISumable.Sum(object sum)
        {
            if ((sum is string) || (this.Symbol == EmptySymbol))
            {
                return sum;
            }
            Currency currency = (Currency)sum;
            if (currency.Symbol == EmptySymbol)
            {
                return this;
            }
            Currency currency2 = (Currency)sum;
            if (currency2.Symbol != this.Symbol)
            {
                return "r\x00f3żne waluty";
            }
            return (((Currency)sum) + this);
        }

        object ISumable.Div(int divider)
        {
            return new Currency(this.Value / divider, this.Symbol);
        }

        public static implicit operator DoubleCy(Currency value)
        {
            return new DoubleCy(value);
        }

        public static implicit operator Currency(decimal value)
        {
            return new Currency(value);
        }

        public static bool EqualSymbols(string symbol1, string symbol2)
        {
            return (symbol1 == symbol2);
        }

        public static bool EqualSymbols(Currency cy1, Currency cy2)
        {
            return (cy1.Symbol == cy2.Symbol);
        }

        object[] ICompoundType.Values
        {
            get
            {
                if (this == Empty)
                {
                    return emptyValues;
                }
                if (this == Multi)
                {
                    return multiValues;
                }
                return new object[] { this.Value, this.symbol };
            }
        }

        public static Currency Max(Currency c1, Currency c2)
        {
            if (c1 >= c2)
            {
                return c1;
            }
            return c2;
        }

        public static Currency Min(Currency c1, Currency c2)
        {
            if (c1 <= c2)
            {
                return c1;
            }
            return c2;
        }

        public static Currency Min0(Currency c1, Currency c2)
        {
            Currency currency = (c1 > c2) ? c2 : c1;
            if (currency < Zero)
            {
                return new Currency(0M, currency.Symbol);
            }
            return currency;
        }

        public static Currency Abs(Currency c)
        {
            if (c.Value < 0M)
            {
                return new Currency(-c.Value, c.Symbol);
            }
            return c;
        }

        public static Currency Round(Currency c, int digits)
        {
            return new Currency(Math.Round(c.Value, digits), c.Symbol);
        }

        public Currency Round(int digits)
        {
            return new Currency(Math.Round(this.Value, digits), this.Symbol);
        }

        public static Currency RoundZl(Currency c)
        {
            return Round(c, 0);
        }

        public Currency RoundZl()
        {
            return this.Round(0);
        }

        [Browsable(false)]
        public bool IsNull
        {
            get
            {
                return (this.Symbol == EmptySymbol);
            }
        }

        static Currency()
        {
            SystemSymbol = "PLN";
            EmptySymbol = "???";
            MultiSymbol = "$$$";
            Całkowity = "zł";
            Dziesiętny = "gr";
            Zero = new Currency();
            Multi = new Currency(MultiSymbol);
            Empty = new Currency(EmptySymbol);
            digits = new string[] { "zer", "jed", "dwa", "trz", "czt", "pie", "sze", "sie", "osi", "dzi" };
            slownie_1n = new string[] { "", "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć", "dziesięć" };
            numbers = new string[] { 
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", 
            "sixteen", "seventeen", "eighteen", "nineteen"
         };
            numbers2 = new string[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety", "hundred" };
            numbers3 = new string[] { "zero", "thousand", "million" };
            emptyValues = new object[] { "", "" };
            multiValues = new object[] { "?", "?" };
        }
    }

}
