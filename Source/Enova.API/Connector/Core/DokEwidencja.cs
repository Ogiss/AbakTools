using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Core
{
    internal class DokEwidencja : Business.GuidedTable<API.Core.DokEwidencji>, API.Core.DokEwidencja
    {
        public override string TableName
        {
            get
            {
                return "DokEwidencja";
            }
        }

        public API.Business.SubTable WgTyp(API.Core.TypDokumentu typ, Types.Date datadokumentu = null)
        {
            return GetSubTable("WgTyp", typ, datadokumentu);
        }


        public API.Business.SubTable WgPodmiot(API.Core.IPodmiot podmiot, Types.Date datadokumentu = null)
        {
            return GetSubTable("WgPodmiot", podmiot, datadokumentu);
        }
    }
}
