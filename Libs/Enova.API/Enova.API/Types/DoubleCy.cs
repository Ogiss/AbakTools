using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Types.DoubleCy, Soneta.Types", null, typeof(Enova.API.Types.DoubleCy))]

namespace Enova.API.Types
{
    public class DoubleCy : ObjectBase
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
