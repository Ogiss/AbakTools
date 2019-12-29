using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Towary
{
    internal class DefinicjeCen : Business.GuidedTable<API.Towary.DefinicjaCeny>, API.Towary.DefinicjeCen
    {
        public override string TableName
        {
            get
            {
                return "DefinicjeCen";
            }
        }

        public API.Towary.DefinicjaCeny this[string nazwa]
        {
            get
            {
                var obj = GetValue("Item", new object[] { nazwa });
                return obj == null ? null : new DefinicjaCeny() { EnovaObject = obj };
            }
        }

        public IEnumerable<API.Business.FeatureDefinition> GrupyTowarowe
        {
            get
            {
                //return new Business.EnovaEnumerable<API.Business.IFeatureDefinition>(GetValue("GrupyTowarowe"));
                var grupy = (IEnumerable)GetValue("GrupyTowarowe");
                var list = new List<API.Business.FeatureDefinition>();
                if (grupy != null)
                {
                    foreach (var gr in grupy)
                        list.Add(new Business.FeatureDefinition() { EnovaObject = gr });
                }
                return list;
            }
        }
    }
}
