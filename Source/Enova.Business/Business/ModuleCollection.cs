using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public sealed class ModuleCollection : IEnumerable<Module>, IEnumerable
    {
        #region Fields

        private HybridDictionary byName;
        private readonly Dictionary<Type, Module> byType;
        private readonly Session session;

        #endregion

        #region Properties

        public Module this[Type type]
        {
            get
            {
                return this.loadByType(type);
            }
        }

        #endregion

        #region Methods

        internal ModuleCollection(Session session)
        {
            this.byName = new HybridDictionary(true);
            this.byType = new Dictionary<Type, Module>();
            this.session = session;
        }

        private Module loadByType(Type type)
        {
            Module module;
            if (this.byType.TryGetValue(type, out module))
            {
                return module;
            }
            /*
            if (!Login.byType.Contains(type))
            {
                throw new ArgumentException("Niezarejestrowany moduł '" + type.Name + "'");
            }
             */
            return this.NewModule(type);
        }

        private Module NewModule(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Niezarejestrowany moduł");
            }
            Module module = (Module)type.GetConstructor(new Type[] { typeof(Session) }).Invoke(new object[] { this.session });
            this.byType[type] = module;
            this.byName[module.Name] = module;
            return module;
        }

        public IEnumerator<Module> GetEnumerator()
        {
            return this.byType.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
