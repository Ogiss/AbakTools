using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public class HistoriaDokumentuCollection : ICollection<HistoriaDokumentu>
    {
        #region Fields

        private List<HistoriaDokumentu> _list;
        internal List<HistoriaDokumentu> added;
        private IDokumentZHistoria dokument;
        private WebContext dbContext;

        #endregion

        #region Properties



        public int Count
        {
            get { return this.list.Count + this.added.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        private List<HistoriaDokumentu> list
        {
            get
            {
                if (dokument.State == RowState.Modified || dokument.State == RowState.Unchanged)
                {
                    if (_list == null && this.dokument.ID > 0)
                        _list = dbContext.HistorieDokumentow.Where(r => r.Host_ID == dokument.ID && r.Host_Type == dokument.Table.TableName).ToList();
                    return _list;
                }
                return new List<HistoriaDokumentu>();
            }
        }

        #endregion

        #region Methods

        public HistoriaDokumentuCollection(WebContext dc, IDokumentZHistoria dokument)
        {
            dbContext = dc;
            this.dokument = dokument;
            added = new List<HistoriaDokumentu>();
        }

        public void Add(HistoriaDokumentu item)
        {
            this.added.Add(item);
        }

        public void Refresh()
        {
            this._list = null;
            this.added.Clear();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(HistoriaDokumentu item)
        {
            bool c = this.list.Contains(item);
            if(!c)
                c = this.added.Contains(item);
            return c;
        }

        public void CopyTo(HistoriaDokumentu[] array, int arrayIndex)
        {
            var l = this.list.ToList();
            l.AddRange(this.added);
            l.CopyTo(array, arrayIndex);
        }

        public bool Remove(HistoriaDokumentu item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<HistoriaDokumentu> GetEnumerator()
        {
            //return this.list.GetEnumerator();
            foreach (var i in this.list)
                yield return i;
            foreach (var i in this.added)
                yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

    }
}
