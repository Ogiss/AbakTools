using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public class TableCollection : IEnumerable
    {
        #region Fields

        private readonly ArrayList arr;
        private readonly HybridDictionary byName;
        private string cacheName;
        private ITable cacheTable;
        private readonly Session session;

        #endregion

        #region Methods

        internal TableCollection(Session session)
        {
            this.arr = new ArrayList();
            this.cacheName = "$";
            this.session = session;
            this.byName = new HybridDictionary(true);
            this.arr = new ArrayList();
        }

        internal void Add(ITable table)
        {
            //this.byName[table.Name] = table;
            this.byName[table.TableName] = table;
            this.arr.Add(table);
        }

        public IEnumerator GetEnumerator()
        {
            return this.arr.GetEnumerator();
        }

        #endregion
    }
}
