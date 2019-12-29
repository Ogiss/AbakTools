using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;
using Enova.API.Handel;
using Enova.API.Types;

namespace Enova.Forms.Services
{
    public static class HandelService
    {
        public static HandelModule GetModule(Session session)
        {
            return session.GetModule<HandelModule>();
        }

        public static class DokHandlowe
        {
            public static IEnumerable<DokumentHandlowy> ByPrzedstawicielDefinicja(Session session, string przedstawiciel, API.Magazyny.Magazyn magazyn, FromTo fromTo, DefDokHandlowego definicja,
                StanDokumentuHandlowego stan = StanDokumentuHandlowego.Zatwierdzony)
            {
                List<DokumentHandlowy> list = new List<DokumentHandlowy>();
                if (!string.IsNullOrEmpty(przedstawiciel))
                {
                    var kontrahenci = CRMService.Kontrahenci.ByPrzedstawiciel(session, przedstawiciel).ToList();
                    
                    var hm = GetModule(session);
                    foreach (var k in kontrahenci)
                    {
                        
                        var dokumenty = hm.DokHandlowe.WgKontrahentDataDefinicja(k,magazyn, fromTo, definicja, stan);
                        foreach (var dok in dokumenty)
                            yield return dok;
                         
                        //list.AddRange(hm.DokHandlowe.WgKontrahentDataDefinicja(k, magazyn, fromTo, definicja, stan));
                    }
                }
                //return list;
            }

        }
    }
}
