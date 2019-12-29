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
    public class ProduktyAtrybutyAV : TableBase<ProduktAtrybut>
    {
        ProduktySortOrder sortColumn = ProduktySortOrder.None;
        SortOrder sortOrder = SortOrder.None;

        public ProduktyAtrybutyAV() : this(null, ProduktySortOrder.None, SortOrder.None, null, null) { }

        public ProduktyAtrybutyAV(KategoriaOld kategoria)
            : this(kategoria, ProduktySortOrder.None, SortOrder.None, null, null)
        {
        }

        public ProduktyAtrybutyAV(KategoriaOld kategoria, ProduktySortOrder sortColumn, SortOrder sortOrder, bool? active, bool? available)
            :base()
        {
            this.BaseQuery = this.GetQuery(kategoria, active, available);
            this.sortColumn = sortColumn;
            this.sortOrder = sortOrder;
        }

        public ProduktyAtrybutyAV(KategoriaOld kategoria, ProduktySortOrder sortColumn, SortOrder sortOrder)
            : this(kategoria,sortColumn,sortOrder, true, null)
        {
        }

        public ObjectQuery<ProduktAtrybut> GetQuery(KategoriaOld kategoria, bool? active, bool? avaliable)
        {
            var dc = ContextManager.WebContext;
            /*
            var query = (ObjectQuery<ProduktAtrybut>)(from pa in dc.GetProduktyAtrybutyByKategoria(kategoria)
                                                      join pav in dc.ProductAvaliables on
                                                      new { IDProduct = pa.ID, IDProductAttribute = pa.AtrybutProduktuID } equals
                                                      new { IDProduct = pav.IDProduct, IDProductAttribute = pav.IDProductAttribute }
                                                      where pav.Visible == true && pa.ProduktGrupujacy == false && pa.TowarEnova == false && 
                                                      pa.TowarDisableAVList == false && pa.AtrybutDisableAVList == false
                                                      && (active == null || pa.IsActive == active.Value)
                                                      && (avaliable == null || pa.IsAvailable == avaliable.Value)
                                                      select pa);
             */
            var query = (ObjectQuery<ProduktAtrybut>)(from pa in dc.GetProduktyAtrybutyByKategoria(kategoria)
                                                      where pa.AvailableVisible == true && pa.ProduktGrupujacy == false && pa.TowarEnova == false &&
                                                      pa.TowarDisableAVList == false && pa.AtrybutDisableAVList == false
                                                      && (active == null || pa.IsActive == active.Value)
                                                      && (avaliable == null || pa.IsAvailable == avaliable.Value)
                                                      select pa);

            return query;
        }


        public override int Find(System.ComponentModel.PropertyDescriptor property, object key)
        {
            string s = ((string)key).ToLower();
            int len = s.Length;
            int ret = -1;

            for (var i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];
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
                        return list.OrderBy(p => p.Kod).ThenBy(p=>p.KolejnoscAtrybutu).ThenBy(p => p.AtrybutNazwa).ToList();
                    else
                        return list.OrderByDescending(p => p.Kod).ThenBy(p=>p.KolejnoscAtrybutu).ThenBy(p => p.AtrybutNazwa).ToList();
                }
                else if (sortColumn == ProduktySortOrder.Nazwa)
                {
                    if (sortOrder == SortOrder.Ascending)
                        return list.OrderBy(p => p.Nazwa).ThenBy(p=>p.KolejnoscAtrybutu).ThenBy(p => p.AtrybutNazwa).ToList();
                    else
                        return list.OrderByDescending(p => p.Nazwa).ThenBy(p=>p.KolejnoscAtrybutu).ThenBy(p => p.AtrybutNazwa).ToList();
                }
                else if (sortColumn == ProduktySortOrder.Atrybut)
                {
                    if (sortOrder == SortOrder.Ascending)
                        return list.OrderBy(p => p.AtrybutNazwa).ToList();
                    else
                        return list.OrderByDescending(p => p.AtrybutNazwa).ToList();
                }
                else if (sortColumn == ProduktySortOrder.Cena)
                {
                    if (sortOrder == SortOrder.Ascending)
                        return list.OrderBy(p => p.CenaNetto).ToList();
                    else
                        return list.OrderByDescending(p => p.CenaNetto).ToList();
                }
            }
            return list;
        }
            
    }

}
