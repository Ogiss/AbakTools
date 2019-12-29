using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Enova.API.Connector.Module("Business", typeof(Enova.API.Connector.Business.BusinessModule), typeof(Enova.API.Business.BusinessModule))]

namespace Enova.API.Connector.Business
{
    internal class BusinessModule: Module, API.Business.BusinessModule
    {
        #region Fields

        private Business.FeatureDefs tableFeatureDefs;
        private Business.Dictionary tableDictionary;

        #endregion

        public API.Business.FeatureDefs FeatureDefs
        {
            get
            {
                if (tableFeatureDefs == null)
                    tableFeatureDefs = new FeatureDefs() { EnovaObject = GetEnovaTable("FeatureDefs"), module = this };
                return tableFeatureDefs;
            }
        }

        public API.Business.Dictionary Dictionary
        {
            get
            {
                if (tableDictionary == null)
                    tableDictionary = new Dictionary() { EnovaObject = GetEnovaTable("Dictionary"), module = this };
                return tableDictionary;
            }
        }

        #region Methods

        public BusinessModule(Session session) : base(session, "Business") { }


        #endregion
    }
}
