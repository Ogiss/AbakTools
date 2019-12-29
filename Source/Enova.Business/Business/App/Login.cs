using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Old.Types;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.App
{
    public class Login
    {
        #region Fields

        /*
        public const string EnovaToolsLogin  = "EnovaTools";
        public const string EnovaToolsPassword = "mk024315ws";
         */

        private string user;
        private string password;
        private bool readOnly;
        private Database database;
        public Guid GlobalIdentifier;
        internal Guid databaseGuid;
        internal bool isLoginEntry;
        internal readonly WeakCollection<Session> weakSessions;

        #endregion

        #region Properties

        public Database Database
        {
            get
            {
                return this.database;
            }
        }

        public Operator Operator { get; internal set; }

        public bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }
        }

        #endregion

        #region Methods

        public Login(Database database, string user, string password)
        {
            this.database = database;
            this.user = user;
            this.password = password;
            this.GlobalIdentifier = Guid.NewGuid();
            this.readOnly = false;
            WeakCollectionSession session = new WeakCollectionSession
            {
                Name = "Sessions: " + database.Name
            };
            this.weakSessions = session;
            this.database.logins.Add(this);
        }

        internal Session CreateSessionInternal(bool readOnly, bool config, bool isinternal, string name)
        {
            Session session2;
            lock (this)
            {
                Session session = new Session(this, readOnly, config, name)
                {
                    IsInternal = isinternal
                };
                try
                {
                    //BusApplication.Instance.Licence.SetLogin(this, session);
                    BusApplication.Instance.SetLogin(this, session);
                    session2 = session;
                }
                catch
                {
                    session.Dispose();
                    throw;
                }
            }
            return session2;
        }

        public Session CreateSession(bool readOnly, bool config, string name)
        {
            return this.CreateSessionInternal(readOnly || this.ReadOnly, config, false, name);
        }

        [Obsolete("Brak kodu")]
        internal void checkRole(Session session)
        {
            /*
            if ((this.Operator != null) && (this.checkRoleCounter <= 0))
            {
                lock (this)
                {
                    if (this.actualRole == null)
                    {
                        try
                        {
                            Interlocked.Increment(ref this.checkRoleCounter);
                            if (session != null)
                            {
                                this.loadRoles(session);
                            }
                            else
                            {
                                using (session = this.CreateSession(true, false, "Login.LoadRoles"))
                                {
                                    this.loadRoles(session);
                                }
                            }
                        }
                        finally
                        {
                            Interlocked.Decrement(ref this.checkRoleCounter);
                        }
                    }
                }
            }
             */
        }

        #endregion

        #region Nested Types

        private class WeakCollectionSession : WeakCollection<Session>
        {
            // Methods
            public override string GetObjectInfo(object obj)
            {
                throw new NotImplementedException("Login.GetObjectInfo(object obj)");
                /*
                Session session = (Session)obj;
                int num = 0;
                int num2 = 0;
                foreach (Table table in session.Tables.ToArray())
                {
                    num++;
                    num2 += table.Rows.Loaded.Count;
                }
                string str = "";
                if (!string.IsNullOrEmpty(base.Name))
                {
                    str = " - " + base.Name;
                }
                return string.Concat(new object[] { session.Name, ", live: ", session.LiveCounter, ", tables: ", num, ", rows: ", num2, " l", session.Login.LiveCounter, session.ReadOnly ? ", readonly" : "", session.IsConfig ? ", config" : "", str });
                 */
            }
        }



        #endregion
    }
}
