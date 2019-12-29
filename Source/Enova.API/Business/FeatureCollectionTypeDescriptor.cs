using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Enova.API.Business
{
    public class FeatureCollectionTypeDescriptor : CustomTypeDescriptor
    {
        private static Dictionary<string, PropertyDescriptorCollection> cache = new Dictionary<string, PropertyDescriptorCollection>();

        private FeatureCollection features;

        public FeatureCollectionTypeDescriptor(ICustomTypeDescriptor provider, object instance)
            : base(provider)
        {
            features = (FeatureCollection)instance;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if (attributes.Length > 0)
            {
                var attr = attributes.Where(a => a.GetType() == typeof(TableNameAttribute)).FirstOrDefault() as TableNameAttribute;
                if (attr != null)
                {
                    if (cache.ContainsKey(attr.TableName))
                        return cache[attr.TableName];
                    using (var session = EnovaService.Instance.CreateSession())
                    {
                        var table = session.GetTable(attr.TableName);
                        if (table != null)
                        {
                            var properties = (from fd in table.FeatureDefinitions
                                              orderby fd.Name
                                              select new FeaturePropertyDescriptor(fd, attributes)).ToArray();
                            var col = new PropertyDescriptorCollection(properties);
                            cache[attr.TableName] = col;
                            return col;
                        }
                    }
                }
            }
            return base.GetProperties(attributes);
        }
    }
}
