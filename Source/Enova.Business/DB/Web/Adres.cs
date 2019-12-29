using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace Enova.Business.Old.DB.Web
{
    public partial class Adres
    {
        public Adres() : this(null) { }

        public Adres(Enova.Business.Old.DB.Adres adres)
        {
            Guid = System.Guid.NewGuid();
            PSID = 0;
            Aktywny = true;
            Usuniety = false;
            Adres2 = string.Empty;
            Imie = "firstname";
            Nazwisko = "lastname";
            TelefonKomorkowy = string.Empty;
            Inne = string.Empty;
            Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew;
            Stamp = DateTime.Now;
            Domyslny = false;
            if (adres != null)
            {
                Adres1 = adres.AdresUlicaPelna;
                KodPocztowy = adres.KodPocztowyStr;
                Miasto = adres.AdresMiejscowosc;
                Firma = adres.Kontrahent == null ? string.Empty : ((Enova.Business.Old.DB.Kontrahent)adres.Kontrahent).Nazwa;
                Alias = adres.Kontrahent == null ? string.Empty : ((Enova.Business.Old.DB.Kontrahent)adres.Kontrahent).Kod;
                Telefon = adres.AdresTelefon;
                Domyslny = adres.Typ == 1;
                DomyslnyAdresFaktury = adres.Typ == 1;
                DomyslnyAdresWysylki = adres.Typ == 2;
            }
        }

        public Adres(Enova.API.CRM.Kontrahent kontrahent, Enova.API.Core.Adres adres, int typ)
        {
            Guid = System.Guid.NewGuid();
            PSID = 0;
            Aktywny = true;
            Usuniety = false;
            Adres2 = string.Empty;
            Imie = "firstname";
            Nazwisko = "lastname";
            TelefonKomorkowy = string.Empty;
            Inne = string.Empty;
            Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew;
            Stamp = DateTime.Now;
            Domyslny = false;
            if (adres != null)
            {
                Adres1 = adres.Ulica + (!string.IsNullOrEmpty(adres.NrDomu) ? " " + adres.NrDomu + (!string.IsNullOrEmpty(adres.NrLokalu) ? "/" + adres.NrLokalu : "") : "");
                KodPocztowy = adres.KodPocztowy;
                Miasto = adres.Miejscowosc;
                Firma = kontrahent == null ? string.Empty : kontrahent.Nazwa;
                Alias = kontrahent == null ? string.Empty : kontrahent.Kod;
                Telefon = adres.Telefon;
                Domyslny = typ == 1;
                DomyslnyAdresFaktury = typ == 1;
                DomyslnyAdresWysylki = typ == 2;
            }
        }


        public override string ToString()
        {
            return Firma + "\\r\\n" + Adres1 + "\\r\n\\";
        }

        public void DoSaveChanges(WebContext dc)
        {
            try
            {
                dc.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                dc.Refresh(RefreshMode.ClientWins, this);
                dc.SaveChanges();
            }
            dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
           
        }
    }
}
