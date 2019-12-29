using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Magazyny
{
    internal class Magazyny :Business.GuidedTable<API.Magazyny.Magazyn>, API.Magazyny.Magazyny
    {
        #region Properties

        public override string TableName
        {
            get
            {
                return "Magazyny";
            }
        }

        public API.Magazyny.Magazyn Firma
        {
            get
            {
                /*
                var firma = GetValue("Firma");
                if (firma != null)
                    return new Magazyn() { EnovaObject = firma };
                return null;
                 */
                return FromEnova<API.Magazyny.Magazyn>("Firma");
            }
        }

        #endregion
    }
}
