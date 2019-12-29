using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;
using Enova.API.Towary;

[assembly: TypeMap("Soneta.Handel.BruttoNetto, Soneta.Handel",null, typeof(Enova.API.Handel.BruttoNetto))]

namespace Enova.API.Handel
{
    public class BruttoNetto : ObjectBase
    {
        public decimal Brutto
        {
            get { return FromEnova<decimal>("Brutto"); }
            set { ToEnova("Brutto", value); }
        }

        [Description("Wartość brutto wyrażona w walucie. Zawsze PLN poza dokumentami 0%.")]
        public Currency BruttoCy
        {
            get { return FromEnova<Currency>("BruttoCy"); }
        }

        public bool JestZero
        {
            get { return FromEnova<bool>("JestZero"); }
        }

        //[AttributeInheritance]
        public decimal Netto
        {
            get { return FromEnova<decimal>("Netto"); }
            set { ToEnova("Netto"); }
        }

        [Description("Wartość netto wyrażona w walucie. Zawsze PLN poza dokumentami 0%.")]
        public Currency NettoCy
        {
            get { return FromEnova<Currency>("NettoCy"); }
        }

        //private IBruttoNettoProvider Provider { get; }

        //[AttributeInheritance]
        public decimal VAT
        {
            get { return FromEnova<decimal>("VAT"); }
            set { ToEnova("VAT", value); }
        }

        [Description("Wartość podatku VAT wyrażona w walucie. Zawsze PLN poza dokumentami 0%.")]
        public Currency VATCy
        {
            get { return FromEnova<Currency>("VARCy"); }
        }

    }
}
