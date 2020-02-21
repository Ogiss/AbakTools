using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using BAL.Types;
using BAL.Tools;
using BAL.Configuration;
using BAL.Business.App;

namespace BAL.Business
{
    public sealed class AppController
    {
        #region Fields

        private static AppController instance;
        private AttributesCollection assemblyAttributes;
        private IFileStorageService fileStorageService;
        private Dictionary<Type, object> services;
        private IAppConfigurationService configuration;

        internal ILogin currentLogin = null;
        //private static IDatabase currentDatabase;
        

        
        public static ExceptionMethodHandler ExceptionProcess;

        #endregion

        #region Properties

        public IDatabase this[string name]
        {
            get
            {
                return this.Configuration.DatabasesConfiguration[name];
            }
        }

        public static AppController Instance
        {
            get
            {
                if (AppController.instance == null)
                    AppController.instance = new AppController();
                return AppController.instance;
            }
        }

        public AttributesCollection AssemblyAttributes
        {
            get
            {
                if (this.assemblyAttributes == null)
                    this.initAssemblyAttributes();
                return assemblyAttributes;
            }
        }

        public IFileStorageService FileStorageService
        {
            get
            {
                if (this.fileStorageService == null)
                    this.fileStorageService = (IFileStorageService)GetService<IFileStorageService, FileStorageService>();
                return this.fileStorageService;
            }
        }

        public IAppConfigurationService Configuration
        {
            get
            {
                if (configuration == null)
                    initConfiguration();
                return configuration;
            }
        }

        public Func<Assembly, bool> IsAppAssembly { get; set; }

        public ILogin CurrentLogin
        {
            get
            {
                return currentLogin;
            }
        }

        #endregion

        #region Methods

        public AppController()
        {
            this.IsAppAssembly = (asm) => { return true; };
        }

        public void Finish()
        {
            foreach (var session in Session.sessionsByGuid.Values.ToList())
                session.Dispose();

            if (CurrentLogin != null)
                CurrentLogin.Logout();

        }

        public static void ThrowException(Exception ex)
        {
            if (ExceptionProcess != null)
                ExceptionProcess.Invoke(ex);
        }

        public static void Login(IDatabase database, string domain, string user, string password)
        {
            database.Login(domain, user, password);
        }

        private void initAssemblyAttributes()
        {
            if (assemblyAttributes == null)
            {
                assemblyAttributes = new AttributesCollection();

                var asms = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a =>
                        !a.FullName.StartsWith("Microsoft") &&
                        !a.FullName.StartsWith("System") &&
                        !a.FullName.StartsWith("mscorlib") &&
                        !a.FullName.StartsWith("vshost32")).ToList();

                var t = typeof(AttributeBase);

                foreach (var asm in asms)
                {
                    if (IsAppAssembly(asm))
                    {
                        var attrs = asm.GetCustomAttributes(true);
                        foreach (var attr in attrs)
                            if (t.IsAssignableFrom(attr.GetType()))
                                assemblyAttributes.Add((AttributeBase)attr);
                    }
                }
            }
        }

        private void initConfiguration()
        {
            var service = AppController.Instance.GetService<IAppConfigurationService, AppConfigurationService>();
            if (service == null || !typeof(IAppConfigurationService).IsAssignableFrom(service.GetType()))
                throw new AppServiceException(typeof(IAppConfigurationService));

            configuration = (IAppConfigurationService)service;
            configuration.Init();
        }

        public object GetService(Type serviceInterface, Type defaultServiceType = null)
        {
            if (services == null)
                services = new Dictionary<Type, object>();
            if (services.ContainsKey(serviceInterface))
                return services[serviceInterface];

            Type type = AppServiceAttribute.GetServiceType(serviceInterface, defaultServiceType);

            if (type != null)
            {
                var service = type.GetConstructor(new Type[0]).Invoke(new object[0]);
                services.Add(serviceInterface, service);
                return service;
            }
            return null;
        }

        public object GetService<I, D>()
        {
            return GetService(typeof(I), typeof(D));
        }

        public object GetService<I>()
        {
            return GetService(typeof(I));
        }

        #endregion
    }
}
