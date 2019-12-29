using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;


namespace Enova.Business.Old.Web
{
    public class StanyMagazynoweView : Enova.Business.Old.Core.TableBase<StanMagazynowyView>
    {
        public StanyMagazynoweView(ObjectQuery<StanMagazynowyView> query) : base(query) { }


    }
}
