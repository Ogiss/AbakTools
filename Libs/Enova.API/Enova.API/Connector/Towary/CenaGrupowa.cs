using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Towary
{
    internal class CenaGrupowa : Business.GuidedRow, API.Towary.CenaGrupowa
    {
        public API.Business.DictionaryItem Grupa
        {
            get
            {
                var grupa = GetValue("Grupa");
                return grupa == null ? null : new Business.DictionaryItem() { EnovaObject = grupa };
            }
        }

        public API.Business.DictionaryItem GrupaTowarowa
        {
            get
            {
                var grupa = GetValue("GrupaTowarowa");
                return grupa == null ? null : new Business.DictionaryItem() { EnovaObject = grupa };
            }
        }

        public API.CRM.Kontrahent Kontrahent
        {
            get
            {
                return new CRM.Kontrahent() { EnovaObject = GetValue("Kontrahent") };
            }
        }

        public decimal Rabat
        {
            get
            {
                try
                {
                    decimal d = decimal.Parse(GetValue("Rabat").ToString().Replace("%", "")) / 100;
                    return d;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                var e = Type.GetType("Soneta.Types.Percent, Soneta.Types")
                    .GetConstructor(new Type[] { typeof(decimal) }).Invoke(new object[] { value });
                SetValue("Rabat", e);
            }
        }

        public bool RabatZdefiniowany
        {
            get
            {
                return (bool)GetValue("RabatZdefiniowany");
            }
            set
            {
                SetValue("RabatZdefiniowany", value);
            }
        }
    }
}
