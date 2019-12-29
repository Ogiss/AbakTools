using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Xml;
using System.Threading;
using System.Resources;
using System.IO;

namespace Enova.Old.Tools
{
    public static class LangTranslator
    {
        // Fields
        private static List<Assembly> m_Assemblies = new List<Assembly>();
        private static readonly string m_inheritName = "##Inheritances##";
        private static List<string> m_Languages = new List<string>();
        private static bool m_resLoaded = false;
        private static Dictionary<string, Dictionary<string, Dictionary<string, LangProperties>>> m_Resources = new Dictionary<string, Dictionary<string, Dictionary<string, LangProperties>>>();

        // Methods
        public static void AddClassCaption(PropertyDescriptor pd, string lang, string value)
        {
            string className = string.Empty;
            string propName = string.Empty;
            if (TryFindResourcePath(pd, ref className, ref propName))
            {
                int length = className.LastIndexOf('.');
                if (length != -1)
                {
                    string moduleName = className.Substring(0, length);
                    string name = className.Remove(0, length + 1);
                    string resInfo = ResourceInfo(moduleName, lang);
                    LangProperties lp = null;
                    if (!FindResource(resInfo, LangType.Class, name, ref lp))
                    {
                        lp = new LangProperties();
                    }
                    lp.AddItem(PropertyType.Caption, propName, value);
                    AddResource(resInfo, LangType.Class, name, lp);
                }
            }
        }

        public static void AddClassCaption(string path, string name, string lang, string value)
        {
            int length = path.LastIndexOf('.');
            if (length != -1)
            {
                string moduleName = path.Substring(0, length);
                string str2 = path.Remove(0, length + 1);
                string resInfo = ResourceInfo(moduleName, lang);
                LangProperties lp = null;
                if (!FindResource(resInfo, LangType.Class, str2, ref lp))
                {
                    lp = new LangProperties();
                }
                lp.AddItem(PropertyType.Caption, name, value);
                AddResource(resInfo, LangType.Class, str2, lp);
            }
        }

        private static void AddInheritances(string resInfo, LangType lang, string name, XmlNode xNodeParent)
        {
            XmlNode node = xNodeParent.SelectSingleNode("Inheritances");
            if ((node != null) && (node.ChildNodes.Count != 0))
            {
                LangProperties lp = null;
                if (!FindResource(resInfo, lang, m_inheritName, ref lp))
                {
                    lp = new LangProperties();
                }
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    string str = node2.Attributes["name"].Value;
                    if (str.Length != 0)
                    {
                        lp.AddItem(PropertyType.Inherit, str, name);
                    }
                }
                AddResource(resInfo, lang, m_inheritName, lp);
            }
        }

        private static void AddLanguage(string lang)
        {
            if (!m_Languages.Contains(lang))
            {
                m_Languages.Add(lang);
            }
        }

        private static void AddProperties(string resInfo, LangType lang, PropertyType[] attribs, XmlNodeList xNodes)
        {
            foreach (XmlNode node in xNodes)
            {
                string name = node.Attributes["name"].Value;
                if (name.Length != 0)
                {
                    LangProperties lp = null;
                    if (!FindResource(resInfo, lang, name, ref lp))
                    {
                        lp = new LangProperties();
                    }
                    AddInheritances(resInfo, lang, name, node);
                    foreach (XmlNode node2 in node.SelectNodes("Property"))
                    {
                        string str2 = node2.Attributes["name"].Value;
                        if (str2.Length != 0)
                        {
                            foreach (PropertyType type in attribs)
                            {
                                XmlNode namedItem = node2.Attributes.GetNamedItem(type.ToString().ToLower());
                                if (namedItem != null)
                                {
                                    lp.AddItem(type, str2, namedItem.Value);
                                }
                            }
                        }
                    }
                    AddResource(resInfo, lang, name, lp);
                }
            }
        }

        private static void AddResource(string resInfo, LangType lang, string name, LangProperties lp)
        {
            Dictionary<string, Dictionary<string, LangProperties>> dictionary;
            Dictionary<string, LangProperties> dictionary2;
            LangProperties properties = null;
            if (m_Resources.TryGetValue(resInfo, out dictionary))
            {
                if (dictionary.TryGetValue(lang.ToString(), out dictionary2))
                {
                    if (dictionary2.TryGetValue(name, out properties))
                    {
                        dictionary2[name] = lp;
                    }
                    else
                    {
                        dictionary2.Add(name, lp);
                    }
                }
                else
                {
                    dictionary2 = new Dictionary<string, LangProperties>();
                    dictionary2.Add(name, lp);
                    dictionary.Add(lang.ToString(), dictionary2);
                }
            }
            else
            {
                dictionary2 = new Dictionary<string, LangProperties>();
                dictionary2.Add(name, lp);
                dictionary = new Dictionary<string, Dictionary<string, LangProperties>>();
                dictionary.Add(lang.ToString(), dictionary2);
                m_Resources.Add(resInfo, dictionary);
            }
        }

