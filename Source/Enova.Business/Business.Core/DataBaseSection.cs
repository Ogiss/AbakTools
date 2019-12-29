using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Enova.Business.Old.Core
{
    public class DataBaseSection : ConfigurationSection
    {
        public DataBaseSection()
        {
        }
        
        [ConfigurationProperty("settings")]
        public DataBaseSetttingsElement Settings
        {
            get
            {
                return (DataBaseSetttingsElement)this["settings"];
            }
            set
            {
                this["settings"] = value;
            }
        }
    }
}
