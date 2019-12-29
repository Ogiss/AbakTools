using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public class FeatureCollectionTypeDescriptorProvider : TypeDescriptionProvider
    {
        private static TypeDescriptionProvider defaultProvider = TypeDescriptor.GetProvider(typeof(FeatureCollection));

        public FeatureCollectionTypeDescriptorProvider()
            : base(defaultProvider)
        {
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            var defaultDescriptor = base.GetTypeDescriptor(objectType, instance);
            return new FeatureCollectionTypeDescriptor(defaultDescriptor, instance);
        }
    }
}
