using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class Session : API.Types.ObjectBase, API.Business.Session
    {
        #region Fields

        internal Dictionary<Type, API.Business.Module> ModulesByInterfaceType;

        #endregion

        #region Properties


        #endregion

        #region Methods

        public Session()
        {
            ModulesByInterfaceType = new Dictionary<Type, API.Business.Module>();
        }

        public API.Business.Module GetModule(Type t)
        {
            if (ModulesByInterfaceType.ContainsKey(t))
                return ModulesByInterfaceType[t];
            var m = CreateModule(t);
            ModulesByInterfaceType.Add(t, m);
            return m;
        }

        public T GetModule<T>() where T : class, API.Business.Module
        {
            var t = typeof(T);
            if (ModulesByInterfaceType.ContainsKey(t))
                return (T)ModulesByInterfaceType[t];

            var m = CreateModule<T>();
            ModulesByInterfaceType.Add(t, m);
            return m;
        }

        internal object GetEnovaModule(string name)
        {
            foreach (var em in ((IEnumerable)GetObjValue(EnovaObject, "Modules")))
            {
                var n = (string)em.GetType().GetProperty("Name").GetValue(em, null);
                if (n == name)
                    return em;
            }
            return null;
        }

        internal API.Business.Module CreateModule(Type t)
        {
            var attr = ModuleAttribute.GetAttributeByInterfaceType(t);
            if (attr != null)
                return (API.Business.Module)attr.Type.GetConstructor(new Type[] { typeof(Session) }).Invoke(new object[] { this });
            throw new Exception("Brak zarejestrowanego modułu dla interfejsu " + t.Name);
        }

        internal T CreateModule<T>() where T : API.Business.Module
        {
            var attr = ModuleAttribute.GetAttributeByInterfaceType(typeof(T));
            if (attr != null)
                return (T)attr.Type.GetConstructor(new Type[] { typeof(Session) }).Invoke(new object[] { this });
            throw new Exception("Brak zarejestrowanego modułu dla interfejsu " + typeof(T).Name);
        }

        public API.Business.Transaction CreateTransaction()
        {
            return new Transaction(this, true);
        }

        public API.Business.Table GetTable(string tableName)
        {
            var attr = RowMapAttribute.GetByTableName(tableName);
            if (attr != null && attr.ModuleType!=null)
            {
                var module = CreateModule(attr.ModuleType);
                if (module != null)
                    return (API.Business.Table)((Module)module).GetObjValue(module, tableName, null);
            }
            throw new Exception("Brak lub nieprawidłowa rejestracji RowMap dla tabeli: " + tableName);
        }

        public void Save()
        {
            CallMethod("Save");
        }

        public void Dispose()
        {
            CallMethod("Dispose");
        }

        #endregion
    }
}
