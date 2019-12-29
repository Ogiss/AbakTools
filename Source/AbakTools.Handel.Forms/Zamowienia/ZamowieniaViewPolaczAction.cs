using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;
using DBWeb = Enova.Business.Old.DB.Web;

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewPolaczAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{
    [Priority(120), Caption("Połącz")]
    public class ZamowieniaViewPolaczAction : ZamowieniaViewActionBase
    {
        #region Properties

        public int MarginRight
        {
            get { return 30; }
        }

        protected override bool ZmienWidocznosc
        {
            get
            {
                return false;
            }
        }

        protected override bool ZmienDostepnosc
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        public void OnAction()
        {
            DialogResult result = MessageBox.Show("Czy napewno chcesz połączyć zamówienia?", "EnovaTools", MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
                return;

            List<DBWeb.Zamowienie> zamowienia = new List<DBWeb.Zamowienie>();
            List<DBWeb.PozycjaZamowienia> pozycje = new List<DBWeb.PozycjaZamowienia>();
            foreach (ZamowienieView row in StatusAction.SelectedRows)
            {
                DBWeb.Zamowienie zam = row.Zamowienie;
                zamowienia.Add(zam);
                foreach (var poz in zam.PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete))
                {
                    pozycje.Add(poz);
                }
            }

            DBWeb.Kontrahent kontrahent = zamowienia.Select(z => z.Kontrahent).FirstOrDefault();
            DBWeb.Adres adresFaktury = zamowienia.Select(z => z.AdresFaktury).FirstOrDefault();
            DBWeb.Adres adresWysylki = zamowienia.Select(z => z.AdresWysylki).FirstOrDefault();

            DBWeb.Zamowienie noweZam = new DBWeb.Zamowienie()
            {
                AdresFaktury = adresFaktury,
                AdresWysylki = adresWysylki,
                Blokada = false,
                BlokadaEdycji = false,
                DataDodania = DateTime.Now,
                GUID = Guid.NewGuid(),
                Kontrahent = kontrahent,
                NaKiedy = DateTime.Now,
                NaKiedyTyp = "N",
                Pilne = false,
                PSID = 0,
                RodzajTransportu = RodzajTransportu.NieWybrano,
                Stamp = DateTime.Now,
                Synchronizacja = (int)RowSynchronizeOld.Notsaved,
                ZamPrzedstawiciela = false
            };

            int ident = 1;

            foreach (var poz in pozycje)
            {
                try
                {
                    DBWeb.PozycjaZamowienia pozycja = null;
                    if (poz.ProduktIndywidualny != null && poz.ProduktIndywidualny.Value)
                    {
                        pozycja = noweZam.PozycjeZamowienia.Where(p => p.ProduktIndywidualny == true && p.ProduktNazwa == poz.ProduktNazwa).FirstOrDefault();
                    }
                    else
                    {
                        pozycja = noweZam.PozycjeZamowienia.Where(p => p.Produkt != null && p.Produkt.ID == poz.Produkt.ID &&
                            (poz.AtrybutProduktu == null || p.AtrybutProduktu != null && p.AtrybutProduktu.ID == poz.AtrybutProduktu.ID)).FirstOrDefault();
                    }

                    if (pozycja == null)
                    {
                        noweZam.PozycjeZamowienia.Add(new DBWeb.PozycjaZamowienia()
                        {
                            AtrybutProduktu = poz.AtrybutProduktu,
                            Cena = poz.Cena,
                            Ident = ident++,
                            Ilosc = poz.Ilosc,
                            IloscOrg = poz.Ilosc,
                            Opis = poz.Opis,
                            Produkt = poz.Produkt,
                            ProduktNazwa = poz.ProduktNazwa,
                            ProduktIndywidualny = poz.ProduktIndywidualny,
                            PSID = poz.PSID,
                            Stamp = DateTime.Now,
                            StawkaVatSymbol = poz.StawkaVatSymbol,
                            StawkaVatValue = poz.StawkaVatValue,
                            Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                            OgraniczenieSprzedazy = poz.OgraniczenieSprzedazy == null ? 0 : poz.OgraniczenieSprzedazy.Value
                        });
                    }
                    else
                    {
                        pozycja.Ilosc += poz.Ilosc;
                        pozycja.IloscOrg = pozycja.Ilosc;
                        pozycja.OgraniczenieSprzedazy += poz.OgraniczenieSprzedazy == null ? 0 : poz.OgraniczenieSprzedazy.Value;

                    }
                }
                catch (Exception ex)
                {
                    //                        MessageBox.Show("Ident: " + poz.Ident + " ID: " + poz.ID);
                    throw ex;
                }
            }


            Operator @operator = Enova.Business.Old.Core.ContextManager.WebContext.Operatorzy
                .Where(p => p.Nazwa == Enova.Business.Old.DB.Web.User.LoginedUser.Login || p.Nazwa == User.LoginedUser.EnovaOperatorLogin).FirstOrDefault();
            StatusZamowienia status = Enova.Business.Old.Core.ContextManager.WebContext.StatusyZamowien
                .Where(s => s.NoweZamowienie == true).FirstOrDefault();

            noweZam.HistoriaZamowienia.Add(new DBWeb.HistoriaZamowienia()
            {
                DataDodania = DateTime.Now,
                Operator = @operator,
                Status = status,
                Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
            });



            Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();

            foreach (var zam in zamowienia)
            {
                foreach (var poz in zam.PozycjeZamowienia)
                    poz.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;

                foreach (var h in zam.HistoriaZamowienia)
                    h.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;

                foreach (var m in zam.Wiadomosci)
                    m.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;

                zam.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
            }

            noweZam.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
            Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();

            this.Reload();

            MessageBox.Show("Stworzono nowe zamówienie o numerze " + noweZam.NumerPelny, "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Reload();
        }

        protected override bool CheckOr()
        {
            return StatusAction.SelectedRows != null && StatusAction.SelectedRows.Count > 1 && StatusAction.TenSamKontrahent;
        }


        #endregion
    }
}
