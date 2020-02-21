using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business.App;

namespace BAL.Business
{
    public sealed class Session : ISessionable, IDisposable, Old.ISession
    {
        #region Fields

        private Guid guid;
        private bool is_disposed = false;
        private ModulesCollection modules;
        private TableCollection tables;
        private bool readOnly;
        private bool isInternal;
        private App.ILogin login;
        private App.IDatabase database;
        private string name;
        private Session parentSession;
        internal static Dictionary<Guid, Session> sessionsByGuid;
        internal List<IRow> changedRows;

        #endregion

        #region Properties


        public Guid Guid
        {
            get { return this.guid; }
        }


        Session ISessionable.Session
        {
            get { return this; }
        }

        public ModulesCollection Modules
        {
            get { return this.modules; }
        }

        public TableCollection Tables
        {
            get { return this.tables; }
        }

        public ILogin Login
        {
            get
            {
                if (this.parentSession != null)
                    return this.parentSession.Login;
                return this.login;
            }
        }


        public bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }
        }


        #endregion

        #region Methods

        static Session()
        {
            Session.sessionsByGuid = new Dictionary<Guid, Session>();
        }

        internal Session(ILogin login, bool readOnly, bool isInternal, string name)
        {
            this.guid = Guid.NewGuid();
            this.readOnly = readOnly;
            this.isInternal = isInternal;
            this.name = name;
            modules = new ModulesCollection(this);
            tables = new TableCollection(this);
            this.changedRows = new List<IRow>();
            this.toRefreshOld = new List<IRow>();
            if (login != null)
                this.assign(login);
            Session.sessionsByGuid.Add(this.guid, this);
        }

        internal Session(ILogin login, bool readOnly, string name) : this(login, readOnly, false, name) { }

        private void assign(ILogin login)
        {
            this.login = login;
            this.database = login.Database;
        }

        public void Save()
        {

            Saver saver = new Saver(this);
            saver.Run();

            this.database.Save(this);

            foreach (var table in this.Tables.Where(t=>t.Rows.Changed.Count > 0))
            {
                ((Old.ITable)table).AcceptChanges();
            }

            this.database.Save(this);

            /*
            foreach (var obj in toRefreshOld)
                ((Old.IRow)obj).Reload();
            this.toRefreshOld.Clear();
             */
        }


        #endregion

        #region IDisposable Implementation

        private void Dispose(bool userCall)
        {
            if (!is_disposed)
            {
                if (userCall)
                {
                    modules.Dispose();
                    tables.Dispose();
                    //this.database.SetSession(this.parentSession);
                    this.database.CloseSession(this, true);
                    this.parentSession = null;
                    Session.sessionsByGuid.Remove(this.guid);
                }
                is_disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Session()
        {
            Dispose(false);
        }

        #endregion

        #region Do usunięcia

        internal List<IRow> toRefreshOld;

        DBContextBase Old.ISession.GetDataContext(Type type)
        {
            if (type != null && this.database != null)
                return this.database.GetDataContext(this, type);
            return null;
        }

        DBContextBase Old.ISession.GetDataContext(string moduleName)
        {
            return ((Old.ISession)this).GetDataContext(AppModuleAttribute.Collection[moduleName] == null ? null : AppModuleAttribute.Collection[moduleName].DbContextType);
        }

        DBContextBase Old.ISession.GetDataContext(Module module)
        {
            return ((Old.ISession)this).GetDataContext(module.Name);
        }

        void Old.ISession.Undo(Row row)
        {
            if (row.DBContext != null)
            {
                var state = row.DBContext.Entry(row).State;
                switch (state)
                {
                    case System.Data.Entity.EntityState.Added:
                        row.DBContext.Set(row.GetType()).Remove(row);
                        row.SetState(RowState.Detached);
                        break;
                    case System.Data.Entity.EntityState.Modified:
                        row.IsLive = false;
                        row.DBContext.Entry(row).Reload();
                        row.SetState(RowState.Unchanged);
                        break;
                    case System.Data.Entity.EntityState.Deleted:
                        break;
                }
            }
        }

        #endregion
    }
}
