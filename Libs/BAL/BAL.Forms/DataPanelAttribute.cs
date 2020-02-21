using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Forms
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class DataPanelAttribute : BAL.Types.AttributeBase
    {
        #region Fields

        private Type dataType;
        private Type dataPanelType;
        private string path;
        private static Dictionary<Type, List<DataPanelAttribute>> __cache__;

        #endregion

        #region Properties

        public Type DataType
        {
            get { return this.dataType; }
        }

        public Type DataPanelType
        {
            get { return this.dataPanelType; }
        }

        public string Path
        {
            get { return this.path; }
        }

        #endregion

        #region Methods

        public DataPanelAttribute(string path, Type dataType, Type dataPanelType)
        {
            this.path = path;
            this.dataType = dataType;
            this.dataPanelType = dataPanelType;
        }

        private static void checkCache()
        {
            if (__cache__ == null)
            {
                __cache__ = new Dictionary<Type, List<DataPanelAttribute>>();
                var attrs = AppController.Instance.AssemblyAttributes[typeof(DataPanelAttribute)];
                if (attrs != null)
                    foreach (DataPanelAttribute attr in attrs)
                    {
                        if (!__cache__.ContainsKey(attr.DataType))
                            __cache__.Add(attr.DataType, new List<DataPanelAttribute>());
                        var list = __cache__[attr.DataType];
                        list.Add(attr);
                    }
            }
        }

        public static List<DataPanelAttribute> GetDataPanels(object obj)
        {
            checkCache();
            var type = CoreTools.GetObjectType(obj);
            if (__cache__.ContainsKey(type))
                return __cache__[type];
            return null;
        }


        #endregion
    }
}
