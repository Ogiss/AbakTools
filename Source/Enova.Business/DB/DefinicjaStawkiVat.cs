using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

namespace Enova.Business.Old.DB
{
    public partial class DefinicjaStawkiVat
    {
        public static explicit operator Web.StawkaVat(DefinicjaStawkiVat stawkaVat)
        {
            return Enova.Business.Old.Core.ContextManager.WebContext.StawkiVat
                .Where(s => s.GUID == stawkaVat.Guid).FirstOrDefault();
        }
    }
}
