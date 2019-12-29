using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Towary.Quantity, Soneta.Handel", null, typeof(Enova.API.Towary.Quantity))]

namespace Enova.API.Towary
{
    public class Quantity : Types.ObjectBase
    {
        public double Value
        {
            get { return (double)GetValue("Value"); }
            set { SetValue("Value", value); }
        }

        public string Symbol
        {
            get { return (string)GetValue("Symbol"); }
            set { SetValue("Symbol", value); }
        }
    }
}
