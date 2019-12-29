using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Enova.API.Business;
using System.Windows.Forms;


namespace Enova.API.Connector
{
    public class EnovaConnector : MarshalByRefObject
    {
        #region Fields

        private Business.BusApplication busApplication;
        private Business.Database database;
        private Login login;
        private Hashtable assemblies;
        private string apiPath;
        public static EnovaConnector Instance;

        #endregion

        #region Properties

        public Hashtable Assemblies
        {
            get
            {
                if (assemblies == null)
                    assemblies = new Hashtable();
                return assemblies;
            }
        }

        public Login CurrentLogin
        {
            get { return login; }
        }

        #endregion

        #region Methods

        public EnovaConnector()
        {
            EnovaConnector.Instance = this;
        }

        public bool Load(string apiPath )
        {
            this.apiPath = apiPath;
            EnovaService.ConnectorSide = true;
            var asm = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Soneta.Start.dll"));
            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            AppDomain.CurrentDomain.AssemblyLoad+=new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            var loader = asm.CreateInstance("Soneta.Start.Loader");
            if (loader != null)
            {
                var obj = new API.Types.ObjectBase() { EnovaObject = loader };
                obj.SetValue("WithExtra", true);
                obj.SetValue("WithUI", true);
                obj.CallMethod("Load");
                obj.CallMethodFull("UseCurrentDllsAsInstallation", new Type[] { typeof(bool) }, new object[] { false });
                var t = Type.GetType("Soneta.Business.App.BusApplication, Soneta.Business");
                if (t != null)
                {
                    busApplication = new Business.BusApplication() { EnovaObject = t.GetProperty("Instance").GetValue(null, null) };
                    busApplication.ApplicationName = "enova";
                    busApplication.InitDatabases();
                    Application.ThreadException+=new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                    
                    return true;
                }
            }
            
            return false;
        }

        public void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs e)
        {
            var name = e.LoadedAssembly.GetName().Name;
            if (name.StartsWith("Soneta") || name.StartsWith("Enova"))
                Assemblies.Add(e.LoadedAssembly.GetName().Name, e.LoadedAssembly);
        }

        public Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Name))
            {
                var name = new AssemblyName(e.Name);
                if(File.Exists(Path.Combine(apiPath,name.Name+".dll")))
                    return Assembly.LoadFrom(Path.Combine(apiPath,name.Name+".dll"));
            }
            return null;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                throw new SonetaException(ex);
            }
            else
                throw new Exception("Enova wegenerowała wyjatek: " + e.ExceptionObject.GetType().FullName);
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            throw new SonetaException(e.Exception);
        }

        public Login Login(string databaseName, string user, string password)
        {
            
            if (busApplication != null)
            {
                this.database = busApplication[databaseName];
                if (this.database != null)
                {
                    this.login = this.database.Login(false, user, password);
                    return this.login;
                }
            }
            return null;
        }

        public void Logout()
        {
            if (login != null)
            {
                login.Dispose();
                login = null;
            }
        }

        public T CreateObject<T>(object enovaObject = null) where T : API.Types.IObjectBase
        {
            return (T)CreateObject(typeof(T), enovaObject);
        }

        public API.Types.IObjectBase CreateObject(Type type, object enovaObject = null, object[] args = null)
        {
            var attr = type.IsInterface ? TypeMapAttribute.GetByApiType(type) : TypeMapAttribute.GetByConnectorType(type);
            Type enovaType = null;
            API.Types.IObjectBase obj = null;
            if (attr != null)
            {
                enovaType = string.IsNullOrEmpty(attr.EnovaType) ? null : Type.GetType(attr.EnovaType);
                obj = (API.Types.IObjectBase)attr.ConnectorType.GetConstructor(new Type[0]).Invoke(new object[0]);
            }
                /*
            else
            {
                var attr2 = type.IsInterface ? Connector.RowMapAttribute.GetByApiType(type) : Connector.RowMapAttribute.GetByConnectorType(type);
                if (attr2 != null)
                {
                    enovaType = string.IsNullOrEmpty(attr2.EnovaType) ? null : Type.GetType(attr2.EnovaType);
                    obj = (API.Types.IObjectBase)attr2.ConnectorType.GetConstructor(new Type[0]).Invoke(new object[0]);
                }
            }
                 */
            if (enovaType != null && obj != null)
            {
                if (args != null && args.Length > 0)
                {
                    var typeArr = new Type[args.Length];
                    var argsArr = new object[args.Length];
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (args[i] is API.Types.IObjectBase)
                        {
                            typeArr[i] = ((API.Types.IObjectBase)args[i]).EnovaObject.GetType();
                            argsArr[i] = ((API.Types.IObjectBase)args[i]).EnovaObject;
                        }
                        else
                        {
                            typeArr[i] = args[i].GetType();
                            argsArr[i] = args[i];
                        }
                    }
                    obj.EnovaObject = enovaType.GetConstructor(typeArr).Invoke(argsArr);
                    return obj;
                }
                else
                {
                    if (enovaObject != null)
                        obj.EnovaObject = enovaObject;
                    else
                        obj.EnovaObject = enovaType.GetConstructor(new Type[0]).Invoke(new object[0]);
                    return obj;
                }
            }
            else
                throw new Exception("Brak lub źle zarejestrowana mapa dla typu " + type.FullName);
        }

        public object GetStaticValue(string enovaType, string name)
        {
            var t = Type.GetType(enovaType);
            if (t != null)
            {
                MemberInfo minfo = t.GetMember(name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Public | BindingFlags.Static).FirstOrDefault();
                return EnovaHelper.FromEnova(minfo.MemberType == MemberTypes.Property ? ((PropertyInfo)minfo).GetValue(null, null) : ((FieldInfo)minfo).GetValue(null));
            }
            return null;
        }

        public T FromEnova<T>(object obj)
        {
            return EnovaHelper.FromEnova<T>(obj);
        }

        public bool PingPong()
        {
            return true;
        }

        public void Finish()
        {
            Logout();
            Type.GetType("Soneta.Tools.CoreTools, Soneta.Types").GetMethod("FinishApplication").Invoke(null, null);
        }

        #endregion
    }
}
