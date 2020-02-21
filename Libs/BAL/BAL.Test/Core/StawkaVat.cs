using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Test.Core
{
    public class StawkaVat : IDataType, IComparable
    {
        public string Kod { get; set; }
        public decimal Stawka { get; set; }

        public override string ToString()
        {
            return (this.Stawka * 100M).ToString()+"%";
        }

        public int CompareTo(object obj)
        {
            return this.Kod.CompareTo(((StawkaVat)obj).Kod);
        }
    }
}
