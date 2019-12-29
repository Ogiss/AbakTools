using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB;

namespace Enova.Old.Handel
{
    public partial class DokHandlowe : EnovaGuidedTable<DokumentHandlowy>
    {
        #region Fields

        private NumerWgNumeruDokumentuKey numerWgNumeruDokumentu;
        private NumerWgSymboluDokumentuKey numerWgSymboluDokumentu;
        
        #endregion

        #region Properties

        public NumerWgNumeruDokumentuKey NumerWgNumeruDokumentu
        {
            get { return this.numerWgNumeruDokumentu; }
        }

        public NumerWgSymboluDokumentuKey NumerWgSymboluDokumentu
        {
            get { return this.numerWgSymboluDokumentu; }
        }

        #endregion

        #region Methods

        public DokHandlowe()
        {
            this.numerWgNumeruDokumentu = new NumerWgNumeruDokumentuKey(this);
            this.numerWgSymboluDokumentu = new NumerWgSymboluDokumentuKey(this);
        }

        protected override ObjectQuery<DokumentHandlowy> CreateQuery()
        {
            return ((HandelModule)this.Module).DataContext.DokHandlowe;
        }


        #endregion

        #region NestedTypes

        public class NumerWgNumeruDokumentuKey : Key<DokumentHandlowy>
        {
            public NumerWgNumeruDokumentuKey(TableBase<DokumentHandlowy> table)
                : base(table)
            {
                this.InitFields("NumerPelny");
                this.Unique = true;
            }

            public DokumentHandlowy this[string pelny]
            {
                get
                {
                    return this.Find(pelny);
                }
            }
        }

        public class NumerWgSymboluDokumentuKey : Key<DokumentHandlowy>
        {
            public NumerWgSymboluDokumentuKey(TableBase<DokumentHandlowy> table)
                : base(table)
            {
                this.InitFields("NumerSymbol", "NumerNumer");
                this.Unique = true;
            }

            public DokHandlowe this[string symbol]
            {
                get
                {
                    return new DokHandlowe() { BaseQuery = GetQuery(BaseQuery.Where(dh => dh.NumerSymbol == symbol)) };
                }
            }

            public DokumentHandlowy this[string symbol, int numer]
            {
                get
                {
                    return this[symbol].BaseQuery.Where(dh => dh.NumerNumer == numer).FirstOrDefault();
                }
            }

        }

        #endregion
    }
}
