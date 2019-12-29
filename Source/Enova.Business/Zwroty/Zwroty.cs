using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.Zwroty
{
    public class Zwroty : Enova.Business.Old.Core.TableBase<Zwrot>, Core.ICreateNewRecord
    {
        #region Fields

        private RelationKontrahent wgKontrahent;
        private WgDatyDodaniaKey wgDatyDodania;
        private WgPrzedstawicielKey wgPrzedstawiciel;
        private WgSezonKey wgSezon;

        #endregion

        #region Properties

        public RelationKontrahent WgKontrahent
        {
            get { return this.wgKontrahent; }
        }

        public WgDatyDodaniaKey WgDatyDodania
        {
            get { return this.wgDatyDodania; }
        }

        public WgPrzedstawicielKey WgPrzedstawiciel
        {
            get { return this.wgPrzedstawiciel; }
        }

        public WgSezonKey WgSezon
        {
            get { return this.wgSezon; }
        }

        #endregion


        public Zwroty(WebContext dc) :
            this((ObjectQuery<Zwrot>)dc.Zwroty.Include("OstatniStatus").Include("Kontrahent")
            .Where(z=>z.Deleted == false && z.Synchronizacja != (int)RowSynchronizeOld.Notsaved && 
                z.Synchronizacja != (int)RowSynchronizeOld.Synchronizing && z.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete))
        {
            this.DataContext = dc;
        }

        public Zwroty(WebContext dc, DateTime? dataOd, DateTime? dataDo, int? kontrahentID, bool? deleted)
            : this((ObjectQuery<Zwrot>)dc.Zwroty.Include("OstatniStatus").Include("Kontrahent").Where(z => (kontrahentID == null || z.KontrahentID == kontrahentID) &&
                (deleted == null || z.Deleted == deleted) && (dataOd == null || z.DataDodania >= dataOd) && (dataDo == null || z.DataDodania <= dataDo)))
        {
            this.DataContext = dc;
        }

        public Zwroty(ObjectQuery<Zwrot> query)
            : base(query)
        {
            this.MergeOption = System.Data.Objects.MergeOption.OverwriteChanges;
            this.wgKontrahent = new RelationKontrahent(this);
            this.wgDatyDodania = new WgDatyDodaniaKey(this);
            this.wgPrzedstawiciel = new WgPrzedstawicielKey(this);
            this.wgSezon = new WgSezonKey(this);
        }


        public override object CreateNewRecord()
        {
            var now = DateTime.Now;

            return new Zwrot()
            {
                DataDodania = now,
                DataModyfikacji = now,
                Guid = Guid.NewGuid(),
                Deleted = false,
                Synchronizacja = (int)Types.RowSynchronizeOld.NotsynchronizedNew,
                WartoscNetto = 0
            };
        }

        protected override List<Zwrot> PostLoadProcess(List<Zwrot> list)
        {
            return list.OrderBy(z => z.Kolejnosc).ThenByDescending(z => z.DataDodania).ToList();
        }

        #region Nested Types

        public class RelationKontrahent : Key<Zwrot>
        {
            public RelationKontrahent(TableBase<Zwrot> table)
                : base(table)
            {
            }

            public Zwroty this[Kontrahent kontrahent]
            {
                get
                {
                    if (kontrahent == null)
                        return new Zwroty(this.GetQuery(BaseQuery));
                    return new Zwroty(this.GetQuery(BaseQuery.Where(z => z.Kontrahent.CzyAgent == false && z.Kontrahent.ID == kontrahent.ID)));
                    
                }
            }

            public Zwroty this[Guid[] guids]
            {
                get
                {
                    return new Zwroty(this.GetQuery(BaseQuery.Where(z => z.Kontrahent != null && z.Kontrahent.Guid != null && guids.Contains(z.Kontrahent.Guid.Value))));
                }
            }
        }

        public class WgDatyDodaniaKey : Key<Zwrot>
        {
            public WgDatyDodaniaKey(TableBase<Zwrot> table)
                : base(table)
            {
            }

            public Zwroty this[DateTime? dataOd, DateTime? dataDo, bool nieZalatwione = false]
            {
                get
                {
                    DateTime? from = ((dataOd == null) ? (DateTime?)null : dataOd.Value.Date);
                    DateTime? to = ((dataDo == null) ? (DateTime?)null : dataDo.Value.AddDays(1).Date);
                    if (nieZalatwione)
                    {
                        return new Zwroty(GetQuery(BaseQuery.Where(z =>
                            (z.OstatniStatus.Typ != (int)TypStatusuZwrotu.Załatwiony || from == null || z.DataDodania >= from) &&
                            (to == null || z.DataDodania < to))));
                    }
                    return new Zwroty(GetQuery(BaseQuery
                        .Where(z => (from == null || z.DataDodania >= from) &&
                            (to == null || z.DataDodania < to))));
                }
            }

            public Zwroty this[DateTime? dataOd]
            {
                get { return this[dataOd, null]; }
            }
        }

        public class WgPrzedstawicielKey : Key<Zwrot>
        {
            public WgPrzedstawicielKey(TableBase<Zwrot> table)
                : base(table)
            {
            }

            public Zwroty this[string przedstawiciel]
            {
                get
                {
                    if (string.IsNullOrEmpty(przedstawiciel))
                        return (Zwroty)Table;

                    return new Zwroty(this.GetQuery(BaseQuery
                        .Where(z => z.Kontrahent.CzyAgent == false && z.Kontrahent.Przedstawiciel != null && z.Kontrahent.Przedstawiciel.Kod == przedstawiciel)));

                }
            }
        }

        public class WgSezonKey : Key<Zwrot>
        {
            public WgSezonKey(TableBase<Zwrot> table)
                : base(table)
            {
            }

            public Zwroty this[string sezon]
            {
                get
                {
                    if (string.IsNullOrEmpty(sezon))
                        return (Zwroty)Table;
                    return new Zwroty(this.GetQuery(BaseQuery.Where(z => z.Sezon == sezon || z.Sezon2 == sezon || z.Sezon3 == sezon || z.Sezon4 == sezon)));
                }
            }
        }

        public class WgTrasyKey : Key<Zwrot>
        {
            public WgTrasyKey(TableBase<Zwrot> table)
                : base(table)
            {
            }


        }
    

        #endregion
    }
}
