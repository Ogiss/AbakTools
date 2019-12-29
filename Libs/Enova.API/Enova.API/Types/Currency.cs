using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Types.Currency, Soneta.Types", null, typeof(Enova.API.Types.Currency))]

namespace Enova.API.Types
{
    public class Currency : API.Types.ObjectBase
    {
        public decimal Value
        {
            get { return (decimal)GetValue("Value"); }
            set { SetValue("Value", value); }
        }

        public string Symbol
        {
            get { return (string)GetValue("Symbol"); }
            set { SetValue("Symbol", value); }
        }

        public static implicit operator decimal(Currency c)
        {
            return c.Value;
        }

        public static implicit operator Currency(decimal d)
        {
            return new Currency() { Value = d, Symbol = "PLN" };
        }

        public override string ToString()
        {
            return (string)CallMethod("ToString");
        }
    }
}
