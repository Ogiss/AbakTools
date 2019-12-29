using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Zwroty
{
    public class PozycjeAnalizyZwrotu : SortedSet<PozycjaAnalizyZwrotu>
    {
        #region Fields

        internal bool removeWithDocument = false;

        #endregion

        #region Properties
        #endregion

        #region Methods

        public PozycjeAnalizyZwrotu() { }

        public PozycjeAnalizyZwrotu(IEnumerable<PozycjaAnalizyZwrotu> list) : base(list)
        {
        }

        public bool Remove(PozycjaAnalizyZwrotu item)
        {
            if (this.Contains(item))
            {
                if (this.removeWithDocument)
                    item.RemoveDokumenty();
                return base.Remove(item);
                    
            }
            return false;
        }
        
        public void Renumeruj()
        {
            int i = 1;
            foreach (var p in this)
                p.ident = i++;

        }


        #endregion
    }
}
