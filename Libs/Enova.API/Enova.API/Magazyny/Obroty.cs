using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;
using Enova.API.Types;
using Enova.API.CRM;
using Enova.API.Towary;
using Enova.API.Handel;

namespace Enova.API.Magazyny
{
    public interface Obroty : Business.Table<Obrot>
    {
        SubTable WgMagazyn(Magazyn magazyn, OkresMagazynowy okres = null, Towar towar = null, Date data = null, Time czas = null);
        SubTable WgOkres(OkresMagazynowy okres, Date data = null, Time czas = null);
        SubTable WgPrzychodDokument(DokumentHandlowy dokument, int? pozycjaIdent = null);
        SubTable WgPrzychodKontrahent(Kontrahent przychodkontrahent, OkresMagazynowy okres = null, Magazyn magazyn = null, Date data = null, Time czas = null);
        SubTable WgPrzychodKontrahentMagazyn(Kontrahent przychodkontrahent, Magazyn magazyn = null, Date data = null, Time czas = null);
        SubTable WgPrzychodPartiaTowaru(Dostawy.IGrupaDostaw partiatowaru);
        SubTable WgPrzychodPartiaTowaruMagazyn (Dostawy.IGrupaDostaw przychod_partiatowaru, Magazyn magazyn = null, Towar towar = null);
        SubTable WgPrzychodPartiaTowaruTowar(Dostawy.IGrupaDostaw przychod_partiatowaru, Towar towar = null);
        SubTable WgRozchodDokument(DokumentHandlowy dokument, int? pozycjaident = null);
        SubTable WgRozchodKontrahent(Kontrahent rozchodkontrahent, OkresMagazynowy okres = null, Magazyn magazyn = null, Date data = null, Time czas = null);
        SubTable WgRozchodKontrahentMagazyn(Kontrahent rozchodkontrahent, Magazyn magazyn = null, Date data = null, Time czas = null);
        SubTable WgRozchodPartiaTowaru(Dostawy.IGrupaDostaw partiatowaru);
        SubTable WgStornowany(Obrot stornowany);
        SubTable WgTowar(Towar towar, Magazyn magazyn, Date data = null, Time czas = null);
        SubTable WgTowarOkres(Towar towar, OkresMagazynowy okres, Date data, Time czas);
    }
}
