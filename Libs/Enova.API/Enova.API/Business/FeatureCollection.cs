using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    [TypeDescriptionProvider(typeof(API.Business.FeatureCollectionTypeDescriptorProvider))]
    public class FeatureCollection : MarshalByRefObject, IFeatureCollection
    {
        #region Fields

        private Row row;

        #endregion

        #region Properties

        public object this[string name]
        {
            get { return GetValue(name); }
            set { SetValue(name, value); }
        }

        #endregion

        #region Method

        public FeatureCollection(Row row)
        {
            this.row = row;
        }

        public object GetValue(string name)
        {
            object val = null;
            try
            {
                var col = row.GetValue("Features", null);
                val = col.GetType().GetProperty("Item", new Type[] { typeof(string) }).GetValue(col, new object[] { name });
            }
            catch { }
            return Connector.EnovaHelper.FromEnova(val);
        }

        public void SetValue(string name, object value)
        {
            try
            {
                object val = value;
                if (value is API.Types.IObjectBase)
                    val = ((API.Types.IObjectBase)value).EnovaObject;
                var f = row.GetValue("Features", null);
                f.GetType().GetProperty("Item", new Type[] { typeof(string) }).SetValue(f, val, new object[] { name });
            }
            catch { }
        }
        

        #endregion
    }
}
