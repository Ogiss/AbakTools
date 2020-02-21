using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BAL.Configuration
{
    public class DatabasesConfigurationCollection : IEnumerable , IEnumerable<IDatabaseConfiguration>
    {
        #region Fields

        private Dictionary<string, IDatabaseConfiguration> byName;

        #endregion

        #region Properties

        public IDatabaseConfiguration this[string name]
        {
            get
            {
                if (this.byName.ContainsKey(name))
                    return (IDatabaseConfiguration)this.byName[name];
                return null;
            }
        }

        #endregion

        #region Methods

        public DatabasesConfigurationCollection()
        {
            this.byName = new Dictionary<string, IDatabaseConfiguration>();
        }

        public void Add(IDatabaseConfiguration dbconfig)
        {
            byName.Add(dbconfig.Name, dbconfig);
        }

        public IEnumerator<IDatabaseConfiguration> GetEnumerator()
        {
            return this.byName.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

    }
}
