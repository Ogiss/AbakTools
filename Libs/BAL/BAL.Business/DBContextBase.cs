using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace BAL.Business
{
    [DbModelBuilderVersion(DbModelBuilderVersion.Latest)]
    public class DBContextBase : DbContext, ISessionable
    {
        #region Fields

        internal App.IDatabase database;
        internal Session session;

        #endregion

        #region Properties

        public Session Session
        {
            get { return this.session; }
        }

        #endregion

        #region Methods

        public DBContextBase(App.IDatabase database)
           : base(database.Configuration.ConnectionString)
        {
            this.database = database;
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized+=new ObjectMaterializedEventHandler(ObjectContext_ObjectMaterialized);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private void ObjectContext_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (typeof(Row).IsAssignableFrom(e.Entity.GetType()))
            {
                ((Row)e.Entity).dbContext = this;
                if (this.Session != null)
                {
                    ((Row)e.Entity).session = this.Session;
                    ((Row)e.Entity).table = this.Session.Tables[CoreTools.GetObjectType(e.Entity)]; // ????
                }

                //Nowe api
                ((Row)e.Entity).state = RowState.Unchanged;
                ((Row)e.Entity).IsLive = true;

                if (e.Entity is IRowInvoker)
                    ((IRowInvoker)e.Entity).Invoke(RowInvokeType.Loaded, new EventArgs());
            }
        }

        public long GetDBTS()
        {
            return this.Database.SqlQuery<long>("SELECT CONVERT(BIGINT, @@DBTS)", null).First();
        }

        #endregion
    }
}
