using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public class FeaturePropertyDescriptor : PropertyDescriptor
    {
        private FeatureDefinition featureDef;

        public FeaturePropertyDescriptor(FeatureDefinition featureDef, Attribute[] attributes)
            : base(featureDef.Name, attributes)
        {
            this.featureDef = featureDef;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return typeof(FeatureCollection); }
        }

        public override object GetValue(object component)
        {
            try
            {
                return ((IFeatureCollection)component)[featureDef.Name];
            }
            catch
            {
                return null;
            }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get { return typeof(string); }
        }

        public override void ResetValue(object component)
        {
            
        }

        public override void SetValue(object component, object value)
        {
            
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}
