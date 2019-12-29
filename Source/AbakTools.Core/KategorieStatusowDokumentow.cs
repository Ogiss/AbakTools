using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AbakTools.Core
{
    public class KategorieStatusowDokumentow : IEnumerable, IEnumerable<string>, IListSource
    {
        private SortedSet<string> list;

        public KategorieStatusowDokumentow()
        {
            initList();
        }

        private void initList()
        {
            list = new SortedSet<string>();
            list.Add("Reklamacje");
            list.Add("Dostawy");
        }

        public IEnumerator<string> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool ContainsListCollection
        {
            get { return true; }
        }

        public IList GetList()
        {
            return this.list.ToList();
        }
    }
}
