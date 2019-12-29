using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Enova.API.Types
{
    public class ObjectBase : MarshalByRefObject, IObjectBase
    {
        #region Fields
        #endregion

        #region Properties

        public virtual object EnovaObject { get; set; }

        #endregion

        #region Methods

        public object FromEnova(object obj)
        {
            return Enova.API.Connector.EnovaHelper.FromEnova(obj);
        }

        public object FromEnova(string name)
        {
            return FromEnova(GetValue(name));
        }

        public T FromEnova<T>(object obj)
        {
            return Enova.API.Connector.EnovaHelper.FromEnova<T>(obj);
        }

        public T FromEnova<T>()
        {
            return FromEnova<T>(EnovaObject);
        }

        public T FromEnova<T>(string name, Type fromType = null)
        {
            return FromEnova<T>(GetObjValue(EnovaObject, name, null, null, fromType));
        }

        public T FromEnova<T, F>(string name)
        {
            var f = typeof(F);
            var tm = TypeMapAttribute.GetByApiType(f);
            if (tm != null && !string.IsNullOrEmpty(tm.EnovaType))
                f = Type.GetType(tm.EnovaType);
            return FromEnova<T>(name, f);
        }

        public object ToEnova(object obj)
        {
            return Enova.API.Connector.EnovaHelper.ToEnova(obj);
        }

        public object ToEnova(string name, object obj)
        {
            var enova = Enova.API.Connector.EnovaHelper.ToEnova(obj);
            SetValue(name, enova);
            return enova;
        }

        public object GetObjValue(object obj, string name, Type[] types = null, object[] index = null, Type fromType = null)
        {
            if (obj != null)
            {
                var t = fromType == null ? obj.GetType() : fromType;
                var pinfo = types!=null ? t.GetProperty(name,types) : t.GetProperty(name);
                return pinfo.GetValue(obj, index);
            }
            return null;
        }

        public object GetValue(string name, object[] indexes)
        {
            if (indexes == null || indexes.Length == 0)
                return GetObjValue(EnovaObject, name);
            Type[] types = new Type[indexes.Length];
            for (var i = 0; i < indexes.Length; i++)
                types[i] = indexes[i].GetType();
            return GetObjValue(EnovaObject, name, types, indexes);
            
            }
        public object GetValue(string name)
        {
            return GetObjValue(EnovaObject, name);
        }

        public T GetValue<T>(string name)
        {
            return GetObjValue<T>(EnovaObject, name);
        }

        public T GetObjValue<T>(object obj, string name)
        {
            var pinfo = obj.GetType().GetProperty(name);
            object val = pinfo.GetValue(obj, null);
            if (val == null)
                return default(T);
            var t = val.GetType();
            if (t != typeof(T))
            {
                try
                {
                    return (T)TypeDescriptor.GetConverter(t).ConvertTo(val, typeof(T));
                }
                catch { }
            }
            return (T)val;
        }

        public void SetObjValue(object obj, string name, object value)
        {
            try
            {
                if (obj != null)
                {
                    var pinfo = obj.GetType().GetProperty(name);
                    if (pinfo != null)
                        pinfo.SetValue(obj, value, null);
                    else
                    {
                        var finfo = obj.GetType().GetField(name);
                        if (finfo != null)
                            finfo.SetValue(obj, value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SonetaException(ex);
            }
        }

        public void SetValue(string name, object value)
        {
            SetObjValue(EnovaObject, name, value);
        }

        public void SetValue(string name, object value, object[] indexes)
        {
            try
            {
                if (indexes == null || indexes.Length == 0)
                    SetObjValue(EnovaObject, name, value);
                Type[] types = new Type[indexes.Length];
                for (var i = 0; i < indexes.Length; i++)
                    types[i] = indexes[i].GetType();
                var pinfo = EnovaObject.GetType().GetProperty(name, types);
                pinfo.SetValue(EnovaObject, value, indexes);
            }
            catch (Exception ex)
            {
                throw new SonetaException(ex);
            }

        }

        public object CallMethod(string name, params object[] args)
        {
            List<Type> types = new List<Type>();
            if (args != null && args.Length > 0)
                foreach (var arg in args)
                    types.Add(arg.GetType());
            return CallMethodFull(name, (Type[])types.ToArray(), args);
        }

        public object CallMethodFull(string name, Type[] paramTypes, object[] parameters)
        {
            try
            {
                var minfo = paramTypes == null ? EnovaObject.GetType().GetMethod(name) : EnovaObject.GetType().GetMethod(name, paramTypes);
                return minfo.Invoke(EnovaObject, parameters);
            }
            catch (Exception ex)
            {
                throw new SonetaException(ex);
            }
        }

        public object CallObjMethod(object obj, string name, Type[] paramTypes, object[] parameters)
        {
            try
            {
                var minfo = paramTypes == null ? obj.GetType().GetMethod(name) : obj.GetType().GetMethod(name, paramTypes);
                return minfo.Invoke(obj, parameters);
            }
            catch (Exception ex)
            {
                throw new SonetaException(ex);
            }
        }

        public virtual Type GetEnovaType()
        {
            if (EnovaObject != null)
                return EnovaObject.GetType();
            return null;
        }

        public bool Is<T>()
        {
            return this is T;
        }

        public T As<T>()
        {
            return (T)(object)this;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public override string ToString()
        {
            return (string)CallMethod("ToString");
        }

        #endregion
    }
}
