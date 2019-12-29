using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class FormyPlatnosci : Business.GuidedTable<API.Kasa.FormaPlatnosci>, API.Kasa.FormyPlatnosci
    {
        public override string TableName
        {
            get
            {
                return "FormyPlatnosci";
            }
        }
    }
}
