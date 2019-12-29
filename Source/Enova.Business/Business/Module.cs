using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Enova.Business.Old
{
    public class Module: ISessionable
    {
        #region Fields

        private ObjectContext dataContext;
        private string name;
        private static Dictionary<string, Module> modules = new Dictionary<string, Module>();
        private Session session;

        #endregion

        #region Properties

        public ObjectContext DataContext
        {
            get
            {
                if (this.dataContext != null)
                    return this.dataContext;
                if (this.session != null)
                    return this.session.DataContext;
                return null;
            }
        }

        public string Name
        {
            get { return this.name; }
        }

        public Session Session
        {
            get { return this.session; }
        }

        #endregion

        #region Methods

        public Module(ObjectContext dc, string name)
        {
            this.dataContext = dc;
            this.name = name;
        }

        public Module(Session session, string name)
        {
            this.session = session;
            this.name = name;
        }

        internal static Module GetModule(string name)
        {
            if (modules.ContainsKey(name))
                return modules[name];

            return null;
        }

        internal static void AddModule(Module module)
        {
            modules[module.Name] = module;
        }

        protected void AddTable(ITable table)
        {
            this.Session.Tables.Add(table);
            table.Adding(this);
        }

        #endregion

    }
}
