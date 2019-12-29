using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Windows.Forms;
using Enova.Business.Old.Core;
using Enova.Business.Old.Types;
using Enova.Old.Handel;

namespace Enova.Business.Old.DB.Web
{
    [DataEditForm("AbakTools.Zwroty.Forms.ZwrotEditForm, AbakTools.Handel.Forms")]
    public partial class Zwrot : ISaveChanges, IDeleteRecord, IUndoChanges, IValidation, IDbContext
    {
        public static int IloscDniAnalizy = 365;

        ObjectContext IDbContext.DbContext { get; set; }
        public WebContext DbContext { get { return (WebContext)((IDbContext)this).DbContext; } }

        public int GetMaxIdent()
        {
            if (this.Pozycje.Count == 0)
                return 0;
            return this.Pozycje.Max(p => p.Ident);
        }

        public HistoriaZwrotu OstHistoriaZwrotu
        {
            get
            {
                return this.HistoriaZwrotu.Where(h => h.Deleted == false && h.Synchronize != (int)RowSynchronizeOld.NotsynchronizedDelete)
                    .OrderBy(h => h.Data).LastOrDefault();
            }
        }

        public StatusZwrotu Status
        {
            get
            {
                var h = this.OstHistoriaZwrotu;
                if (h != null)
                    return h.Status;
                return null;
            }
        }

