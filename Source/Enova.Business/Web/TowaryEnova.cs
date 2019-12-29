using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;

namespace Enova.Business.Old.Web
{
    public class TowaryEnova : Core.TableBase<DB.Web.Produkt>
    {
        public DB.Web.KategoriaOld Kategoria;

        /*
        public TowaryEnova(ObjectQuery<DB.Web.Produkt> query) : base(query) { }

        public TowaryEnova(DB.Web.Kategoria category)
            : this((ObjectQuery<DB.Web.Produkt>)category.RelationKategorieProduktow.CreateSourceQuery().Select(kp => kp.Produkt)) { }

        public TowaryEnova(DB.Web.WebContext dc)
            : this((ObjectQuery<DB.Web.Produkt>)dc.Produkty.Where(p => p.TowarEnova == true && (p.Usuniety==null || p.Usuniety == false) && p.Synchronizacja!=(int)Types.RowSynchronize.NotsynchronizedDelete )) { }
        */

        public override ObjectQuery<DB.Web.Produkt> BaseQuery
        {
            get
            {
                if (Kategoria != null)
                    return (ObjectQuery<DB.Web.Produkt>)Kategoria.RelationKategorieProduktow.CreateSourceQuery()
                        .Where(r => r.Deleted == false && (r.Produkt.Usuniety == null || r.Produkt.Usuniety == false)
                        && r.Produkt.Synchronizacja != (int)Types.RowSynchronizeOld.NotsynchronizedDelete && r.Produkt.TowarEnova == true).Select(r => r.Produkt);
                else
                    return (ObjectQuery<DB.Web.Produkt>)((DB.Web.WebContext)DataContext).Produkty
                        .Where(r => r.TowarEnova == true && (r.Usuniety == null || r.Usuniety == false) && r.Synchronizacja != (int)Types.RowSynchronizeOld.NotsynchronizedDelete);
            }
            set
            {
                base.BaseQuery = value;
            }
        }

        public TowaryEnova(DB.Web.WebContext dbContext) : base(dbContext, "Produkty") { }

        public override int Find(System.ComponentModel.PropertyDescriptor property, object key)
        {
            int len = ((string)key).Length;
            string s = ((string)key).ToLower();
            int idx = -1;

            for (int i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];
                if (!string.IsNullOrEmpty(row.Kod) && row.Kod.Length >= len && row.Kod.ToLower().StartsWith(s))
                {
                    idx = i;
                    break;
                }
            }

            if (idx == -1)
            {
                for (var i = 0; i < Rows.Count; i++)
                {
                    var row = Rows[i];
                    if ((!string.IsNullOrEmpty(row.Nazwa) && row.Nazwa.Length >= len && (row.Nazwa.Substring(0, len).ToLower() == s || row.Nazwa.ToLower().Contains(s))))
                        return i;
                }
            }

            return idx;

        }
    }
}
