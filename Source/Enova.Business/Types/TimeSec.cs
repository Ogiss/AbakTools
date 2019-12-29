using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data.SqlTypes;
using Enova.Old.Types.Design;

namespace Enova.Old.Types
{
    [StructLayout(LayoutKind.Sequential), BinSerializable, DefaultWidth(15), TypeConverter(typeof(TimeSecConverter))]
    public struct TimeSec : IComparable, INullable, ISumable
    {
        public static readonly TimeSec Empty;
        public static readonly TimeSec Zero;
        public static readonly TimeSec Midnight;
        public static readonly TimeSec MinValue;
        public static readonly TimeSec MaxValue;
        private readonly TimeSpan timeSec;

        public TimeSec(int Hour, int Minute, int Second)
        {
            this.timeSec = new TimeSpan(Hour, Minute, Second);
        }

        public TimeSec(DateTime DateTime)
            : this(DateTime.Hour, DateTime.Minute, DateTime.Second)
        {
        }

        public TimeSec(TimeSpan TimeSpan)
        {
            if (TimeSpan == TimeSpan.MinValue)
            {
                this.timeSec = TimeSpan.MinValue;
            }
            else if (TimeSpan == TimeSpan.MaxValue)
            {
                this.timeSec = TimeSpan.MaxValue;
            }
            else
            {
                this.timeSec = new TimeSpan(TimeSpan.Days, TimeSpan.Hours, TimeSpan.Minutes, TimeSpan.Seconds);
            }
        }

        public TimeSec(double Time)
        {
            int minutes = (int)Math.Truncate(Time);
            int seconds = (int)Math.Round((double)((Time - minutes) * 60.0));
            this.timeSec = new TimeSpan(0, minutes, seconds);
        }

        public static TimeSec Parse(string s)
        {
            return Parse(s, null);
        }

        public static TimeSec Parse(string str, IFormatProvider formatProvider)
        {
            str = str.Trim();
            int hour = 0;
            int minute = 0;
            string[] strArray = str.Split(new char[] { ':' });
            hour = (strArray.Length > 0) ? int.Parse(strArray[0]) : 0;
            minute = (strArray.Length > 1) ? int.Parse(strArray[1]) : 0;
            return new TimeSec(hour, minute, (strArray.Length > 2) ? int.Parse(strArray[2]) : 0);
        }

        public double TotalMinutes
        {
            get
            {
                return this.timeSec.TotalMinutes;
            }
        }

        public double TotalSeconds
        {
            get
            {
                return this.timeSec.TotalSeconds;
            }
        }

        public double TotalHours
        {
            get
            {
                return this.timeSec.TotalHours;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (!(obj is TimeSec))
            {
                throw new ArgumentException();
            }
            return this.timeSec.CompareTo(((TimeSec)obj).timeSec);
        }

        public override string ToString()
        {
            return this.timeSec.ToString();
        }

        public override bool Equals(object obj)
        {
            return ((obj is TimeSec) && (this.timeSec == ((TimeSec)obj).timeSec));
        }

        public override int GetHashCode()
        {
            return this.timeSec.GetHashCode();
        }

        public bool IsNull
        {
            get
            {
                return (this == Empty);
            }
        }

        public static bool operator ==(TimeSec ts1, TimeSec ts2)
        {
            return (ts1.timeSec == ts2.timeSec);
        }

        public static bool operator !=(TimeSec ts1, TimeSec ts2)
        {
            return (ts1.timeSec != ts2.timeSec);
        }

        public static bool operator >(TimeSec ts1, TimeSec ts2)
        {
            return (ts1.timeSec > ts2.timeSec);
        }

        public static bool operator >=(TimeSec ts1, TimeSec ts2)
        {
            return (ts1.timeSec >= ts2.timeSec);
        }

        public static bool operator <(TimeSec ts1, TimeSec ts2)
        {
            return (ts1.timeSec < ts2.timeSec);
        }

        public static bool operator <=(TimeSec ts1, TimeSec ts2)
        {
            return (ts1.timeSec <= ts2.timeSec);
        }

        public static TimeSec operator +(TimeSec ts1, TimeSec ts2)
        {
            return new TimeSec(ts1.timeSec + ts2.timeSec);
        }

        public static TimeSec operator /(TimeSec t, int divider)
        {
            return new TimeSec((double)((int)(t.timeSec.TotalMinutes / ((double)divider))));
        }

        object ISumable.Sum(object sum)
        {
            if (sum is string)
            {
                return sum;
            }
            TimeSec sec = (TimeSec)sum;
            if (sec == Empty)
            {
                return this;
            }
            if (this == Empty)
            {
                return sec;
            }
            if ((sec == MaxValue) || (this == MaxValue))
            {
                return MaxValue;
            }
            if (!(sec == MinValue) && !(this == MinValue))
            {
                return (sec + this);
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

        public static implicit operator Time(TimeSec ts)
        {
            return new Time(ts.timeSec.Hours, ts.timeSec.Minutes);
        }

        public static explicit operator TimeSpan(TimeSec ts)
        {
            return ts.timeSec;
        }

        public TimeSec AddSeconds(int seconds)
        {
            return new TimeSec(this.timeSec.Add(new TimeSpan(0, 0, seconds)));
        }

        static TimeSec()
        {
            Empty = new TimeSec(TimeSpan.MinValue);
            Zero = new TimeSec(TimeSpan.Zero);
            Midnight = new TimeSec(0x18, 0, 0);
            MinValue = Empty;
            MaxValue = new TimeSec(TimeSpan.MaxValue);
        }
    }

}
