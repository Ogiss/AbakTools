using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public class ZwrotAnalizaDokHandlowyCollectionOld : CollectionBase
    {
        #region Properties

        public ZwrotAnalizaDokHandlowyOld this[DokumentHandlowy dh]
        {
            get
            {
                foreach (var i in this.List)
                {
                    if (((ZwrotAnalizaDokHandlowyOld)i).DokumentHandlowy.Equals(dh))
                        return (ZwrotAnalizaDokHandlowyOld)i;
                }
                return null;
            }
        }

        #endregion

        public ZwrotAnalizaDokHandlowyCollectionOld() { }

        public ZwrotAnalizaDokHandlowyCollectionOld(IEnumerable<ZwrotAnalizaDokHandlowyOld> list)
        {
            foreach (var item in list)
                this.List.Add(item);
        }

        public ZwrotAnalizaDokHandlowyCollectionOld(IEnumerable<DokumentHandlowy> list, PozycjaZwrotuAnalizaOld pozycja)
        {
            foreach (var dh in list)
                this.Add(dh, pozycja);
        }

        public ZwrotAnalizaDokHandlowyCollectionOld(IEnumerable<DokumentHandlowy> list) : this(list, null) { }

        public bool Contains(DokumentHandlowy dh)
        {
            var item = this[dh];

            return item != null;
        }

        private void AddItem(ZwrotAnalizaDokHandlowyOld item)
        {
            this.List.Add(item);
        }

        public ZwrotAnalizaDokHandlowyOld Add(DokumentHandlowy dh, PozycjaZwrotuAnalizaOld pozycja)
        {
            var item = this[dh];
            if (item == null)
            {
                item = new ZwrotAnalizaDokHandlowyOld(dh);
                this.List.Add(item);
            }
            if (pozycja != null)
                item.Pozycje.Add(pozycja);
            return item;
        }

        public ZwrotAnalizaDokHandlowyOld Add(DokumentHandlowy dh)
        {
            return this.Add(dh, null);
        }

        public ZwrotAnalizaDokHandlowyCollectionOld GetOrderByDataDesc()
        {
            var list = this.List.Cast<ZwrotAnalizaDokHandlowyOld>().OrderByDescending(d => d.Data);
            return new ZwrotAnalizaDokHandlowyCollectionOld(list);
        }

        public ZwrotAnalizaDokHandlowyCollectionOld GetOrderByDescIloscPozycji()
        {
            var list = this.List.Cast<ZwrotAnalizaDokHandlowyOld>().OrderByDescending(d => d.IloscPozycjiZwrotu);
            return new ZwrotAnalizaDokHandlowyCollectionOld(list);
        }

        public ZwrotAnalizaDokHandlowyCollectionOld GetContainsPozycja(PozycjaZwrotuAnalizaOld pozycja)
        {
            ZwrotAnalizaDokHandlowyCollectionOld list = new ZwrotAnalizaDokHandlowyCollectionOld();
            foreach (ZwrotAnalizaDokHandlowyOld item in this.List)
            {
                if (item.Contains(pozycja))
                    list.AddItem(item);
            }
            return list;
        }

        public int IndexOf(DokumentHandlowy dh)
        {
            for (var i = 0; i < this.List.Count; i++)
                if (((ZwrotAnalizaDokHandlowyOld)this.List[i]).DokumentHandlowy.Equals(dh))
                    return i;
            return -1;
        }

    }
}
