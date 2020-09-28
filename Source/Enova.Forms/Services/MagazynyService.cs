using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.CRM;
using Enova.API.Magazyny;
using Enova.API.Towary;
using Enova.API.Handel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;

namespace Enova.Forms.Services
{
    public static class MagazynyService
    {
        public static class Obroty
        {
            private static class Filter
            {
                private static string getFilter(string filter)
                {
                    return string.IsNullOrEmpty(filter) ? "" : filter + " AND ";
                }

                public static string ByRozchodKontrahent(string filter, Kontrahent kontrahent, string przedstawiciel)
                {
                    if (kontrahent != null || !string.IsNullOrEmpty(przedstawiciel))
                    {
                        filter = getFilter(filter); ;
                        if (kontrahent != null)
                            filter += "RozchodKontrahent = " + kontrahent.ID;
                        else
                            filter += "RozchodKontrahent.Features.[przedstawiciel = '" + przedstawiciel + "'";
                        return filter;
                    }
                    return filter;
                }

                public static string ByTowar(string filter, Towar towar, FeatureDefinition featureDef, DictionaryItem value)
                {
                    if (towar != null || featureDef != null)
                    {
                        filter = getFilter(filter);
                        if (towar != null)
                            filter += "Towar = " + towar.ID;
                        else
                            filter += "Towar.Features.[" + featureDef.Name + "] = '" + value.Value + "'";
                    }
                    return filter;
                }

                public static string ByFromToRozchodData(string filter, FromTo fromTo)
                {
                    if (fromTo != null && (fromTo.From != null || fromTo.To != null))
                    {
                        if (fromTo.From != null)
                            filter = getFilter(filter) + "RozchodData >= '" + fromTo.From + "'";
                        if (fromTo.To != null)
                            filter = getFilter(filter) + "RozchodData <= '" + fromTo.To + "'";
                    }
                    return filter;
                }

            }

