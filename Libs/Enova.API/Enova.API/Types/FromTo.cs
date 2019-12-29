using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Types.FromTo, Soneta.Types", null, typeof(Enova.API.Types.FromTo))]

namespace Enova.API.Types
{
    public class FromTo : ObjectBase /*, IComparable, IEnumerable, ICompoundType, IFormattable, INullable*/
    {
        #region Properties

        public static FromTo All
        {
            get
            {
                return (FromTo)EnovaService.Instance.GetStaticValue("Soneta.Types.FromTo, Soneta.Types", "All");
            }
        }

        //[NumeratorRef]
        public Date From
        {
            get { return new Date() { EnovaObject = GetValue("From") }; }
        }

        //[NumeratorRef]
        public Date To
        {
            get { return new Date() { EnovaObject = GetValue("To") }; }
        }

        [Browsable(false)]
        public int Days
        {
            get { return (int)GetValue("Days"); }
        }

        [Browsable(false)]
        public int Age
        {
            get { return (int)GetValue("Age"); }
        }

        [Browsable(false)]
        public bool IsFullMonth
        {
            get { return (bool)GetValue("IsFullMonth"); }
        }

        [Browsable(false)]
        public bool IsOneMonth
        {
            get { return (bool)GetValue("IsOneMonth"); }
        }

        [Browsable(false)]
        public FromTo FullMonth
        {
            get { return new FromTo() { EnovaObject = GetValue("FullMonth") }; }
        }

        [Browsable(false)]
        public FromTo ValidDates
        {
            get { return new FromTo() { EnovaObject = GetValue("ValidDates") }; }
        }

        [Browsable(false)]
        public int Months
        {
            get { return (int)GetValue("Months"); }
        }

        [Browsable(false)]
        public int EntireMonths
        {
            get { return (int)GetValue("EntireMonth"); }
        }

        [Browsable(false)]
        public FromTo FullEntireMonth
        {
            get { return new FromTo() { EnovaObject = GetValue("FullEntireMonth") }; }
        }

        [Browsable(false)]
        public bool IsNull
        {
            get { return (bool)GetValue("IsNull"); }
        }

        #endregion

        #region Methods

        public static FromTo Create(Date from, Date to)
        {
            return EnovaService.Instance.CreateObject<FromTo>(null, new object[] { from, to });
        }

        #endregion

        /*
        public static readonly FromTo Empty;
        public static readonly FromTo All;
        private static readonly FromTo AllValid;
        public IEnumerator GetEnumerator();
        public static FromTo Day(Date date);
        public static FromTo Week(Date date);
        public static FromTo Month(int year, int month);
        public static FromTo Month(Date date);
        public static FromTo Quarter(int year, int quarter);
        public static FromTo Quarter(Date date);
        public static FromTo HalfYear(Date date);
        public static FromTo Year(int year);
        public static FromTo Year(Date data);
        public static FromTo FromEnum(DefaultListPeriod @enum);
        public FromTo(Date from, Date to);
        internal FromTo(BinReader bin);
        internal void Write(BinWriter bin);
        public static FromTo CreateSafely(Date from, Date to);
        public bool IsValid();
        public bool Contains(Date date);
        public bool Contains(FromTo ft);
        public bool IsIntersected(FromTo ft);
        public bool IsContinuation(FromTo ft);
        public FromTo Intersection(FromTo ft);
        public static FromTo operator *(FromTo ft1, FromTo ft2);
        public FromTo OuterAdd(FromTo ft);
        public Periods ToPeriods();
        public YearMonth ToYearMonth();
        public Interval ToInterval();
        object[] ICompoundType.Values { get; }
        public FromTo Increment(int multiplier);
        public FromTo Increment();
        public FromTo Decrement(int multiplier);
        private FromTo Decrement();
        public override string ToString();
        public string ToString(string format);
        public string ToString(string format, IFormatProvider formatProvider);
        public static FromTo Parse(string value);
        private static bool IsOnlyDigit(string s);
        public static bool operator ==(FromTo ft1, FromTo ft2);
        public static bool operator !=(FromTo ft1, FromTo ft2);
        public override bool Equals(object o);
        public override int GetHashCode();
        public int CompareTo(object obj);
        public void checkClosePeriod(string message);
        */
    }
}
