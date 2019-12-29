using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Towary
{
    internal class Towary : Business.GuidedTable<API.Towary.Towar>, API.Towary.Towary
    {
        public override string TableName
        {
            get
            {
                return "Towary";
            }
        }

        public API.Towary.Towar this[string kod]
        {
            get
            {
                //return (API.Towary.Towar)GetObjValue(GetValue("WgKodu"), "Item", new Type[] { typeof(string) }, new object[] { kod });
                return EnovaHelper.FromEnova<API.Towary.Towar>(GetObjValue(GetValue("WgKodu"), "Item", new Type[] { typeof(string) }, new object[] { kod }));
            }
        }
    }
}
