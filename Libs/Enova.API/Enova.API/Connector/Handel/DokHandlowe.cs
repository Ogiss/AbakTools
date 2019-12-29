using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Handel
{
    internal class DokHandlowe : Business.GuidedTable<API.Handel.DokumentHandlowy>, API.Handel.DokHandlowe
    {
        #region Fields
        #endregion

        #region Properties

        public override string TableName
        {
            get
            {
                return "DokHandlowe";
            }
        }

        public Business.SubTable WgKontrahent(API.CRM.Kontrahent kontrahent)
        {
            return GetSubTable("WgKontrahent", kontrahent);
        }



        public IEnumerable<API.Handel.DokumentHandlowy> WgKontrahentDataDefinicja(API.CRM.Kontrahent kontrahent, API.Magazyny.Magazyn magazyn, 
            API.Types.FromTo fromTo, API.Handel.DefDokHandlowego definicja, API.Handel.StanDokumentuHandlowego stan)
        {
            string filter = "";
            if (magazyn != null)
                filter += " AND Magazyn = " + magazyn.ID;
            if (fromTo != null)
                filter += " AND Data >='" + fromTo.From.ToString() + "' AND Data <= '" + fromTo.To.ToString() + "'";
            if (definicja != null)
                filter += " AND Definicja = " + definicja.ID;
            /*
            if (stan == API.Handel.StanDokumentuHandlowego.Zatwierdzony || stan == API.Handel.StanDokumentuHandlowego.Zablokowany)
                filter += " AND (Stan = " + (int)API.Handel.StanDokumentuHandlowego.Zatwierdzony + " OR Stan = " + (int)API.Handel.StanDokumentuHandlowego.Zablokowany + ")";
             */
            //return WgKontrahent(kontrahent).CreateView().SetFilter(filter).Cast<API.Handel.DokumentHandlowy>();
            if (filter != "")
                filter = filter.Substring(5);
            return new Business.EnovaEnumerable<API.Handel.DokumentHandlowy>(WgKontrahent(kontrahent).CreateView().SetFilter(filter));
            
        }


        #endregion

        #region Methods
        #endregion
    }
}
