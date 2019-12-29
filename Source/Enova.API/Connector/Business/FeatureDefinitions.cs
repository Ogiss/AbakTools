using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class FeatureDefinitions : API.Types.ObjectBase, API.Business.IFeatureDefinitions
    {
        #region Fields

        #endregion

        #region Methods

        public FeatureDefinitions(object enovaObject)
        {
            this.EnovaObject = enovaObject;
        }

        public IEnumerator<API.Business.FeatureDefinition> GetEnumerator()
        {
            return new EnovaEnumerator<API.Business.FeatureDefinition>(CallMethod("GetEnumerator"));
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
