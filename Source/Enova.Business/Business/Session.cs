using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Threading;
using Enova.Business.Old.App;

namespace Enova.Business.Old
{
    public partial class Session : ISessionable, IDisposable
    {
        #region Fields

        private static int liveCounter;
        private readonly int live;
        internal Guid ImportGuid;
        internal AccessRights accessRight;
        internal AccessRights specialRight;
        public readonly bool ReadOnly;
        internal List<IRow> changedRows;
        internal Queue<IRow> lockedRows;
        private Login login;
        internal Database database;
        private bool disposed;
        private ModuleCollection modules;
        private TableCollection tables;
        private Session parentSession;
        internal static Dictionary<Database, Session> CurrentSessions = new Dictionary<Database, Session>();
 
 
        #endregion

        #region Properties

        public ObjectContext DataContext
        {
            get
            {
                if (this.database != null)
                    return this.database.GetDataContext(this);
                return null;
            }
        }

        Session ISessionable.Session
        {
            get { return this; }
        }

        internal Session RootSession
        {
            get
            {
                if (this.parentSession == null)
                    return this;
                return this.parentSession.RootSession;
            }
        }

        public string Name { get; private set; }
        public bool IsConfig { get; private set; }
        public bool IsInternal { get; internal set; }
        public Login Login
        {
            get
            {
                return this.login;
            }
        }
        public ModuleCollection Modules
        {
            get
            {
                return this.modules;
            }
        }

        public TableCollection Tables
        {
            get
            {
                return this.tables;
            }
        }

        public Guid DatabaseGuid
        {
            get
            {
                if (this.Login.databaseGuid == Guid.Empty)
                {
                    BusinessModule instance = BusinessModule.GetInstance(this);
                    this.Login.databaseGuid = new Guid(instance.SystemInfos[SysInfoIdentifier.Guid]);
                }
                return this.Login.databaseGuid;
            }
        }
 
        #endregion

        #region Methods

        internal Session(Login login, bool readOnly, bool isConfig, string name)
        {
            this.live = Interlocked.Increment(ref liveCounter);
            this.ImportGuid = Guid.Empty;
            this.accessRight = AccessRights.Granted;
            this.specialRight = AccessRights.NoInit;
            this.attach(login);
            this.Name = name;
            this.ReadOnly = readOnly;
            if (readOnly)
            {
                this.accessRight = AccessRights.ReadOnly;
            }
            else
            {
                this.changedRows = new List<IRow>();
                this.lockedRows = new Queue<IRow>();
            }
            this.IsConfig = isConfig;
            this.tables = new TableCollection(this);
            this.modules = new ModuleCollection(this);
            //this.events = new EventCollection(this);
            //this.serverEvents = new EventCollection.Server(this);
            //this.changeInfos = new ChangeInfoCollection(this);
            //this.verifiers = new VerifierCollection(this);
            //this.keys = new KeyCollection(this);
            /*
            if (this.listener != null)
            {
                this.listener.Created(this);
            }
             */
            if (Session.CurrentSessions.ContainsKey(this.database))
            {
                this.parentSession = Session.CurrentSessions[this.database]; 
            }
            Session.CurrentSessions[this.database] = this;
            
        }

        internal void attach(Login login)
        {
            this.login = login;
            this.database = login.Database;
            if (this.database == null)
            {
                throw new InvalidOperationException("Pr\x00f3ba utworzenia sesji danych dla połączenia (login), kt\x00f3re zostało już zamknięte.");
            }
            //this.connector = new Connector(this);
            //Connection connection = login.Database.PeekConnection(DatabaseType.Operational);
            //this.dbTransactionGeneration = ((connection != null) && connection.TransactionMode) ? connection.LiveGeneration : 0;
            this.Login.weakSessions.Add(this);
            //this.listener = (ISessionListener)this.GetService(typeof(ISessionListener), false);
            //this.currentTransaction = this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return this.ImportGuid.Equals(((Session)obj).ImportGuid);
        }

        public override int GetHashCode()
        {
            return this.ImportGuid.GetHashCode();
        }

        public void Save()
        {
            this.DataContext.SaveChanges();
        }

        #endregion

        #region IDisposable Implementation

        private void Dispose(bool userCall)
        {
            if (!disposed)
            {
                lock (this)
                {
                    if (userCall)
                    {
                    }
                    if (Session.CurrentSessions.ContainsKey(this.database) && Session.CurrentSessions[this.database] == this)
                        Session.CurrentSessions[this.database] = this.parentSession;
                    disposed = true;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Session()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
