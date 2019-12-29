using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Kasa
{
    /*
    public interface DokRozliczeniowe : Business.GuidedTable<DokRozlicz>
    {
        IEnumerable<DokRozlicz> WgTypDokumentu(Core.TypDokumentu typ);
        IEnumerable<DokRozlicz> WgTypDokumentu(Core.TypDokumentu type, Types.FromTo fromTo, CRM.Kontrahent kontrahent);
    }
     */

    public interface DokRozliczeniowe : Business.GuidedTable<DokRozliczBase>
    {
        //IEnumerable<DokRozliczBase> WgTypDokumentu(Core.TypDokumentu typ);
        IEnumerable<DokRozliczBase> WgTypDokumentu(Core.TypDokumentu type, Types.FromTo fromTo, CRM.Kontrahent kontrahent);

        Business.SubTable WgTypData(API.Core.TypDokumentu typ);
        Business.SubTable WgPodmiot(IPodmiotKasowy podmiot, Core.TypDokumentu typ, Types.Date data = null);

    }

 }
