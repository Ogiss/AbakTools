using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kadry
{
    internal class Pracownicy : Business.GuidedTable<API.Kadry.IPracownik>, API.Kadry.Pracownicy
    {
        #region Properties

        public override string TableName
        {
            get
            {
                return "Pracownicy";
            }
        }

        #endregion
    }
}
