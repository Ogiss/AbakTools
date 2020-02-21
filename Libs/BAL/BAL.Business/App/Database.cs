using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using BAL.Configuration;

namespace BAL.Business.App
{
    public class Database : IDatabase, IDBTS, IDisposable
    {
        #region Fields

        private IDatabaseConfiguration config;
        private ISet<ILogin> logins;
        private bool disposed;
        private static Dictionary<Type, Type> dataContextTypeMap;
        private Dictionary<Guid, Dictionary<Type, DBContextBase>> dataContextBySession;
        private object syncRoot = new object();
        
        #endregion

        #region Properties

        public IDatabaseConfiguration Configuration
        {
            get { return this.config; }
        }

        public ISet<ILogin> Logins
        {
            get { return this.logins; }
        }

        public string ConnectionString
        {
            get
            {
                if (this.config != null)
                    return this.config.ConnectionString;
                return null;
            }
        }
 
        #endregion

        #region Methods

        public Database(IDatabaseConfiguration config)
        {
            this.disposed = false;
            this.config = config;
            this.logins = new HashSet<ILogin>();
            this.dataContextBySession = new Dictionary<Guid, Dictionary<Type, DBContextBase>>();
        }

        public virtual ILogin Login(string domain, string user, string password)
        {
            ILoginService service = (ILoginService)AppController.Instance.GetService<ILoginService, LoginService>();
            if (service != null)
            {
                ILogin login = service.Login(this, domain, user, password);
                if (login != null)
                {
                    this.logins.Add(login);
                    AppController.Instance.currentLogin = login;
                    return login;
                }
            }
            return null;
        }

        public virtual void Logout(ILogin login)
        {
            this.logins.Remove(login);
            AppController.Instance.currentLogin = null;
            ILoginService service = (ILoginService)AppController.Instance.GetService<ILoginService, LoginService>();
            if (service != null)
            {
                service.Logout(login);
            }
        }


        private static void checkDataContextTypeMap()
        {
            if (dataContextTypeMap == null)
            {
                dataContextTypeMap = new Dictionary<Type, Type>();

                foreach (var attr in AppModuleAttribute.Collection)
                {
                    if (attr.DbContextType == null)
                        continue;

                    if (!dataContextTypeMap.ContainsKey(attr.DbContextType))
                    {
                        dataContextTypeMap.Add(attr.DbContextType, attr.DbContextType);
                        var baseTypes = CoreTools.GetBaseTypes(attr.DbContextType, typeof(DBContextBase));
                        foreach (var bt in baseTypes)
                            dataContextTypeMap[bt] = attr.DbContextType;
                    }
                }
            }
        }

        internal static Type GetMapDataContextType(Type type)
        {
            checkDataContextTypeMap();
            if (dataContextTypeMap.ContainsKey(type))
                return dataContextTypeMap[type];
            return type;
        }

        public virtual DBContextBase GetDataContext(ISessionable session, Type dataContextType)
        {

            if (!this.dataContextBySession.ContainsKey(session.Session.Guid))
                this.dataContextBySession.Add(session.Session.Guid, new Dictionary<Type, DBContextBase>());
            var dataContexts = this.dataContextBySession[session.Session.Guid];

            Type mapType = GetMapDataContextType(dataContextType);
            if (mapType != null)
            {
                if (dataContexts.ContainsKey(mapType))
                    return dataContexts[mapType];

                ConstructorInfo cinfo = mapType.GetConstructor(new Type[] { typeof(IDatabase) });
                if (cinfo == null)
                {
                    throw new BusException("Typ " + mapType.Name + " nie posiada Kontruktora " + mapType.Name + "(IDatabase database, string connectionString)");
                }
                DBContextBase dbContext = (DBContextBase)cinfo.Invoke(new object[] { this });
                if (dbContext != null)
                {
                    dbContext.session = session.Session;
                    dataContexts.Add(mapType, dbContext);
                    return dbContext;
                }
            }
            throw new BusException("Nieprawidłowo zarejestrowany DataContext (" + dataContextType.Name + ")");
        }

        public long GetDBTS()
        {
            using (SqlConnection connection = new SqlConnection(this.config.ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT CONVERT(BIGINT, @@DBTS)", connection);
                return (long)command.ExecuteScalar();
            }
        }

        public void Save(ISessionable session)
        {
            try
            {
                if (session != null && session.Session != null && this.dataContextBySession.ContainsKey(session.Session.Guid))
                    foreach (var dc in this.dataContextBySession[session.Session.Guid].Values)
                        dc.SaveChanges();
            }
            catch (Exception ex)
            {
                AppController.ThrowException(ex);
            }
        }

        public void CloseSession(ISessionable session, bool save = true)
        {
            if (save)
                this.Save(session);
            if (session != null && session.Session !=null && this.dataContextBySession != null && this.dataContextBySession.ContainsKey(session.Session.Guid))
            {
                foreach (var dc in this.dataContextBySession[session.Session.Guid].Values)
                    dc.Dispose();
                this.dataContextBySession.Remove(session.Session.Guid);
            }
        }

        #endregion

        #region IDisposable Implementation

        protected virtual void Dispose(bool userCall)
        {
            if (!this.disposed)
            {
                foreach (var login in this.logins.ToArray())
                    this.Logout(login);
                foreach(Guid guid in this.dataContextBySession.Keys)
                foreach (var dc in this.dataContextBySession[guid].Values)
                    dc.Dispose();
                this.dataContextBySession.Clear();
                this.dataContextBySession = null;
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Database()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
