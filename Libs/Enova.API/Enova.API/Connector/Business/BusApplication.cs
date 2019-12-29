using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class BusApplication : API.Types.ObjectBase
    {
        #region Properties

        public Database this[string name]
        {
            get
            {
                foreach (var edb in ((IEnumerable)EnovaObject))
                {
                    var db = new Database() { EnovaObject = edb };
                    if (db.Name == name)
                        return db;
                }
                return null;
            }
        }

        public string ApplicationName
        {
            get { return GetValue<string>("ApplicationName"); }
            set { SetValue("ApplicationName", value); }
        }

        public void InitDatabases()
        {
            foreach (var db in ((IEnumerable)EnovaObject))
                break;
        }


        #endregion
    }
}