            public static T Sumuj<T>(SqlConnection con, string fieldName, int? okresID, int? magazynID, int? kontrahentID, int? definicjaDokID, DateTime? from, DateTime? to,
                int? towarID, string featureName = null, string featureValue = null)
            {
                var sb = new StringBuilder("SELECT SUM(t0.[");
                sb.Append(fieldName).Append("]) field0 FROM ");
                sb.Append("(SELECT dh.Definicja, (pdh.IloscMagazynuValue - ISNULL(");
                sb.Append("(SELECT TOP 1 kpdh.IloscMagazynuValue FROM dbo.PozRelHandlowej prh ");
                sb.Append("INNER JOIN dbo.RelacjeHandlowe rh ON rh.ID=prh.Relacja ");
                sb.Append("INNER JOIN dbo.PozycjeDokHan kpdh ON (kpdh.Ident = prh.NadrzednaIdent AND kpdh.Dokument = prh.NadrzednyDok) ");
                sb.Append("WHERE prh.PodrzednyDok=dh.ID AND prh.PodrzednaIdent=pdh.Ident and prh.Dodatkowa=0 AND rh.Typ=1),0)) IloscValue ");
                sb.Append("FROM dbo.PozycjeDokHan pdh ");
                sb.Append("INNER JOIN dbo.DokHandlowe dh ON dh.ID=pdh.Dokument ");
                sb.Append("INNER JOIN dbo.DefDokHandlowych ddh ON ddh.ID=dh.Definicja ");
                sb.Append("WHERE dh.TypPartii = 1 AND ddh.DuplikatWartosci = 0 AND pdh.KierunekMagazynu = -1 AND dh.Stan IN (1,2) ");
                if (featureName != null && featureValue != null)
                    sb.Append(" AND pdh.Towar IN (SELECT Parent FROM Features WHERE ParentType='Towary' AND Lp=0 AND Name='" + featureName + "' AND Data='" + featureValue + "')");
                if (magazynID != null)
                    sb.Append(" AND dh.Magazyn=" + magazynID.Value);
                if (kontrahentID != null)
                    sb.Append(" AND dh.Kontrahent=" + kontrahentID.Value);
                if (definicjaDokID != null)
                    sb.Append(" AND dh.Definicja=" + definicjaDokID.Value);
                if (from != null)
                    sb.Append(" AND pdh.Data >= '" + from.Value.Date.ToString("yyyy-MM-dd") + "'");
                if (to != null)
                    sb.Append(" AND pdh.Data < '" + to.Value.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");
                sb.Append(")t0 GROUP BY t0.Definicja");

                var result = new SqlCommand(sb.ToString(), con).ExecuteScalar();
                return DBNull.Value.Equals(result) ? default(T) : (T)result;
            }

            public static T Sumuj<T>(SqlConnection con, string fieldName, int? okresID, int? magazynID, int? kontrahentID, int? definicjaDokID, DateTime? from, DateTime? to,
                Guid towarGuid)
            {
                var sb = new StringBuilder("SELECT SUM(t0.[");
                sb.Append(fieldName).Append("]) field0 FROM ");
                sb.Append("(SELECT dh.Definicja, (pdh.IloscMagazynuValue - ISNULL(");
                sb.Append("(SELECT TOP 1 kpdh.IloscMagazynuValue FROM dbo.PozRelHandlowej prh ");
                sb.Append("INNER JOIN dbo.RelacjeHandlowe rh ON rh.ID=prh.Relacja ");
                sb.Append("INNER JOIN dbo.PozycjeDokHan kpdh ON (kpdh.Ident = prh.NadrzednaIdent AND kpdh.Dokument = prh.NadrzednyDok) ");
                sb.Append("WHERE prh.PodrzednyDok=dh.ID AND prh.PodrzednaIdent=pdh.Ident and prh.Dodatkowa=0 AND rh.Typ=1),0)) IloscValue ");
                sb.Append("FROM dbo.PozycjeDokHan pdh ");
                sb.Append("INNER JOIN dbo.DokHandlowe dh ON dh.ID=pdh.Dokument ");
                sb.Append("INNER JOIN dbo.DefDokHandlowych ddh ON ddh.ID=dh.Definicja ");
                sb.Append("INNER JOIN dbo.Towary t ON t.ID=pdh.Towar ");
                sb.Append("WHERE dh.TypPartii = 1 AND ddh.DuplikatWartosci = 0 AND pdh.KierunekMagazynu = -1 AND dh.Stan IN (1,2) AND t.Guid = @towarGuid");

                if (magazynID.HasValue)
                {
                    sb.Append(" AND dh.Magazyn= @magazynId");
                }

                if (kontrahentID.HasValue)
                {
                    sb.Append(" AND dh.Kontrahent = @kontrahentId");
                }

                if (definicjaDokID.HasValue)
                {
                    sb.Append(" AND dh.Definicja = @definicjaDokID");
                }

                if (from.HasValue)
                {
                    sb.Append(" AND pdh.Data >= @from");
                }

                if (to.HasValue)
                {
                    sb.Append(" AND pdh.Data < @to");
                }

                sb.Append(")t0 GROUP BY t0.Definicja");

                var cmd = new SqlCommand(sb.ToString(), con);
                cmd.Parameters.Add("@towarGuid", towarGuid);

                if (magazynID.HasValue)
                {
                    cmd.Parameters.Add("@magazynId", magazynID.Value);
                }

                if (kontrahentID.HasValue)
                {
                    cmd.Parameters.Add("@kontrahentId", kontrahentID.Value);
                }

                if (definicjaDokID.HasValue)
                {
                    cmd.Parameters.Add("@definicjaDokID", definicjaDokID.Value);
                }

                if (from.HasValue)
                {
                    cmd.Parameters.Add("@from", from.Value.Date);
                }

                if (to.HasValue)
                {
                    cmd.Parameters.Add("@to", to.Value.AddDays(1).Date);
                }

                var result = cmd.ExecuteScalar();

                return DBNull.Value.Equals(result) ? default(T) : (T)result;
            }
        }
    }
}
