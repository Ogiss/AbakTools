using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.Zwroty
{
    public class DokumentyAnalizyZwrotu : SortedSet<DokumentAnalizyZwrotu>
    {
        #region Fields

        private AnalizaZwrotu analiza;


        #endregion

        #region Properties

        public DokumentAnalizyZwrotu this[DokumentHandlowy dh]
        {
            get
            {
                foreach (var d in this)
                    if (d.DokumentHandlowy.Guid == dh.Guid)
                        return d;
                return null;
            }
        }

        public DokumentAnalizyZwrotu this[int index]
        {
            get
            {
                int idx = 0;
                foreach (var dok in this)
                {
                    if (idx == index)
                        return dok;
                    idx++;
                }
                return null;
            }
        }


        #endregion

        #region Methods

        public DokumentyAnalizyZwrotu(AnalizaZwrotu analiza)
        {
            this.analiza = analiza;
        }

        public DokumentyAnalizyZwrotu(AnalizaZwrotu analiza, IComparer<DokumentAnalizyZwrotu> comparer)
            : base(comparer)
        {
            this.analiza = analiza;
        }

        public DokumentyAnalizyZwrotu(AnalizaZwrotu analiza, IEnumerable<DokumentAnalizyZwrotu> collection, IComparer<DokumentAnalizyZwrotu> comparer)
            : base(collection, comparer)
        {
            this.analiza = analiza;
        }

        public void Add(DokumentHandlowy dh, PozycjaAnalizyZwrotu pozycja)
        {
            var item = this[dh];
            if (item == null)
            {
                item = new DokumentAnalizyZwrotu(analiza, dh);
                this.Add(item);
            }

            item.Pozycje.Add(pozycja);
            pozycja.Dokumenty.Add(item);
        }


        #endregion

    }
}
