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

namespace Enova.Business.Old.Web
{
    public class Produkty : TableBase<Produkt>
    {
        public Produkty()
            : base((ObjectQuery<Produkt>)ContextManager.WebContext.Produkty.Include("RelationStawkaVat").Where(p => p.AktywnyOld == true && p.ProduktGrupujacy == false)) { }


        public Produkty(KategoriaOld kategoria)
            : base((ObjectQuery<Produkt>)ContextManager.WebContext.GetProduktyByKategoria(kategoria).Include("RelationStawkaVat")
                .Where(p => p.AktywnyOld == true && p.ProduktGrupujacy == false))
        {
        }

        public Produkty(KategoriaOld kategoria, bool? aktywny, bool? gotowy, bool? widoczny, int? synchronizacja, bool withRoot, bool? towarEnova = null, int? dostawcaID = null)
            : base((ObjectQuery<Produkt>)ContextManager.WebContext.GetProduktyByKategoria(kategoria, withRoot).Include("RelationStawkaVat")
            .Where(p => (aktywny == null || p.AktywnyOld == aktywny) && (gotowy == null || p.Gotowy == gotowy) && (widoczny == null || p.Widoczny == widoczny)
                && (towarEnova == null || p.TowarEnova == towarEnova) && (synchronizacja == null || p.Synchronizacja == synchronizacja) 
                && p.Synchronizacja != (int)RowSynchronizeOld.Notsaved && (p.Usuniety == null || p.Usuniety == false) && (dostawcaID==null || p.DostawcaID == dostawcaID.Value))) { }

        public Produkty(KategoriaOld kategoria, bool? aktywny, bool? gotowy, bool? widoczny, int? synchronizacja)
            : this(kategoria, aktywny, gotowy, widoczny, synchronizacja, false) { }

        public override int Find(System.ComponentModel.PropertyDescriptor property, object key)
        {
            int len = ((string)key).Length;
            string s = ((string)key).ToLower();
            for (var i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];
                if ((!string.IsNullOrEmpty(row.Kod) && row.Kod.Length >= len && row.Kod.Substring(0, len).ToLower() == s) ||
                    (!string.IsNullOrEmpty(row.Nazwa) && row.Nazwa.Length >= len && row.Nazwa.Substring(0, len).ToLower() == s))
                    return i;
            }
            return -1;

        }

        protected override IEnumerable<Produkt> GetSortedRows(ObjectQuery<Produkt> query)
        {
            var bl = (System.ComponentModel.IBindingList)this;
            if(bl.SortProperty!= null && bl.SortProperty.DisplayName == "Kod")
            {
                if (bl.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                    return query.OrderBy(r => r.Kod).ThenBy(r => r.KodDostawcy);
                return query.OrderByDescending(r => r.Kod).ThenByDescending(r => r.KodDostawcy);
            }
            return base.GetSortedRows(query);
        }

        public override object CreateNewRecord()
        {
            var product = (Produkt)base.CreateNewRecord();
            product.DostepnyOld = true;
            return product;
        }
    }
}
