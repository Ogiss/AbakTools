using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;
using System.Windows.Forms;

namespace Enova.Business.Old.Web
{
    public class ProduktyAtrybuty : TableBase<ProduktAtrybut>
    {
        ProduktySortOrder sortColumn = ProduktySortOrder.None;
        SortOrder sortOrder = SortOrder.None;

        public ProduktyAtrybuty(ObjectQuery<ProduktAtrybut> query) : base(query) { }

        public ProduktyAtrybuty() : base((ObjectQuery<ProduktAtrybut>)ContextManager.WebContext.ProduktyAtrybuty.Where(p => p.ProduktGrupujacy == false)) { }

        public ProduktyAtrybuty(KategoriaOld kategoria)
            : base((ObjectQuery<ProduktAtrybut>)ContextManager.WebContext.GetProduktyAtrybutyByKategoria(kategoria).Where(p => p.ProduktGrupujacy == false))
        {
        }

        public ProduktyAtrybuty(KategoriaOld kategoria, ProduktySortOrder sortColumn, SortOrder sortOrder, bool? active, bool? available)
            :base((ObjectQuery<ProduktAtrybut>)ContextManager.WebContext.GetProduktyAtrybutyByKategoria(kategoria)
            .Where(p => p.TowarEnova == false && p.ProduktGrupujacy == false && (active == null || p.IsActive == active.Value) && (available == null || p.IsAvailable == available.Value)))
        {
            this.sortColumn = sortColumn;
            this.sortOrder = sortOrder;
        }

        public ProduktyAtrybuty(KategoriaOld kategoria, ProduktySortOrder sortColumn, SortOrder sortOrder)
            : this(kategoria,sortColumn,sortOrder, true, null)
        {
        }


        public override int Find(System.ComponentModel.PropertyDescriptor property, object key)
        {
            string s = ((string)key).ToLower();
            int len = s.Length;
            int ret = -1;

            for (var i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];
                if(!string.IsNullOrWhiteSpace(row.SearchIndex) && row.SearchIndex.Length >= len && row.SearchIndex.ToLower().StartsWith(s))
                {
                    ret = i;
                    break;
                }
                if (!string.IsNullOrEmpty(row.Kod) && row.Kod.Length >= len && row.Kod.ToLower().StartsWith(s))
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
                    if ((!string.IsNullOrEmpty(row.Nazwa) && row.Nazwa.Length >= len && row.Nazwa.ToLower().Contains(s)) ||
                        (!string.IsNullOrEmpty(row.KodDostawcy) && row.KodDostawcy.Length >= len && row.KodDostawcy.ToLower().Contains(s))
                        )
                    {
                        ret = i;
                        break;
                    }

                        
                }
            }
            return ret;
        }

        protected override List<ProduktAtrybut> PostLoadProcess(List<ProduktAtrybut> list)
        {
            if (sortOrder != SortOrder.None)
            {
                if (sortColumn == ProduktySortOrder.Kod)
                {
                    if (sortOrder == SortOrder.Ascending)
                        return list.OrderBy(p => p.Kod).ThenBy(p=>p.KodDostawcy).ThenBy(p=>p.GrupaAtrubutowNazwa).ThenBy(p=>p.KolejnoscAtrybutu).ThenBy(p => p.AtrybutNazwa).ToList();
                    else
                        return list.OrderByDescending(p => p.Kod).ThenByDescending(p=>p.KodDostawcy).ThenByDescending(p=>p.GrupaAtrubutowNazwa)
                            .ThenByDescending(p=>p.KolejnoscAtrybutu).ThenByDescending(p => p.AtrybutNazwa).ToList();
                }
                else if (sortColumn == ProduktySortOrder.Nazwa)
                {
                    if (sortOrder == SortOrder.Ascending)
                        return list.OrderBy(p => p.Nazwa).ThenBy(p=>p.GrupaAtrubutowNazwa).ThenBy(p=>p.KolejnoscAtrybutu).ThenBy(p => p.AtrybutNazwa).ToList();
                    else
                        return list.OrderByDescending(p => p.Nazwa).ThenBy(p=>p.KolejnoscAtrybutu).ThenBy(p => p.AtrybutNazwa).ToList();
                }
                else if (sortColumn == ProduktySortOrder.Atrybut)
                {
                    if (sortOrder == SortOrder.Ascending)
                        return list.OrderBy(p=>p.GrupaAtrubutowNazwa).ThenBy(p=>p.KolejnoscAtrybutu).ThenBy(p => p.AtrybutNazwa).ToList();
                    else
                        return list.OrderByDescending(p => p.GrupaAtrubutowNazwa).ThenByDescending(p=>p.KolejnoscAtrybutu).ThenByDescending(p=>p.AtrybutNazwa).ToList();
                }
                else if (sortColumn == ProduktySortOrder.Cena)
                {
                    if (sortOrder == SortOrder.Ascending)
                        return list.OrderBy(p => p.CenaNetto).ToList();
                    else
                        return list.OrderByDescending(p => p.CenaNetto).ToList();
                }
            }
            //return (List<ProduktAtrybut>)list.Distinct(new ProductAttributeComparer());
            return list;
        }

        public class ProductAttributeComparer : IEqualityComparer<ProduktAtrybut>
        {
            public bool Equals(ProduktAtrybut pa1, ProduktAtrybut pa2)
            {
                return pa1.ID == pa2.ID && pa1.AtrybutProduktuID == pa2.AtrybutProduktuID;
            }

            public int GetHashCode(ProduktAtrybut pa)
            {
                int h = pa.ID ^ (pa.AtrybutProduktuID == null ? 0 : pa.AtrybutProduktuID.Value);
                return h.GetHashCode();
            }
        }
            
    }

    public enum ProduktySortOrder { None, Kod, Nazwa, Atrybut, Cena };
}
