using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Enova.API.Connector
{
    internal static class EnovaHelper
    {

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
                if (tm != null && tm.ConnectorType != null)
                {
                    var r = tm.ConnectorType.GetConstructor(new Type[0]).Invoke(new object[0]);
                    if (r is API.Types.ObjectBase)
                        ((API.Types.ObjectBase)r).EnovaObject = obj;
                    return (T)r;
                }

                throw new Exception("Brak zarejestrowanej mapy dla " + obj.GetType().FullName);
            }
            return default(T);
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
