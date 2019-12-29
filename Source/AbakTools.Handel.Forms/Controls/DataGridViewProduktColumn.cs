using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;

namespace AbakTools.Handel.Forms
{
    public class DataGridViewProduktColumn : Enova.Business.Old.Controls.DataGridViewSelectColumn<Enova.Business.Old.DB.Web.Produkt>
    {
        public override Type SelectFormType
        {
            get{return typeof(AbakTools.Towary.Forms.WyborProduktuForm);}
        }

        public override string DisplayMember
        {
            get
            {
                return "Nazwa";
            }
        }

        public override string SearchMember
        {
            get
            {
                return "Kod";
            }
        }

        public override System.Data.Objects.ObjectQuery<Enova.Business.Old.DB.Web.Produkt> SearchQuery
        {
            get
            {
                return (ObjectQuery<Produkt>)ContextManager.WebContext.Produkty.Where(p => p.AktywnyOld == true && p.ProduktGrupujacy == false);
            }
        }
    }
}
