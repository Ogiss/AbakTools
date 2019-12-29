using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.Kasa;
using Enova.API.CRM;

namespace Enova.Forms.Services
{
    public static class KasaService
    {
        public static KasaModule GetModule(Session session)
        {
            return session.GetModule<API.Kasa.KasaModule>();
        }

        public static class RozrachunkiIdx
        {
            public static View<RozrachunekIdx> ByKontrahent(Session session, Kontrahent kontrahent, FromTo dataFromTo, FromTo rozliczenieFromTo)
            {
                var view = new View<RozrachunekIdx>(GetModule(session).RozrachunkiIdx.WgPodmiot(kontrahent).CreateView());
                if (dataFromTo != null)
                    view.And("Data >= '" + dataFromTo.From + "' AND Data <= '" + dataFromTo.To + "'");
                if (rozliczenieFromTo != null)
                    view.And("DataRozliczenia >= '" + rozliczenieFromTo.From + "' AND DataRozliczenia <= '" + rozliczenieFromTo.To + "'");
                return view;
            }
        }
    }
}
