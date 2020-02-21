using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class ModulesCollection : IEnumerable, IEnumerable<Module>, IDisposable, ISessionable
    {
        #region Fields

        private bool is_disposed;
        private Session session;
        private HybridDictionary byName;
        private Dictionary<Type, Module> byType;

        #endregion

        #region Properties

        public Session Session
        {
            get { return session; }
        }

        public Module this[string moduleName]
        {
            get
            {
                Module module = (Module)byName[moduleName];
                if (module == null)
                {
                    module = NewModule(moduleName);
                    byName.Add(moduleName, module);
                    byType.Add(module.GetType(), module);
                }
                return module;
            }
        }

        public Module this[Type moduleType]
        {
            get
            {
                if (byType.ContainsKey(moduleType))
                    return byType[moduleType];

                string moduleName = AppModuleAttribute.Collection[moduleType];

                if (string.IsNullOrEmpty(moduleName))
                    throw new BadRegistrationModuleException(moduleType.Name);

                return this[moduleName];
            }
        }

        #endregion

        #region Methods

        public ModulesCollection(Session session)
        {
            this.session = session;
            byName = new HybridDictionary(true);
            byType = new Dictionary<Type, Module>();
        }

        private Module NewModule(string moduleName)
        {
            Type type = AppModuleAttribute.Collection[moduleName];
            if (type == null)
                throw new BadRegistrationModuleException(moduleName);
            return this.NewModule(type);
        }

        private Module NewModule(Type type)
        {
            return (Module)type.GetConstructor(new Type[] { typeof(Session) }).Invoke(new object[] { this.Session });
        }

        public IEnumerator<Module> GetEnumerator()
        {
            return byType.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Module loadByType(Type type)
        {
            Module module;
            if (this.byType.TryGetValue(type, out module))
            {
                return module;
            }
            return this.NewModule(type);
        }

        public void LoadModule(string moduleName)
        {
            var module = this[moduleName];
        }

        #endregion

        #region IDisposable Implemantation

        private void Dispose(bool userCall)
        {
            if (!is_disposed)
            {
                if (userCall)
                {
                    foreach (var module in this)
                        module.Dispose();
                }
                is_disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ModulesCollection()
        {
            Dispose(false);
        }

        #endregion
    }
}
