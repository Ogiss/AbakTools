using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Enova.Business.Old.Core
{
    public class DataBaseSetttingsElement : ConfigurationElement
    {
        public DataBaseSetttingsElement() { }

        public DataBaseSetttingsElement(bool isDefault)
        {
            this.Default = isDefault;
        }

        [ConfigurationProperty("default", DefaultValue = false, IsRequired = false)]
        public bool Default
        {
            get
            {
                return (bool)this["default"];
            }
            set
            {
                this["default"] = value;
            }
        }
    }
}
