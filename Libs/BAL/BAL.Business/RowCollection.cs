using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class RowCollection<T> : IEnumerable, IEnumerable<T>, ISessionable, IRowCollection
        where T : Row
    {
        #region Fields

        private Table<T> table;
        private List<T> changed;

        #endregion

        #region Properties

        public Session Session
        {
            get { return this.table.Session; }
        }

        internal List<T> Changed
        {
            get
            {
                if (this.changed == null)
                {
                    this.changed = new List<T>();
                }
                return this.changed;
            }
        }

        IList IRowCollection.Changed
        {
            get { return this.Changed; }
        }


        #endregion

        #region Methods

        internal RowCollection(Table<T> table)
        {
            this.table = table;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var row in this.table.Query.ToList())
                yield return row;

            foreach (var row in this.Changed)
            {
                //if (((Old.IRow)row).IsLive && ((Old.IRow)row).Status == Old.RowStatus.Added)
                if (row.IsLive && row.ID <= 0)
                    yield return row;
            }
                
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Nester Types


        #endregion
    }
}
