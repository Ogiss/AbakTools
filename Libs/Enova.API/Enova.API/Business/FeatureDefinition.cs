using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Business.FeatureDefinition, Soneta.Business", typeof(Enova.API.Business.FeatureDefinition), typeof(Enova.API.Connector.Business.FeatureDefinition)),
           RowMap("FeatureDefs", typeof(Enova.API.Business.FeatureDefinition), typeof(Enova.API.Business.BusinessModule))]

namespace Enova.API.Business
{
    public interface FeatureDefinition : GuidedRow
    {
        int ID { get; }
        string Name { get; }
        string TableName { get; }
        bool StrictDictionary { get; }
        bool Group { get; }
        string Dictionary { get; }
        FeatureTypeNumber TypeNumber { get; }
        IEnumerable<DictionaryItem> DictionaryList { get; }
    }
}
