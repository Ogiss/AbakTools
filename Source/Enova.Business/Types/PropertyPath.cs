using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Enova.Old.Types
{
    public class PropertyPath
    {
        #region Fields

        private PropertyInfo[] propertyInfos;
        private Type[] types;

        #endregion

        #region Properties


        #endregion

        #region Methods

        public PropertyPath(Type type, string propertyName)
        {
            this.init(type, propertyName);
        }

        private void init(Type rootType, string propertyName)
        {
            var fields = propertyName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            this.propertyInfos = new PropertyInfo[fields.Length];
            this.types = new Type[fields.Length];
            Type type = rootType;
            for(int i = 0; i<fields.Length; i++)
            {
                var pinfo = type.GetProperty(fields[i]);
                if (pinfo == null)
                    throw new Exception(string.Format("Właściwiść {0} nie wystepuje w type {1}", fields[i], type.Name));
                this.propertyInfos[i] = pinfo;
                this.types[i] = pinfo.PropertyType;
                type = pinfo.PropertyType;
            }
        }

        public PropertyInfo GetLastProperty()
        {
            if (this.propertyInfos.Length > 0)
                return this.propertyInfos[this.propertyInfos.Length - 1];
            return null;
        }

        public Type GetLastType()
        {
            if (this.types.Length > 0)
                return this.types[this.types.Length - 1];
            return null;
        }

        #endregion
    }
}
