using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class Module : API.Types.ObjectBase, API.Business.Module
    {

        #region Fields

        private Session session;
        private string name;
        private Dictionary<string, object> enovaTables;

        #endregion

        #region Properties

        public API.Business.Session Session
        {
            get { return session; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion

        #region Methods

        public Module(Session session, string name)
        {
            enovaTables = new Dictionary<string, object>();
            this.session = session;
            this.name = name;
            this.EnovaObject = session.GetEnovaModule(name);
        }

        public object GetEnovaTable(string name)
        {
            if (enovaTables.ContainsKey(name))
                return enovaTables[name];
            var table = GetObjValue(EnovaObject, name);
            if (table != null)
                enovaTables.Add(name, table);
            return table;
            
        }

        #endregion
    }
}
