using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Handel
{
    internal class DefRelHandlowych : Business.GuidedTable<API.Handel.DefRelacjiHandlowej>, API.Handel.DefRelHandlowych
    {
        public override string TableName
        {
            get
            {
                return "DefRelHandlowych";
            }
        }

        public API.Handel.DefRelacjiHandlowej Korekta
        {
            get { return (API.Handel.DefRelacjiHandlowej)EnovaHelper.FromEnova(GetValue("Korekta")); }
        }
    }
}
