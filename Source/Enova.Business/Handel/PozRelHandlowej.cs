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
    public partial class PozRelHandlowej : TableBase<PozycjaRelacjiHandlowej>
    {
        #region Fields

        internal bool KasujPodrzędny = true;
        internal int PozycjePoJednej;

        private WgRelacjaNadrzednaPodrzednaIdentKey keyWgRelacjaNadrzednaPodrzednaIdent;
        private NadrzednyDokRelation relationNadrzednyDok;
        private PodrzednyDokRelation relationPodrzednyDok;
        private RelacjaRelation relationRelacja;

        #endregion

        #region Properties

        public WgRelacjaNadrzednaPodrzednaIdentKey WgRelacjaNadrzednaPodrzednaIdent
        {
            get
            {
                return this.keyWgRelacjaNadrzednaPodrzednaIdent;
            }
        }
        public NadrzednyDokRelation WgNadrzednyDok
        {
            get
            {
                return this.relationNadrzednyDok;
            }
        }
        public PodrzednyDokRelation WgPodrzednyDok
        {
            get
            {
                return this.relationPodrzednyDok;
            }
        }
        public RelacjaRelation WgRelacja
        {
            get
            {
                return this.relationRelacja;
            }
        }

        #endregion

        #region Methods

        public PozRelHandlowej()
        {
            this.keyWgRelacjaNadrzednaPodrzednaIdent = new WgRelacjaNadrzednaPodrzednaIdentKey(this);
            this.relationNadrzednyDok = new NadrzednyDokRelation(this);
            this.relationPodrzednyDok = new PodrzednyDokRelation(this);
        }

        protected override ObjectQuery<PozycjaRelacjiHandlowej> CreateQuery()
        {
            return ((HandelModule)this.Module).DataContext.PozRelHandlowej;
        }


        #endregion

        #region Nested Types

        public class WgRelacjaNadrzednaPodrzednaIdentKey : Key<PozycjaRelacjiHandlowej>
        {
            public WgRelacjaNadrzednaPodrzednaIdentKey(TableBase<PozycjaRelacjiHandlowej> table) : base(table) { }


            public PozRelHandlowej this[RelacjaHandlowa relacja]
            {
                get
                {
                    return new PozRelHandlowej() { BaseQuery = GetQuery(Table.BaseQuery.Where(p => p.Relacja.ID == relacja.ID)) };
                }
            }

            public PozRelHandlowej this[RelacjaHandlowa relacja, int nadrzednaident]
            {
                get
                {
                    return new PozRelHandlowej()
                    {
                        BaseQuery = GetQuery(Table.BaseQuery
                            .Where(p => p.Relacja.ID == relacja.ID && p.NadrzednaIdent == nadrzednaident))
                    };
                }
            }

            public PozRelHandlowej this[RelacjaHandlowa relacja, int nadrzednaident, int podrzednaident]
            {
                get
                {
                    return new PozRelHandlowej()
                    {
                        BaseQuery = GetQuery(Table.BaseQuery
                            .Where(p => p.Relacja.ID == relacja.ID && p.NadrzednaIdent == nadrzednaident && p.PodrzednaIdent == podrzednaident))
                    };
                }
            }

        
        }

        public class NadrzednyDokRelation : Key<PozycjaRelacjiHandlowej>
        {
            public NadrzednyDokRelation(TableBase<PozycjaRelacjiHandlowej> table) : base(table) { }

            public PozRelHandlowej this[DokumentHandlowy nadrzednydok]
            {
                get
                {
                    return new PozRelHandlowej() { BaseQuery = GetQuery(Table.BaseQuery.Where(p => p.NadrzednyDok.ID == nadrzednydok.ID)) };
                }
            }

            public PozRelHandlowej this[DokumentHandlowy nadrzednydok, int nadrzednaident]
            {
                get
                {
                    return new PozRelHandlowej()
                    {
                        BaseQuery = GetQuery(Table.BaseQuery
                            .Where(p => p.NadrzednyDok.ID == nadrzednydok.ID && p.NadrzednaIdent == nadrzednaident))
                    };
                }
            }
        }

        public class PodrzednyDokRelation : Key<PozycjaRelacjiHandlowej>
        {
            public PodrzednyDokRelation(TableBase<PozycjaRelacjiHandlowej> table) : base(table) { }

            public PozRelHandlowej this[DokumentHandlowy podrzednydok]
            {
                get
                {
                    return new PozRelHandlowej()
                    {
                        BaseQuery = GetQuery(Table.BaseQuery
                            .Where(p => p.PodrzednyDok.ID == podrzednydok.ID))
                    };
                }
            }

            public PozRelHandlowej this[DokumentHandlowy podrzednydok, int podrzednaident]
            {
                get
                {
                    return new PozRelHandlowej()
                    {
                        BaseQuery = GetQuery(Table.BaseQuery
                            .Where(p => p.PodrzednyDok.ID == podrzednydok.ID && p.PodrzednaIdent == podrzednaident))
                    };
                }
            }
        }

        public class RelacjaRelation : Key<PozycjaRelacjiHandlowej>
        {
            public RelacjaRelation(TableBase<PozycjaRelacjiHandlowej> table) : base(table) { }

            public PozRelHandlowej this[RelacjaHandlowa relacja]
            {
                get
                {
                    return new PozRelHandlowej()
                    {
                        BaseQuery = GetQuery(Table.BaseQuery
                            .Where(p => p.Relacja.ID == relacja.ID))
                    };
                }
            }
        }

        #endregion
    }
}
