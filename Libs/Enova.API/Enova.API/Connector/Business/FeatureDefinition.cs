using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class FeatureDefinition : GuidedRow, API.Business.FeatureDefinition
    {
        #region Properties

        public string Name
        {
            get { return GetValue<string>("Name"); }
        }

        public string TableName
        {
            get { return GetValue<string>("TableName"); }
        }

        public bool StrictDictionary
        {
            get { return GetValue<bool>("StrictDictionary"); }
        }

        public bool Group
        {
            get { return GetValue<bool>("Group"); }
        }

        public string Dictionary
        {
            get { return GetValue<string>("Dictionary"); }
        }

        public API.Business.FeatureTypeNumber TypeNumber
        {
            get { return (API.Business.FeatureTypeNumber)GetValue<int>("TypeNumber"); }
        }

        public IEnumerable<API.Business.DictionaryItem> DictionaryList
        {
            get { return new EnovaEnumerable<API.Business.DictionaryItem>(GetValue("DictionaryList")); }
        }

        #endregion

        #region Nested Types

        #endregion
    }
}
