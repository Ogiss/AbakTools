using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.DB;
using Enova.Business.Old.DB.Web;

namespace Enova.Business.Old.Core
{
    public static class ContextManager
    {

        private static string dataBaseName = null;
        public static string DataBaseName
        {
            get
            {
                if (dataBaseName == null)
                {
                    if (dataBaseSettings != null)
                        return dataBaseSettings.Name;
                }
                return dataBaseName;
            }

            set
            {
                //ContextManager.Dispose();
                if (ContextManager.dataContext != null)
                {
                    ContextManager.dataContext.Dispose();
                    ContextManager.dataContext = null;
                }
                ContextManager.dataBaseName = value;
            }
        }

        private static Enova.Business.Old.Types.DataBaseSettings dataBaseSettings = null;
        public static Enova.Business.Old.Types.DataBaseSettings DataBaseSettings
        {
            get
            {
                if (ContextManager.dataBaseSettings == null)
                {
                    if (string.IsNullOrEmpty(ContextManager.dataBaseName))
                    {
                        ContextManager.dataBaseSettings = Enova.Business.Old.Core.Configuration.GetDefaultDataBaseSettings();
                    }
                    else
                    {
                        ContextManager.dataBaseSettings = Enova.Business.Old.Core.Configuration.GetDataBaseSettings(ContextManager.dataBaseName);
                    }
                }
                return ContextManager.dataBaseSettings;
            }

            set
            {
                if (ContextManager.dataContext != null)
                {
                    ContextManager.dataContext.Dispose();
                    ContextManager.dataContext = null;
                }
                ContextManager.dataBaseSettings = value;
            }
                
        }

        public static System.Data.Objects.ObjectContext GetContextByType(Type type)
        {
            if (type == typeof(EnovaContext))
                return ContextManager.DataContext;
            else if (type == typeof(WebContext))
                return ContextManager.WebContext;
            return null;
        }


        private static EnovaContext dataContext = null;
        public static EnovaContext DataContext
        {
            get
            {

                if (ContextManager.dataContext == null)
                {
                    var settings = ContextManager.DataBaseSettings;
                    if (settings == null)
                    {
                        return null;
                        //throw new Exception("Błędna konfiguracja baz danych lub jej brak");
                    }
                    ContextManager.dataContext = new EnovaContext(settings.ConnectionString);
                    ContextManager.dataContext.CommandTimeout = int.MaxValue;
                }
                return ContextManager.dataContext;
            }
        }

        private static WebContext webContext = null;
        public static WebContext WebContext
        {
            get
            {
                if (webContext == null)
                {
                    webContext = new WebContext();
                    webContext.CommandTimeout = int.MaxValue;
                }
                return webContext;
            }
        }

        /*
        private static Enova.Business.Old.DB.Demo.DemoContext demoContext = null;
        public static Enova.Business.Old.DB.Demo.DemoContext DemoContext
        {
            get
            {
                if (demoContext == null)
                {
                    demoContext = new DB.Demo.DemoContext();
                    demoContext.CommandTimeout = int.MaxValue;
                }
                return demoContext;
            }
        }
         */

        public static void DisposeDataContext()
        {
            if (ContextManager.dataContext != null)
            {
                ContextManager.dataContext.Dispose();
                ContextManager.dataContext = null;
            }
        }

        public static void Dispose()
        {
            ContextManager.dataBaseSettings = null;
            if (ContextManager.dataContext != null)
            {
                ContextManager.dataContext.Dispose();
                ContextManager.dataContext = null;
            }
            /*
            if (webContext != null)
            {
                webContext.Dispose();
                webContext = null;
            }
             */
        }
    }
}
