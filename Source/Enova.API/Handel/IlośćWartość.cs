using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Towary;

namespace Enova.API.Handel
{
    public class IlośćWartość : ObjectBase
    {
        public Quantity Ilość
        {
            get { return new Quantity() { EnovaObject = GetValue("Ilość") }; }
        }

        public Currency Wartość
        {
            get { return new Currency() { EnovaObject = GetValue("Wartość") }; }
        }
    }
}
