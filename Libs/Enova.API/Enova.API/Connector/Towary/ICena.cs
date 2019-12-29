using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Towary.ICena, Soneta.Handel", typeof(Enova.API.Towary.ICena), typeof(Enova.API.Connector.Towary.ICena))]


namespace Enova.API.Connector.Towary
{
    internal class ICena : API.Types.ObjectBase, API.Towary.ICena
    {
        public Types.DoubleCy Brutto
        {
            get
            {
                return FromEnova<Types.DoubleCy>("Brutto");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Types.DoubleCy Netto
        {
            get
            {
                return FromEnova<Types.DoubleCy>("Netto");
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public double StandardowaIlosc
        {
            get { return FromEnova<double>("StandardowaIlosc"); }
        }
    }
}
