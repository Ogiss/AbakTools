using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Magazyny
{
    internal class Obroty : Business.Table<API.Magazyny.Obrot>, API.Magazyny.Obroty
    {
        #region Properties

        public override string TableName
        {
            get
            {
                return "Obroty";
            }
        }

        #endregion

        #region Methods

        public API.Business.SubTable WgMagazyn(API.Magazyny.Magazyn magazyn, API.Magazyny.OkresMagazynowy okres = null, API.Towary.Towar towar = null, Types.Date data = null, Types.Time czas = null)
        {
            return GetSubTable("WgMagazyn", magazyn, okres, towar, data, czas);
        }

        public API.Business.SubTable WgOkres(API.Magazyny.OkresMagazynowy okres, Types.Date data = null, Types.Time czas = null)
        {
            return GetSubTable("WgOkres", okres, data, czas);
        }

        public API.Business.SubTable WgPrzychodDokument(API.Handel.DokumentHandlowy dokument, int? pozycjaIdent = null)
        {
            if (pozycjaIdent == null)
                return GetSubTable("WgPrzychodDokument", dokument);
            return GetSubTable("WgPrzychodDokument", dokument, pozycjaIdent.Value);
        }

        public API.Business.SubTable WgPrzychodKontrahent(API.CRM.Kontrahent przychodkontrahent, API.Magazyny.OkresMagazynowy okres = null, API.Magazyny.Magazyn magazyn = null, Types.Date data = null, Types.Time czas = null)
        {
            return GetSubTable("WgPrzychodKontrahent", przychodkontrahent, okres, magazyn, data, czas);
        }

        public API.Business.SubTable WgPrzychodKontrahentMagazyn(API.CRM.Kontrahent przychodkontrahent, API.Magazyny.Magazyn magazyn = null, Types.Date data = null, Types.Time czas = null)
        {
            return GetSubTable("WgPrzychodKontrahentMagazyn", przychodkontrahent, magazyn, data, czas);
        }

        public API.Business.SubTable WgPrzychodPartiaTowaru(API.Magazyny.Dostawy.IGrupaDostaw partiatowaru)
        {
            return GetSubTable("WgPrzychodPartiaTowaru", partiatowaru);
        }

        public API.Business.SubTable WgPrzychodPartiaTowaruMagazyn(API.Magazyny.Dostawy.IGrupaDostaw przychod_partiatowaru, API.Magazyny.Magazyn magazyn = null, API.Towary.Towar towar = null)
        {
            return GetSubTable("WgPrzychodPartiaTowaruMagazyn", przychod_partiatowaru, magazyn, towar);
        }

        public API.Business.SubTable WgPrzychodPartiaTowaruTowar(API.Magazyny.Dostawy.IGrupaDostaw przychod_partiatowaru, API.Towary.Towar towar = null)
        {
            return GetSubTable("WgPrzychodPartiaTowaruTowar", przychod_partiatowaru, towar);
        }

        public API.Business.SubTable WgRozchodDokument(API.Handel.DokumentHandlowy dokument, int? pozycjaident = null)
        {
            if (pozycjaident == null)
                return GetSubTable("WgRozchodDokument", dokument);
            return GetSubTable("WgRozchodDokument", dokument, pozycjaident.Value);
        }

        public API.Business.SubTable WgRozchodKontrahent(API.CRM.Kontrahent rozchodkontrahent, API.Magazyny.OkresMagazynowy okres = null, API.Magazyny.Magazyn magazyn = null, Types.Date data = null, Types.Time czas = null)
        {
            return GetSubTable("WgRozchodKontrahent", rozchodkontrahent, okres, magazyn, data, czas);
        }

        public API.Business.SubTable WgRozchodKontrahentMagazyn(API.CRM.Kontrahent rozchodkontrahent, API.Magazyny.Magazyn magazyn = null, Types.Date data = null, Types.Time czas = null)
        {
            return GetSubTable("WgRozchodKontrahentMagazyn", rozchodkontrahent, magazyn, data, czas);
        }

        public API.Business.SubTable WgRozchodPartiaTowaru(API.Magazyny.Dostawy.IGrupaDostaw partiatowaru)
        {
            return GetSubTable("WgRozchodPartiaTowaru", partiatowaru);
        }

        public API.Business.SubTable WgStornowany(API.Magazyny.Obrot stornowany)
        {
            return GetSubTable("WgStornowany", stornowany);
        }

        public API.Business.SubTable WgTowar(API.Towary.Towar towar, API.Magazyny.Magazyn magazyn, Types.Date data = null, Types.Time czas = null)
        {
            return GetSubTable("WgTowar", towar, magazyn, data, czas);
        }

        public API.Business.SubTable WgTowarOkres(API.Towary.Towar towar, API.Magazyny.OkresMagazynowy okres, Types.Date data, Types.Time czas)
        {
            return GetSubTable("WgTowarOkres", towar, okres, data, czas);
        }

        #endregion
    }
}
