using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Enova.Old.Types;
using Enova.Old.Towary;

namespace Enova.Old.Handel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IlośćWartość
    {
        public readonly Quantity Ilość;
        public readonly Currency Wartość;
        public static readonly IlośćWartość Zero;
        public static readonly IlośćWartość SystemZero;

        public IlośćWartość(Quantity ilość, Currency wartość)
        {
            this.Ilość = ilość;
            this.Wartość = wartość;
        }

        public override string ToString()
        {
            return (this.Wartość + " " + this.Ilość);
        }

        public static IlośćWartość operator +(IlośćWartość iw1, IlośćWartość iw2)
        {
            Currency currency;
            if (iw1.Wartość.IsNull)
            {
                currency = iw2.Wartość;
            }
            else if (iw2.Wartość.IsNull)
            {
                currency = iw1.Wartość;
            }
            else
            {
                currency = iw1.Wartość + iw2.Wartość;
            }
            return new IlośćWartość(iw1.Ilość + iw2.Ilość, currency);
        }

        public static IlośćWartość operator +(IlośćWartość iw1, Currency cy)
        {
            return new IlośćWartość(iw1.Ilość, iw1.Wartość + cy);
        }

        public static IlośćWartość operator -(IlośćWartość iw1, IlośćWartość iw2)
        {
            return new IlośćWartość(iw1.Ilość - iw2.Ilość, iw1.Wartość.Add(-iw2.Wartość));
        }

        public static IlośćWartość operator -(IlośćWartość iw1, Currency cy)
        {
            return new IlośćWartość(iw1.Ilość, iw1.Wartość - cy);
        }

        public static IlośćWartość operator -(IlośćWartość iw1)
        {
            return new IlośćWartość(-iw1.Ilość, -iw1.Wartość);
        }

        public bool IsZero
        {
            get
            {
                return this.Ilość.IsZero;
            }
        }

        public bool IsZeroWithValue
        {
            get
            {
                return (this.Ilość.IsZero && (this.Wartość == Currency.Zero));
            }
        }

        public static bool operator ==(IlośćWartość iw1, IlośćWartość iw2)
        {
            return ((iw1.Ilość == iw2.Ilość) && (iw1.Wartość == iw2.Wartość));
        }

        public static bool operator !=(IlośćWartość iw1, IlośćWartość iw2)
        {
            return !(iw1 == iw2);
        }

        public override bool Equals(object obj)
        {
            return ((obj is IlośćWartość) && (this == ((IlośćWartość)obj)));
        }

        public override int GetHashCode()
        {
            return (this.Ilość.GetHashCode() | (0x1d * this.Wartość.GetHashCode()));
        }

        public IlośćWartość Proporcja(Quantity v)
        {
            return new IlośćWartość(v, new Currency(this.ProporcjaV(v), this.Wartość.Symbol));
        }

        public decimal ProporcjaV(Quantity v)
        {
            if (this.Ilość.Symbol != v.Symbol)
            {
                throw new ArgumentException("Niezgodność jednostek ilości.", "v");
            }
            if (v.Value == this.Ilość.Value)
            {
                return this.Wartość.Value;
            }
            if (this.Wartość.Value == 0M)
            {
                return 0M;
            }
            return Tools.Math.RoundCy((double)((((double)this.Wartość.Value) * v.Value) / this.Ilość.Value));
        }

        public static bool NegativeEquals(object iw1, object iw2)
        {
            if ((iw1 == null) && (iw2 == null))
            {
                return true;
            }
            if (iw1 == null)
            {
                IlośćWartość wartość = (IlośćWartość)iw2;
                return wartość.IsZeroWithValue;
            }
            if (iw2 == null)
            {
                IlośćWartość wartość2 = (IlośćWartość)iw1;
                return wartość2.IsZeroWithValue;
            }
            return (-((IlośćWartość)iw1) == ((IlośćWartość)iw2));
        }

        public static bool Equals(object iw1, object iw2)
        {
            if ((iw1 == null) && (iw2 == null))
            {
                return true;
            }
            if (iw1 == null)
            {
                IlośćWartość wartość = (IlośćWartość)iw2;
                return wartość.IsZeroWithValue;
            }
            if (iw2 == null)
            {
                IlośćWartość wartość2 = (IlośćWartość)iw1;
                return wartość2.IsZeroWithValue;
            }
            return (((IlośćWartość)iw1) == ((IlośćWartość)iw2));
        }

        static IlośćWartość()
        {
            Zero = new IlośćWartość(Quantity.Zero, Currency.Empty);
            SystemZero = new IlośćWartość(Quantity.Zero, new Currency(0M, Currency.SystemSymbol));
        }
    }

}
