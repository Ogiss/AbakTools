using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Core;
using Enova.Business.Old.Types;
using BAL.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Enova.Business.Old.DB.Web
{
    [DataEditForm("AbakTools.Forms.Zamowienia.ZamowienieEditForm,AbakTools.Handel.Forms")]
    [DataForm("AbakTools.Zamowienia.Forms.ZamowienieEditForm,AbakTools.Handel.Forms")]
    public partial class Zamowienie : ISaveChanges, IUndoChanges, IDeleteRecord, IValidation, IBlockOnEditRecord, /*Enova.API.Handel.IDokumentHandlowy,*/ IDbContext
    {
        public Zamowienie()
        {
            this.PSID = 0;
            this.GUID = Guid.NewGuid();
            this.ZamPrzedstawiciela = false;
            this.Status = 0;
            this.Stamp = DateTime.Now;
            this.Synchronizacja = (int)RowSynchronizeOld.Notsaved;
            this.Transport = (int)RodzajTransportu.NieWybrano;
            this.DataDodania = DateTime.Now;
        }


        ObjectContext IDbContext.DbContext { get; set; }
        [XmlIgnore]
        public WebContext DbContext { get { return (WebContext)((IDbContext)this).DbContext; } }

        public bool BlokadaEdycji = false;
        public bool IstniejaBraki = false;

        public string NumerPelny
        {
            get
            {
                if (BrakiDoZamowienia != null && (bool)BrakiDoZamowienia)
                    return "BRAKI";
                else if (Pilne != null && (bool)Pilne && ((bool)StatusZamowienia.NoweZamowienie || (bool)StatusZamowienia.DoMagazynu))
                    return "PILNE";
                return ID.ToString() + "/" + PSID.ToString();
            }
        }
        /*
                Guid Enova.API.Business.IGuidedRow.Guid
                {
                    get { return this.GUID == null ? Guid.Empty : this.GUID.Value; }
                }

                string Enova.API.Handel.IDokumentHandlowy.NumerPelny
                {
                    get { return ID.ToString() + "/" + PSID.ToString(); }
                }

                Enova.API.CRM.IKontrahent Enova.API.Handel.IDokumentHandlowy.Kontrahent
                {
                    get { return (Enova.API.CRM.IKontrahent)this.Kontrahent; }
                    set { }
                }

                IEnumerable<Enova.API.Handel.IPozycjaDokHan> Enova.API.Handel.IDokumentHandlowy.Pozycje
                {
                    get
                    {
                        foreach (var poz in this.PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronize.NotsynchronizedDelete &&
                            p.Produkt != null && p.Produkt.EnovaGuid != null && p.Ilosc > 0))
                            yield return (Enova.API.Handel.IPozycjaDokHan)poz;
                    }
                }

                Enova.API.Types.Date Enova.API.Handel.IDokumentHandlowy.Data
                {
                    get { throw new NotImplementedException(); }
                    set { }
                }

                Enova.API.Types.Currency Enova.API.Handel.IDokumentHandlowy.WartoscNetto
                {
                    get { throw new NotImplementedException(); }
                }

                Enova.API.Types.Currency Enova.API.Handel.IDokumentHandlowy.WartoscVat
                {
                    get { throw new NotImplementedException(); }
                }

                Enova.API.Types.Currency Enova.API.Handel.IDokumentHandlowy.WartoscBrutto
                {
                    get { throw new NotImplementedException(); }
                }

                bool Enova.API.Handel.IDokumentHandlowy.Korekta
                {
                    get { throw new NotImplementedException(); }
                }

                Enova.API.Handel.IDokumentHandlowy Enova.API.Handel.IDokumentHandlowy.DokumentKorygowany
                {
                    get { throw new NotImplementedException(); }
                }
                */

        public string Numer
        {
            get
            {
                return ID.ToString() + "/" + this.PSID.ToString();
            }
        }

        public DateTime DataAkt
        {
            get { return this.DataAtualizacji == null ? (this.DataDodania == null ? DateTime.Now : this.DataDodania.Value) : this.DataAtualizacji.Value; }
        }

        public EntityCollection<HistoriaZamowienia> HistoriaZamowienia
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !HistoriaZamowieniaRef.IsLoaded)
                    HistoriaZamowieniaRef.Load();
                return HistoriaZamowieniaRef;
            }
        }

        public string KontrahentKod
        {
            get
            {
                if (Kontrahent != null)
                    return Kontrahent.Kod;

                return null;
            }
        }

        public string KontrahentZam
        {
            get
            {
                if (ZamPrzedstawiciela.Value)
                {
                    return KontrahentInfo;
                }
                else if (Kontrahent != null)
                    return Kontrahent.Kod;

                return null;
            }
        }

        public string KontrahentNazwa
        {
            get
            {
                if ((bool)ZamPrzedstawiciela)
                {
                    return KontrahentInfo;
                }
                else
                {
                    if (Kontrahent != null)
                        return Kontrahent.Nazwa;
                }
                return null;
            }
        }

        public Adres AdresFaktury
        {
            get
            {
                if ((bool)ZamPrzedstawiciela)
                {
                    return new Adres()
                    {
                        Firma = KontrahentInfo
                    };
                }
                else
                {

                    if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !AdresFakturyRefReference.IsLoaded)
                        AdresFakturyRefReference.Load();
                    return AdresFakturyRef;
                }
            }
            set
            {
                AdresFakturyRef = value;
            }
        }

        public Adres AdresWysylki
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !AdresWysylkiRefReference.IsLoaded)
                    AdresWysylkiRefReference.Load();
                return AdresWysylkiRef;
            }
            set
            {
                AdresWysylkiRef = value;
            }
        }

        public string FakturaFirma
        {
            get
            {
                return null;
            }
        }

        public string AdresFakturyText
        {
            get
            {
                string adres = "";
                if ((bool)ZamPrzedstawiciela)
                {
                    adres += KontrahentInfo;
                }
                else
                {
                    if (AdresFaktury != null)
                    {
                        adres += AdresFaktury.Firma + "\r\n" +
                            AdresFaktury.Adres1 + "\r\n" +
                            AdresFaktury.KodPocztowy + " " + AdresFaktury.Miasto;
                    }
                    else
                    {
                        adres += KontrahentKod;
                    }
                }
                return adres;
            }
        }

        public string AdresWysylkiText
        {
            get
            {
                if (AdresWysylki != null)
                {
                    return AdresWysylki.Firma + "\r\n" +
                        AdresWysylki.Adres1 + "\r\n" +
                        AdresWysylki.KodPocztowy + " " + AdresWysylki.Miasto;
                }
                return null;
            }
        }

        public string WiadomosciText
        {
            get
            {
                string str = string.Empty;

                foreach (var m in this.Wiadomosci.Where(m => m.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList())
                {
                    str += m.Utf8Tekst + "\r\n";
                }

                return str;
            }
        }

        public string PierwszaWiadomosc
        {
            get
            {
                DateTime? min = Wiadomosci.Where(w => w.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && !string.IsNullOrEmpty(w.Tekst)).Min(w => w.Stamp);
                if (min != null)
                {
                    return Wiadomosci.Where(w => w.Stamp == min).ToList().Select(w => w.Utf8Tekst).FirstOrDefault();
                }
                return null;
            }
        }

        public StatusZamowienia StatusZamowienia
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !OstatniStatusOperatorReference.IsLoaded)
                    OstatniStatusOperatorReference.Load();

                if (RelationOstStatus == null)
                {
                    DateTime? max = HistoriaZamowienia.Where(h => h.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).Max(h => h.DataDodania);
                    if (max != null)
                    {
                        return HistoriaZamowienia.Where(h => h.DataDodania == max && h.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).Select(h => h.StatusRef).FirstOrDefault();
                    }
                }
                else
                {
                    return RelationOstStatus;
                }
                return null;


            }
        }

        public Operator StatusZamowieniaOperator
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !OstatniStatusOperatorReference.IsLoaded)
                    OstatniStatusOperatorReference.Load();
                if (OstatniStatusOperator == null)
                {
                    HistoriaZamowienia hist = OstatniaHistoriaZamowienia;
                    if (hist != null)
                    {
                        return hist.Operator;
                    }
                }
                else
                {
                    return OstatniStatusOperator;
                }
                return null;
            }
        }

        public HistoriaZamowienia OstatniaHistoriaZamowienia
        {
            get
            {
                DateTime? max = HistoriaZamowienia.Where(h => h.Deleted == false && h.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).Max(h => h.DataDodania);
                if (max != null)
                {
                    return HistoriaZamowienia.Where(h => h.DataDodania == max && h.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && h.Deleted == false).FirstOrDefault();
                }
                return null;
            }
        }

        public string StatusZamowieniaPelny
        {
            get
            {
                return StatusZamowienia == null ? null : (StatusZamowienia.Nazwa + (StatusZamowieniaOperator != null ? " (" + StatusZamowieniaOperator.Nazwa + ")" : ""));
            }
        }

        public RodzajTransportu RodzajTransportu
        {
            get
            {
                return this.Transport == null ? RodzajTransportu.NieWybrano : (RodzajTransportu)this.Transport;
            }
            set
            {
                this.Transport = (int)value;
            }
        }

        public string RodzajTransportuStr
        {
            get
            {
                switch (this.RodzajTransportu)
                {
                    case Types.RodzajTransportu.NieWybrano:
                        return "Nie wybrano";
                    case Types.RodzajTransportu.Kurier:
                        return "Kurier";
                    case Types.RodzajTransportu.Przedstawiciel:
                        return "Przedstawiciel";
                    case Types.RodzajTransportu.DoDostawcy:
                        return "Do dostawcy";
                }
                return null;
            }
        }

        public string Info
        {
            get
            {
                if (!string.IsNullOrEmpty(KontrahentInfo))
                    return Enova.Business.Old.Core.Tools.FromHtmlText(KontrahentInfo).Replace("\r\n", " ");
                return string.Empty;
            }
        }

        public string KtoZamowil
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ZrodloKod))
                    return this.ZrodloKod;
                if (!ZamPrzedstawiciela.Value)
                {
                    HistoriaZamowienia his = HistoriaZamowienia.Where(h => h.Status.NoweZamowienie == true).FirstOrDefault();
                    return his?.Operator?.Nazwa;
                }
                return (ZamPrzedstawiciela.Value ? PrzedstawicielKod + "-" : "") + "WWW";
            }
        }

        public decimal? WartoscNetto
        {
            get
            {
                return decimal.Round((decimal)PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList().Sum(p => p.WartoscNetto), 2);
            }
        }

        public decimal? WartoscBrutto
        {
            get
            {
                return decimal.Round((decimal)PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList().Sum(p => p.WartoscBrutto), 2);
            }
        }

        public decimal? WartoscVat
        {
            get
            {
                decimal? netto = WartoscNetto;
                decimal? brutto = WartoscBrutto;
                if (netto != null && brutto != null)
                    return Decimal.Round(brutto.Value - netto.Value, 2);
                return null;
            }
        }

        public decimal? WartoscOrgNetto
        {
            get
            {
                return decimal.Round((decimal)PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList().Sum(p => p.WartoscOrgNetto), 2);
            }
        }

        public decimal? WartoscBrakiNetto
        {
            get
            {
                return decimal.Round((decimal)PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList().Sum(p => p.WartoscBrakiNetto), 2);
            }
        }

        public string WartoscBrakiStr
        {
            get
            {
                if (WartoscBraki != null && WartoscBraki.Value != 0)
                {
                    if (AnulujBraki != null && AnulujBraki.Value)
                        return decimal.Round(WartoscBraki.Value, 2).ToString() + "zł (OLAĆ)";
                    else if (ZamowionoBraki != null && ZamowionoBraki.Value)
                        return decimal.Round(WartoscBraki.Value, 2).ToString() + "zł (" + ZamowienieBrakow.ToString() + ")";
                    return decimal.Round(WartoscBraki.Value, 2).ToString() + "zł";
                }
                return null;
            }
        }

        public void PrzeliczZamowienie()
        {
            this.TotalNetto = WartoscNetto;
            this.TotalBrutto = WartoscBrutto;
            this.TotalProduktyNetto = WartoscNetto;
            this.TotalProduktyBrutto = WartoscBrutto;
            this.WartoscBraki = WartoscBrakiNetto;
        }

        public void PrzeliczRabaty()
        {

            foreach (var p in this.PozycjeZamowienia)
            {
                decimal rabat = 0;
                if (p.ZmienionoRabat == null || p.ZmienionoRabat == false)
                {
                    if (Kontrahent != null)
                        rabat = Kontrahent.GetRabat(p.Produkt);

                    if (p.Rabat != rabat)
                    {
                        bool save = p.IsLive;
                        p.IsLive = false;
                        p.Rabat = rabat;
                        p.IsLive = save;

                    }
                }
            }
        }

        public ZamowienieView GetZamowienieView()
        {
            return Enova.Business.Old.Core.ContextManager.WebContext.ZamowieniaView.Where(z => z.ID == this.ID).FirstOrDefault();
        }

        public void ZmienStatus(Operator @operator, StatusZamowienia status)
        {
            HistoriaZamowienia hist = new HistoriaZamowienia()
            {
                GUID = Guid.NewGuid(),
                Operator = @operator,
                Status = status,
                DataDodania = DateTime.Now,
                PSID = 0,
                //Zamowienie = this,
                Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew
            };
            this.HistoriaZamowienia.Add(hist);
            if (status.Pakowanie.Value)
            {
                this.PrzeliczPozycje();

                int ident = 1;
                /*
                foreach (var p in this.PozycjeZamowienia.OrderBy(pz => pz.Kod).ThenBy(pz => pz.NazwaProduktu).ThenBy(pz => pz.AtrybutNazwa))
                {
                    p.Ident = ident++;
                }
                 */
                var pozycje = this.PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList();
                foreach (var pz in pozycje.OrderBy(p => p.Kolejnosc).ThenBy(p => p.Kod).ThenBy(p => p.NazwaProduktu).ThenBy(p => p.AtrybutNazwa))
                {
                    pz.Ident = ident++;
                }

            }

            //Enova.Business.Core.ContextManager.WebContext.AddToHistoriaZamowien(hist);
        }

        public void SortujPozycje()
        {
            int ident = 1;
            var pozycje = this.PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList()
                .OrderBy(p => p.Kolejnosc).ThenBy(p => p.Kod).ThenBy(p => p.NazwaProduktu).ThenBy(p => p.AtrybutNazwa).ToList();
            foreach (var pz in pozycje)
                pz.Ident = ident++;
        }

        public void SortujPozycjeAZ()
        {
            int ident = 1;
            var pozycje = this.PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList()
                .OrderBy(p => p.Kod).ThenBy(p => p.NazwaProduktu).ThenBy(p => p.AtrybutNazwa).ToList();
            foreach (var pz in pozycje)
                pz.Ident = ident++;

        }

        public void ZmienStatus(Operator @operator, StatusyZamowieniaTyp st)
        {
            StatusZamowienia status = null;
            switch (st)
            {
                case StatusyZamowieniaTyp.NoweZamowienie:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.NoweZamowienie == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.DoMagazynu:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.Pakowanie == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.DoDostawcy:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.DoDostawcy == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.Anulowane:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.Anulowane == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.Blokada:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.Blokada == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.Wstrzymane:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.Wstrzymane == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.Pakowane:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.Pakowanie == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.Spakowane:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.Spakowane == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.Przedstawiciel:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.Przedstawiciel == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.Kurier:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.Kurier == true).FirstOrDefault();
                    break;
                case StatusyZamowieniaTyp.Wyslane:
                    status = ContextManager.WebContext.StatusyZamowien.Where(s => s.Wysłane == true).FirstOrDefault();
                    break;
            }

            if (status != null)
                ZmienStatus(@operator, status);
        }

        public void ZmienStatus(StatusyZamowieniaTyp st)
        {
            Operator @operator = ContextManager.WebContext.Operatorzy
                .Where(p => p.Nazwa == User.LoginedUser.Login || p.Nazwa == User.LoginedUser.EnovaOperatorLogin).FirstOrDefault();
            ZmienStatus(@operator, st);
        }

        public Kontrahent Przedstawiciel
        {
            get
            {
                if ((ZamPrzedstawiciela == null || !(bool)ZamPrzedstawiciela) && Kontrahent != null)
                    return Kontrahent.Przedstawiciel;
                return null;

            }
        }

        public string PrzedstawicielKod
        {
            get
            {
                if (ZamPrzedstawiciela != null && (bool)ZamPrzedstawiciela)
                {
                    return KontrahentKod;
                }
                else
                {
                    if (Przedstawiciel != null)
                        return Przedstawiciel.Kod;
                    return null;
                }
            }
        }


        private Dictionary<string, double> getPozycjeEnova(Dictionary<Guid, double> pozycje)
        {
            Dictionary<string, double> pozycjeEnova = new Dictionary<string, double>();
            foreach (KeyValuePair<Guid, double> kvp in pozycje)
            {
                Guid guid = Enova.Business.Old.Core.ContextManager.WebContext.MapujGuid(kvp.Key, "Towary");
                Enova.Business.Old.DB.Towar towar = Enova.Business.Old.Core.ContextManager.DataContext.Towary
                .Where(t => t.Guid == guid).FirstOrDefault();
                if (towar != null)
                {
                    if (towar.Typ == 4 && towar.RozdzielProdukt)
                    {
                        var elementy = Enova.Business.Old.Core.ContextManager.DataContext.ElemKompletow.Include("Towar")
                            .Where(e => e.Typ == 1 && e.Komplet.ID == towar.ID).OrderBy(e => e.Towar.Kod).ToList();
                        foreach (var e in elementy)
                        {
                            if (pozycjeEnova.ContainsKey(e.Towar.Kod))
                            {
                                pozycjeEnova[e.Towar.Kod] += kvp.Value + e.IloscValue;
                            }
                            else
                            {
                                pozycjeEnova.Add(e.Towar.Kod, kvp.Value * e.IloscValue);
                            }
                        }

                    }
                    else
                    {
                        if (pozycjeEnova.ContainsKey(towar.Kod))
                        {
                            pozycjeEnova[towar.Kod] += kvp.Value;
                        }
                        else
                        {
                            pozycjeEnova.Add(towar.Kod, kvp.Value);
                        }
                    }
                }
            }

            return pozycjeEnova;
        }

        public void PrzeliczPozycje()
        {
            var ec = Core.ContextManager.DataContext;
            var lc = Core.ContextManager.WebContext;
            var kontr = this.Kontrahent.GetKontrahentEnova(ec);

            if (kontr != null)
            {

                foreach (var poz in this.PozycjeZamowienia.Where(p => p.Produkt != null && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && p.Ilosc > 0).ToList())
                {
                    if (poz.Produkt.EnovaGuid != null)
                    {
                        var towar = Towar.GetByGuid(ec, poz.Produkt.EnovaGuid.Value);
                        if (towar != null && towar.Typ == 4)
                        {
                            var f = towar.GetFeatures(ec, "ZESTAW LICZONY Z OBROTÓW").FirstOrDefault();
                            if (f != null && f.Data.Trim() == "1")
                            {
                                f = towar.GetFeatures(ec, "OKRES LICZENIA OBROTÓW").FirstOrDefault();
                                if (!string.IsNullOrEmpty(f.Data))
                                {
                                    var parts = f.Data.Trim().Split(new string[] { "..." }, StringSplitOptions.RemoveEmptyEntries);
                                    if (parts.Length == 2)
                                    {
                                        var from = DateTime.Parse(parts[0]);
                                        var to = DateTime.Parse(parts[1]);
                                        foreach (var elk in towar.ElemKompletow.Where(e => e.Typ == 1).ToList())
                                        {
                                            var obroty = elk.Towar.GetObroty(ec, kontr, from, to);
                                            var ilosc = obroty.Sum(ob => ob.IloscValue);
                                            if (ilosc != null && ilosc.Value > 0)
                                            {
                                                double il = ilosc.Value;
                                                if (!elk.Towar.ZaokragnijObroty(ec, ref il))
                                                    towar.ZaokragnijObroty(ec, ref il);
                                                if (il > 0)
                                                {
                                                    var pr = (Produkt)elk.Towar;
                                                    if (pr == null)
                                                        pr = Produkt.CreateEnovaTowar(lc, elk.Towar);
                                                    this.PozycjeZamowienia.Add(new PozycjaZamowienia()
                                                    {
                                                        Cena = pr.Cena,
                                                        GUID = Guid.NewGuid(),
                                                        Ilosc = il,
                                                        IloscOrg = il,
                                                        IloscObroty = ilosc,
                                                        ProduktIndywidualny = false,
                                                        Produkt = pr,
                                                        ProduktNazwa = pr.FullName,
                                                        StawkaVatSymbol = pr.StawkaVat.Nazwa,
                                                        StawkaVatValue = pr.StawkaVat.Procent,
                                                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                                                        Stamp = DateTime.Now,
                                                        ZmienionoRabat = false
                                                    });
                                                }
                                            }

                                        }
                                    }
                                    poz.Ilosc = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        class Pozycja
        {
            public Guid TowarGuid;
            public string TowarKod;
            public double Ilosc;
            public decimal Rabat;
            public bool ZmienionoRabat;
        }

        public string GetTextPozycje()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Towar:Kod\tIlosc\tIlosc.Jednostka\r\n");

            List<Pozycja> pozycje = new List<Pozycja>();

            this.PozycjeZamowienia.Load();
            foreach (var pozycja in this.PozycjeZamowienia.Where(p => p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete
                && p.Ilosc > 0 && (p.ProduktIndywidualny == null || p.ProduktIndywidualny == false)).ToList())
            {

                Guid guid = Enova.Business.Old.Core.ContextManager.WebContext.MapujGuid(pozycja.Produkt.EnovaGuid.Value, "Towary");
                var towar = Enova.Business.Old.Core.ContextManager.DataContext.Towary.Where(t => t.Guid == guid).FirstOrDefault();

                Pozycja poztmp = pozycje.Where(p => p.TowarGuid == towar.Guid).FirstOrDefault();
                bool zmienionoRabat = pozycja.ZmienionoRabat == null ? false : pozycja.ZmienionoRabat.Value;

                decimal rabat = !zmienionoRabat ?
                    Enova.Business.Old.DB.Kontrahent.GetRabat(Enova.Business.Old.Core.ContextManager.DataContext, Kontrahent.Guid.Value, towar.Kod) :
                    (pozycja.Rabat == null ? 0 : pozycja.Rabat.Value);


                if (towar.Typ == 4 && towar.RozdzielProdukt)
                {
                    var elementy = Enova.Business.Old.Core.ContextManager.DataContext.ElemKompletow.Include("Towar")
                    .Where(e => e.Typ == 1 && e.Komplet.ID == towar.ID).ToList();

                    foreach (var element in elementy)
                    {
                        guid = Enova.Business.Old.Core.ContextManager.WebContext.MapujGuid(element.Towar.Guid, "Towary");
                        towar = guid != element.Towar.Guid ? Enova.Business.Old.Core.ContextManager.DataContext.Towary.Where(t => t.Guid == guid).FirstOrDefault()
                            : element.Towar;
                        poztmp = pozycje.Where(p => p.TowarGuid == towar.Guid).FirstOrDefault();
                        rabat = !zmienionoRabat ?
                            Enova.Business.Old.DB.Kontrahent.GetRabat(Enova.Business.Old.Core.ContextManager.DataContext, Kontrahent.Guid.Value, towar.Kod) :
                            (pozycja.Rabat == null ? 0 : pozycja.Rabat.Value);
                        if (poztmp == null)
                        {
                            pozycje.Add(new Pozycja()
                            {
                                TowarGuid = guid,
                                TowarKod = towar.Kod,
                                Ilosc = pozycja.Ilosc.Value * element.IloscValue,
                                Rabat = rabat,
                                ZmienionoRabat = zmienionoRabat
                            });
                        }
                        else
                        {
                            poztmp.Ilosc += pozycja.Ilosc.Value * element.IloscValue;
                        }
                    }

                }
                else
                {

                    if (poztmp == null || (poztmp.ZmienionoRabat != zmienionoRabat && poztmp.Rabat != rabat))
                    {
                        pozycje.Add(new Pozycja()
                        {
                            TowarGuid = towar.Guid,
                            TowarKod = towar.Kod,
                            Ilosc = pozycja.Ilosc.Value,
                            Rabat = rabat,
                            ZmienionoRabat = pozycja.ZmienionoRabat == null ? false : pozycja.ZmienionoRabat.Value
                        });
                    }
                    else
                        poztmp.Ilosc += pozycja.Ilosc.Value;
                }
            }

            foreach (var pozycja in pozycje.OrderBy(p => p.TowarKod))
            {
                sb.Append(pozycja.TowarKod + "\t" + pozycja.Ilosc.ToString() + "\tszt" + "\r\n");
            }

            return sb.ToString();
        }

        public bool Pakowanie
        {
            get
            {
                return this.StatusZamowienia == null ? false : this.StatusZamowienia.Pakowanie.Value;
            }
        }

        public bool Spakowane
        {
            get
            {
                return HistoriaZamowienia.Any(h => h.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && h.Status.Spakowane.Value);
            }
        }

        public bool StatusZmUzytkownik
        {
            get
            {
                var @operator = Operator.CurrentOperator;
                if (@operator != null && OstatniaHistoriaZamowienia.Operator != null && @operator.ID == OstatniaHistoriaZamowienia.Operator.ID)
                    return true;
                return false;
            }
        }

        public int? MaxIdent
        {
            get
            {
                return this.PozycjeZamowienia.Max(p => p.Ident);
            }
        }

        string[] deyMap = new string[]
        {
            "N",
            "Pn",
            "Wt",
            "Śr",
            "Cz",
            "Pt",
            "So"
        };

        public int Kolejnosc
        {
            get
            {
                StatusZamowienia status = this.StatusZamowienia;
                if (Pilne != null && (bool)Pilne && status != null && ((bool)status.NoweZamowienie || (bool)status.DoMagazynu))
                    return 100;
                if (status != null && status.DoDostawcy.Value)
                    return 2000;
                if (status != null && (status.Wysłane.Value || status.Anulowane.Value))
                    return 1000;
                if (status != null && status.Blokada.Value)
                    return 900;
                else if (ZamowienieOdlozone)
                    return 800;
                else if (status != null && (status.Kurier.Value || status.Przedstawiciel.Value))
                    return 500;
                else
                {
                    return 300;
                }
            }
        }

        public int KolejnoscPora
        {
            get
            {
                if (RodzajTransportu == Types.RodzajTransportu.Kurier)
                    return 5;
                else if (string.IsNullOrEmpty(NaKiedyTyp) || NaKiedyTyp == "W")
                    return 10;
                else if (NaKiedyTyp == "P")
                    return 5;
                else
                    return 1;

            }
        }

        public DateTime? NaKiedyData
        {
            get
            {
                if (this.NaKiedy != null)
                    return this.NaKiedy.Value.Date;
                return null;
            }
        }


        public bool ZamowienieOdlozone
        {
            get
            {
                if (this.StatusZamowienia == null || (this.StatusZamowienia.Wysłane == false && this.StatusZamowienia.Spakowane == false))
                {
                    DateTime n = DateTime.Now;
                    DateTime k = this.NaKiedy == null ? this.DataDodania.Value : this.NaKiedy.Value;
                    DateTime now = new DateTime(n.Year, n.Month, n.Day, 0, 0, 0);
                    DateTime nakiedy = new DateTime(k.Year, k.Month, k.Day, 0, 0, 0);

                    int diff = (nakiedy - now).Days;
                    if ((now.DayOfWeek == DayOfWeek.Saturday && diff > 2) || (now.DayOfWeek != DayOfWeek.Saturday && diff >= 2))
                        return true;
                }
                return false;
            }
        }


        public string NaKiedyDzienTygodnia
        {
            get
            {
                return NaKiedy == null ? null : deyMap[(int)NaKiedy.Value.DayOfWeek];
            }
        }

        public string NaKiedyDzienFull
        {
            get
            {
                return NaKiedyDzienTygodnia + ((string.IsNullOrEmpty(NaKiedyTyp) || NaKiedyTyp.ToLower() == "n") ? "" :
                    (NaKiedyTyp.ToLower() == "r" ? " - Rano" : (NaKiedyTyp.ToLower() == "p" ? " - Południe" :
                    (NaKiedyTyp.ToLower() == "w" ? " - Wieczór" : ""))));
            }
        }

        public int? TerminPlatnosci
        {
            get
            {
                if (Termin != null)
                    return Termin.Value;
                if (this.Kontrahent != null && this.ZamPrzedstawiciela.Value == false)
                {
                    Enova.Business.Old.DB.Kontrahent enovaKontrahent = this.Kontrahent.EnovaKontrahent;
                    if (enovaKontrahent != null && enovaKontrahent.Termin != null)
                        return enovaKontrahent.Termin.Value;
                }
                return null;
            }
            set
            {
                this.Termin = value;
            }
        }

        public DateTime TerminData
        {
            get
            {
                int? termin = this.TerminPlatnosci;
                if (this.DataDodania != null && termin != null)
                    return this.DataDodania.Value.AddDays(termin.Value);
                return this.DataDodania == null ? DateTime.Now : this.DataDodania.Value;
            }
            set
            {
                if (this.DataDodania != null)
                {
                    if (value < this.DataDodania)
                    {
                        System.Windows.Forms.MessageBox.Show("Termin nie może byś wcześniejszy niż data dodania zamówienia!!!", "EnovaTools",
                                 System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.TerminPlatnosci = (value - this.DataDodania.Value).Days;
                    }
                }
            }
        }

        public bool DrukujTermin
        {
            get { return Termin != null; }
        }

        public bool JestBlokada
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached)
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(RefreshMode.StoreWins, this);
                return this.Blokada == null ? false : this.Blokada.Value;
            }
        }

        public bool CheckCzyZmienicDostepnosci()
        {
            try
            {
                foreach (var poz in this.PozycjeZamowienia.ToList())
                {
                    if (poz.ProduktIndywidualny != null && poz.ProduktIndywidualny == true ||
                        poz.Produkt != null && poz.Produkt.TowarEnova == true)
                        continue;

                    //var dostepny = poz.Produkt.Dostepny && (poz.AtrybutProduktu == null || poz.AtrybutProduktu.Dostepny);
                    var dostepny = poz.AtrybutProduktu != null ? poz.AtrybutProduktu.Dostepny : poz.Produkt.Dostepny;

                    if (poz.Ilosc < poz.IloscOrg && dostepny)
                        return true;
                    if (poz.Ilosc > 0 && !dostepny)
                        return true;

                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public IList<TowarAtrybut> GetTowaryAtrybutyDlaZmienionychIlosci()
        {
            List<TowarAtrybut> list = new List<TowarAtrybut>();
            var pozycje = this.PozycjeZamowienia.Where(p => (p.ProduktIndywidualny == null || p.ProduktIndywidualny == false) &&
                p.Produkt != null && p.Produkt.TowarEnova == false).ToList();

            var dc = this.DbContext != null ? this.DbContext : ContextManager.WebContext;

            foreach (var poz in pozycje)
            {
                var dostepny = poz.Produkt.Dostepny && (poz.AtrybutProduktu == null || poz.AtrybutProduktu.Dostepny);

                bool flag = poz.Ilosc < poz.IloscOrg && dostepny;
                if (!flag)
                {
                    flag = poz.Ilosc > 0 && !dostepny;
                }

                if (flag)
                {
                    var attr = poz.AtrybutProduktu;
                    var produkt = poz.Produkt;
                    int id2 = attr == null ? 0 : attr.ID;
                    var ta = dc.TowaryAtrybuty.Where(r => r.ID1 == produkt.ID && r.ID2 == id2).FirstOrDefault();
                    if (ta != null)
                        list.Add(ta);
                }
            }

            return list;
        }

        public void AktualizujStanMagazynu()
        {
            using (var ec = new Enova.Business.Old.DB.EnovaContext("name=EnovaContext"))
            using (var lc = new Enova.Business.Old.DB.Web.WebContext("name=WebContext"))
            {
                var zam = lc.Zamowienia.Where(z => z.ID == this.ID).FirstOrDefault();
                if (zam != null)
                {
                    string connectionString = lc.GetProviderConnectionString();
                    foreach (var pozycja in zam.PozycjeZamowienia.Where(p => p.Produkt != null && p.Produkt.EnovaGuid != null).ToList())
                    {
                        Guid towarGuid = Guid.Empty;
                        double zmianaStanu = 0;
                        double stanMag = 0;

                        pozycja.AktualizujStanMagazynu(connectionString, out towarGuid, out zmianaStanu, out stanMag);
                        if (towarGuid != Guid.Empty)
                        {
                            if (zmianaStanu != 0)
                                pozycja.Produkt.SetOgraniczenieSprzedazyStan(ec, (int)stanMag);
                            if (stanMag > 0 && !pozycja.Produkt.Dostepny)
                            {
                                pozycja.Produkt.Dostepny = true;
                                if (pozycja.Produkt.Synchronizacja == (int)RowSynchronizeOld.Synchronized || pozycja.Produkt.Synchronizacja == (int)RowSynchronizeOld.NotsynchronizedEdit)
                                    pozycja.Produkt.Gotowy = true;

                                lc.OptimisticSaveChanges();
                            }
                            else if (stanMag <= 0 && pozycja.Produkt.Dostepny)
                            {
                                pozycja.Produkt.Dostepny = false;
                                if (pozycja.Produkt.Synchronizacja == (int)RowSynchronizeOld.Synchronized || pozycja.Produkt.Synchronizacja == (int)RowSynchronizeOld.NotsynchronizedEdit)
                                    pozycja.Produkt.Gotowy = true;
                                lc.OptimisticSaveChanges();
                            }

                        }
                    }
                }
            }
        }

        private static ICollection<PozycjaDokHan> agregujPozycje(IEnumerable<PozycjaDokHan> pozycje)
        {
            return (ICollection<PozycjaDokHan>)(from p in pozycje
                                                group p by new { p.TowarGuid, p.Rabat } into g
                                                select new PozycjaDokHan()
                                                {
                                                    TowarGuid = g.Key.TowarGuid,
                                                    TowarKod = g.First().TowarKod,
                                                    Ilosc = g.Sum(poz => poz.Ilosc),
                                                    //Rabat = g.FirstOrDefault().Rabat,
                                                    Rabat = g.Key.Rabat,
                                                    Cena = g.FirstOrDefault().Cena,
                                                    ZmienionoRabat = g.FirstOrDefault().ZmienionoRabat
                                                }).ToList();
        }

        private ICollection<PozycjaDokHan> przeliczProdukty(API.Business.Session session, IEnumerable<PozycjaDokHan> pozycje, ref bool agreguj)
        {
            List<PozycjaDokHan> list = new List<PozycjaDokHan>();

            var tm = session.GetModule<API.Towary.TowaryModule>();

            foreach (var poz in pozycje)
            {
                var towar = tm.Towary[poz.TowarGuid];
                if (towar.Typ == API.Towary.TypTowaru.Produkt && (bool)towar.Features["ROZDZIEL_PRODUKT"])
                {
                    foreach (var ek in towar.ElementyKompletu)
                    {
                        if (ek.Typ == API.Towary.TypElementuKompletu.Produkt)
                            continue;

                        list.Add(new PozycjaDokHan()
                        {
                            TowarGuid = ek.Towar.Guid,
                            TowarKod = ek.Towar.Kod,
                            Ilosc = poz.Ilosc * ek.Ilosc,
                            Rabat = (decimal?)null,
                        });
                    }
                    agreguj = true;
                }
                else
                    list.Add(poz);
            }

            return list;
        }

        public Guid GetMapGuid(Guid guid)
        {
            using (var dc = new Enova.Business.Old.DB.Web.WebContext())
            {
                var cel = dc.GuidMaps.Where(g => g.Tabela == "Towary" && g.Zrodlo == guid).Select(g => g.Cel).FirstOrDefault();
                if (cel == null)
                    return guid;
                return cel.Value;
            }
        }

        private ICollection<PozycjaDokHan> mapujTowary(API.Business.Session session, IEnumerable<PozycjaDokHan> pozycje, ref bool agreguj)
        {
            List<PozycjaDokHan> list = new List<PozycjaDokHan>();
            var hm = session.GetModule<API.Towary.TowaryModule>();
            foreach (var poz in pozycje)
            {
                var guid = GetMapGuid(poz.TowarGuid);
                if (guid != poz.TowarGuid)
                {
                    var towar = hm.Towary[guid];
                    if (towar == null)
                        throw new Exception("Wystąpił błąd w trakcie mapowania towarów./r/nBrak towaru z Guid: " + guid.ToString());

                    list.Add(new PozycjaDokHan()
                    {
                        TowarGuid = towar.Guid,
                        TowarKod = towar.Kod,
                        Ilosc = poz.Ilosc,
                        Rabat = poz.Rabat
                    });
                    agreguj = true;
                }
                else
                    list.Add(poz);
            }
            return list;

        }
        /*
                public Enova.API.Handel.DokumentHandlowy FakturaDoZamowienia(Enova.API.Magazyny.Magazyn magazynDOD, DateTime data,
            Enova.API.Handel.DefDokHandlowego definicja, IDictionary<string, object> features = null,
        bool zatwierdz = true, Enova.API.Towary.Towar towarKosztowWysylki=null, decimal kosztWysylki=0, int? termin = null, Enova.API.Kasa.FormaPlatnosci sposobZaplaty = null)
        */
        public Enova.API.Handel.DokumentHandlowy FakturaDoZamowienia(Enova.API.Business.Session session, Enova.API.Magazyny.Magazyn magazynDOD, DateTime data,
Enova.API.Handel.DefDokHandlowego definicja, IDictionary<string, object> features = null,
bool zatwierdz = true, Enova.API.Towary.Towar towarKosztowWysylki = null, decimal kosztWysylki = 0, int? termin = null, Enova.API.Kasa.FormaPlatnosci sposobZaplaty = null)

        {
            API.Handel.DokumentHandlowy dokument = null;

            /*
            if (Enova.API.EnovaService.Instance.IsLogined)
            {
             */
            //using (var session = Enova.API.EnovaService.Instance.CreateSession())
            //{
            var crm = session.GetModule<API.CRM.CRMModule>();
            var handel = session.GetModule<API.Handel.HandelModule>();
            var kasa = session.GetModule<API.Kasa.KasaModule>();
            var tm = session.GetModule<API.Towary.TowaryModule>();
            using (var t = session.CreateTransaction())
            {
                API.CRM.Kontrahent kontrahent = crm.Kontrahenci[this.Kontrahent.Guid.Value];
                API.Magazyny.Magazyn magazyn = session.GetModule<API.Magazyny.MagazynyModule>().Magazyny[magazynDOD.Guid];
                API.Handel.DefDokHandlowego def = handel.DefDokHandlowych[definicja.Guid];
                dokument = API.EnovaService.Instance.CreateObject<API.Handel.DokumentHandlowy>();
                dokument.Definicja = def;
                dokument.Magazyn = magazyn;
                handel.DokHandlowe.AddRow(dokument);
                dokument.Kontrahent = kontrahent;
                dokument.Data = data;
                var defCeny = tm.DefinicjeCen["Hurtowa"];

                ICollection<PozycjaDokHan> pozycje = new List<PozycjaDokHan>();
                foreach (var p in this.PozycjeZamowienia.Where(r => r.Ilosc > 0 && r.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete))
                {
                    if (p.Produkt != null && p.Produkt.EnovaGuid != null)
                    {
                        pozycje.Add(new PozycjaDokHan()
                        {
                            TowarGuid = p.Produkt.EnovaGuid.Value,
                            TowarKod = p.Produkt.Kod,
                            Ilosc = p.Ilosc.Value,
                            Rabat = p.Rabat,
                            Cena = p.Cena,
                            ZmienionoRabat = p.ZmienionoRabat == null ? false : p.ZmienionoRabat.Value
                        });
                    }
                }

                bool agreguj;
                do
                {
                    agreguj = false;
                    pozycje = agregujPozycje(pozycje);
                    pozycje = mapujTowary(session, pozycje, ref agreguj);
                    pozycje = przeliczProdukty(session, pozycje, ref agreguj);
                } while (agreguj);

                pozycje = pozycje.OrderBy(p => p.TowarKod).ToList();
                string msg = "";
                //var stanMagWorker = API.EnovaService.Instance.CreateObject<API.Magazyny.StanMagazynuWorker>();

                foreach (var poz in pozycje)
                {
                    var towar = tm.Towary[poz.TowarGuid];
                    /*
                    stanMagWorker.Towar = towar;
                    if (stanMagWorker.Stan.Value < poz.Ilosc)
                    {
                        msg += "Stan ujemny: " + towar.Kod + " - " + towar.Nazwa + "\r\n";
                        continue;
                    }
                     */
                    var pdh = API.EnovaService.Instance.CreateObject<API.Handel.PozycjaDokHandlowego>(null, new object[] { dokument });
                    handel.PozycjeDokHan.AddRow(pdh);
                    pdh.Towar = towar;
                    pdh.Ilosc = poz.Ilosc;

                    pdh.UstawCenę(API.Towary.WyliczenieCeny.DodaniePozycji, defCeny, false);

                    var rabat = pdh.Rabat;

                    if (poz.Rabat != null && poz.ZmienionoRabat)
                        pdh.Rabat = poz.Rabat.Value;

                }
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);

                if (towarKosztowWysylki != null && kosztWysylki > 0)
                {
                    var pdh = API.EnovaService.Instance.CreateObject<API.Handel.PozycjaDokHandlowego>(null, new object[] { dokument });
                    handel.PozycjeDokHan.AddRow(pdh);
                    pdh.Towar = towarKosztowWysylki;
                    pdh.Ilosc = 1D;
                    pdh.Cena = kosztWysylki;
                }

                if (features != null && features.Count > 0)
                {
                    foreach (var kvp in features)
                    {
                        dokument.Features[kvp.Key] = kvp.Value;
                    }
                }

                session.EventsInvoke();

                /*
                if (termin != null)
                    dokument.UstawTermin(termin.Value);

                session.EventsInvoke();
                 */

                //dokument.Stan = API.Handel.StanDokumentuHandlowego.Zatwierdzony;
                dokument.Stan = API.Handel.StanDokumentuHandlowego.Bufor;

                t.Commit();
            }
            session.Save();

            //} // using

            //} //if is logined


            return dokument;
        }


        #region Edit Record Implementation

        public bool SaveChanges()
        {
            try
            {
                int synch = (int)this.Synchronizacja;
                if (this.EntityState == EntityState.Detached)
                    ContextManager.WebContext.AddToZamowienia(this);

                if (this.EntityState == EntityState.Added || synch == (int)RowSynchronizeOld.Notsaved)
                {
                    synch = (int)RowSynchronizeOld.NotsynchronizedNew;
                    this.Stamp = DateTime.Now;
                }
                else if (this.EntityState == EntityState.Modified && synch == (int)RowSynchronizeOld.Synchronized)
                {
                    synch = (int)RowSynchronizeOld.NotsynchronizedEdit;
                    this.Stamp = DateTime.Now;
                }

                ContextManager.WebContext.OptimisticSaveChanges();
                ContextManager.WebContext.Refresh(RefreshMode.StoreWins, this);
                if (StatusZamowienia == null || StatusZamowienia.NoweZamowienie.Value || StatusZamowienia.DoMagazynu.Value)
                    this.DataAtualizacji = DateTime.Now;

                /*
                if (StatusZamowienia != null && StatusZamowienia.Spakowane.Value)
                {
                    var pozycje = this.PozycjeZamowienia.Where(p => p.Ilosc < p.IloscOrg).ToList();
                    foreach (var poz in pozycje)
                    {
                        if (poz.GetTowarDostepny())
                            poz.SetVisibleAV(true);
                    }
                }
                 */

                this.Synchronizacja = synch;
                ContextManager.WebContext.OptimisticSaveChanges();
                ContextManager.WebContext.Refresh(RefreshMode.StoreWins, this);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UndoChanges()
        {
            if (EntityState == EntityState.Modified && Synchronizacja != (int)RowSynchronizeOld.Notsaved)
                ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);

            if (this.EntityState != EntityState.Added && this.EntityState != EntityState.Detached)
            {
                this.PozycjeZamowienia.Load();
                this.HistoriaZamowienia.Load();
                this.Wiadomosci.Load();
            }
            else if (this.EntityState == EntityState.Added)
            {
                foreach (var p in this.PozycjeZamowienia.ToList())
                {
                    ContextManager.WebContext.DeleteObject(p);
                }



                foreach (var h in this.HistoriaZamowienia.ToList())
                {
                    ContextManager.WebContext.DeleteObject(h);
                }

                foreach (var w in this.Wiadomosci.ToList())
                {
                    ContextManager.WebContext.DeleteObject(w);
                }
                ContextManager.WebContext.DeleteObject(this);
            }


            return true;
        }

        public bool DeleteRecord()
        {
            if (User.LoginedUser.CheckPerissions(true, null, false) || User.LoginedUser.CheckAgent(this.PrzedstawicielKod))
            {
                this.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                ContextManager.WebContext.SaveChanges();
                ContextManager.WebContext.Refresh(RefreshMode.StoreWins, this);
                //this.AktualizujStanMagazynu();
                return true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Nie posiadasz wystarczających uprawnień do wykonania tej operacji", "EnovaTools",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region IValidation Implementation

        bool IValidation.IsValid
        {
            get
            {
                if (this.Kontrahent == null)
                {
                    validationError = "Błąd: Nie wybrano kontrahenta!!!";
                    return false;
                }
                if (StatusZamowienia.NoweZamowienie.Value && RodzajTransportu == Types.RodzajTransportu.NieWybrano)
                {
                    validationError = "Błąd: Nie wybrano rodzaju transportu!!!";
                    return false;
                }
                if (string.IsNullOrEmpty(this.Sezon))
                {
                    validationError = "Błąd: Nie wybrano sezonu!!!";
                    return false;
                }
                return true;
            }
        }

        private string validationError = "";
        string IValidation.ValidationError
        {
            get
            {
                return validationError;
            }
        }

        object IValidation.ValidationInfo
        {
            get { return null; }
        }

        #endregion

        #region IBlockOnEditRecord Implementation

        bool IBlockOnEditRecord.Block
        {
            get
            {
                return Blokada == null ? false : Blokada.Value;
            }
            set
            {
                Blokada = value;
            }
        }

        string IBlockOnEditRecord.BlockInfo
        {
            get
            {
                return BlokadaKto;
            }
            set
            {
                BlokadaKto = value;
            }
        }

        DateTime IBlockOnEditRecord.BlockStamp
        {
            get
            {
                return BlokadaStamp == null ? DateTime.Now : BlokadaStamp.Value;
            }
            set
            {
                BlokadaStamp = value;
            }
        }

        #endregion


        #region Do usuniecia

        /*
                public bool Zatwierdzony
                {
                    get { throw new NotImplementedException(); }
                }

                public bool Anulowany
                {
                    get { throw new NotImplementedException(); }
                }

                public bool Bufor
                {
                    get { throw new NotImplementedException(); }
                }

                public Enova.API.Handel.StanDokumentuHandlowego Stan
                {
                    get { throw new NotImplementedException(); }
                    set { }
                }

                public API.Handel.IDefDokHandlowego Definicja
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                    set
                    {
                        throw new NotImplementedException();
                    }
                }

                public API.Handel.IPozycjaDokHan NowaPozycja(API.Towary.ITowar towar, double ilosc)
                {
                    throw new NotImplementedException();
                }

                public object Record
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                    set
                    {
                        throw new NotImplementedException();
                    }
                }

                public object GetValue(string name)
                {
                    throw new NotImplementedException();
                }

                public void SetValue(string name, object value)
                {
                    throw new NotImplementedException();
                }

                public API.Business.ISession Session
                {
                    get { throw new NotImplementedException(); }
                }

                public object CallMethod(string name, Type[] argsTypes, object[] args)
                {
                    throw new NotImplementedException();
                }

                public API.Magazyny.IMagazyn Magazyn
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                    set
                    {
                        throw new NotImplementedException();
                    }
                }

                public API.Magazyny.IMagazyn MagazynDo
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                    set
                    {
                        throw new NotImplementedException();
                    }
                }

                public string Opis
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                    set
                    {
                        throw new NotImplementedException();
                    }
                }

                public object EnovaObject
                {
                    get { throw new NotImplementedException(); }
                }

                public T GetValue<T>(string name)
                {
                    throw new NotImplementedException();
                }

                public object CallMethod(string name, params object[] args)
                {
                    throw new NotImplementedException();
                }

                public object CallMethodFull(string name, Type[] paramTypes, object[] parameters)
                {
                    throw new NotImplementedException();
                }

                public object GetValue(string name, object[] idexes)
                {
                    throw new NotImplementedException();
                }


                API.Business.FeatureCollection API.Business.IRow.Features
                {
                    get { throw new NotImplementedException(); }
                }

                object API.Types.IObjectBase.EnovaObject
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                    set
                    {
                        throw new NotImplementedException();
                    }
                }

                public object GetObjValue(object obj, string name, Type[] types = null, object[] index = null)
                {
                    throw new NotImplementedException();
                }

                public void SetObjValue(object obj, string name, object value)
                {
                    throw new NotImplementedException();
                }

                public object CallObjMethod(object obj, string name, Type[] paramTypes, object[] parameters)
                {
                    throw new NotImplementedException();
                }

         */
        #endregion

        #region Nested Types

        public class PozycjaDokHan
        {
            public Guid TowarGuid { get; set; }
            public string TowarKod { get; set; }
            public double Ilosc { get; set; }
            public decimal? Rabat { get; set; }
            public decimal? Cena { get; set; }
            public bool ZmienionoRabat { get; set; }
        }


        #endregion
    }
}
