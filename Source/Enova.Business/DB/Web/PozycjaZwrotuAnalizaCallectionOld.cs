using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public class PozycjaZwrotuAnalizaCallectionOld : CollectionBase
    {
        #region Properties

        public PozycjaZwrotuAnalizaOld this[int ident]
        {
            get
            {
                foreach (var item in this.List)
                {
                    if (((PozycjaZwrotuAnalizaOld)item).Ident == ident)
                        return (PozycjaZwrotuAnalizaOld)item;
                }
                return null;
            }
        }

        #endregion

        #region Methods

        public bool Contains(int ident)
        {
            var p = this[ident];
            return p != null;
        }

        public bool Contains(PozycjaZwrotuAnalizaOld poz)
        {
            return this.Contains(poz.Ident);
        }

        public void Add(PozycjaZwrotuAnalizaOld poz)
        {
            if (poz != null && !this.Contains(poz))
                this.List.Add(poz);
        }

        #endregion
    }
}