        public string OpisLine
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Opis))
                    return this.Opis.Trim().Replace("\r\n", "  ");
                return null;
            }
        }

        public int Kolejnosc
        {
            get
            {
                if (this.Status == null)
                    return 0;

                switch ((TypStatusuZwrotu)this.Status.Typ)
                {
                    case TypStatusuZwrotu.Nieznany:
                        return 100;
                    case TypStatusuZwrotu.Zarejestrowany:
                        return 200;
                    case TypStatusuZwrotu.Liczony:
                        return 300;
                    case TypStatusuZwrotu.Sprawdzony:
                        return 400;
                    case TypStatusuZwrotu.ZwrotZNieskorygowanych:
                        return 450;
                    case TypStatusuZwrotu.Załatwiony:
                        return 500;
                }

                return 1000;
            }
        }

        public string Przedstawiciel
        {
            get
            {
                if (this.Kontrahent != null && this.Kontrahent.Przedstawiciel != null)
                    return this.Kontrahent.Przedstawiciel.Kod;
                return null;
            }
        }

        public string SezonAll
        {
            get
            {
                var sezony = new ArrayList();
                if (Sezon != null)
                    sezony.Add(Sezon);
                if (Sezon2 != null)
                    sezony.Add(Sezon2);
                if (Sezon3 != null)
                    sezony.Add(Sezon3);
                if (Sezon4 != null)
                    sezony.Add(Sezon4);

                return String.Join("+", sezony.ToArray());
            }
        }

        public void SetStatus(TypStatusuZwrotu typ)
        {
            var status = StatusZwrotu.GetByTyp(this.DbContext, typ);
            if (status != null)
            {
                SetStatus(status);
            }
            else
                throw new Exception("Nie istnieje status zwrotu dla typu " + typ.ToString());
        }

        public void SetStatus(StatusZwrotu status)
        {
            var user = this.DbContext.Users.Where(r => r.ID == User.LoginedUser.ID).FirstOrDefault();
            var hist = new HistoriaZwrotu()
            {
                Guid = Guid.NewGuid(),
                Data = DateTime.Now,
                Status = status,
                Uzytkownik = user,
                Deleted = false,
                Synchronize = (int)RowSynchronizeOld.NotsynchronizedNew
            };

            this.HistoriaZwrotu.Add(hist);

            this.OstatniStatus = status;
            this.OstatniaHistoriaUser = user;
        }

        public string OstatniStatusStr
        {
            get
            {
                string str = this.OstatniStatus.ToString();
                if (this.OstatniStatus.Typ == (int)TypStatusuZwrotu.Załatwiony)
                {
                    if (this.SkorygowanyWCalosci != null && !this.SkorygowanyWCalosci.Value)
                        str += " częściowo";
                }
                str += this.OstatniaHistoriaUser != null ? " (" + this.OstatniaHistoriaUser.Login + ")" : "";
                return str;
            }
        }

        public bool SaveChanges()
        {
            try
            {
                var dc = Enova.Business.Old.Core.ContextManager.WebContext;

                if (this.Kontrahent == null && this.EntityState != System.Data.EntityState.Detached)
                    dc.Detach(this);

                if (this.EntityState == System.Data.EntityState.Detached)
                    return false;

                if (this.HistoriaZwrotu.Count == 0)
                {
                    StatusZwrotu status = StatusZwrotu.GetByTyp(DbContext, TypStatusuZwrotu.Zarejestrowany);
                    this.HistoriaZwrotu.Add(new HistoriaZwrotu()
                    {
                        Data = DateTime.Now,
                        Deleted = false,
                        Guid = Guid.NewGuid(),
                        Status = status,
                        Synchronize = (int)RowSynchronizeOld.NotsynchronizedNew,
                        Uzytkownik = User.LoginedUser
                    });
                }


                if (this.EntityState == System.Data.EntityState.Modified && this.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedNew)
                {
                    this.DataModyfikacji = DateTime.Now;
                    this.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                }


                foreach (var pozycja in this.Pozycje.ToList())
                {
                    if (pozycja.ToRemove)
                    {
                        dc.DeleteObject(pozycja);
                    }
                }
                this.ReIdentPozycje();

                if (this.Status != null && this.Status.Typ == (int)TypStatusuZwrotu.Liczony)
                {
                    DialogResult result = MessageBox.Show("Czy zwrot został przeliczony w całości?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        this.SetStatus(TypStatusuZwrotu.Sprawdzony);
                    }
                }

                dc.OptimisticSaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return false;
            }
        }

        public void ReIdentPozycje()
        {
            int ident = 1;
            foreach (var poz in this.Pozycje
                .Where(p => p.ToRemove == false && p.Deleted == false && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).OrderBy(p => p.Ident).ToList())
                poz.Ident = ident++;
        }

        public bool DeleteRecord()
        {
            try
            {
                /*
                var dc = Core.ContextManager.WebContext;
                foreach (var p in this.Pozycje.ToList())
                    dc.DeleteObject(p);

                dc.DeleteObject(this);
                dc.OptimisticSaveChanges();
                return true;
                 */
                var dc = Core.ContextManager.WebContext;
                this.Deleted = true;
                dc.OptimisticSaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return false;
            }
        }

        public bool UndoChanges()
        {
            try
            {
                var dc = Core.ContextManager.WebContext;
                foreach (var p in this.Pozycje.ToList())
                {
                    if (p.EntityState == System.Data.EntityState.Added)
                        dc.DeleteObject(p);
                    else if (p.EntityState == System.Data.EntityState.Modified)
                        dc.Refresh(RefreshMode.StoreWins, p);
                    else if (p.ToRemove)
                        p.ToRemove = false;
                }

                if (this.EntityState == System.Data.EntityState.Added)
                    dc.DeleteObject(this);
                else if (this.EntityState == System.Data.EntityState.Modified)
                    dc.Refresh(RefreshMode.StoreWins, this);
                
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                return false;
            }
        }

        public void PrzeliczWartosc()
        {
            this.WartoscNetto = decimal.Round(this.Pozycje.ToList()
                .Where(p=>p.Deleted == false && p.Synchronizacja!=(int)RowSynchronizeOld.NotsynchronizedDelete && p.ToRemove == false).Sum(p => p.WartoscNetto), 2);
        }

        public List<PozycjaZwrotuAnalizaOld> GetPozycjeEnova(EnovaContext ec)
        {
            var pozycje = (from p in this.Pozycje
                           where p.Deleted == false && p.Synchronizacja!= (int)RowSynchronizeOld.NotsynchronizedDelete
                           group p by p.Towar.EnovaGuid into g
                           select new { TowarGuid = g.Key.Value, Ilocsc = g.Sum(r => r.Ilosc) }).ToList();

            var enovaPozycje = new List<PozycjaZwrotuAnalizaOld>();

            foreach (var p in pozycje)
            {
                var towar = Enova.Business.Old.DB.Towar.GetByGuid(ec, p.TowarGuid);
                if (towar == null)
                    throw new Exception("Nie znaleziono Towaru o guid " + p.TowarGuid.ToString());
                enovaPozycje.Add(new PozycjaZwrotuAnalizaOld() { Towar = towar, Ilosc = p.Ilocsc });
            }

            enovaPozycje = enovaPozycje.OrderBy(p => p.TowarKod).ToList();

            int ident = 1;
            foreach (var p in enovaPozycje)
                p.Ident = ident++;

            return enovaPozycje;
                          
        }

        public ZwrotAnaliza AnalizujZwrot(Session session)
        {
            if (this.Kontrahent != null)
            {
                var kontrahent = this.Kontrahent.GetKontrahentEnova((EnovaContext)session.DataContext);
                if (kontrahent != null)
                {
                    ZwrotAnaliza analiza = new ZwrotAnaliza();

                    var pozycje = this.GetPozycjeEnova((EnovaContext)session.DataContext);

                    var hm = Enova.Old.Handel.HandelModule.GetInstance(session);

                    var defFV = hm.DefDokHandlowych.FakturaSprzedaży;

                    DateTime dateFrom = this.DataDodania.AddDays(-IloscDniAnalizy);

                    ZwrotAnalizaDokHandlowyCollectionOld dokumenty = new ZwrotAnalizaDokHandlowyCollectionOld();

                    foreach (var pozycja in pozycje)
                    {
                        pozycja.PozycjeDokHan = pozycja.Towar.PozycjeDokHandlowych.WgDefDokHan[defFV].WgDaty[dateFrom].WgKontrahenta[kontrahent]
                            .WgStanuDokHan[StanDokumentuHandlowego.Zatwierdzony];

                        if (pozycja.PozycjeDokHan.Count() == 0)
                        {
                            var zamienniki = pozycja.Towar.ZamiennikiTowaru.Select(zt => zt.Zamiennik).ToList();
                            foreach (var z in zamienniki)
                            {
                                var pozDok = z.PozycjeDokHandlowych.WgDefDokHan[defFV].WgDaty[dateFrom].WgKontrahenta[kontrahent].WgStanuDokHan[StanDokumentuHandlowego.Zatwierdzony];
                                if (pozDok.Count() > 0)
                                {
                                    pozycja.Towar = z;
                                    pozycja.PozycjeDokHan = pozDok;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            var iloscPoKorektachSuma = pozycja.PozycjeDokHan.ToList().Sum(p => p.IloscPoKorektach);
                            if (iloscPoKorektachSuma < pozycja.Ilosc)
                            {
                                var zamienniki = pozycja.Towar.ZamiennikiTowaru.Select(zt => zt.Zamiennik).ToList();
                                foreach (var z in zamienniki)
                                {
                                    var pozDok = z.PozycjeDokHandlowych.WgDefDokHan[defFV].WgDaty[dateFrom].WgKontrahenta[kontrahent].WgStanuDokHan[StanDokumentuHandlowego.Zatwierdzony];
                                    if (pozDok.Count() > 0)
                                    {
                                        var iloscZamPoKorektachSuma = pozDok.ToList().Sum(p => p.IloscPoKorektach);
                                        if (iloscZamPoKorektachSuma >= pozycja.Ilosc)
                                        {
                                            pozycja.Towar = z;
                                            pozycja.PozycjeDokHan = pozDok;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        


                        pozycja.DokHandlowe = pozycja.PozycjeDokHan.ToList().Select(p => p.Dokument);


                        foreach (var dh in pozycja.DokHandlowe)
                        {
                            dokumenty.Add(dh, pozycja);
                        }
                        
                    }

                    dokumenty = dokumenty.GetOrderByDataDesc();

                    Type ptype =typeof(PozycjaZwrotuAnalizaOld);

                    foreach (var pozycja in pozycje)
                    {

                        pozycja.DokHandloweAnaliza = dokumenty.GetContainsPozycja(pozycja).GetOrderByDescIloscPozycji();

                        foreach (var dh in pozycja.DokHandlowe)
                        {
                            int idx = dokumenty.IndexOf(dh);
                            if (idx <= 9)
                            {
                                var pdh = pozycja.PozycjeDokHan.WgDokument[dh].First();
                                var pkorekty = pdh.PozycjaKorygującaOstatnia;
                                var ilosc = pkorekty != null ? pkorekty.IloscValue : pdh.IloscValue;
                                var pinfo = ptype.GetProperty("Dokument" + idx.ToString());
                                var piinfo = ptype.GetProperty("Dokument" + idx.ToString() + "Ilosc");
                                if (pinfo != null)
                                {
                                    pinfo.SetValue(pozycja, dh, null);
                                }
                                if (piinfo != null)
                                {
                                    piinfo.SetValue(pozycja, ilosc, null);
                                }

                            }
                        }
                    }

                    analiza.Dokumenty = dokumenty;
                    analiza.Pozycje = pozycje;


                    return analiza;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return this.ID.ToString() + " (" + this.DataDodania.ToShortDateString() + ")";
        }

        #region IValidation implementation

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Sezon))
                {
                    validationError = "Błąd: nie wybrano sezonu !!!";
                    return false;
                }
                return true;
            }
        }

        private string validationError;
        public string ValidationError
        {
            get { return validationError; }
        }

        public object ValidationInfo
        {
            get { throw new NotImplementedException(); }
        }

        #endregion


    }
}
