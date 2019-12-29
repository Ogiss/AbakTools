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
    public partial class PozycjeDokHan : TableBase<PozycjaDokHandlowego>
    {
        #region Fields

        private WgDefDokHanKey wgDefDokHan;
        private WgDatyKey wgDaty;
        private WgKontrahentaKey wgKontrahenta;
        private WgStanuDokHanKey wgStanuDokHan;
        private WgIdentKey wgIdentKey;
        private DokumentRelation wgDokument;
        private TowarRelation wgTowar;

        #endregion

        #region Properties

        public WgDefDokHanKey WgDefDokHan
        {
            get { return this.wgDefDokHan; }
        }

        public WgDatyKey WgDaty
        {
            get { return this.wgDaty; }
        }

        public WgKontrahentaKey WgKontrahenta
        {
            get { return this.wgKontrahenta; }
        }

        public WgStanuDokHanKey WgStanuDokHan
        {
            get { return this.wgStanuDokHan; }
        }

        public DokumentRelation WgDokument
        {
            get { return this.wgDokument; }
        }

        public TowarRelation WgTowar
        {
            get { return this.wgTowar; }
        }

        public WgIdentKey WgIdent
        {
            get { return this.wgIdentKey; }
        }

        #endregion

        #region Methods

        public PozycjeDokHan()
        {
            this.wgDefDokHan = new WgDefDokHanKey(this);
            this.wgDaty = new WgDatyKey(this);
            this.wgKontrahenta = new WgKontrahentaKey(this);
            this.wgStanuDokHan = new WgStanuDokHanKey(this);
            this.wgIdentKey = new WgIdentKey(this);
            this.wgDokument = new DokumentRelation(this);
            this.wgTowar = new TowarRelation(this);
        }

        protected override ObjectQuery<PozycjaDokHandlowego> CreateQuery()
        {
            return ((HandelModule)this.Module).DataContext.PozycjeDokHan;
        }

        #endregion

        #region Nested Types

        public class WgDefDokHanKey : Key<PozycjaDokHandlowego>
        {
            #region Methods

            internal WgDefDokHanKey(TableBase<PozycjaDokHandlowego> table)
                : base(table)
            {
            }

            public PozycjeDokHan this[DefDokHandlowego def]
            {
                get
                {
                    return new PozycjeDokHan() { BaseQuery = GetQuery(Table.BaseQuery.Where(p => p.Dokument.RelationDefinicja.ID == def.ID)) };
                }
            }

            #endregion
        }

        public class WgDatyKey : Key<PozycjaDokHandlowego>
        {
            internal WgDatyKey(TableBase<PozycjaDokHandlowego> table)
                : base(table)
            {
            }

            public PozycjeDokHan this[DateTime dataOd]
            {
                get
                {
                    return new PozycjeDokHan() { BaseQuery = GetQuery(Table.BaseQuery.Where(p => p.Data >= dataOd.Date)) };
                }
            }

            public PozycjeDokHan this[DateTime dataOd, DateTime dataDo]
            {
                get
                {
                    var to = dataOd.Date.AddDays(1);
                    return new PozycjeDokHan() { BaseQuery = GetQuery(Table.BaseQuery.Where(p => p.Data >= dataOd.Date && p.Data < to)) };
                }
            }
        }

        public class WgKontrahentaKey : Key<PozycjaDokHandlowego>
        {
            internal WgKontrahentaKey(TableBase<PozycjaDokHandlowego> table) : base(table) { }

            public PozycjeDokHan this[Kontrahent kontrahent]
            {
                get
                {
                    return new PozycjeDokHan() { BaseQuery = GetQuery(Table.BaseQuery.Where(p => p.Dokument.Kontrahent.ID == kontrahent.ID)) };
                }
            }
        }

        public class WgStanuDokHanKey : Key<PozycjaDokHandlowego>
        {
            public WgStanuDokHanKey(TableBase<PozycjaDokHandlowego> table) : base(table) { }

            public PozycjeDokHan this[StanDokumentuHandlowego stan]
            {
                get
                {
                    var query = this.Table.BaseQuery;
                    if (stan == StanDokumentuHandlowego.Zatwierdzony)
                        return new PozycjeDokHan() { Module = Table.Module , BaseQuery = GetQuery(query.Where(p => p.Dokument.StanInt == (int)stan || p.Dokument.StanInt == (int)StanDokumentuHandlowego.Zablokowany)) };
                    else
                        return new PozycjeDokHan() { Module = Table.Module , BaseQuery = GetQuery(query.Where(p => p.Dokument.StanInt == (int)stan)) };
                }
            }
        }

        public class WgIdentKey : Key<PozycjaDokHandlowego>
        {
            public WgIdentKey(TableBase<PozycjaDokHandlowego> table) : base(table) { }

            public PozycjeDokHan this[int ident]
            {
                get
                {
                    return new PozycjeDokHan() { BaseQuery = GetQuery(Table.BaseQuery.Where(p => p.Ident == ident)) };
                }
            }

            public PozycjaDokHandlowego this[DokumentHandlowy dh, int ident]
            {
                get
                {
                    return this.Table.BaseQuery.Where(p => p.Dokument.ID == dh.ID && p.Ident == ident).FirstOrDefault();
                }
            }
        }

        public class DokumentRelation : Key<PozycjaDokHandlowego>
        {
            public DokumentRelation(TableBase<PozycjaDokHandlowego> table) : base(table) { }

            public PozycjeDokHan this[DokumentHandlowy dokument]
            {
                get
                {
                    return new PozycjeDokHan() { BaseQuery = GetQuery(Table.BaseQuery.Where(p => p.Dokument.ID == dokument.ID)) };
                }
            }
        }

        public class TowarRelation : Key<PozycjaDokHandlowego>
        {
            public TowarRelation(TableBase<PozycjaDokHandlowego> table) : base(table) { }

            public PozycjeDokHan this[Towar towar]
            {
                get
                {
                    return new PozycjeDokHan() { BaseQuery = GetQuery(Table.BaseQuery.Where(p => p.Towar.ID == towar.ID)) };
                }
            }
        }


        #endregion
    }
}
