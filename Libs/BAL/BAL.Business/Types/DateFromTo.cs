using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public class DateFromTo :FromTo<DateTime>
    {
        public int DaysInterval
        {
            get
            {
                return (this.To - this.From).Days;
            }
        }

        #region Methods

        public DateFromTo(DateTime from, DateTime to)
            : base(from, to)
        {
        }

        public DateFromTo(DateTime date) : this(date, date) { }

        public DateFromTo AddDays(int days)
        {
            return new DateFromTo(this.From.AddDays(days), this.To.AddDays(days));
        }

        public DateFromTo AddWeeks(int value)
        {
            return new DateFromTo(this.From.AddDays(value * 7), this.To.AddDays(value * 7));
        }

        public DateFromTo AddMonths(int months)
        {
            return new DateFromTo(this.From.AddMonths(months), this.To.AddMonths(months));
        }

        public DateFromTo AddYears(int years)
        {
            return new DateFromTo(this.From.AddYears(years), this.To.AddYears(years));
        }

        public override string ToString()
        {
            if (this.From == DateTime.MinValue && this.To == DateTime.MaxValue)
                return "Wszystko";
            return  this.From.ToString("yyyy-MM-dd") + "..." + this.To.ToString("yyyy-MM-dd");
        }

        #endregion
    }
}
