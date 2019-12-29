using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Windows.Forms;
using Enova.Business.Old.DB;
using Enova.Old.Towary;
using Enova.Old.Handel;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.Zwroty
{
    public partial class AnalizaZwrotu : IDisposable
    {
        #region Fields

        private bool disposed;
        public static int OkresAnalizy = 427;
        private DB.Web.Zwrot zwrot;
        private Kontrahent kontrahent;
        private DokumentyAnalizyZwrotu dokumenty;
        private PozycjeAnalizyZwrotu pozycje;
        private Session session;
        private TowaryModule towaryModule;
        private HandelModule handelModule;
        private Enova.Old.CRM.CRMModule crmModule;
        private PozycjeDokHan pozycjeDokHan;
        private DateTime? analizujOd;

        #endregion

        #region Properties

        public DB.Web.Zwrot Zwrot
        {
            get { return this.zwrot; }
        }

        public Kontrahent Kontrahent
        {
            get { return this.kontrahent; }
        }

        public DokumentyAnalizyZwrotu Dokumenty
        {
            get { return this.dokumenty; }
        }

        public PozycjeAnalizyZwrotu Pozycje
        {
            get { return this.pozycje; }
        }

        public TowaryModule TowaryModule
        {
            get
            {
                if (this.towaryModule == null)
                    this.towaryModule = TowaryModule.GetInstance(this.session);
                return this.towaryModule;
            }
        }

        public HandelModule HandelModule
        {
            get
            {
                if (this.handelModule == null)
                    this.handelModule = HandelModule.GetInstance(this.session);
                return this.handelModule;
            }
        }

        public Enova.Old.CRM.CRMModule CRMModule
        {
            get
            {
                if (this.crmModule == null)
                    this.crmModule = Enova.Old.CRM.CRMModule.GetInstance(this.session);
                return this.crmModule;
            }
        }

        public DateTime AnalizujOd
        {
            get
            {
                if (this.analizujOd == null)
                    this.analizujOd = this.Zwrot.DataDodania.Date.AddDays(-OkresAnalizy);
                return this.analizujOd.Value;
            }
            set
            {
                this.analizujOd = value;
            }
        }

        /*
        public API.IEnovaService EnovaService
        {
            get
            {
                if (this.enovaService == null)
                    this.enovaService = API.EnovaServiceAttribute.EnovaService;
                return this.enovaService;
            }
        }
         */

        /*
        public API.Business.ISession EnovaSession
        {
            get
            {
                if (this.enovaSession == null)
                    this.enovaSession = this.EnovaService.CreateSession();
                return this.enovaSession;
            }
        }
         */

        #endregion

        #region Methods

        public AnalizaZwrotu(Session session, DB.Web.Zwrot zwrot, DateTime? analizujOd = null)
        {
            this.session = session;
            this.zwrot = zwrot;
            this.analizujOd = analizujOd;
            this.kontrahent = this.CRMModule.Kontrahenci[this.zwrot.Kontrahent.Guid.Value];
            /*
            var defFV = HandelModule.DefDokHandlowych.FakturaSprzedaży;
            DateTime dateFrom = this.Zwrot.DataDodania.Date.AddDays(-OkresAnalizy);
            this.pozycjeDokHan = HandelModule.PozycjeDokHan.WgStanuDokHan[StanDokumentuHandlowego.Zatwierdzony].WgDefDokHan[defFV].WgDaty[AnalizujOd].WgKontrahenta[this.kontrahent];
            this.initPozycje();
            this.initDokumenty();
            this.analizujIlosci();
            this.cleanDokumenty();
            this.Pozycje.Renumeruj();
             */
            this.Analizuj();
        }

        public void Analizuj()
        {
            this.dokumenty = null;
            this.pozycje = null;
            this.pozycjeDokHan = null;
            var defFV = HandelModule.DefDokHandlowych.FakturaSprzedaży;
            this.pozycjeDokHan = HandelModule.PozycjeDokHan.WgStanuDokHan[StanDokumentuHandlowego.Zatwierdzony].WgDefDokHan[defFV].WgDaty[this.AnalizujOd].WgKontrahenta[this.kontrahent];
            this.initPozycje();
            this.initDokumenty();
            this.analizujIlosci();
            this.cleanDokumenty();
            this.Pozycje.Renumeruj();
        }

        private void initPozycje()
        {
            var list = (from p in this.Zwrot.Pozycje
                           where p.Deleted == false && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete
                           group p by p.Towar.EnovaGuid into g
                           select new { TowarGuid = g.Key.Value, Ilosc = g.Sum(r => r.Ilosc), Group = g }).ToList();

            this.pozycje = new PozycjeAnalizyZwrotu();
            this.pozycje.removeWithDocument = true;

            foreach (var p in list)
            {
                Towar towar = TowaryModule.Towary[p.TowarGuid];
                if (towar == null)
                    throw new Exception("AnalizaZwrotu.initPozycje(): Nie znaleziono Towaru o Guid: " + p.TowarGuid.ToString());

                this.pozycje.Add(new PozycjaAnalizyZwrotu(this, towar, p.Ilosc));
            }

        }

        private void initDokumenty()
        {
            this.dokumenty = new DokumentyAnalizyZwrotu(this);
            foreach (var pozycja in this.Pozycje)
            {
                this.initDokumenty(pozycja);
            }
        }

        private void initDokumenty(PozycjaAnalizyZwrotu pozycja)
        {
            var dokumentyHan = this.pozycjeDokHan.WgTowar[pozycja.Towar].BaseQuery.Select(p => p.Dokument).ToList();

            foreach (var dh in dokumentyHan)
                this.dokumenty.Add(dh, pozycja);
        }

        private void cleanDokumenty()
        {
            foreach (var doc in this.dokumenty.Where(d => d.IloscPozycji == 0).ToList())
                this.dokumenty.Remove(doc);
        }

        private void zamienTowarPozycji(PozycjaAnalizyZwrotu pozycja, Towar nowyTowar)
        {
            this.Pozycje.Remove(pozycja);
            var nowaPozycja = new PozycjaAnalizyZwrotu(this, nowyTowar, pozycja.Ilosc);
            this.Pozycje.Add(nowaPozycja);
            this.initDokumenty(nowaPozycja);
        }

        private void dodajTowarDoPozycji(PozycjaAnalizyZwrotu pozycja, Towar towar, double ilosc)
        {
            pozycja.ilosc -= ilosc;
            var nowaPozycja = new PozycjaAnalizyZwrotu(this, towar, ilosc);
            this.Pozycje.Add(nowaPozycja);
            this.initDokumenty(nowaPozycja);
        }

        public SortedSet<ZamiennikIlosc> GetZamienniki(PozycjaAnalizyZwrotu pozycja)
        {
            var zamienniki = pozycja.Towar.ZamiennikiTowaru.Select(z => z.Zamiennik).ToList();
            SortedSet<ZamiennikIlosc> ilosciZam = new SortedSet<ZamiennikIlosc>();

            foreach (var zam in zamienniki)
            {
                var pdhs = this.pozycjeDokHan.WgTowar[zam].ToList();
                double iloscPoKorektach = 0;
                foreach (var pdh in pdhs)
                    iloscPoKorektach += pdh.IloscPoKorektach;
                if (iloscPoKorektach > 0)
                    ilosciZam.Add(new ZamiennikIlosc() { Towar = zam, Ilosc = iloscPoKorektach });

            }
            return ilosciZam;
        }

        private void analizujIlosci()
        {
            foreach (var poz in this.Pozycje.ToList())
            {
                if (poz.Dokumenty.Count == 0)
                {
                    var zamienniki = this.GetZamienniki(poz);

                    if (zamienniki.Count > 0)
                    {
                        this.zamienTowarPozycji(poz, zamienniki.First().Towar);
                    }
                    continue;
                }

                var iloscPoKor = poz.GetIlosciPoKorektech();

                if (iloscPoKor == 0)
                {
                    poz.RemoveDokumenty();
                    var zamienniki = this.GetZamienniki(poz);
                    if (zamienniki.Count > 0)
                    {
                        this.zamienTowarPozycji(poz, zamienniki.First().Towar);
                    }
                }
                else if (iloscPoKor < poz.Ilosc)
                {
                    var zamienniki = this.GetZamienniki(poz);

                    if (zamienniki.Count > 0)
                    {
                        var first = zamienniki.First();
                        if (first.Ilosc >= poz.Ilosc)
                        {
                            this.zamienTowarPozycji(poz, first.Towar);
                        }
                        else
                        {
                            double brakujacaIlosc = poz.ilosc - iloscPoKor;
                            foreach (var zam in zamienniki)
                            {
                                var ilosc = brakujacaIlosc < zam.Ilosc ? brakujacaIlosc : zam.Ilosc;
                                this.dodajTowarDoPozycji(poz, zam.Towar, ilosc);
                                brakujacaIlosc -= ilosc;

                                if (brakujacaIlosc == 0)
                                    break;
                            }

                        }
                    }


                }
            }
        }

        public void Koryguj()
        {
            foreach (var p in this.pozycje)
            {
                p.Koryguj();
            }
        }

        public DokumentAnalizyZwrotu GetDokumentByName(string documentName)
        {
            if (documentName.StartsWith("Dokument"))
            {
                int idx;
                if (int.TryParse(documentName.Substring(8), out idx))
                {
                    return this.Dokumenty[idx];
                }
            }
            return null;
        }

        public void WystawKorekty(Enova.API.Business.Session session)
        {

            if (!Enova.API.EnovaService.Instance.IsLogined)
            {
                throw new Exception("Nie jesteś zalogowany do programu Enova");
            }

            try
            {
                throw new NotImplementedException();
                /*
 
                foreach (var adh in this.Dokumenty.Where(d => d.WystawicKorekte))
                {
                    //var korekta = Enova.API.EnovaServiceAttribute.EnovaService.HandelModule.KorektaDoZwrotu(DateTime.Now.Date, adh, true);
                    //var korekta = this.EnovaSession.GetModule<API.Handel.IHandelModule>().KorektaDoZwrotu(DateTime.Now.Date, adh, true);
                    var korekta = session.GetModule<API.Handel.IHandelModule>().KorektaDoZwrotu(DateTime.Now.Date, adh, true);
                }

                this.session.Save();
                 */
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                BAL.Business.AppController.ThrowException(ex);
            }

        }

        private void Dispose(bool userCall)
        {
            if (!disposed)
            {
                /*
                if (this.enovaSession != null)
                    this.enovaSession.Dispose();
                 */
                disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AnalizaZwrotu()
        {
            this.Dispose(false);
        }

        #endregion

        #region NestedTypes

        public class ZamiennikIlosc : IEquatable<ZamiennikIlosc>, IComparable<ZamiennikIlosc>
        {
            public Towar Towar { get; set; }
            public double Ilosc { get; set; }

            public int CompareTo(ZamiennikIlosc zi)
            {
                return zi.Ilosc.CompareTo(this.Ilosc);
            }

            public bool Equals(ZamiennikIlosc zm)
            {
                return this.Towar.Guid == zm.Towar.Guid;
            }

            public override bool Equals(object obj)
            {
                return this.Equals((ZamiennikIlosc)obj);
            }

            public override int GetHashCode()
            {
                return this.Towar.GetHashCode();
            }
        }

        #endregion
    }
}
