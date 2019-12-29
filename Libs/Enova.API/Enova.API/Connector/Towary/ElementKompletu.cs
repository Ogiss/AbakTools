using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Towary
{
    internal class ElementKompletu : Business.Row, API.Towary.ElementKompletu
    {
        public API.Towary.TypElementuKompletu Typ
        {
            get { return (API.Towary.TypElementuKompletu)(int)GetValue("Typ"); }
        }

        public API.Magazyny.Magazyn Magazyn
        {
            get { return EnovaHelper.FromEnova<API.Magazyny.Magazyn>(GetValue("Magazyn")); }
        }

        public API.Towary.Towar Towar
        {
            get { return EnovaHelper.FromEnova<API.Towary.Towar>(GetValue("Towar")); }
        }

        public double Ilosc
        {
            get { return (double)GetObjValue(GetValue("Ilosc"), "Value"); }
        }
    }
}
