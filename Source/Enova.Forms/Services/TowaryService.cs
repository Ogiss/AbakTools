using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;
using Enova.API.Towary;

namespace Enova.Forms.Services
{
    public static class TowaryService
    {
        public static TowaryModule GetModule(Session session)
        {
            return session.GetModule<TowaryModule>();
        }

        public static Towar GetTowarObliczObrotyZ(Towar towar)
        {
            throw new NotImplementedException();
        }

        public static class Towary
        {
            public static IEnumerable<Towar> Get(Session session, string filter)
            {
                return GetModule(session).Towary.CreateView().SetFilter(filter).Cast<Towar>();
            }

            public static IEnumerable<Towar> GetByFeature(Session session, string name, string value)
            {
                string filter = "";
                if (!string.IsNullOrEmpty(name))
                {
                    filter = "Features.[" + name + "]";
                    if (!string.IsNullOrEmpty(value))
                        filter += " = '" + value + "'";
                    else
                        filter += " <> '' AND " + filter + " IS NOT NULL";
                }
                return Get(session, filter);
            }
        }
    }
}
