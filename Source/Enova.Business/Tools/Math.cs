using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Data.SqlTypes;
using System.Globalization;

namespace Enova.Old.Tools
{
    public static class Math
    {
        // Fields
        [DecimalConstant(0x11, 0x80, (uint)0, (uint)0, (uint)1)]
        public static readonly decimal BlankDecimal = -0.00000000000000001M;
        [DecimalConstant(10, 0, (uint)0, (uint)0, (uint)1)]
        private static readonly decimal PrecisionDecimal = 0.0000000001M;
        private const double PrecisionDouble = 1E-10;
        private static int[] tensT = new int[] { 1, 10, 100, 0x3e8, 0x2710, 0x186a0, 0xf4240, 0x989680, 0x5f5e100, 0x3b9aca00 };

        // Methods
        public static double Ceiling(double d)
        {
            return Math.Ceiling(d);
        }

        public static decimal Ceiling(decimal d, int digits)
        {
            if (digits < 0)
            {
                return (decimal.Ceiling(d / tens(-digits)) * tens(-digits));
            }
            if (digits == 0)
            {
                return decimal.Ceiling(d);
            }
            return (decimal.Ceiling(d * tens(digits)) / tens(digits));
        }

        public static double Ceiling(double d, int digits)
        {
            if (digits < 0)
            {
                return (Math.Ceiling((double)(d / ((double)tens(-digits)))) * tens(-digits));
            }
            if (digits == 0)
            {
                return Math.Ceiling(d);
            }
            return (Math.Ceiling((double)(d * tens(digits))) / ((double)tens(digits)));
        }

        public static decimal CeilingCy(decimal d)
        {
            return Ceiling(d, 2);
        }

        public static decimal CeilingCy(double d)
        {
            return (decimal)Ceiling(d, 2);
        }

        public static decimal CeilingSafeCy(decimal d)
        {
            return Ceiling(Round(d, 6), 2);
        }

        public static decimal CeilingSafeCy(double d)
        {
            return (decimal)Ceiling(Round(d, 6), 2);
        }

        public static int Compare(decimal v1, decimal v2)
        {
            if (v1 < (v2 - 0.0000000001M))
            {
                return -1;
            }
            if (v1 > (v2 + 0.0000000001M))
            {
                return 1;
            }
            return 0;
        }

        public static int Compare(double v1, double v2)
        {
            if (v1 < (v2 - 1E-10))
            {
                return -1;
            }
            if (v1 > (v2 + 1E-10))
            {
                return 1;
            }
            return 0;
        }

        public static decimal Floor(decimal d)
        {
            return decimal.Floor(d);
        }

        public static double Floor(double d)
        {
            return Math.Floor(d);
        }

        public static decimal Floor(decimal d, int digits)
        {
            if (digits < 0)
            {
                return (decimal.Floor(d / tens(-digits)) * tens(-digits));
            }
            if (digits == 0)
            {
                return decimal.Floor(d);
            }
            return (decimal.Floor(d * tens(digits)) / tens(digits));
        }

        public static double Floor(double d, int digits)
        {
            if (digits < 0)
            {
                return (Math.Floor((double)(d / ((double)tens(-digits)))) * tens(-digits));
            }
            if (digits == 0)
            {
                return Math.Floor(d);
            }
            return (Math.Floor((double)(d * tens(digits))) / ((double)tens(digits)));
        }

        public static decimal FloorCy(decimal d)
        {
            return Floor(d, 2);
        }

        public static decimal FloorCy(double d)
        {
            return (decimal)Floor(d, 2);
        }

        public static bool IsBlank(decimal d)
        {
            return ((d != 0M) && (System.Math.Abs(d) < 0.00000000000100000M));
        }

        public static bool IsNull(object v)
        {
            if (v == null)
            {
                return true;
            }
            if (v is decimal)
            {
                return IsBlank((decimal)v);
            }
            INullable nullable = v as INullable;
            return ((nullable != null) && nullable.IsNull);
        }

        public static bool IsValid(decimal value)
        {
            return ((-10000000000M < value) && (value < 10000000000M));
        }

        public static int NDW(int x, int y)
        {
            x = System.Math.Abs(x);
            y = System.Math.Abs(y);
            if (x == 0)
            {
                if (y == 0)
                {
                    return 1;
                }
                return y;
            }
            if (y != 0)
            {
                while (x != y)
                {
                    if (x > y)
                    {
                        x -= y;
                    }
                    else
                    {
                        y -= x;
                    }
                }
                return x;
            }
            return x;
        }

        public static long Round(decimal d)
        {
            return (long)Round(d, 0);
        }

        public static long Round(double d)
        {
            return (long)Round(d, 0);
        }

        public static decimal Round(decimal d, int digits)
        {
            if (digits < 0)
            {
                return (tens(-digits) * Round((decimal)(d / tens(-digits))));
            }
            return Math.Round((decimal)(d + 0.0000000001M), digits);
        }

        public static double Round(double d, int digits)
        {
            if (digits < 0)
            {
                return (double)(tens(-digits) * Round((double)(d / ((double)tens(-digits)))));
            }
            return Math.Round((double)(d + 1E-10), digits);
        }

        public static decimal RoundCy(decimal d)
        {
            return Round(d, 2);
        }

        public static decimal RoundCy(double d)
        {
            return (decimal)Round(d, 2);
        }

        public static decimal Sum(decimal d1, decimal d2)
        {
            if (IsBlank(d1))
            {
                return d2;
            }
            if (IsBlank(d2))
            {
                return d1;
            }
            return (d1 + d2);
        }

        private static int tens(int digits)
        {
            if ((0 <= digits) && (digits < tensT.Length))
            {
                return tensT[digits];
            }
            int num = tensT.Length - 1;
            throw new ArgumentException(string.Concat(new object[] { "Argument może przyjmować wartości od -", num, " do ", num }), "digits");
        }

        public static string ToString(double value)
        {
            string str = value.ToString(CultureInfo.InvariantCulture);
            int index = str.IndexOf('.');
            if (index != -1)
            {
                for (int i = str.Length - 1; i > index; i++)
                {
                    if (str[i] != ' ')
                    {
                        return str.Substring(0, i);
                    }
                }
            }
            return str;
        }
    }


}
