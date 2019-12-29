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
    public partial class RelacjeHandlowe : TableBase<RelacjaHandlowa>
    {
        #region Fields

        private DefinicjaRelation definicjaRelation;
        private PodrzednyRelation podrzednyRelation;
        private NadrzednyRelation nadrzednyRelation;
        private WgPodrzednyTypKey keyWgPodrzednyTyp;
        private WgTypRelacjiKey keyWgTypRelacji;

        #endregion

        #region Properties

        public DefinicjaRelation WgDefinicja
        {
            get { return this.definicjaRelation; }
        }

        public PodrzednyRelation WgPodrzedny
        {
            get { return this.podrzednyRelation; }
        }

        public NadrzednyRelation WgNadrzedny
        {
            get { return this.nadrzednyRelation; }
        }

        public WgPodrzednyTypKey WgPodrzednyTyp
        {
            get { return this.keyWgPodrzednyTyp; }
        }

        public WgTypRelacjiKey WgTypRelacji
        {
            get { return this.keyWgTypRelacji; }
        }

        #endregion

        #region Methods

        public RelacjeHandlowe()
        {
            this.definicjaRelation = new DefinicjaRelation(this);
            this.podrzednyRelation = new PodrzednyRelation(this);
            this.nadrzednyRelation = new NadrzednyRelation(this);
            this.keyWgPodrzednyTyp = new WgPodrzednyTypKey(this);
            this.keyWgTypRelacji = new WgTypRelacjiKey(this);
        }

        protected override ObjectQuery<RelacjaHandlowa> CreateQuery()
        {
            return ((HandelModule)this.Module).DataContext.RelacjeHandlowe;
        }


        #endregion

        #region Nested Types

        public class DefinicjaRelation : Key<RelacjaHandlowa>
        {
            public DefinicjaRelation(TableBase<RelacjaHandlowa> table) : base(table) { }

            public RelacjeHandlowe this[DefRelacjiHandlowej definicja]
            {
                get { return new RelacjeHandlowe() { BaseQuery = GetQuery(Table.BaseQuery.Where(rh => rh.Definicja.ID == definicja.ID)) }; }
            }
        }

        public class PodrzednyRelation : Key<RelacjaHandlowa>
        {
            internal PodrzednyRelation(TableBase<RelacjaHandlowa> table) : base(table) { }

            public RelacjeHandlowe this[DokumentHandlowy podrzedny]
            {
                get
                {
                    return new RelacjeHandlowe() { BaseQuery = GetQuery(Table.BaseQuery.Where(rh => rh.Podrzedny.ID == podrzedny.ID)) };
                }
            }

            public RelacjeHandlowe this[DokumentHandlowy podrzedny, DefRelacjiHandlowej definicja]
            {
                get
                {
                    return new RelacjeHandlowe() { BaseQuery = GetQuery(Table.BaseQuery.Where(rh => rh.Podrzedny.ID == podrzedny.ID && rh.Definicja.ID == definicja.ID)) };
                }
            }
        }

        public class NadrzednyRelation : Key<RelacjaHandlowa>
        {
            internal NadrzednyRelation(TableBase<RelacjaHandlowa> table) : base(table) { }

            public RelacjeHandlowe this[DokumentHandlowy nadrzedny]
            {
                get
                {
                    return new RelacjeHandlowe() { BaseQuery = GetQuery(this.Table.BaseQuery.Where(rh => rh.Nadrzedny.ID == nadrzedny.ID)) };
                }
            }

            public RelacjeHandlowe this[DokumentHandlowy nadrzedny, DefRelacjiHandlowej definicja]
            {
                get
                {
                    return new RelacjeHandlowe() { BaseQuery = GetQuery(Table.BaseQuery.Where(rh => rh.Nadrzedny.ID == nadrzedny.ID && rh.Definicja.ID == definicja.ID)) };
                }
            }
        }

        public class WgPodrzednyTypKey : Key<RelacjaHandlowa>
        {
            internal WgPodrzednyTypKey(TableBase<RelacjaHandlowa> table) : base(table) { }

            public RelacjeHandlowe this[DokumentHandlowy podrzedny, TypRelacjiHandlowej typ]
            {
                get { return new RelacjeHandlowe() { BaseQuery = GetQuery(Table.BaseQuery.Where(rh => rh.Podrzedny.ID == podrzedny.ID && rh.TypInt == (int)typ)) }; }
            }
        }

        public class WgTypRelacjiKey : Key<RelacjaHandlowa>
        {
            internal WgTypRelacjiKey(TableBase<RelacjaHandlowa> table) : base(table) { }

            public RelacjeHandlowe this[TypRelacjiHandlowej typ]
            {
                get { return new RelacjeHandlowe() { BaseQuery = GetQuery(Table.BaseQuery.Where(rh => rh.TypInt == (int)typ)) }; }
            }
        }

        #endregion
    }
}
