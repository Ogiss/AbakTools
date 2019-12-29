using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Threading;
using Enova.Old.Types.Design;

namespace Enova.Old.Types
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(TimeConverter)), BinSerializable, DefaultWidth(10)]
    public struct Time : IComparable, ISumable, IFormattable, INullable
    {
        public static readonly int EmptyMinutes;
        public static readonly int MaxMinutes;
        public static readonly string enovaBaseCulture;
        public static readonly Time Empty;
        public static readonly Time Zero;
        public static readonly Time MinValue;
        public static readonly Time MaxValue;
        public static readonly Time Midnight;
        private readonly TimeSpan time;

        public Time(int hours, int minutes)
        {
            this.time = new TimeSpan(0, hours, minutes, 0);
            if ((this < MinValue) || (this > MaxValue))
            {
                throw new ArgumentException();
            }
        }

        private Time(DateTime dt)
            : this(dt.Hour, dt.Minute)
        {
        }

        public Time(int minutes)
        {
            this.time = new TimeSpan(0, minutes, 0);
        }

        internal Time(TimeSpan span)
        {
            if (span == TimeSpan.MinValue)
            {
                this.time = TimeSpan.MinValue;
            }
            else if (span == TimeSpan.MaxValue)
            {
                this.time = TimeSpan.MaxValue;
            }
            else
            {
                this.time = new TimeSpan(span.Days, span.Hours, span.Minutes, 0);
            }
        }

        public double TotalHours
        {
            get
            {
                return this.time.TotalHours;
            }
        }

        public int TotalMinutes
        {
            get
            {
                if (this == Empty)
                {
                    return EmptyMinutes;
                }
                if (this == MaxValue)
                {
                    return MaxMinutes;
                }
                return ((((this.time.Days * 0x18) + this.time.Hours) * 60) + this.time.Minutes);
            }
        }

        private static decimal Round(double d)
        {
            return Tools.Math.Round(d);
        }

        public static DoubleCy operator *(Time t, DoubleCy dcy)
        {
            if (t == Empty)
            {
                throw new ArgumentException();
            }
            return new DoubleCy(t.TotalHours * dcy.Value, dcy.Symbol);
        }

        public static DoubleCy operator *(DoubleCy dcy, Time t)
        {
            if (t == Empty)
            {
                throw new ArgumentException();
            }
            return new DoubleCy(t.TotalHours * dcy.Value, dcy.Symbol);
        }

        public double Div(decimal cy)
        {
            if ((this == Empty) || (this == Zero))
            {
                throw new ArgumentException();
            }
            return (((double)cy) / this.time.TotalHours);
        }

        public decimal Mul(double cyph)
        {
            if (this == Empty)
            {
                throw new ArgumentException();
            }
            return Tools.Math.RoundCy((double)(this.time.TotalHours * cyph));
        }

        public static Time operator *(Time time, double x)
        {
            if (time == Empty)
            {
                throw new ArgumentException();
            }
            double d = time.TotalMinutes * x;
            int minutes = (d > 0.0) ? ((int)Round(d)) : -((int)Round(-d));
            if (minutes != 0)
            {
                return new Time(minutes);
            }
            return Zero;
        }

        public static double operator /(Time time1, Time time2)
        {
            if ((time2 == Empty) || (time2 == Zero))
            {
                throw new ArgumentException();
            }
            return (time1.time.TotalMinutes / time2.time.TotalMinutes);
        }

        public static Time operator +(Time t1, Time t2)
        {
            if (t1 == Empty)
            {
                return t2;
            }
            if (t2 == Empty)
            {
                return t1;
            }
            return new Time(t1.time + t2.time);
        }

        public static Time operator -(Time t1, Time t2)
        {
            if (t1 == Empty)
            {
                throw new ArgumentException();
            }
            if (t2 == Empty)
            {
                return t1;
            }
            return new Time(t1.time - t2.time);
        }

        public static Time operator -(Time t)
        {
            if (t == Empty)
            {
                return t;
            }
            return new Time(-t.time);
        }

        public override bool Equals(object o)
        {
            return ((o is Time) && (this.time == ((Time)o).time));
        }

        public override int GetHashCode()
        {
            return this.time.GetHashCode();
        }

        public static bool operator ==(Time t1, Time t2)
        {
            return (t1.time == t2.time);
        }

        public static bool operator !=(Time t1, Time t2)
        {
            return (t1.time != t2.time);
        }

        public static explicit operator TimeSpan(Time t)
        {
            return t.time;
        }

        public static explicit operator Time(TimeSpan t)
        {
            return new Time(t);
        }

        public int CompareTo(object o)
        {
            if (o == null)
            {
                return 1;
            }
            if (!(o is Time))
            {
                throw new ArgumentException();
            }
            return this.time.CompareTo(((Time)o).time);
        }

        public static bool operator <(Time t1, Time t2)
        {
            return (t1.time < t2.time);
        }

        public static bool operator >(Time t1, Time t2)
        {
            return (t1.time > t2.time);
        }

        public static bool operator <=(Time t1, Time t2)
        {
            return (t1.time <= t2.time);
        }

        public static bool operator >=(Time t1, Time t2)
        {
            return (t1.time >= t2.time);
        }

        public static Time operator /(Time t, int divider)
        {
            return new Time((int)(t.time.TotalMinutes / ((double)divider)));
        }

        object ISumable.Sum(object sum)
        {
            if (sum is string)
            {
                return sum;
            }
            Time time = (Time)sum;
            if (time == Empty)
            {
                return this;
            }
            if (this == Empty)
            {
                return time;
            }
            if ((time == MaxValue) || (this == MaxValue))
            {
                return MaxValue;
            }
            if (!(time == MinValue) && !(this == MinValue))
            {
                return (time + this);
            }
            return MinValue;
        }

        object ISumable.Div(int divider)
        {
            if (this == MaxValue)
            {
                return MaxValue;
            }
            if (this == MinValue)
            {
                return MinValue;
            }
            return (this / divider);
        }

        public static Time Parse(string s)
        {
            return Parse(s, null);
        }

        public static Time Parse(string s, IFormatProvider formatProvider)
        {
            s = s.Trim();
            switch (s.ToLower())
            {
                case "":
                case "pusty":
                case "empty":
                case "(pusty)":
                case "(empty)":
                    return Empty;

                case "zero":
                case "(zero)":
                    return Zero;
            }
            if (s.IndexOf('.') >= 0)
            {
                return new Time(TimeSpan.Parse(s));
            }
            Time zero = Zero;
            bool flag = false;
            if (s[0] == '+')
            {
                zero = Midnight;
                s = s.Substring(1);
            }
            else if (s[0] == '-')
            {
                flag = true;
                s = s.Substring(1);
            }
            int hours = 0;
            int minutes = 0;
            bool flag2 = false;
            string[] strArray = null;
            if (Thread.CurrentThread.CurrentCulture.Name != enovaBaseCulture)
            {
                if (!string.IsNullOrEmpty(Thread.CurrentThread.CurrentCulture.DateTimeFormat.PMDesignator) && s.Contains(Thread.CurrentThread.CurrentCulture.DateTimeFormat.PMDesignator))
                {
                    s = s.Replace(Thread.CurrentThread.CurrentCulture.DateTimeFormat.PMDesignator, "").Trim();
                    flag2 = true;
                }
                if (!string.IsNullOrEmpty(Thread.CurrentThread.CurrentCulture.DateTimeFormat.AMDesignator) && s.Contains(Thread.CurrentThread.CurrentCulture.DateTimeFormat.AMDesignator))
                {
                    s = s.Replace(Thread.CurrentThread.CurrentCulture.DateTimeFormat.AMDesignator, "").Trim();
                }
            }
            strArray = s.Split(new char[] { ':' });
            hours = int.Parse(strArray[0]);
            hours += (flag2 && (hours < 12)) ? 12 : 0;
            minutes = (strArray.Length == 1) ? 0 : int.Parse(strArray[1]);
            if (flag)
            {
                return -new Time(hours, minutes);
            }
            return (zero + new Time(hours, minutes));
        }

        public override string ToString()
        {
            return this.ToString(null, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == Empty)
            {
                return "";
            }
            if (this < Zero)
            {
                Time time = -this;
                return ("-" + time.ToString());
            }
            if ((format == "") || (format == null))
            {
                return string.Format(formatProvider, "{0}:{1:D2}", new object[] { (int)Math.Round(this.time.TotalHours, 8), this.time.Minutes });
            }
            if (format == "f")
            {
                return string.Format(formatProvider, "{0:D7}:{1:D2}", new object[] { (int)Math.Round(this.time.TotalHours, 8), this.time.Minutes });
            }
            if (!(format == "+"))
            {
                throw new FormatException("Nierozpoznany format dla wartości typu czas '" + format + "'.");
            }
            if (this <= Midnight)
            {
                return this.ToString("", formatProvider);
            }
            int num = (int)Math.Round(this.time.TotalHours, 8);
            int num2 = num % 0x18;
            int num3 = num / 0x18;
            switch (num3)
            {
                case 0:
                    return this.ToString("", formatProvider);

                case 1:
                    return string.Format(formatProvider, "+{0}:{1:D2}", new object[] { num2, this.time.Minutes });
            }
            return string.Format(formatProvider, "{0}.{1}:{2:D2}", new object[] { num3, num2, this.time.Minutes });
        }

        public static Time Now
        {
            get
            {
                return new Time(DateTime.Now);
            }
        }

        public static Time Max(Time t1, Time t2)
        {
            if (t1 >= t2)
            {
                return t1;
            }
            return t2;
        }

        public static Time Min(Time t1, Time t2)
        {
            if (t1 <= t2)
            {
                return t1;
            }
            return t2;
        }

        [Browsable(false)]
        public bool IsNull
        {
            get
            {
                return (this == Empty);
            }
        }

        public static implicit operator TimeSec(Time t)
        {
            return new TimeSec(t.time.Hours, t.time.Minutes, 0);
        }

        static Time()
        {
            EmptyMinutes = -2000000000;
            MaxMinutes = 0x77359400;
            enovaBaseCulture = "pl-PL";
            Empty = new Time(TimeSpan.MinValue);
            Zero = new Time(TimeSpan.Zero);
            MinValue = new Time(TimeSpan.MinValue);
            MaxValue = new Time(TimeSpan.MaxValue);
            Midnight = new Time(0x18, 0);
        }
    }

}
