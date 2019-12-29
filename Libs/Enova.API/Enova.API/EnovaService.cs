using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Timers;

namespace Enova.API
{
    public class EnovaService : IDisposable
    {
        #region Fields

        private static EnovaService instance;
        private string enovaPath;
        private AppDomain appDomain;
        private bool is_disposed;
        private Connector.EnovaConnector connector;
        private bool loaded;
        private static Timer pingPong;
        internal static Dictionary<Type, object> tools;
        public static ILogger Logger { get; private set; }
        internal static Enova.API.ITowaryTools TowaryTools
        {
            get
            {
                if (tools != null && tools.ContainsKey(typeof(Enova.API.ITowaryTools)))
                    return (Enova.API.ITowaryTools)tools[typeof(Enova.API.ITowaryTools)];
                return null;
            }
        }
        internal static bool ConnectorSide;

        #endregion

        #region Properties

        public static EnovaService Instance
        {
            get { return instance; }
        }

        public bool Loaded
        {
            get
            {
                return loaded;
            }
        }

        public bool IsLogined
        {
            get { return CurrentLogin != null; }
        }

        public Business.Login CurrentLogin
        {
            get { return connector != null ? connector.CurrentLogin : null; }
        }

        public static T FromEnova<T>(object obj)
        {
            if (ConnectorSide)
                return Connector.EnovaHelper.FromEnova<T>(obj);
            if (instance.connector != null)
                return instance.connector.FromEnova<T>(obj);
            return (T)(object)null;
        }

        #endregion

        #region Methods

        static EnovaService()
        {
            tools = new Dictionary<Type, object>();
        }

        private EnovaService()
        {
            EnovaService.instance = this;
        }

        public static EnovaService Initialize(string enovaPath, ILogger logger)
        {
            Logger = logger;
            try
            {
                Logger.Info("Initializing EnovaApi");

                if (string.IsNullOrEmpty(enovaPath) || !Directory.Exists(enovaPath) || !File.Exists(Path.Combine(enovaPath, "Soneta.Business.dll")))
                    throw new Exception("Nieprawidłowa ścieżka do programu enova.");

                var service = new EnovaService();
                service.enovaPath = enovaPath;
                service.init();

                Logger.Info("EnovaApi initialized");
                return service;
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }
        }

        private void init()
        {
            /*
            AppDomainSetup ads = new AppDomainSetup();
            ads.ApplicationBase = enovaPath;
            ads.PrivateBinPath = enovaPath;
            ads.ShadowCopyFiles = "true";
            ads.LoaderOptimization = LoaderOptimization.MultiDomainHost;
            
            //ads.DisallowBindingRedirects = false;
            //ads.DisallowCodeDownload = true;
            //ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            Evidence ev = new Evidence(AppDomain.CurrentDomain.Evidence);

            appDomain = AppDomain.CreateDomain("EnovaConnectorDomain", ev, ads);
            connector = (Connector.EnovaConnector)appDomain.CreateInstanceFromAndUnwrap(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Enova.API.dll"), typeof(Connector.EnovaConnector).FullName);
            if (connector != null)
            {
                loaded = connector.Load(AppDomain.CurrentDomain.BaseDirectory);
                pingPong = new Timer();
                pingPong.Interval = 1000;
                pingPong.Elapsed += (o, e) => {
                    if (connector != null)
                        connector.PingPong();
                };
                pingPong.Enabled = true;

            }
            */
            connector = new Connector.EnovaConnector();
            loaded = connector.Load(AppDomain.CurrentDomain.BaseDirectory, enovaPath);
        }

        public Business.Login Login(string dataBase, string user, string password)
        {
            if (connector != null)
                return connector.Login(dataBase, user, password);
            return null;
        }

        public void Logout()
        {
            if (connector != null)
                connector.Logout();
        }

        public Business.Session CreateSession()
        {
            if (CurrentLogin != null)
                return CurrentLogin.CreateSession(false, false);
            return null;
        }

        public T CreateObject<T>(object enovaObject = null, object[] args = null) where T : Types.IObjectBase
        {
            return (T)CreateObject(typeof(T), enovaObject, args);
        }

        public API.Types.IObjectBase CreateObject(Type apiType, object enovaObject = null, object[] args = null)
        {
            if (connector != null)
                return connector.CreateObject(apiType, enovaObject, args);
            return null;
        }

        

        //public Business.IRow CreateRow(object enovaObject = null)

        public void DrukujRow(System.Windows.Forms.Form form, Business.Row row, string template = null, API.Printer.Destinations destination = API.Printer.Destinations.Preview, string outputFile = null)
        {
            throw new NotImplementedException();
            /*
            template = GetTemplate(row, template);
            var erow = row.Record as Soneta.Business.Row;
            if (!string.IsNullOrEmpty(template) && erow != null)
            {
                var context = Soneta.Business.Context.Empty.Clone(((Soneta.Business.ISessionable)erow).Session);
                context.Set(erow);

                var generator = new Soneta.Printer.AspGenerator()
                {
                    TemplateFileName = template,
                    Destination = (Soneta.Printer.AspGenerator.Destinations)(int)destination,
                };

                if (!string.IsNullOrEmpty(outputFile))
                    generator.OutputFileName = outputFile;

                try
                {

                    if (generator != null)
                        generator.Print(form != null ? form : System.Windows.Forms.Form.ActiveForm, context, destination == Enova.API.Printer.Destinations.Preview, new System.Collections.ArrayList());
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
             */

        }

        public object GetStaticValue(string enovaType, string name)
        {
            if (connector != null)
                return connector.GetStaticValue(enovaType, name);
            return null;
        }

        public void SetTools(Type toolsType, object tools)
        {
            EnovaService.tools.Add(toolsType, tools);
        }

        private void Dispose(bool userCall)
        {
            if (!is_disposed)
            {
                is_disposed = true;
                if (userCall)
                {
                    //user call
                }

                if (appDomain != null)
                {
                    if (pingPong != null)
                        pingPong.Dispose();
                    pingPong = null;
                    if (connector != null)
                        connector.Finish();
                    connector = null;
                    AppDomain.Unload(appDomain);
                    appDomain = null;

                }
                instance = null;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EnovaService()
        {
            this.Dispose(false);
        }

        #endregion

    }
}
