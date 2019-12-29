using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;

namespace Enova.Forms.Services
{
    public static class BusinessService
    {
        public static BusinessModule GetModule(Session session)
        {
            return session.GetModule<BusinessModule>();
        }

        public static IEnumerable<FeatureDefinition> GetGrupyTowarowe(Session session)
        {
            return GetModule(session).FeatureDefs.CreateView().SetFilter("TableName = 'Towary' AND Group = true AND StrictDictionary = true").Cast<FeatureDefinition>();
        }

        public static class Dictionary
        {
            public static IEnumerable<DictionaryItem> Get(Session session, string filter = null)
            {
                return GetModule(session).Dictionary.CreateView().SetFilter(filter).Cast<DictionaryItem>();
            }

            public static IEnumerable<DictionaryItem> GetByFeatureName(Session session, string tableName, string featureName)
            {
                var fdef = GetModule(session).FeatureDefs[tableName, featureName];
                if (fdef != null)
                    return fdef.DictionaryList.Cast<DictionaryItem>();
                return new DictionaryItem[0];
            }

            public static IEnumerable<string> GetListaPrzedstawicieli(Session session)
            {
                return GetByFeatureName(session, "Kontrahenci", "przedstawiciel").OrderBy(r => r.Value).Select(r => r.Value);
            }

        }
    }
}
