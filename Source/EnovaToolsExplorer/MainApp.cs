using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.Objects;
using System.Configuration;
using Enova.Business.Old;
using Enova.Business.Old.DB;
using System.IO;
using System.Threading;
using Core = Enova.Business.Old.Core;
using AbakTools.Framework;
using AbakTools.Framework.Logging;
using System.Globalization;

namespace EnovaToolsExplorer
{
    static class MainApp
    {

        #region Fields

        private static ILogger Logger { get; set; }

        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            if (args.Length > 0 && args[0] == "update")
                Update();

            var bootstrapper = new Bootstrapper();
            bootstrapper.ConfigureComponents();
            bootstrapper.RunApplication();

            Logger = LogProvider.GetLogger(LogNames.Application);
            Logger.Debug("Application starting");

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(CurrentDomain_UnhandledException);

            Enova.Business.Old.Core.Configuration.LoadConfiguration();
            //var enovaPath = Enova.Business.Old.Core.Configuration.GetSetting("EnovaPath");
            //Enova.API.EnovaService.Initialize(enovaPath);
            AbakTools.Business.Forms.LoginForm.LoginToEnova = Enova.API.EnovaService.Instance.Loaded;
            BAL.Forms.ColumnSelectForm.ExcludedTypes.Add(typeof(Enova.API.Types.IObjectBase));
            BAL.Forms.ColumnSelectForm.ExcludedTypes.Add(typeof(Enova.API.Business.ISessionable));
            BAL.Forms.ColumnSelectForm.ExcludedTypes.Add(typeof(Enova.API.Business.Session));

            CheckdefaultDatabase();
            Assembly.Load(new AssemblyName("AbakTools.Business"));
            Assembly.Load(new AssemblyName("AbakTools.Core"));
            //Assembly.Load(new AssemblyName("AbakTools.CRM"));
            //Assembly.Load(new AssemblyName("AbakTools.Handel"));

            Assembly.Load(new AssemblyName("AbakTools.Business.Forms"));
            Assembly.Load(new AssemblyName("AbakTools.Core.Forms"));
            Assembly.Load(new AssemblyName("AbakTools.CRM.Forms"));
            Assembly.Load(new AssemblyName("AbakTools.Handel.Forms"));
            Assembly.Load(new AssemblyName("AbakTools.Ksiegowosc.Forms"));
            Assembly.Load(new AssemblyName("AbakTools.Kadry.Forms"));
            Assembly.Load(new AssemblyName("AbakTools.Analizy.Forms"));
            Assembly.Load(new AssemblyName("AbakTools.Finanse.Forms"));
            //Assembly.Load(new AssemblyName("AbakTools.FedEx.Forms"));
            Assembly.Load(new AssemblyName("Enova.Forms"));

            //Assembly.Load(new AssemblyName("AbakTools.Tetris"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BAL.Business.AppController.Instance.IsAppAssembly = (asm) =>
            {
                return !asm.FullName.StartsWith("Soneta");
            };
            BAL.Forms.FormManager.StartApplication();
            BAL.Forms.FormManager.StopApplication();
            Enova.Business.Old.Core.ContextManager.Dispose();
            Enova.API.EnovaService.Instance.Dispose();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!e.IsTerminating)
            {
                Exception ex = (Exception)e.ExceptionObject;
                Logger.Error(ex.ToString());
                BAL.Forms.ExceptionForm.Show(ex);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = (Exception)e.Exception;
            Logger.Error(ex.ToString());
            BAL.Forms.ExceptionForm.Show(ex);
        }

        private static void Update()
        {
            KeyValueConfigurationElement el = Enova.Business.Old.Core.Configuration.Config.AppSettings.Settings["UpdatePath"];
            if (el != null && Directory.Exists(el.Value))
            {
                string[] dirs = Directory.GetDirectories(el.Value);
                if (dirs.Length > 0)
                {
                    string ver = dirs.Max().Split(new char[] { '\\' }).Last();
                    var files = Directory.GetFiles(el.Value + "\\" + ver, "EnovaToolsStart.*");
                    foreach (var f in files)
                    {
                        string file = Path.GetFileName(f);
                        File.Copy(f, Directory.GetCurrentDirectory() + "\\" + file, true);
                    }
                }
            }
        }

        private static void CheckdefaultDatabase()
        {
            //<add name="AbakTools" connectionString="Data Source=SERWER\SQLEXPRESS;Initial Catalog=EnovaWebTools;Integrated Security=True;MultipleActiveResultSets=True" />
            if (Core.Configuration.ConnectionStrings["AbakTools"] == null)
            {
                Core.Configuration.ConnectionStrings.Add(
                    new ConnectionStringSettings("AbakTools", @"Data Source=SERWER\SQLEXPRESS;Initial Catalog=AbakTools;Integrated Security=True;MultipleActiveResultSets=True"));
                Core.Configuration.SaveConfiguration();
            }
        }
        
    }
}
