using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BAL.Business;
using BAL.Business.App;

namespace BAL.Configuration
{
    public class DatabaseConfigurationService : AppServiceBase, IDatabasesConfigurationService
    {
        #region Fields

        private DatabasesConfigurationCollection databasesConfigurations;
        private Dictionary<string, IDatabase> databases;

        #endregion

        #region Properties

        public IDatabase this[string name, bool throwEx]
        {
            get
            {
                if (databases.ContainsKey(name))
                    return databases[name];

                if (databasesConfigurations[name] != null)
                {
                    var db = CreateDatabase(databasesConfigurations[name]);
                    databases.Add(name, db);
                    return db;
                }

                if (throwEx)
                    throw new UnknownDatabaseException(name);

                return null;
                
            }
        }

        public IDatabase this[string name]
        {
            get
            {
                return this[name, true];
            }
        }

        public DatabasesConfigurationCollection DatabasesConfigurations
        {
            get { return this.databasesConfigurations; }
        }

        #endregion

        #region Methods

        public DatabaseConfigurationService(Session session)
            : base(session)
        {
            databasesConfigurations = this.CreateDatabasesCollection();
            databases = new Dictionary<string, IDatabase>();
        }

        public DatabaseConfigurationService() : this(null) { }

        protected virtual DatabasesConfigurationCollection CreateDatabasesCollection()
        {
            var databases = new DatabasesConfigurationCollection();

            foreach (ConnectionStringSettings css in AppController.Instance.Configuration.Configuration.ConnectionStrings.ConnectionStrings)
            {
                databases.Add(new DatabaseConfiguration()
                {
                    Name = css.Name,
                    //ConnectionString = "name=" + css.Name,
                    ConnectionString = css.ConnectionString,
                    ProviderName = css.ProviderName
                });
            }

            return databases;
        }

        public virtual IDatabase CreateDatabase(IDatabaseConfiguration config)
        {
            return new Database(config);
        }

        #endregion

    }
}
