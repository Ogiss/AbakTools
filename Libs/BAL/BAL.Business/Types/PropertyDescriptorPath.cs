using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    public class PropertyDescriptorPath
    {
        #region Fields

        private string path;
        private PropertyDescriptor[] infos;
        private string[] names;

        #endregion

        #region Properties

        public PropertyDescriptor Last
        {
            get
            {
                if (infos != null && infos.Length > 0)
                    return infos[infos.Length - 1];
                return null;
            }
        }

        public PropertyDescriptor First
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

        public PropertyDescriptorPath(Type dataType, string path)
        {
            this.path = path;
            this.initInfos(dataType);
        }

        public PropertyDescriptorPath(PropertyDescriptor info)
        {
            this.infos = new PropertyDescriptor[] { info };
            this.names = new string[] { info.Name };
            this.path = info.Name;
        }

        private void initInfos(Type dataType)
        {
            var parts = this.path.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            this.infos = new PropertyDescriptor[parts.Length];
            this.names = new string[parts.Length];
            Type type = dataType;
            var attributes = dataType.GetCustomAttributes(true).Cast<Attribute>().ToArray();
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                PropertyDescriptor info = null; // type.GetProperty(part);

                List<PropertyDescriptor> infoList = new List<PropertyDescriptor>();

                var properties = TypeDescriptor.GetProperties(type).Cast<PropertyDescriptor>().ToList();
                properties.AddRange(TypeDescriptor.GetProperties(type, attributes).Cast<PropertyDescriptor>());

                infoList = properties.Where(p => p.Name == part).ToList();

                if (type.IsInterface)
                {
                    foreach (var it in type.GetInterfaces())
                    {
                        properties = TypeDescriptor.GetProperties(it).Cast<PropertyDescriptor>().ToList();
                        properties.AddRange(TypeDescriptor.GetProperties(it, attributes).Cast<PropertyDescriptor>());
                        infoList.AddRange(properties.Where(p => p.Name == part));
                    }
                }

                if (infoList.Count() == 1)
                    info = infoList.First();
                else
                {
                    foreach (var pinfo in infoList)
                    {
                        if (info == null)
                            info = pinfo;
                            /*
                        else if (info.DeclaringType.IsAssignableFrom(pinfo.DeclaringType))
                            info = pinfo;
                             */
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
                    value = pinfo.GetValue(value);
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
