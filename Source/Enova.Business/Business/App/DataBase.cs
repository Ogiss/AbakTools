using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using Enova.Old.Types;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.App
{
    public class Database : IDisposable
    {
        #region Fields

        private Type dataContextType;
        private ObjectContext dataContext;
        private string name;
        private string connectionString;
        private bool disposed;
        internal Set<Login> logins;
        private static Dictionary<string, Database> databases = new Dictionary<string, Database>();

        #endregion

        #region Properties

        public Type DataContextType
        {
            get { return this.dataContextType; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(connectionString) && string.IsNullOrEmpty(name))
                    return null;
                if (string.IsNullOrEmpty(this.connectionString))
                    return "name=" + name;
                return this.connectionString;
            }
        }

        public static Dictionary<string, Database> Databases
        {
            get { return databases; }
        }

        #endregion

        #region Methods

        public Database(Type dataContextType, string name, string connectionString)
        {
            if (dataContextType == null)
                throw new ArgumentException("DataBase: dataContextType jest wymagany");

            this.dataContextType = dataContextType;
            this.name = name;
            this.connectionString = connectionString;
            this.disposed = false;
            this.logins = new Set<App.Login>();
            Database.databases.Add(string.IsNullOrEmpty(name) ? dataContextType.Name : name, this);
        }

        public Database(Type dataContextType, string name) : this(dataContextType, name, null) { }

        public Database(Type dataContextType) : this(dataContextType, null, null) { }

        public ObjectContext GetDataContext(Session session)
        {
            if (this.dataContext != null)
            {
                if (this.dataContext is ISessionable)
                {
                    if (!((ISessionable)this.dataContext).Session.Equals(session))
                    {
                        if (this.dataContext is ISetSession)
                            ((ISetSession)this.dataContext).Session = session;
                    }
                }
                return this.dataContext;
            }

            var cs = this.ConnectionString;
            System.Reflection.ConstructorInfo cinfo;
            if (string.IsNullOrEmpty(cs))
                cinfo = this.dataContextType.GetConstructor(new Type[0] );
            else
                cinfo = this.dataContextType.GetConstructor(new Type[] { typeof(string) });
            if (cinfo != null)
            {
                this.dataContext = (ObjectContext)cinfo.Invoke(string.IsNullOrEmpty(cs) ? new object[0] : new object[] { cs });
                if (this.dataContext is ISetSession)
                    ((ISetSession)this.dataContext).Session = session;

                return this.dataContext;
            }
            return null;
        }

        public Login Login(string user, string password)
        {
            lock (this)
            {
                try
                {
                    Login login = new Login(this, user, password);
                    using (Session session = login.CreateSessionInternal(false, false, true, "Login.Operator"))
                    {
                        BusinessModule instance = BusinessModule.GetInstance(session);
                        /*
                        if (instance.SystemInfos[SysInfoIdentifier.ConversionInProgress] == "True")
                        {
                            throw new LoginException("Baza danych podczas konwersji lub uszkodzona nieudaną konwersją.");
                        }
                         */
                        Operator row = instance.Operators.ByName[user];
                        if ((row == null) || (bool)row.Locked)
                        {
                            throw new OperatorNotFoundException(user, this);
                        }
                        if (row.Trusting(password) && !row.IsValidPassword(password))
                        {
                            /*
                            try
                            {
                                if (row.Module.Config.AccountPolicies.AccountLockoutThreshold > 0)
                                {
                                    this.modifyOperator(row, delegate(Operator op2)
                                    {
                                        op2.Extension.InvalidLogin();
                                    });
                                }
                            }
                            catch (InvalidDatabaseStructureException)
                            {
                            }
                            catch (ConfigKeyNotFoundException)
                            {
                            }
                             */
                            throw new IncorrectPasswordException(user, row.PromptPassword, row.Guid == Operators.Administrator, this);
                        }
                        //this.checkDB(instance, row.ID);
                        //login.IsEntitleAvailable = instance.Config.Authorization.UseEntitle && (EnovaLicence.KolorLicencji(this, instance.SystemInfos[SysInfoIdentifier.NumerSeryjny]) == Kolor.Platynowy);
                        login.Operator = row;
                        login.checkRole(session);
                        bool singleOperatorLogin = false;
                        bool flag2 = BusApplication.Instance.SetLogin(login, session, singleOperatorLogin);
                        /*
                        if (singleOperatorLogin && flag2)
                        {
                            throw new RowException(row, "Operator {0} jest już zalogowany do programu. Nie można logować się jednocześnie na dw\x00f3ch stanowiskach.", new object[] { row });
                        }
                        if (row.IsAccountLocked || (row.Extension.InvalidLoginCounter > 0))
                        {
                            this.modifyOperator(row, delegate(Operator op2)
                            {
                                op2.IsAccountLocked = false;
                            });
                        }
                        instance.Config.ChangeInfos.LoadInfos();
                        string machineName = Environment.MachineName;
                        if (BusApplication.Instance.ApplicationName != null)
                        {
                            machineName = BusApplication.Instance.ApplicationName + " - " + machineName;
                        }
                        session.ChangeInfos.Add(null, ChangeInfoType.Login, machineName);
                         */
                        session.Save();
                        login.isLoginEntry = true;

                    }
                    return login;
                }
                catch
                {
                    return null;
                }
            }
        }


        #endregion

        #region IDisposable Implementation

        private void Dispose(bool userCall)
        {
            if (!this.disposed)
            {
                if (userCall)
                {
                    if (this.dataContext != null)
                    {
                        this.dataContext.Dispose();
                        Database.databases.Remove(this.Name);
                        this.dataContext = null;
                    }
                }
                this.disposed = true;
            }
        }

        public override bool Equals(object obj)
        {
            Database db = (Database)obj;
            return db.name == this.name && db.DataContextType == this.DataContextType;
        }

        public override int GetHashCode()
        {
            return this.DataContextType.GetHashCode() + (string.IsNullOrEmpty(this.Name) ? 0 : 7 * this.Name.GetHashCode());
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
