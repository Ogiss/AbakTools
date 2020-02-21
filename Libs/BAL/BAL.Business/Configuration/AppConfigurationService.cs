using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BAL.Business;

namespace BAL.Configuration
{
    public class AppConfigurationService : AppServiceBase, IAppConfigurationService
    {
        #region Fields

        private System.Configuration.Configuration configuration;
        private IDatabasesConfigurationService databasesConfiguration;

        #endregion

        #region Properties

        public IDatabasesConfigurationService DatabasesConfiguration
        {
            get { return this.databasesConfiguration; }
        }

        public System.Configuration.Configuration Configuration
        {
            get { return this.configuration; }
        }

        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return configuration.ConnectionStrings.ConnectionStrings; }
        }

        #endregion

        #region Methods

        public AppConfigurationService(Session session) : base(session)
        {
        }

        public AppConfigurationService() : base() { }

        public virtual void Init()
        {
            this.configuration = GetConfiguration();
            this.databasesConfiguration = (IDatabasesConfigurationService)AppController.Instance.GetService<IDatabasesConfigurationService, DatabaseConfigurationService>();
        }


        protected virtual System.Configuration.Configuration GetConfiguration()
        {
            return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        #endregion
    }
}