        private static bool FindResource(string resInfo, LangType lang, string name, ref LangProperties lp)
        {
            Dictionary<string, Dictionary<string, LangProperties>> dictionary;
            Dictionary<string, LangProperties> dictionary2;
            return ((m_Resources.TryGetValue(resInfo, out dictionary) && dictionary.TryGetValue(lang.ToString(), out dictionary2)) && dictionary2.TryGetValue(name, out lp));
        }

        public static string GetAction(PropertyDescriptor pd)
        {
            string className = string.Empty;
            string propName = string.Empty;
            if (!TryFindResourcePath(pd, ref className, ref propName))
            {
                return string.Empty;
            }
            LangProperties langProperties = GetLangProperties(className, LangType.Action);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Caption, propName);
        }

        public static string GetAction(string path, string name)
        {
            LangProperties langProperties = GetLangProperties(path, LangType.Action);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Caption, name);
        }

        public static string GetActionDescription(PropertyDescriptor pd)
        {
            string className = string.Empty;
            string propName = string.Empty;
            if (!TryFindResourcePath(pd, ref className, ref propName))
            {
                return string.Empty;
            }
            LangProperties langProperties = GetLangProperties(className, LangType.Action);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Description, propName);
        }

        public static string GetActionDescription(string path, string name)
        {
            LangProperties langProperties = GetLangProperties(path, LangType.Action);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Description, name);
        }

        public static string GetCategory(PropertyDescriptor pd)
        {
            string className = string.Empty;
            string propName = string.Empty;
            if (!TryFindResourcePath(pd, ref className, ref propName))
            {
                return string.Empty;
            }
            LangProperties langProperties = GetLangProperties(className, LangType.Class);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Category, propName);
        }

        public static string GetCategory(string path, string name)
        {
            LangProperties langProperties = GetLangProperties(path, LangType.Class);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Category, name);
        }

        public static string GetDescription(PropertyDescriptor pd)
        {
            string className = string.Empty;
            string propName = string.Empty;
            if (!TryFindResourcePath(pd, ref className, ref propName))
            {
                return string.Empty;
            }
            LangProperties langProperties = GetLangProperties(className, LangType.Class);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Description, propName);
        }

        public static string GetDescription(string path, string name)
        {
            LangProperties langProperties = GetLangProperties(path, LangType.Class);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Description, name);
        }

        public static string GetEnum(string path, string name)
        {
            LangProperties langProperties = GetLangProperties(path, LangType.Enum);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Caption, name);
        }

        public static Dictionary<string, string> GetEnumList(PropertyDescriptor pd)
        {
            string className = string.Empty;
            string propName = string.Empty;
            if (!TryFindResourcePath(pd, ref className, ref propName))
            {
                return null;
            }
            LangProperties langProperties = GetLangProperties(className, LangType.Enum);
            if (langProperties == null)
            {
                return null;
            }
            return langProperties.GetItems(PropertyType.Caption);
        }

        public static Dictionary<string, string> GetEnumList(string path)
        {
            LangProperties langProperties = GetLangProperties(path, LangType.Enum);
            if (langProperties == null)
            {
                return null;
            }
            return langProperties.GetItems(PropertyType.Caption);
        }

        public static string GetEnumText(FieldInfo fi)
        {
            string enumName = string.Empty;
            string valueName = string.Empty;
            if (!IsActive)
            {
                return valueName;
            }
            if (!TryFindResourcePath(fi, ref enumName, ref valueName))
            {
                return string.Empty;
            }
            LangProperties langProperties = GetLangProperties(enumName, LangType.Enum);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Caption, valueName);
        }

        public static string GetException(PropertyDescriptor pd)
        {
            string className = string.Empty;
            string propName = string.Empty;
            if (!TryFindResourcePath(pd, ref className, ref propName))
            {
                return string.Empty;
            }
            LangProperties langProperties = GetLangProperties(className, LangType.Exception);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Caption, propName);
        }

        public static string GetException(string path, string name)
        {
            LangProperties langProperties = GetLangProperties(path, LangType.Exception);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Caption, name);
        }

        private static LangProperties GetLangProperties(string path, LangType lang)
        {
            LangProperties properties = null;
            int length = path.LastIndexOf('.');
            if (length != -1)
            {
                Dictionary<string, Dictionary<string, LangProperties>> dictionary;
                Dictionary<string, LangProperties> dictionary2;
                string moduleName = path.Substring(0, length);
                string key = path.Remove(0, length + 1);
                string str3 = ResourceInfo(moduleName, Thread.CurrentThread.CurrentCulture.Name);
                LoadResources();
                if ((m_Resources.TryGetValue(str3, out dictionary) && dictionary.TryGetValue(lang.ToString(), out dictionary2)) && !dictionary2.TryGetValue(key, out properties))
                {
                    LangProperties properties2 = null;
                    if (dictionary2.TryGetValue(m_inheritName, out properties2))
                    {
                        key = properties2.GetItem(PropertyType.Inherit, key);
                        if (key.Length != 0)
                        {
                            dictionary2.TryGetValue(key, out properties);
                        }
                    }
                }
            }
            return properties;
        }

        public static string GetLocalizedResource(string path)
        {
            string str = path;
            string twoLetterISOLanguageName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if ((path == null) || (!path.StartsWith("@") && !path.StartsWith("$")))
            {
                return path;
            }
            if (path.StartsWith("$") && path.Contains(twoLetterISOLanguageName + ":"))
            {
                foreach (string str3 in path.Replace("$", "").Split(new char[] { ';' }))
                {
                    if (str3.StartsWith(twoLetterISOLanguageName + ":"))
                    {
                        return str3.Replace(twoLetterISOLanguageName + ":", "");
                    }
                }
                return path;
            }
            string[] strArray2 = path.Split(new char[] { '\\' });
            if (strArray2.Length != 3)
            {
                return path;
            }
            string str4 = strArray2[0].Replace("@", "");
            string str5 = strArray2[1];
            string name = strArray2[2];
            try
            {
                AppDomain.CurrentDomain.GetAssemblies();
                Assembly assembly = null;
                foreach (Assembly assembly2 in AssemblyAttributes.GetAssemblies())
                {
                    if (assembly2.GetName().Name.ToLower() == str4.ToLower())
                    {
                        assembly = assembly2;
                        break;
                    }
                }
                if (assembly == null)
                {
                    return path;
                }
                string str8 = new ResourceManager(string.Format("{0}.{1}", str4, str5), assembly).GetString(name);
                str = string.IsNullOrEmpty(str8) ? path : str8;
            }
            catch
            {
            }
            return str;
        }

        public static bool GetLocalizedResource(string path, out string value)
        {
            bool flag = false;
            value = string.Empty;
            string localizedResource = GetLocalizedResource(path);
            if (!string.IsNullOrEmpty(localizedResource) && (localizedResource != path))
            {
                value = localizedResource;
                flag = true;
            }
            return flag;
        }

        public static string GetText(PropertyDescriptor pd)
        {
            string className = string.Empty;
            string propName = string.Empty;
            if (!TryFindResourcePath(pd, ref className, ref propName))
            {
                return string.Empty;
            }
            LangProperties langProperties = GetLangProperties(className, LangType.Class);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Caption, propName);
        }

        public static string GetText(string path, string name)
        {
            LangProperties langProperties = GetLangProperties(path, LangType.Class);
            if (langProperties == null)
            {
                return string.Empty;
            }
            return langProperties.GetItem(PropertyType.Caption, name);
        }

        public static void Initialize(Assembly asm)
        {
            if ((IsCorrectAssembly(asm) && (asm != null)) && !m_Assemblies.Contains(asm))
            {
                m_Assemblies.Add(asm);
            }
        }

        private static bool IsCorrectAssembly(Assembly ass)
        {
            bool flag = ass.GetName().FullName.StartsWith("Soneta.Business");
            foreach (AssemblyName name in ass.GetReferencedAssemblies())
            {
                if (name.FullName.Contains("Soneta.Business"))
                {
                    return true;
                }
            }
            return flag;
        }

        private static bool LoadResource(Stream stream)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(stream);
                XmlElement documentElement = document.DocumentElement;
                if (documentElement == null)
                {
                    return false;
                }
                foreach (XmlNode node in documentElement.SelectNodes("/Modules/Module"))
                {
                    string moduleName = node.Attributes["name"].Value;
                    string langName = node.Attributes["lang"].Value;
                    if ((moduleName.Length != 0) && (langName.Length != 0))
                    {
                        string resInfo = ResourceInfo(moduleName, langName);
                        AddLanguage(langName);
                        XmlNodeList xNodes = node.SelectNodes("Classes/Class");
                        PropertyType[] typeArray5 = new PropertyType[3];
                        typeArray5[1] = PropertyType.Description;
                        typeArray5[2] = PropertyType.Category;
                        PropertyType[] attribs = typeArray5;
                        AddProperties(resInfo, LangType.Class, attribs, xNodes);
                        xNodes = node.SelectNodes("Enums/Enum");
                        PropertyType[] typeArray2 = new PropertyType[1];
                        AddProperties(resInfo, LangType.Enum, typeArray2, xNodes);
                        xNodes = node.SelectNodes("Exceptions/Exception");
                        PropertyType[] typeArray3 = new PropertyType[1];
                        AddProperties(resInfo, LangType.Exception, typeArray3, xNodes);
                        xNodes = node.SelectNodes("Actions/Action");
                        PropertyType[] typeArray8 = new PropertyType[2];
                        typeArray8[1] = PropertyType.Description;
                        PropertyType[] typeArray4 = typeArray8;
                        AddProperties(resInfo, LangType.Action, typeArray4, xNodes);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static bool LoadResources()
        {
            if (m_resLoaded)
            {
                return true;
            }
            bool flag = true;
            foreach (Assembly assembly in m_Assemblies)
            {
                string str = ResourceInfo(assembly.GetName().Name, "Localization");
                foreach (string str2 in assembly.GetManifestResourceNames())
                {
                    if (str2.StartsWith(str))
                    {
                        Stream manifestResourceStream = assembly.GetManifestResourceStream(str2);
                        if (manifestResourceStream == null)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag &= LoadResource(manifestResourceStream);
                        }
                    }
                }
            }
            m_resLoaded = true;
            return flag;
        }

        private static string ResourceInfo(string moduleName, string langName)
        {
            return string.Format("{0}.{1}", moduleName, langName);
        }

        private static bool TryFindResourcePath(PropertyDescriptor pd, ref string className, ref string propName)
        {
            className = string.Empty;
            propName = string.Empty;
            if (pd == null)
            {
                return false;
            }
            Type componentType = pd.ComponentType;
            string[] strArray = pd.Name.Split(new char[] { '.' });
            propName = strArray[strArray.Length - 1];
            for (byte i = 0; i < (strArray.Length - 1); i = (byte)(i + 1))
            {
                componentType = componentType.GetProperty(strArray[i]).PropertyType;
            }
            className = componentType.FullName;
            return true;
        }

        private static bool TryFindResourcePath(FieldInfo fi, ref string enumName, ref string valueName)
        {
            enumName = string.Empty;
            valueName = string.Empty;
            if (fi == null)
            {
                return false;
            }
            enumName = fi.DeclaringType.FullName;
            valueName = fi.Name;
            return true;
        }

        // Properties
        public static bool IsActive
        {
            get
            {
                return (Thread.CurrentThread.CurrentCulture.Name != "pl-PL");
            }
        }

        // Nested Types
        private class LangProperties
        {
            // Fields
            private Dictionary<string, Dictionary<string, string>> m_dict = new Dictionary<string, Dictionary<string, string>>();

            // Methods
            public void AddItem(LangTranslator.PropertyType type, string name, string value)
            {
                Dictionary<string, string> dictionary;
                string str = string.Empty;
                if (!this.m_dict.TryGetValue(name, out dictionary))
                {
                    dictionary = new Dictionary<string, string>();
                    dictionary.Add(type.ToString(), value);
                    this.m_dict.Add(name, dictionary);
                }
                else if (!dictionary.TryGetValue(type.ToString(), out str))
                {
                    dictionary.Add(type.ToString(), value);
                }
                else
                {
                    dictionary[type.ToString()] = value;
                }
            }

            public string GetItem(LangTranslator.PropertyType type, string name)
            {
                if (!this.m_dict.ContainsKey(name))
                {
                    return string.Empty;
                }
                Dictionary<string, string> dictionary = this.m_dict[name];
                if (!dictionary.ContainsKey(type.ToString()))
                {
                    return string.Empty;
                }
                return dictionary[type.ToString()];
            }

            public Dictionary<string, string> GetItems(LangTranslator.PropertyType type)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                string key = type.ToString();
                foreach (string str2 in this.m_dict.Keys)
                {
                    Dictionary<string, string> dictionary2 = this.m_dict[str2];
                    if (dictionary2.ContainsKey(key))
                    {
                        dictionary.Add(str2, dictionary2[key]);
                    }
                }
                return dictionary;
            }
        }

        private enum LangType
        {
            Class,
            Enum,
            Exception,
            Action
        }

        private enum PropertyType
        {
            Caption,
            Description,
            Category,
            Inherit
        }
    }
}
