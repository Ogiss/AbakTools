using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Linq.Expressions;

namespace BAL.Types
{
    public class PropertyPath
    {
        #region Fields

        private string path;
        private PropertyInfo[] infos;
        private string[] names;

        #endregion

        #region Properties

        public PropertyInfo Last
        {
            get
            {
                if (infos != null && infos.Length > 0)
                    return infos[infos.Length - 1];
                return null;
            }
        }

        public PropertyInfo First
        {
            get
            {
                if (infos != null && infos.Length > 0)
                    return infos[0];
                return null;
            }
        }

        #endregion

        #region Methods

        public PropertyPath(Type dataType, string path)
        {
            this.path = path;
            this.initInfos(dataType);
        }

        public PropertyPath(PropertyInfo info)
        {
            this.infos = new PropertyInfo[] { info };
            this.names = new string[] { info.Name };
            this.path = info.Name;
        }

        private void initInfos(Type dataType)
        {
            var parts = this.path.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            this.infos = new PropertyInfo[parts.Length];
            this.names = new string[parts.Length];
            Type type = dataType;
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                PropertyInfo info = null; // type.GetProperty(part);
                var infoList = type.GetProperties().Where(r => r.Name == part).ToList();
                if (type.IsInterface)
                {
                    foreach (var it in type.GetInterfaces())
                        infoList.AddRange(it.GetProperties().Where(r => r.Name == part));
                }

                if (infoList.Count() == 1)
                    info = infoList.First();
                else
                {
                    foreach (var pinfo in infoList)
                    {
                        if (info == null)
                            info = pinfo;
                        else if (info.DeclaringType.IsAssignableFrom(pinfo.DeclaringType))
                            info = pinfo;
                    }
                }

                if (info != null)
                {
                    infos[i] = info;
                    names[i] = part;
                    type = info.PropertyType;
                }
                else
                    throw new PropertyPathException(dataType, path);
            }
        }

        public object GetValue(object data)
        {
            try
            {
                object value = data;
                for (int i = 0; i < this.infos.Length; i++)
                {
                    var pinfo = this.infos[i];
                    value = pinfo.GetValue(value, null);
                }
                return value;
            }
            catch
            {
                return null;
            }
        }

        public static PropertyPath Create<T>(string path)
        {
            return new PropertyPath(typeof(T), path);
        }

        /*
        public static PropertyPath Create<T, R>(Expression<Func<T, R>> exp)
        {
        }
        */

        public override string ToString()
        {
            return this.path;
        }

        #endregion
    }
}
