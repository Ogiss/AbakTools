using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class Zaplaty : Business.GuidedTable<API.Kasa.Zaplata>, API.Kasa.Zaplaty
    {
        public override string TableName
        {
            get
            {
                return "Zaplaty";
            }
        }

        public API.Business.SubTable WgEwidencjaData(API.Kasa.EwidencjaSP ewidencjasp, Types.Date datadokumentu = null)
        {
            return GetSubTable("WgEwidencjaData", ewidencjasp, datadokumentu);
        }
    }
}
