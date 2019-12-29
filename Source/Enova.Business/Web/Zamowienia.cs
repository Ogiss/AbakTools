using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.Web
{
    public class Zamowienia : Enova.Business.Old.Core.TableBase<Zamowienie>
    {
        private bool autoSort = false;

        public Zamowienia(ObjectQuery<Zamowienie> query) : base(query) 
        {
            RefreshContext = true;
            DataContext = Enova.Business.Old.Core.ContextManager.WebContext;
        }

        public Zamowienia(ObjectQuery<Zamowienie> query, bool autoSort)
            : base(query)
        {
            //RefreshContext = true;
            DataContext = Enova.Business.Old.Core.ContextManager.WebContext;
            this.autoSort = autoSort;
        }


        public Zamowienia()
            : this((ObjectQuery<Zamowienie>)Enova.Business.Old.Core.ContextManager.WebContext.Zamowienia
            .Include("KontrahentRef").Where(z=>z.Synchronizacja!=(int)RowSynchronizeOld.NotsynchronizedDelete).OrderByDescending(z=>z.DataDodania)) { }

        public static implicit operator Zamowienia(ObjectQuery<Zamowienie> query)
        {
            return new Zamowienia(query);
        }

        protected override List<Zamowienie> PostLoadProcess(List<Zamowienie> list)
        {
            if (autoSort)
                return list.OrderBy(z => z.Kolejnosc).ThenBy(z => z.NaKiedyData).ThenBy(z => z.KolejnoscPora).ToList();
            else
                return list;
        }

    }
}
