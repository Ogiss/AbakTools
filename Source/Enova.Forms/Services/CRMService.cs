using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Services
{
    public static class CRMService
    {
        public static API.CRM.CRMModule GetModule(API.Business.Session session)
        {
            return session.GetModule<API.CRM.CRMModule>();
        }

        public static class Kontrahenci
        {
            public static IEnumerable<API.CRM.Kontrahent> Get(API.Business.Session session, string filter = null)
            {
                return GetModule(session).Kontrahenci.CreateView().SetFilter(filter).Cast<API.CRM.Kontrahent>();
            }

            public static IEnumerable<API.CRM.Kontrahent> ByPrzedstawiciel(API.Business.Session session, string przedstawiciel)
            {
                string filter = "";
                if (!string.IsNullOrEmpty(przedstawiciel))
                    filter = "Features.[przedstawiciel] = '" + przedstawiciel + "'";
                return Get(session, filter);
            }

            public static IEnumerable<int> IdsByPrzedstawiciel(System.Data.SqlClient.SqlConnection con, string przedstawiciel)
            {
                var sql = string.Format(
                    "SELECT k.ID FROM dbo.Kontrahenci k INNER JOIN dbo.Features f ON (f.Parent = k.ID AND f.ParentType='Kontrahenci' AND f.Name='przedstawiciel' AND f.Lp=0) "+
                    "WHERE f.Data='{0}'", przedstawiciel);
                var reader = new System.Data.SqlClient.SqlCommand(sql, con).ExecuteReader();
                try
                {
                    List<int> ids = new List<int>();
                    while (reader.Read())
                        ids.Add(reader.GetInt32(0));
                    return ids;
                }
                finally
                {
                    reader.Dispose();
                }
            }
        }
    }
}
