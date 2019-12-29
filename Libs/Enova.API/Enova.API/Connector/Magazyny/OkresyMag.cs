using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Magazyny
{
    internal class OkresyMag : Business.GuidedTable<API.Magazyny.OkresMagazynowy>, API.Magazyny.OkresyMag
    {
        #region Properties

        public override string TableName
        {
            get
            {
                return "OkresyMag";
            }
        }

        #endregion

        public API.Magazyny.OkresMagazynowy Aktualny
        {
            get { return FromEnova<API.Magazyny.OkresMagazynowy>("Aktualny"); }
        }

        public API.Magazyny.OkresMagazynowy this[Types.Date data]
        {
            get { return (API.Magazyny.OkresMagazynowy)EnovaHelper.FromEnova(GetValue("Item", new object[] { data })); }
        }

    }
}
