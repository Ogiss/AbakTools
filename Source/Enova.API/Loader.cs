using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Enova.API
{
    public class Loader
    {
        // Fields
        private static Hashtable assemblies = new Hashtable();
        private static Dictionary<string, int> assemblyCounters = new Dictionary<string, int>();
        private static string assemblyPath;
        private static bool CommonAssembliesNoPermission = false;
        private static List<string> Errors = new List<string>();
        private Queue<string> extList = new Queue<string>();
        private Dictionary<string, string> extMap;
        private static bool loaded;
        private static List<string> loadedAssemblies = new List<string>();
        private ArrayList privateExtensions = new ArrayList();
        private ArrayList privateFileExtensions = new ArrayList();
        private LoadSettings settings = LoadSettings.Load();
        private static readonly char[] slashAndBack = new char[] { '/', '\\' };
        private bool withExtensions = true;

        // Methods
        static Loader()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Loader.ad_AssemblyResolve);
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(Loader.ad_AssemblyLoad);
            try
            {
                if (!Directory.Exists(CommonAssembliesPath))
                {
                    Directory.CreateDirectory(CommonAssembliesPath);
                }
            }
            catch (UnauthorizedAccessException)
            {
                CommonAssembliesNoPermission = true;
            }
        }

        public Loader()
        {
            this.calcAssemblyPath();
            foreach (string str in Environment.GetCommandLineArgs())
            {
                if (str.StartsWith("/ext="))
                {
                    if (this.extMap == null)
                    {
                        this.extMap = new Dictionary<string, string>();
                    }
                    string str2 = str.Substring(5).Trim();
                    if (!string.IsNullOrEmpty(str2))
                    {
                        if (str2.IndexOfAny(slashAndBack) >= 0)
                        {
                            this.extList.Enqueue(str2);
                        }
                        str2 = NoExt(str2).ToLower();
                        this.extMap[str2] = str2;
                    }
                }
                if (str.StartsWith("/extpath="))
                {
                    if (this.extMap == null)
                    {
                        this.extMap = new Dictionary<string, string>();
                    }
                    string[] files = Directory.GetFiles(str.Substring(9));
                    string item = "";
                    foreach (string str5 in files)
                    {
                        if (Path.GetExtension(str5) == ".dll")
                        {
                            item = str5;
                            if (item.IndexOfAny(slashAndBack) >= 0)
                            {
                                this.extList.Enqueue(item);
                            }
                            item = NoExt(item).ToLower();
                            this.extMap[item] = item;
                        }
                    }
                }
            }
        }

        private static void ad_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            string key = args.LoadedAssembly.FullName.Split(new char[] { ',' })[0];
            lock (assemblyCounters)
            {
                int num;
                assemblyCounters.TryGetValue(key, out num);
                assemblyCounters[key] = ++num;
            }
        }

        private static Assembly ad_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string str = args.Name.Split(new char[] { ',' })[0];
            if (str.EndsWith(".resources"))
            {
                if (Environment.Version.Major >= 4)
                {
                    return null;
                }
                str = str.Substring(0, str.Length - 10);
            }
            lock (assemblies)
            {
                return (Assembly)assemblies[str];
            }
        }

        public void AddPrivateExtension(string name)
        {
            this.privateExtensions.Add(name);
        }

        public void AddPrivateFileExtension(string name)
        {
            this.privateFileExtensions.Add(name);
        }

        private bool AllowedDll(string name)
        {
            if (this.extMap == null)
            {
                return true;
            }
            name = NoExt(name).ToLower();
            return this.extMap.ContainsKey(name);
        }

        private void calcAssemblyPath()
        {
            if (assemblyPath == null)
            {
                assemblyPath = getSonetaBusiness();
                if (assemblyPath == null)
                {
                    assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
                    if (!testPath(assemblyPath))
                    {
                        if (this.settings.Installations != null)
                        {
                            int length = this.settings.Installations.Length;
                            while (--length >= 0)
                            {
                                assemblyPath = this.settings.Installations[length];
                                if (testPath(assemblyPath))
                                {
                                    return;
                                }
                            }
                        }
                        throw new InvalidOperationException("Nie znaleziono instalacji programu enova.");
                    }
                }
            }
        }

        private string GetAttr(XmlNode node, string name)
        {
            XmlAttribute attribute = node.Attributes[name];
            if (attribute == null)
            {
                return null;
            }
            return attribute.Value;
        }

        public static IList<string> GetListOfErrors()
        {
            return Errors;
        }

        public static string[] GetLoadedExtensions()
        {
            lock (loadedAssemblies)
            {
                loadedAssemblies.Sort();
                return loadedAssemblies.ToArray();
            }
        }

        public static IEnumerable<string> GetMultipleLoadedAssemblies()
        {
            Queue<string> queue = new Queue<string>();
            lock (assemblyCounters)
            {
                foreach (KeyValuePair<string, int> pair in assemblyCounters)
                {
                    if (pair.Value > 1)
                    {
                        queue.Enqueue(pair.Key);
                    }
                }
            }
            return queue;
        }

        private static string getSonetaBusiness()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (string.Compare(assembly.GetName().Name, "Soneta.Business", true) == 0)
                {
                    string codeBase = assembly.CodeBase;
                    if ((codeBase != null) && codeBase.StartsWith("file:///", StringComparison.OrdinalIgnoreCase))
                    {
                        codeBase = codeBase.Substring(8);
                    }
                    else
                    {
                        codeBase = assembly.Location;
                    }
                    return Path.GetDirectoryName(codeBase);
                }
            }
            return null;
        }

        public void Load()
        {
            if (!loaded)
            {
                loaded = true;
                loadedAssemblies.Clear();
                XmlDocument document = new XmlDocument();
                document.Load(Path.Combine(assemblyPath, "Soneta.files.xml"));
                foreach (XmlNode node in document.SelectNodes("files/file"))
                {
                    string attr = this.GetAttr(node, "name");
                    bool flag = string.Equals(this.GetAttr(node, "ui"), "true", StringComparison.OrdinalIgnoreCase);
                    bool flag2 = string.Equals(this.GetAttr(node, "extra"), "true", StringComparison.OrdinalIgnoreCase);
                    bool flag3 = string.Equals(this.GetAttr(node, "net"), "true", StringComparison.OrdinalIgnoreCase);
                    bool conditional = string.Equals(this.GetAttr(node, "optional"), "true", StringComparison.OrdinalIgnoreCase);
                    bool withUI = !string.IsNullOrWhiteSpace(attr);
                    if (flag)
                    {
                        withUI = this.WithUI;
                    }
                    if (flag2)
                    {
                        withUI = this.WithExtra;
                    }
                    if (flag3)
                    {
                        withUI = this.WithNet;
                    }
                    if (withUI)
                    {
                        this.loadAssembly(attr, conditional, true);
                    }
                }
                foreach (string str2 in this.privateExtensions)
                {
                    this.loadAssemblyEx(str2, false, false);
                }
                foreach (string str3 in this.privateFileExtensions)
                {
                    this.loadFileAssembly(str3, false, false, true);
                }
                foreach (string str4 in this.extList)
                {
                    this.loadFileAssembly(str4, false, false, true);
                }
                if (!CommonAssembliesNoPermission && this.WithExtensions)
                {
                    foreach (string str5 in Directory.GetFiles(CommonAssembliesPath, "*.dll"))
                    {
                        bool flag6 = false;
                        try
                        {
                            AssemblyName.GetAssemblyName(str5);
                        }
                        catch (BadImageFormatException)
                        {
                            flag6 = true;
                        }
                        if (this.WithNet)
                        {
                            foreach (AssemblyName name in Assembly.ReflectionOnlyLoadFrom(str5).GetReferencedAssemblies())
                            {
                                if (name.FullName.Contains("System.Windows.Forms"))
                                {
                                    throw new Exception(string.Format("Ładowana biblioteka [{0}] zawiera referencje do biblioteki System.Windows.Forms.", Path.GetFileName(str5)));
                                }
                            }
                        }
                        if (!flag6)
                        {
                            this.loadFileAssembly(str5, false, false, true);
                        }
                    }
                }
                Type type = Type.GetType("Soneta.Tools.AssemblyAttributes, Soneta.Types", false);
                if (type != null)
                {
                    PropertyInfo property = type.GetProperty("IsAssembliesLoaded");
                    if (property != null)
                    {
                        property.SetValue(null, true, null);
                    }
                }
            }
        }

        private void loadAssembly(string name, bool conditional, bool checkKey)
        {
            if ((name.IndexOf('/') < 0) && (name.IndexOf('\\') < 0))
            {
                name = name + ".dll";
                name = Path.Combine(assemblyPath, name);
            }
            this.loadFileAssembly(name, conditional, checkKey, false);
        }

        private void loadAssemblyEx(string name, bool conditional, bool checkKey)
        {
            if ((name.IndexOf('/') < 0) && (name.IndexOf('\\') < 0))
            {
                name = name + ".dll";
                name = Path.Combine(assemblyPath, name);
            }
            this.loadFileAssembly(name, conditional, checkKey, true);
        }

        private void loadFileAssembly(string name, bool conditional, bool checkKey, bool extension)
        {
            if ((!conditional || File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name))) && (!extension || this.AllowedDll(name)))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(name);
                    if (checkKey)
                    {
                        byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
                        if ((((publicKeyToken == null) || (publicKeyToken.Length != 8)) || ((publicKeyToken[0] != 0xa1) || (publicKeyToken[1] != 0x9f))) || ((((publicKeyToken[2] != 0xc6) || (publicKeyToken[3] != 0x23)) || ((publicKeyToken[4] != 0x6f) || (publicKeyToken[5] != 0xd3))) || ((publicKeyToken[6] != 0x43) || (publicKeyToken[7] != 0x93))))
                        {
                            throw new InvalidOperationException("Błąd wczytywania biblioteki '" + name + "'. Skontaktuj się z administratorem programu.");
                        }
                    }
                    lock (assemblies)
                    {
                        string str2 = assembly.GetName().Name;
                        if (extension)
                        {
                            lock (loadedAssemblies)
                            {
                                loadedAssemblies.Add(str2.ToLower());
                            }
                        }
                        assemblies[str2] = assembly;
                    }
                }
                catch (FileLoadException exception)
                {
                    if (exception.InnerException.ToString().Contains("CAS Policy"))
                    {
                        Errors.Add("Błąd wczytywania biblioteki '" + name + "'. Prawdopodobnie biblioteka nie pochodzi z zaufanego źr\x00f3dła. Proszę ją odblokować z poziomu waściwości pliku dll.");
                    }
                    else
                    {
                        Errors.Add("Błąd wczytywania biblioteki '" + name + "'. Biblioteka może być uszkodzona i nie została załadowana. Skontaktuj się z administratorem programu.");
                    }
                }
                catch (Exception)
                {
                    Errors.Add("Błąd wczytywania biblioteki '" + name + "'. Biblioteka może być uszkodzona i nie została załadowana. Skontaktuj się z administratorem programu.");
                }
            }
        }

        private static string NoExt(string fn)
        {
            fn = Path.GetFileName(fn);
            if (fn.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            {
                fn = fn.Substring(0, fn.Length - 4);
            }
            return fn;
        }

        public void Run()
        {
            object application = this.Application;
            application.GetType().GetMethod("Run").Invoke(application, null);
        }

        private static bool testPath(string path)
        {
            return File.Exists(Path.Combine(path, "Soneta.Business.dll"));
        }

        public void UseCurrentDllsAsInstallation(bool standard)
        {
            string strB = getSonetaBusiness();
            ArrayList list = new ArrayList();
            bool flag = false;
            if (this.settings.Installations != null)
            {
                foreach (string str2 in this.settings.Installations)
                {
                    if (testPath(str2))
                    {
                        list.Add(str2);
                    }
                    if (string.Compare(str2, strB, true) == 0)
                    {
                        flag = true;
                    }
                }
                if ((list.Count == this.settings.Installations.Length) && flag)
                {
                    return;
                }
            }
            if (!flag)
            {
                list.Add(strB);
            }
            this.settings.Installations = (string[])list.ToArray(typeof(string));
            this.settings.Save();
        }

        // Properties
        public object Application
        {
            get
            {
                if (!this.WithUI)
                {
                    throw new InvalidOperationException("Do uruchomienia aplikacji wymagane jest załadowanie interface'u użytkownika. Właściwość: Loader.WithUI.");
                }
                this.Load();
                return Type.GetType("Soneta.Business.Forms.Core.Application,Soneta.Business.Forms", true).GetField("Instance").GetValue(null);
            }
        }

        public static string AssembliesPath
        {
            get
            {
                if (assemblyPath == null)
                {
                    new Loader().calcAssemblyPath();
                }
                return assemblyPath;
            }
            set
            {
                assemblyPath = value;
            }
        }

        public static string CommonAssembliesPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles), "Soneta/Assemblies");
            }
        }

        public bool WithExtensions
        {
            get
            {
                return this.withExtensions;
            }
            set
            {
                this.withExtensions = value;
            }
        }

        public bool WithExtra { get; set; }

        public bool WithNet { get; set; }

        public bool WithUI { get; set; }
    }
}
