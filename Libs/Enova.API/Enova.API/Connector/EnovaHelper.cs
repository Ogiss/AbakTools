using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Enova.API.Connector
{
    internal static class EnovaHelper
    {
        private static Dictionary<Type, Type> dynamicConnectorTypes = new Dictionary<Type, Type>();

        public static object ToEnova(this API.Types.FromTo fromto)
        {
            return Type.GetType("Soneta.Types.FromTo, Soneta.Types").GetConstructor(new Type[]{
                Type.GetType("Soneta.Types.Date, Soneta.Types"),
                Type.GetType("Soneta.Types.Date, Soneta.Types")
            }).Invoke(new object[] { fromto.From.EnovaObject, fromto.To.EnovaObject });

        }

        public static object ToEnova(this DateTime date)
        {
            return Type.GetType("Soneta.Types.Date, Soneta.Types").GetConstructor(new Type[] { typeof(DateTime) }).Invoke(new object[] { date });
        }

        public static object FromEnova(object obj)
        {
            if (obj == null)
                return null;
            var t = obj.GetType();
            if (t == typeof(string))
                return (string)obj;
            else if (t == typeof(bool))
                return (bool)obj;
            else if (t == typeof(int))
                return (int)obj;
            else if (t == typeof(long))
                return (long)obj;
            else if (t == typeof(double))
                return (double)obj;
            else if (t == typeof(decimal))
                return (decimal)obj;
            else if (t == typeof(DateTime))
                return (DateTime)obj;
            else if (t == typeof(string[]))
                return (string[])obj;
            return FromEnova<API.Types.ObjectBase>(obj);
        }

        public static T FromEnova<T>(object obj)
        {
            if (obj != null)
            {
                if (obj is API.Types.IObjectBase)
                    obj = ((API.Types.IObjectBase)obj).EnovaObject;
                if (typeof(T).IsAssignableFrom(obj.GetType()))
                    return (T)obj;
                if (obj.GetType().IsEnum && typeof(T).IsEnum)
                    return (T)Enum.ToObject(typeof(T), (int)obj);
                var con = TypeDescriptor.GetConverter(obj.GetType());
                if (con != null && con.CanConvertTo(typeof(T)))
                    return (T)con.ConvertTo(obj, typeof(T));
                else
                {
                    con = TypeDescriptor.GetConverter(typeof(T));
                    if (con != null && con.CanConvertFrom(obj.GetType()))
                        return (T)con.ConvertFrom(obj);
                }
                var tm = TypeMapAttribute.GetByEnova(obj);
                if(tm == null)
                    tm = TypeMapAttribute.GetByApiType(typeof(T));
                if (tm == null && EnovaService.ConnectorSide)
                {
                    var t = obj.GetType().BaseType;
                    while (t != typeof(object))
                    {
                        tm = TypeMapAttribute.GetByEnova(t.FullName);
                        if (tm != null)
                            break;
                        t = t.BaseType;
                    }
                }

                Type connectorType = tm.ConnectorType;
                if (connectorType == null)
                    connectorType = getConnectorType(tm.ApiType, obj);

                if (tm != null && connectorType != null)
                {
                    var r = connectorType.GetConstructor(new Type[0]).Invoke(new object[0]);
                    if (r is API.Types.ObjectBase)
                        ((API.Types.ObjectBase)r).EnovaObject = obj;
                    return (T)r;
                }

                throw new Exception("Brak zarejestrowanej mapy dla " + obj.GetType().FullName);
            }
            return default(T);
        }

        private static Type getConnectorType(Type apiType, object obj)
        {
            if (dynamicConnectorTypes.ContainsKey(apiType))
                return dynamicConnectorTypes[apiType];

            Type baseType = getMappedConnectorBaseType(obj);
            var typeName = apiType.FullName.Replace("Enova.API.", "Enova.API.Connector.Dynamic.");
            var tb = DynamicTypesHelper.GetTypeBuilder(typeName, baseType);
            tb.AddInterfaceImplementation(apiType);

            var baseFromEnovaMethod = baseType.GetMethod("FromEnova", new Type[] { typeof(string), typeof(Type) });
            var baseToEnovaMethod = baseType.GetMethod("ToEnova", new Type[] { typeof(string), typeof(object) });
            var getSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.Virtual;
            var properties = apiType.GetProperties();
            foreach(var pinfo in properties)
            {
                var pb = tb.DefineProperty(pinfo.Name, PropertyAttributes.None, pinfo.PropertyType, null);
                if(pinfo.CanRead)
                {
                    var getter = tb.DefineMethod("get_" + pinfo.Name, getSetAttr , pinfo.PropertyType, Type.EmptyTypes);
                    var fromInstance = baseFromEnovaMethod.MakeGenericMethod(pinfo.PropertyType);
                    var il = getter.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldstr, pinfo.Name);
                    il.Emit(OpCodes.Ldnull);
                    il.Emit(OpCodes.Call, fromInstance);
                    il.Emit(OpCodes.Ret);
                    pb.SetGetMethod(getter);
                }
                if(pinfo.CanWrite)
                {
                    var setter = tb.DefineMethod("set_" + pinfo.Name, getSetAttr, null, new Type[] { pinfo.PropertyType });
                    var il = setter.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldstr, pinfo.Name);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Call, baseToEnovaMethod);
                    il.Emit(OpCodes.Pop);
                    il.Emit(OpCodes.Ret);
                    pb.SetSetMethod(setter);
                }
            }

            var type = tb.CreateType();
            dynamicConnectorTypes[apiType] = type;
            return type;
        }

        private static Type getMappedConnectorBaseType(object obj)
        {
            var t = obj.GetType().BaseType;
            TypeMapAttribute tm = null;
            while (t != typeof(object))
            {
                tm = TypeMapAttribute.GetByEnova(t.FullName);
                if (tm != null)
                    break;
                t = t.BaseType;
            }
            return tm == null ? typeof(API.Types.ObjectBase) : tm.ConnectorType;
        }

        public static Type GetType(string type)
        {
            return Type.GetType(type);
        }

        public static object ToEnova(object obj)
        {
            if (obj != null)
            {
                if (obj.GetType().IsEnum)
                {
                    var map = TypeMapAttribute.GetByConnectorType(obj.GetType());
                    if (map != null && !string.IsNullOrEmpty(map.EnovaType))
                        return Enum.ToObject(Type.GetType(map.EnovaType), (int)obj);
                }
                else if (obj is API.Types.ObjectBase && ((API.Types.ObjectBase)obj) != null)
                    return ((API.Types.ObjectBase)obj).EnovaObject;

            }
            return null;
        }

    }
}
