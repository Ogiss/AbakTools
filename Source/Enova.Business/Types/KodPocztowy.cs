using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class KodPocztowy
    {
        private int kodPoczt = 0;

        public KodPocztowy(int kod)
        {
            kodPoczt = kod;
        }
        public KodPocztowy() : this(0) { }

        public KodPocztowy(string kod)
        {
            string str = kod.Replace("-", "");
            kodPoczt = int.Parse(str);
        }

        public static implicit operator KodPocztowy(int kod)
        {
            return new KodPocztowy(kod);
        }

        public static implicit operator KodPocztowy(string kod)
        {
            return new KodPocztowy(kod);
        }

        public static implicit operator KodPocztowy(System.DBNull kod)
        {
            return new KodPocztowy(0);
        }

        public static explicit operator int(KodPocztowy kod)
        {
            return kod.kodPoczt;
        }

        public static explicit operator string(KodPocztowy kod)
        {
            return kod.ToString();
        }

        public override string ToString()
        {
            if (kodPoczt == 0)
                return null;
            var str = kodPoczt.ToString().PadLeft(5, '0');
            return str.Substring(0, 2) + '-' + str.Substring(2);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(System.DBNull) || obj == null)
            {
                return kodPoczt == 0;
            }
            return ((KodPocztowy)obj).kodPoczt == kodPoczt;
        }

        public override int GetHashCode()
        {
            return kodPoczt.GetHashCode();
        }
    }
}
