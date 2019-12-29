using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface FeatureDefs : GuidedTable<FeatureDefinition>
    {
        FeatureDefinition this[string tableName, string name] { get; }

        /*
        IEnumerable<IFeatureDefinition> this[string tableName] { get; }
         */

    }
}
