using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class DokRozliczeniowe : Business.GuidedTable<API.Kasa.DokRozliczBase>, API.Kasa.DokRozliczeniowe
    {
        public override string TableName
        {
            get
            {
                return "DokRozliczeniowe";
            }
        }

        public API.Business.SubTable WgTypData(API.Core.TypDokumentu typ)
        {
            return GetSubTable("WgTypData", typ);
        }


        public API.Business.SubTable WgPodmiot(API.Kasa.IPodmiotKasowy podmiot, API.Core.TypDokumentu typ, Types.Date data = null)
        {
            return GetSubTable("WgPodmiot", podmiot, typ , data);
        }

        public IEnumerable<API.Kasa.DokRozliczBase> WgTypDokumentu(API.Core.TypDokumentu typ, Types.FromTo fromTo, API.CRM.Kontrahent kontrahent)
        {
            API.Business.View view = null;
            if (kontrahent != null)
                view = WgPodmiot(kontrahent, typ).CreateView();
            else
                view = WgTypData(typ).CreateView();
            view.Filter = "Data >= '" + fromTo.From + "' AND Data <= '" + fromTo.To + "'";
            return view.Cast<API.Kasa.DokRozliczBase>().ToList(); ;
        }
    }
}
