using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class FeatureDefs : GuidedTable<API.Business.FeatureDefinition>, API.Business.FeatureDefs
    {
        #region Properties

        public API.Business.FeatureDefinition this[string tableName, string name]
        {
            get
            {
                return EnovaHelper.FromEnova<API.Business.FeatureDefinition>(GetObjValue(GetValue("ByName"), "Item", new Type[] { typeof(string), typeof(string) }, new object[] { tableName, name }));
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}
