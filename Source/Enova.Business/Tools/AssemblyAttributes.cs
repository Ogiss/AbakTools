using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Enova.Old.Tools
{
    public static class AssemblyAttributes
    {
        // Fields
        private static Dictionary<Type, Attribute[]> cache = new Dictionary<Type, Attribute[]>();
        private static List<Attribute> globalIdents;
        private static object globalIdentsLock = new object();

        // Methods
        public static Attribute Find(Type dataType, Type attributeType)
        {
            object[] customAttributes = dataType.GetCustomAttributes(attributeType, true);
            if (customAttributes.Length > 0)
            {
                return (Attribute)customAttributes[0];
            }
            foreach (Type type in dataType.GetInterfaces())
            {
                Attribute attribute = Find(type, attributeType);
                if (attribute != null)
                {
                    return attribute;
                }
            }
            return null;
        }

        public static IEnumerable<Assembly> GetAssemblies()
        {
            foreach (Assembly iteratorVariable0 in AppDomain.CurrentDomain.GetAssemblies())
            {
                if ((!(iteratorVariable0 is AssemblyBuilder) && !iteratorVariable0.IsDynamic) && !string.IsNullOrEmpty(iteratorVariable0.Location))
                {
                    yield return iteratorVariable0;
                }
            }
        }

        public static T[] GetCustom<T>() where T : Attribute
        {
            return (T[])GetCustom(typeof(T));
        }

        public static Attribute[] GetCustom(Type type)
        {
            lock (cache)
            {
                Attribute[] attributeArray;
                if (!cache.TryGetValue(type, out attributeArray))
                {
                    if (typeof(PriorityAttribute).IsAssignableFrom(type))
                    {
                        List<PriorityAttribute> list = new List<PriorityAttribute>();
                        foreach (Attribute attribute in GetFromAssemblies())
                        {
                            if (type.IsAssignableFrom(attribute.GetType()))
                            {
                                list.Add((PriorityAttribute)attribute);
                            }
                        }
                        list.Sort();
                        PriorityAttribute[] array = (PriorityAttribute[])Array.CreateInstance(type, list.Count);
                        list.CopyTo(array);
                        attributeArray = array;
                    }
                    else
                    {
                        List<Attribute> list2 = new List<Attribute>();
                        foreach (Attribute attribute2 in GetFromAssemblies())
                        {
                            if (type.IsAssignableFrom(attribute2.GetType()))
                            {
                                list2.Add(attribute2);
                            }
                        }
                        attributeArray = (Attribute[])Array.CreateInstance(type, list2.Count);
                        list2.CopyTo(attributeArray);
                    }
                    cache[type] = attributeArray;
                }
                return attributeArray;
            }
        }

        public static T[] GetCustom<T>(Predicate<T> match) where T : Attribute
        {
            return Array.FindAll<T>(GetCustom<T>(), match);
        }

        public static ICollection GetCustom(Type type, Type dataType, IAttributeFilter af)
        {
            Attribute[] custom = GetCustom(type);
            ArrayList list = new ArrayList(custom.Length);
            foreach (IIdentAttribute attribute in custom)
            {
                if (attribute.DataType.IsAssignableFrom(dataType) && ((af == null) || af.Filter(dataType, attribute)))
                {
                    list.Add(attribute);
                }
            }
            return list;
        }

        public static IEnumerable<Attribute> GetFromAssemblies()
        {
            lock (globalIdentsLock)
            {
                if (globalIdents == null)
                {
                    IEnumerable<Assembly> assemblies = GetAssemblies();
                    globalIdents = new List<Attribute>();
                    foreach (Assembly assembly in assemblies)
                    {
                        string fullName = assembly.FullName;
                        if ((!fullName.StartsWith("System") && !fullName.StartsWith("Microsoft")) && !fullName.StartsWith("DevExpress"))
                        {
                            foreach (Attribute attribute in assembly.GetCustomAttributes(true))
                            {
                                string str2 = attribute.GetType().Namespace;
                                if ((str2 != null) && str2.StartsWith("Soneta."))
                                {
                                    globalIdents.Add(attribute);
                                    AssemblyAttribute attribute2 = attribute as AssemblyAttribute;
                                    if (attribute2 != null)
                                    {
                                        attribute2.SetAssembly(assembly);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return globalIdents;
        }

        // Properties
        public static bool IsAssembliesLoaded { get; set; }

    }

}
