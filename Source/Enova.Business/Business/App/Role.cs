using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Enova.Old.Types;
using Enova.Old.Tools;

namespace Enova.Business.Old.App
{
    /*
    [Obsolete("Brak kodu")]
    [RightsSource, Caption("Rola"), XmlRoot("Role")]
    public class Role : IAttributeFilter, IComparable, ICloneable
    {
        // Fields
        [XmlAttribute]
        public AccessRights AccessRight;
        [XmlElement]
        public AssemblyPermission[] AssemblyPermissions;
        public bool ClearPermissions;
        public static readonly Role[] EmptyArray = new Role[0];
        public static readonly Role FullAccess = new Role(true);
        private static LinkedList<IIdentAttribute> globalIdents;
        private static object globalIdentsLock = new object();
        [XmlAttribute]
        public Guid Guid;
        [XmlIgnore]
        public LicencjaProgramu[] LicModules;
        [XmlElement]
        public ModulePermission[] ModulePermissions;
        public static readonly Dictionary<string, string> oldFoldersMap = new Dictionary<string, string>();
        public static readonly string[] oldFoldersTable = new string[] { 
        "Faktury/Cennik", "Faktury/Towary i usługi", "Faktury/Sprzedaż", "Faktury/Sprzedaż/Faktury sprzedaży", "Faktury/Sprzedaż wg towar\x00f3w", "Faktury/Zestawienia/Sprzedaż wg towar\x00f3w", "Faktury/Umowy i urządzenia", "Faktury/Sprzedaż/Umowy", "Faktury/Umowy i urządzenia/Faktury", "Faktury/Sprzedaż/Umowy/Faktury do um\x00f3w", "Faktury/Umowy i urządzenia/Umowy", "Faktury/Sprzedaż/Umowy/Umowy", "Faktury/Umowy i urządzenia/Urządzenia", "Faktury/Sprzedaż/Umowy/Urządzenia", "Handel/Cennik", "Handel/Towary i usługi", 
        "Handel/Dane analityczne", "Handel/Magazyn/Rozch\x00f3d zasob\x00f3w", "Handel/Dane analityczne", "Handel/Dokumenty razem i pozostałe/Pozycje dokument\x00f3w", "Handel/Dane analityczne", "Handel/Magazyn/Zasoby", "Handel/Dane analityczne/Obroty", "Handel/Magazyn/Rozch\x00f3d zasob\x00f3w", "Handel/Dane analityczne/Pozycje", "Handel/Dokumenty razem i pozostałe/Pozycje dokument\x00f3w", "Handel/Dane analityczne/Zasoby", "Handel/Magazyn/Zasoby", "Handel/Faktury", "Handel/Sprzedaż/Faktury sprzedaży", "Handel/Faktury", "Handel/Zakup/Faktury wewnętrzne", 
        "Handel/Faktury", "Handel/Zakup/Faktury zakupu", "Handel/Faktury", "Handel/Sprzedaż/Ceny i rabaty/Grupy cenowe i rabatowe", "Handel/Faktury", "Handel/Sprzedaż/Ceny i rabaty/Przeceny okresowe", "Handel/Faktury", "Handel/Zestawienia/Sprzedaż wg towar\x00f3w", "Handel/Faktury/Faktury sprzedaży", "Handel/Sprzedaż/Faktury sprzedaży", "Handel/Faktury/Faktury wewnętrzne", "Handel/Zakup/Faktury wewnętrzne", "Handel/Faktury/Faktury zakupu", "Handel/Zakup/Faktury zakupu", "Handel/Faktury/Grupy cenowe i rabatowe", "Handel/Sprzedaż/Ceny i rabaty/Grupy cenowe i rabatowe", 
        "Handel/Faktury/Grupy cenowe i rabatowe", "Handel/Sprzedaż/Ceny i rabaty/Przeceny okresowe", "Handel/Faktury/Grupy cenowe i rabatowe/Grupy cenowe i rabatowe", "Handel/Sprzedaż/Ceny i rabaty/Grupy cenowe i rabatowe", "Handel/Faktury/Grupy cenowe i rabatowe/Przeceny okresowe", "Handel/Sprzedaż/Ceny i rabaty/Przeceny okresowe", "Handel/Faktury/Sprzedaż wg towar\x00f3w", "Handel/Zestawienia/Sprzedaż wg towar\x00f3w", "Handel/Magazyn", "Handel/Zestawienia/Obroty wg dokument\x00f3w", "Handel/Magazyn", "Handel/Zestawienia/Obroty wg kontrahent\x00f3w", "Handel/Magazyn", "Handel/Zestawienia/Obroty wg opiekun\x00f3w", "Handel/Magazyn", "Handel/Zestawienia/Obroty wg towar\x00f3w", 
        "Handel/Magazyn/Dokumenty wg kategorii/Inwentaryzacje", "Handel/Magazyn/Dokumenty wg kategorii/Inwentaryzacja", "Handel/Magazyn/Dokumenty wg kategorii/Kompletacje", "Handel/Magazyn/Dokumenty wg kategorii/Kompletacja", "Handel/Magazyn/Lista dokument\x00f3w", "Handel/Magazyn/Dokumenty razem", "Handel/Magazyn/Obroty wg dokument\x00f3w", "Handel/Zestawienia/Obroty wg dokument\x00f3w", "Handel/Magazyn/Obroty wg kontrahent\x00f3w", "Handel/Zestawienia/Obroty wg kontrahent\x00f3w", "Handel/Magazyn/Obroty wg opiekun\x00f3w", "Handel/Zestawienia/Obroty wg opiekun\x00f3w", "Handel/Magazyn/Obroty wg towar\x00f3w", "Handel/Zestawienia/Obroty wg towar\x00f3w", "Handel/Magazyn/Opakowania", "Handel/Magazyn/Dokumenty wg kategorii/Opakowania razem", 
        "Handel/Oferty i zam\x00f3wienia", "Handel/Sprzedaż/Ceny i rabaty/Cennik kontrahenta", "Handel/Oferty i zam\x00f3wienia", "Handel/Sprzedaż/Oferty do odbiorc\x00f3w", "Handel/Oferty i zam\x00f3wienia", "Handel/Zakup/Oferty od dostawc\x00f3w", "Handel/Oferty i zam\x00f3wienia", "Handel/Zestawienia/Oferty wg towar\x00f3w", "Handel/Oferty i zam\x00f3wienia", "Handel/Zakup/Zam\x00f3wienia do dostawc\x00f3w", "Handel/Oferty i zam\x00f3wienia", "Handel/Sprzedaż/Zam\x00f3wienia od odbiorc\x00f3w", "Handel/Oferty i zam\x00f3wienia", "Handel/Zestawienia/Zam\x00f3wienia wg towar\x00f3w", "Handel/Oferty i zam\x00f3wienia/Cennik kontrahenta", "Handel/Sprzedaż/Ceny i rabaty/Cennik kontrahenta", 
        "Handel/Oferty i zam\x00f3wienia/Oferty do odbiorcy", "Handel/Sprzedaż/Oferty do odbiorc\x00f3w", "Handel/Oferty i zam\x00f3wienia/Oferty od dostawcy", "Handel/Zakup/Oferty od dostawc\x00f3w", "Handel/Oferty i zam\x00f3wienia/Oferty wg towar\x00f3w", "Handel/Zestawienia/Oferty wg towar\x00f3w", "Handel/Oferty i zam\x00f3wienia/Zam\x00f3wienia do dostawcy", "Handel/Zakup/Zam\x00f3wienia do dostawc\x00f3w", "Handel/Oferty i zam\x00f3wienia/Zam\x00f3wienia od odbiorcy", "Handel/Sprzedaż/Zam\x00f3wienia od odbiorc\x00f3w", "Handel/Oferty i zam\x00f3wienia/Zam\x00f3wienia wg towar\x00f3w", "Handel/Zestawienia/Zam\x00f3wienia wg towar\x00f3w", "Handel/Pozostałe", "Handel/Dokumenty razem i pozostałe", "Handel/Pozostałe/Pozostałe", "Handel/Dokumenty razem i pozostałe/Pozostałe", 
        "Handel/Pozostałe/Wewnętrzne", "Handel/Dokumenty razem i pozostałe/Wewnętrzne", "Handel/Pozostałe/Wszystkie dokumenty", "Handel/Dokumenty razem i pozostałe/Wszystkie dokumenty", "Handel/Umowy i urządzenia", "Handel/Sprzedaż/Umowy", "Handel/Umowy i urządzenia/Faktury", "Handel/Sprzedaż/Umowy/Faktury do um\x00f3w", "Handel/Umowy i urządzenia/Umowy", "Handel/Sprzedaż/Umowy/Umowy", "Handel/Umowy i urządzenia/Urządzenia", "Handel/Sprzedaż/Umowy/Urządzenia", "Ewidencja Środk\x00f3w Pieniężnych/Rozrachunki/Wg dokument\x00f3w", "Ewidencja Środk\x00f3w Pieniężnych/Rozrachunki wg dokument\x00f3w", "Ewidencja Środk\x00f3w Pieniężnych/Rozrachunki/Wg kontrahent\x00f3w", "Ewidencja Środk\x00f3w Pieniężnych/Rozrachunki wg kontrahent\x00f3w", 
        "Ewidencja Środk\x00f3w Pieniężnych/Rozrachunki", "Ewidencja Środk\x00f3w Pieniężnych/Rozrachunki wg dokument\x00f3w", "Ewidencja Środk\x00f3w Pieniężnych/Rozrachunki", "Ewidencja Środk\x00f3w Pieniężnych/Rozrachunki wg kontrahent\x00f3w", "Księgowość/Rozrachunki/Wg dokument\x00f3w", "Księgowość/Rozrachunki wg dokument\x00f3w", "Księgowość/Rozrachunki/Wg kontrahent\x00f3w", "Księgowość/Rozrachunki wg kontrahent\x00f3w", "Księgowość/Rozrachunki", "Księgowość/Rozrachunki wg dokument\x00f3w", "Księgowość/Rozrachunki", "Księgowość/Rozrachunki wg kontrahent\x00f3w", "Ewidencja dokument\x00f3w/Ewidencja akcyzy/Deklaracja AKC-2", "Ewidencja dokument\x00f3w/Deklaracja AKC-2", "Ewidencja dokument\x00f3w/Ewidencja akcyzy/Dokumenty wg element\x00f3w", "Ewidencja dokument\x00f3w/Dokumenty wg element\x00f3w", 
        "Ewidencja dokument\x00f3w/Ewidencja akcyzy/Dokumenty wg nagł\x00f3wk\x00f3w", "Ewidencja dokument\x00f3w/Dokumenty wg nagł\x00f3wk\x00f3w", "Ewidencja dokument\x00f3w/Ewidencja akcyzy/Rejestry wg element\x00f3w", "Ewidencja dokument\x00f3w/Rejestry wg element\x00f3w", "Ewidencja dokument\x00f3w/Ewidencja akcyzy/Rejestry wg nagł\x00f3wk\x00f3w", "Ewidencja dokument\x00f3w/Rejestry wg nagł\x00f3wk\x00f3w", "Ewidencja dokument\x00f3w/Ewidencja akcyzy", "Ewidencja dokument\x00f3w/Dokumenty wg element\x00f3w", "Ewidencja dokument\x00f3w/Ewidencja akcyzy", "Ewidencja dokument\x00f3w/Dokumenty wg nagł\x00f3wk\x00f3w", "Ewidencja dokument\x00f3w/Ewidencja akcyzy", "Ewidencja dokument\x00f3w/Rejestry wg element\x00f3w", "Ewidencja dokument\x00f3w/Ewidencja akcyzy", "Ewidencja dokument\x00f3w/Rejestry wg nagł\x00f3wk\x00f3w", "Ewidencja dokument\x00f3w/Ewidencja akcyzy", "Ewidencja dokument\x00f3w/Deklaracja AKC-2", 
        "Ewidencja dokument\x00f3w/Ewidencja VAT/Ewidencja VAT", "Ewidencja dokument\x00f3w/Ewidencja dokument\x00f3w VAT", "Ewidencja dokument\x00f3w/Ewidencja VAT/Rejestr VAT", "Ewidencja dokument\x00f3w/Rejestr VAT", "Ewidencja dokument\x00f3w/Ewidencja VAT/Deklaracja VAT-7", "Ewidencja dokument\x00f3w/Deklaracja VAT-7", "Ewidencja dokument\x00f3w/Ewidencja VAT/Deklaracja VAT-UE", "Ewidencja dokument\x00f3w/Deklaracja VAT-UE", "Ewidencja dokument\x00f3w/Ewidencja VAT", "Ewidencja dokument\x00f3w/Ewidencja dokument\x00f3w VAT", "Ewidencja dokument\x00f3w/Ewidencja VAT", "Ewidencja dokument\x00f3w/Rejestr VAT", "Ewidencja dokument\x00f3w/Ewidencja VAT", "Ewidencja dokument\x00f3w/Deklaracja VAT-7", "Ewidencja dokument\x00f3w/Ewidencja VAT", "Ewidencja dokument\x00f3w/Deklaracja VAT-UE"
     };
        private static readonly XmlReflectionSerializer serializer = new XmlReflectionSerializer(typeof(Role));
        private Hashtable types;

        // Methods
        static Role()
        {
            for (int i = 0; i < oldFoldersTable.Length; i += 2)
            {
                string str;
                if (oldFoldersMap.TryGetValue(oldFoldersTable[i], out str))
                {
                    oldFoldersMap[oldFoldersTable[i]] = str + ';' + oldFoldersTable[i + 1];
                }
                else
                {
                    oldFoldersMap[oldFoldersTable[i]] = oldFoldersTable[i + 1];
                }
            }
        }

        public Role()
        {
            this.Guid = Guid.Empty;
            this.AccessRight = AccessRights.Granted;
            this.ClearPermissions = true;
        }

        private Role(bool temp)
        {
            this.Guid = Guid.Empty;
            this.AccessRight = AccessRights.Granted;
            this.ClearPermissions = true;
            this.types = new Hashtable();
        }

        private Role(string name, Guid guid, ModulePermission[] modulePermissions, AssemblyPermission[] assemblyPermissions)
        {
            this.Guid = Guid.Empty;
            this.AccessRight = AccessRights.Granted;
            this.ClearPermissions = true;
            this.Name = name;
            this.ModulePermissions = modulePermissions;
            this.AssemblyPermissions = assemblyPermissions;
            this.Guid = guid;
        }

        public void Check(Row row, string simpleIdentName, bool fullAccess)
        {
            bool flag;
            AccessRights rights = this[row.GetType()];
            if (fullAccess)
            {
                flag = rights == AccessRights.Granted;
            }
            else
            {
                flag = rights != AccessRights.Denied;
            }
            if (!flag)
            {
                throw new AccessException(row, string.Format("Brak prawa do wykonania operacji '{0}' dla obiektu '{1}'.", simpleIdentName, row));
            }
        }

        public Role Clone()
        {
            return new Role("Kopia " + this.Name, Guid.NewGuid(), this.ModulePermissions, this.AssemblyPermissions);
        }

        public int CompareTo(object obj)
        {
            return string.Compare(this.Name, ((Role)obj).Name, true);
        }

        public static Role Concat(Role r1, Role r2)
        {
            Role role = new Role
            {
                types = new Hashtable(),
                AccessRight = (r1.AccessRight > r2.AccessRight) ? r1.AccessRight : r2.AccessRight
            };
            foreach (Type type in r1.GetTypes())
            {
                role.types[type] = TypePermission.Concat(r1.getPermission(type), r2.getPermission(type));
            }
            Set set = new Set();
            if (r1.LicModules != null)
            {
                foreach (LicencjaProgramu programu in r1.LicModules)
                {
                    set += programu;
                }
            }
            if (r2.LicModules != null)
            {
                foreach (LicencjaProgramu programu2 in r2.LicModules)
                {
                    set += programu2;
                }
            }
            role.LicModules = (LicencjaProgramu[])set.ToArray(typeof(LicencjaProgramu));
            return role;
        }

        public static IIdentAttribute FindIdent(Type dataType, string type, string name)
        {
            foreach (IIdentAttribute attribute in GetAssembliesIdents())
            {
                if (((attribute.Type == type) && (attribute.Name == name)) && (attribute.DataType == dataType))
                {
                    return attribute;
                }
            }
            return null;
        }

        public static Role FromStream(Stream stream, bool isStandard)
        {
            Role role = (Role)serializer.Deserialize(stream);
            role.IsStandard = isStandard;
            role.OnLoad();
            return role;
        }

        public static Role FromXml(string xml, bool isStandard)
        {
            using (StringReader reader = new StringReader(xml))
            {
                Role role = (Role)serializer.Deserialize(reader);
                role.IsStandard = isStandard;
                role.OnLoad();
                return role;
            }
        }

        public static IEnumerable<IIdentAttribute> GetAssembliesIdents()
        {
            lock (globalIdentsLock)
            {
                if (globalIdents == null)
                {
                    globalIdents = new LinkedList<IIdentAttribute>();
                    foreach (Attribute attribute in AssemblyAttributes.GetFromAssemblies())
                    {
                        IIdentAttribute attribute2 = attribute as IIdentAttribute;
                        if ((attribute2 != null) && attribute2.IsRightsSource)
                        {
                            globalIdents.AddLast(attribute2);
                        }
                    }
                }
            }
            return globalIdents;
        }

        public static bool GetBaseDataType(Type type, out Type dataType, out string moduleName)
        {
            if (typeof(Table).IsAssignableFrom(type))
            {
                TableNameAttribute customAttribute = (TableNameAttribute)Attribute.GetCustomAttribute(type, typeof(TableNameAttribute));
                if (customAttribute != null)
                {
                    type = customAttribute.RowType;
                }
            }
            dataType = type;
            moduleName = "";
            if (type == null)
            {
                dataType = UnknownDataType;
                return true;
            }
            if (typeof(Row).IsAssignableFrom(type))
            {
                while (type != typeof(object))
                {
                    Type baseType = type.BaseType;
                    Type declaringType = baseType.DeclaringType;
                    if ((declaringType != null) && typeof(Module).IsAssignableFrom(declaringType))
                    {
                        moduleName = declaringType.Name.Substring(0, declaringType.Name.Length - 6);
                        dataType = type;
                        return true;
                    }
                    type = baseType;
                }
                return false;
            }
            while (type != typeof(object))
            {
                RightsSourceAttribute attribute2 = (RightsSourceAttribute)Attribute.GetCustomAttribute(type, typeof(RightsSourceAttribute), false);
                if (attribute2 != null)
                {
                    dataType = type;
                    return attribute2.Enabled;
                }
                type = type.BaseType;
            }
            return false;
        }

        private TypePermission getPermission(Type dataType)
        {
            if (this.types == null)
            {
                lock (this)
                {
                    this.initTypes();
                }
            }
            if (typeof(Table).IsAssignableFrom(dataType))
            {
                TableNameAttribute customAttribute = (TableNameAttribute)Attribute.GetCustomAttribute(dataType, typeof(TableNameAttribute));
                dataType = customAttribute.RowType;
            }
            Type unknownDataType = dataType;
            if (unknownDataType == null)
            {
                unknownDataType = UnknownDataType;
            }
            while (unknownDataType != null)
            {
                TypePermission permission = (TypePermission)this.types[unknownDataType];
                if (permission != null)
                {
                    return permission;
                }
                unknownDataType = unknownDataType.BaseType;
            }
            return null;
        }

        internal ICollection GetTypes()
        {
            if (this.types == null)
            {
                lock (this)
                {
                    this.initTypes();
                }
            }
            return this.types.Keys;
        }

        private void initIdents(Hashtable modules, Hashtable types)
        {
            foreach (IIdentAttribute attribute in GetAssembliesIdents())
            {
                Type type;
                string str;
                if (((attribute.DataType == null) || !attribute.DataType.IsInterface) && GetBaseDataType(attribute.DataType, out type, out str))
                {
                    ModulePermission permission = (ModulePermission)modules[str];
                    AccessRights accessRight = (permission == null) ? this.AccessRight : permission.AccessRight;
                    TypePermission permission2 = (TypePermission)types[type];
                    if (permission2 == null)
                    {
                        types[type] = permission2 = new TypePermission(accessRight);
                    }
                    if (!permission2.idents.Contains(attribute))
                    {
                        permission2.idents[attribute] = permission2.AccessRight;
                    }
                }
            }
            foreach (IIdentAttribute attribute2 in GetAssembliesIdents())
            {
                if ((attribute2.DataType != null) && attribute2.DataType.IsInterface)
                {
                    foreach (DictionaryEntry entry in types)
                    {
                        if (attribute2.DataType.IsAssignableFrom((Type)entry.Key))
                        {
                            TypePermission permission3 = (TypePermission)entry.Value;
                            if (!permission3.idents.Contains(attribute2))
                            {
                                permission3.idents[attribute2] = permission3.AccessRight;
                            }
                        }
                    }
                }
            }
        }

        private void initModules(Hashtable modules, Hashtable types)
        {
            foreach (ModuleTypeAttribute attribute in BusApplication.Instance.Modules)
            {
                foreach (Type type in attribute.ModuleType.GetNestedTypes())
                {
                    if (type.IsSubclassOf(typeof(Table)))
                    {
                        TableNameAttribute customAttribute = (TableNameAttribute)Attribute.GetCustomAttribute(type, typeof(TableNameAttribute));
                        if (!types.Contains(customAttribute.RowType))
                        {
                            ModulePermission permission = (ModulePermission)modules[attribute.Name];
                            TypePermission permission2 = new TypePermission((permission != null) ? permission.AccessRight : this.AccessRight);
                            types[customAttribute.RowType] = permission2;
                        }
                    }
                }
            }
        }

        private void initTypes()
        {
            Hashtable types = new Hashtable();
            Hashtable modules = new Hashtable();
            if (this.ModulePermissions != null)
            {
                foreach (ModulePermission permission in this.ModulePermissions)
                {
                    permission.init(types);
                    modules[permission.Name] = permission;
                }
            }
            this.initModules(modules, types);
            this.initIdents(modules, types);
            if (this.AssemblyPermissions != null)
            {
                foreach (AssemblyPermission permission2 in this.AssemblyPermissions)
                {
                    permission2.init(types);
                }
            }
            this.types = types;
            if (this.ClearPermissions)
            {
                this.AssemblyPermissions = null;
                this.ModulePermissions = null;
            }
        }

        private void OnLoad()
        {
            if (this.ModulePermissions != null)
            {
                foreach (ModulePermission permission in this.ModulePermissions)
                {
                    if (permission.Tables != null)
                    {
                        foreach (TablePermission permission2 in permission.Tables)
                        {
                            Queue<IdentPermission> queue = null;
                            if (permission2.Idents != null)
                            {
                                foreach (IdentPermission permission3 in permission2.Idents)
                                {
                                    string str;
                                    if ((permission3.Type == "Menu") && oldFoldersMap.TryGetValue(permission3.Name, out str))
                                    {
                                        if (queue == null)
                                        {
                                            queue = new Queue<IdentPermission>();
                                        }
                                        foreach (string str2 in str.Split(new char[] { ';' }))
                                        {
                                            IdentPermission item = new IdentPermission(permission3.Type, str2)
                                            {
                                                AccessRight = permission3.AccessRight
                                            };
                                            queue.Enqueue(item);
                                        }
                                    }
                                }
                            }
                            if (queue != null)
                            {
                                permission2.Idents = CoreTools.Add<IdentPermission>(permission2.Idents, queue.ToArray());
                            }
                        }
                    }
                }
            }
        }

        public void ResyncRights()
        {
            this.types = null;
            this.ClearPermissions = false;
        }

        bool IAttributeFilter.Filter(Type dataType, IIdentAttribute ident)
        {
            return (this[dataType, ident] != AccessRights.Denied);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public override string ToString()
        {
            return this.Name;
        }

        public string ToXml()
        {
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }

        // Properties
        [Description("Opis działania roli")]
        public string Description { get; set; }

        [Description("Rola przeznaczona dla modułu enovaNet"), XmlAttribute]
        public bool IsRoleNet { get; set; }

        [Description("Określa rolę standardową, nieprzedefiniowana przez administratora."), XmlIgnore]
        public bool IsStandard { get; internal set; }

        public AccessRights this[Type dataType]
        {
            get
            {
                AccessRights granted = AccessRights.Granted;
                TypePermission permission = this.getPermission(dataType);
                if (permission != null)
                {
                    granted = permission.AccessRight;
                }
                LogRight.SourceRight(dataType, null, null, granted);
                return granted;
            }
        }

        public AccessRights this[Type dataType, IIdentAttribute ident]
        {
            get
            {
                AccessRights granted = AccessRights.Granted;
                TypePermission permission = this.getPermission(dataType);
                if (permission != null)
                {
                    granted = permission[ident];
                }
                LogRight.SourceRight(dataType, ident.Type, ident.Name, granted);
                return granted;
            }
        }

        public AccessRights this[Type dataType, string simpleIdentName]
        {
            get
            {
                AccessRights granted = AccessRights.Granted;
                TypePermission permission = this.getPermission(dataType);
                if (permission != null)
                {
                    granted = permission[new SimpleRightAttribute(dataType, simpleIdentName)];
                }
                LogRight.SourceRight(dataType, "Simple", simpleIdentName, granted);
                return granted;
            }
        }

        [XmlIgnore]
        public LicencjaProgramu Licences
        {
            get
            {
                if (this.LicModules == null)
                {
                    return LicencjaProgramu.Empty;
                }
                LicencjaProgramu empty = LicencjaProgramu.Empty;
                foreach (LicencjaProgramu programu2 in this.LicModules)
                {
                    empty |= programu2;
                }
                return empty;
            }
        }

        [XmlElement("LicenceModule")]
        public string[] LicModulesSer
        {
            get
            {
                if (this.LicModules == null)
                {
                    return null;
                }
                string[] strArray = new string[this.LicModules.Length];
                for (int i = 0; i < this.LicModules.Length; i++)
                {
                    strArray[i] = this.LicModules[i].ToString();
                }
                return strArray;
            }
            set
            {
                if (value == null)
                {
                    this.LicModules = null;
                }
                else
                {
                    this.LicModules = new LicencjaProgramu[value.Length];
                    for (int i = 0; i < this.LicModules.Length; i++)
                    {
                        this.LicModules[i] = LicencjaProgramu.Parse(value[i]);
                    }
                }
            }
        }

        [Description("Nazwa roli"), XmlAttribute]
        public string Name { get; set; }

        [Description("Określa rolę standardową, nieprzedefiniowana przez administratora."), XmlIgnore]
        public string StandardName
        {
            get
            {
                if (!this.IsStandard)
                {
                    return "";
                }
                return "Standard";
            }
        }

        public static Type UnknownDataType
        {
            get
            {
                return typeof(Roles);
            }
        }
    }
*/
}
