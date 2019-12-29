using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Handel
{
    internal class PozycjeDokHan : Business.Table<API.Handel.PozycjaDokHandlowego>, API.Handel.PozycjeDokHan
    {
        public override string TableName
        {
            get
            {
                return "PozycjeDokHan";
            }
        }
    }
}
