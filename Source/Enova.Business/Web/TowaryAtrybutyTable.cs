using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Core;
using DBWeb = Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;
using System.Windows.Forms;

namespace Enova.Business.Old.Web
{
    public class TowaryAtrybutyTable : TableBase<DBWeb.TowarAtrybut>
    {
        #region Fields

        private TowarySortType sortType = TowarySortType.Kod;
        private SortOrder sortOrder = SortOrder.Ascending;
        public bool? aktywne;
        public bool? dostepne;
        public bool? visibleAV;
        public bool? disableAV;
        public DBWeb.KategoriaOld kategoria;
        private ObjectQuery<DBWeb.TowarAtrybut> baseQuery;

        #endregion

        #region Properties

        public bool? Aktywne
        {
            get { return aktywne; }
            set
            {
                if (aktywne != value)
                {
                    aktywne = value;
                    baseQuery = null;
                    ResetRows();
                }
            }
        }
        public bool? Dostepne
        {
            get { return dostepne; }
            set
            {
                if (dostepne != value)
                {
                    dostepne = value;
                    baseQuery = null;
                    ResetRows();
                }
            }
        }
        public bool? VisibleAV
        {
            get { return visibleAV; }
            set
            {
                if (visibleAV != value)
                {
                    visibleAV = value;
                    baseQuery = null;
                    ResetRows();
                }
            }
        }
        public bool? DisableAV
        {
            get { return disableAV; }
            set
            {
                if (disableAV != value)
                {
                    disableAV = value;
                    baseQuery = null;
                    ResetRows();
                }
            }
        }
        public DBWeb.KategoriaOld Kategoria
        {
            get { return kategoria; }
            set
            {
                if ((value == null && kategoria != null) || (value != null && kategoria == null) || value.ID != kategoria.ID)
                {
                    kategoria = value;
                    baseQuery = null;
                    ResetRows();
                }

            }
        }


        #endregion

        #region Properties

        public override ObjectQuery<DBWeb.TowarAtrybut> BaseQuery
        {
            get
            {
                if (baseQuery == null)
                {
                    IQueryable<DBWeb.TowarAtrybut> query;
                    var dc = (DBWeb.WebContext)this.DataContext;
                    if (Kategoria != null && Kategoria.Wlasciciel != null)
                    {
                        query = (from kp in Kategoria.RelationKategorieProduktow.CreateSourceQuery()
                                 join ta in dc.TowaryAtrybuty on kp.ProduktID equals ta.TowarID
                                 select ta);
                    }
                    else
                        query = dc.TowaryAtrybuty;

                    if (Aktywne != null)
                        query = query.Where(r => r.ROAktywny == Aktywne.Value);

                    if (Dostepne != null)
                        query = query.Where(r => r.RODostepny == Dostepne.Value);

                    if (VisibleAV != null)
                        query = query.Where(r => r.ROVisibleAV == VisibleAV.Value);

                    if (DisableAV != null)
                        query = query.Where(r => r.RODisableAV == DisableAV.Value);

                    baseQuery = (ObjectQuery<DBWeb.TowarAtrybut>)query;
                }
                return baseQuery;
            }
            set
            {
                base.BaseQuery = value;
            }
        }

        public TowarySortType SortType
        {
            get
            {
                return sortType;
            }
            set
            {
                sortType = value;
            }
        }

        public SortOrder SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        #endregion

        #region Methods

        public TowaryAtrybutyTable(DBWeb.WebContext dc, bool? aktywne, bool? dostepne, bool? visibleAV = true, bool? disableAV = false, DBWeb.KategoriaOld kategoria = null)
        {
            this.DataContext = dc;
            this.aktywne = aktywne;
            this.dostepne = dostepne;
            this.visibleAV = visibleAV;
            this.disableAV = disableAV;
            this.kategoria = kategoria;
        }

        public override int Find(System.ComponentModel.PropertyDescriptor property, object key)
        {
            string s = ((string)key).ToLower();
            int len = s.Length;
            int ret = -1;

            for (var i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];
                if (!string.IsNullOrWhiteSpace(((DBWeb.TowarAtrybut)row).SearchIndex) && row.SearchIndex.Length >= len && row.SearchIndex.ToLower().StartsWith(s))
                {
                    ret = i;
                    break;
                }
                if (!string.IsNullOrEmpty(row.TowarKod) && row.TowarKod.Length >= len && row.TowarKod.ToLower().StartsWith(s))
                {
                    ret = i;
                    break;
                }
            }

            if (ret == -1)
            {

                for (var i = 0; i < Rows.Count; i++)
                {
                    var row = Rows[i];
                    if ((!string.IsNullOrEmpty(row.TowarNazwa) && row.TowarNazwa.Length >= len && row.TowarNazwa.ToLower().Contains(s)) ||
                        (!string.IsNullOrEmpty(row.TowarKodDostawcy) && row.TowarKodDostawcy.Length >= len && row.TowarKodDostawcy.ToLower().Contains(s))
                        )
                    {
                        ret = i;
                        break;
                    }


                }
            }
            return ret;
        }

        protected override List<DBWeb.TowarAtrybut> PostLoadProcess(List<DBWeb.TowarAtrybut> list)
        {
            if (sortOrder == SortOrder.None)
                sortOrder = SortOrder.Ascending;
            switch (this.SortType)
            {
                case TowarySortType.Kod:
                    if (sortOrder == SortOrder.Ascending)
                        return list.OrderBy(r => r.TowarKod).ThenBy(r => r.TowarKodDostawcy).ThenBy(r => r.AtrybutNazwaPelna).ToList();
                    else
                        return list.OrderByDescending(r => r.TowarKod).ThenBy(r=>r.TowarKodDostawcy).ThenBy(r=>r.AtrybutNazwaPelna).ToList();
                case TowarySortType.Nazwa:
                    if (sortOrder == SortOrder.Ascending)
                        return list.OrderBy(r => r.TowarNazwa).ThenBy(r => r.AtrybutNazwaPelna).ToList();
                    else
                        return list.OrderByDescending(r => r.TowarNazwa).ThenBy(r => r.AtrybutNazwaPelna).ToList();
                default:
                    return base.PostLoadProcess(list);
            }
        }

        #endregion

        #region Nested types

        public enum TowarySortType { None, Kod, Nazwa, Atrybut, Cena };

        #endregion
    }
}
