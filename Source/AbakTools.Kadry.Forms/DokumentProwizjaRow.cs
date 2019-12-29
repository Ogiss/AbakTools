using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Kasa;
using Enova.API.Handel;
using Enova.API.Magazyny;


namespace AbakTools.Kadry.Forms
{
    internal class DokumentProwizjaRow
    {
        #region Fields

        //private DokumentHandlowy dokument;
        //private RozrachunekIdx rozrachunek;
        //private Enova.Business.Old.DB.DokumentHandlowy dokumentEF;
        //private Enova.Business.Old.DB.RozrachunekIdx rozrachunekEF;
        private DokumentInfo dokumentInfo;
        private decimal prowizjaProcent;
        public bool KaryWgCechy;
        public string CechaKary;
        public decimal WspolczynnikKary;
        
        private decimal? wartoscZakupuNetto;
        private decimal? wartoscCenaAbak;
        private decimal? wartoscProwizji;
        private decimal? wartoscKary;
        private decimal? rozliczono;
        private bool? rozliczany;

        #endregion

        #region Properties

        public int DokumentID
        {
            get { return dokumentInfo.ID; }
        }

        public int MagazynID
        {
            get { return dokumentInfo.Magazyn; }
        }

        public DateTime Data
        {
            get
            { 
                return dokumentInfo.Data;

            }
        }

        public string NumerPelny
        {
            get
            { 
                return dokumentInfo.NumerPelny;
            }
        }

        public string KodKontrahenta
        {
            get
            {
                return dokumentInfo.KontrahentKod;
            }
        }

        public bool Korekta
        {
            get
            { 
                return dokumentInfo.Korekta;
            }
        }

        public decimal WartoscSprzedazyNetto
        {
            get
            { 
                return dokumentInfo.SprzedazNetto;
            }
        }

        public decimal WartoscSprzedazyBrutto
        {
            get
            { 
                return dokumentInfo.SprzedazBrutto;
            }
        }

        public decimal WartoscZakupuNetto
        {
            get
            {
                return dokumentInfo.ZakupNetto;
            }
        }

        public decimal WartoscCenaAbak
        {
            get
            {
                return dokumentInfo.ZakupNetto;
            }
        }

        public decimal Prowizja
        {
            get
            {
                return decimal.Round((Dochod * prowizjaProcent / 100M) * (this.Korekta ? WspolczynnikKary : 1M), 2);
            }
        }

        public decimal PotracenieProwizji
        {
            get
            {
                var pr = (dokumentInfo.SprzedazBrutto - Rozliczono) / dokumentInfo.SprzedazBrutto;
                return Prowizja * pr * -0.25M;
                 
            }
        }

        [Obsolete("Do usunięcia")]
        public decimal PotrącenieProwizji
        {
            get { return PotracenieProwizji; }
        }

        public decimal Dochod
        {
            get { return WartoscSprzedazyNetto - WartoscCenaAbak; }
        }

        public decimal Kara
        {
            get
            {
                if (dokumentInfo.Korekta && wartoscKary != null)
                    return wartoscKary.Value;
                return 0;
            }
        }

        public DateTime DataRozliczenia
        {
            get
            { 
                return dokumentInfo.DataRozliczenia;
            }
        }

        public decimal DoRozliczenia
        {
            get
            {
                return dokumentInfo.Kwota - Rozliczono;
            }
        }

        public decimal Rozliczono
        {
            get
            {
                return rozliczono != null ? rozliczono.Value : dokumentInfo.KwotaRozliczona;
            }
        }

        public DateTime Termin
        {
            get
            {
                return dokumentInfo.Termin;
            }
        }

        public decimal? Marza
        {
            get
            {
                if (Dochod != null && WartoscSprzedazyNetto != 0)
                {
                    return Dochod / WartoscSprzedazyNetto;
                }
                return null;
            }
        }

        public string Przedstawiciel
        {
            get
            {
                /*
                return (string)dokument.Features["PRZEDSTAWICIEL"];
                 */
                return "";
            }
        }

        public bool Rozliczany { get; set; }
        public DateTime TerminGraniczny { get; set; }
        public DateTime? RozliczeniaDataDo { get; set; }


        #endregion

        #region Method

        public DokumentProwizjaRow(DokumentInfo dokInfo, decimal prowizjaProcent)
        {
            this.dokumentInfo = dokInfo;
            this.prowizjaProcent = prowizjaProcent;
        }

        /*
        public DokumentProwizjaRow(DokumentHandlowy dokument, RozrachunekIdx rozrachunek, decimal prowizjaProcent)
        {
            if (dokument == null)
                throw new ArgumentNullException("dokument");
            this.dokument = dokument;
            this.rozrachunek = rozrachunek;
            this.prowizjaProcent = prowizjaProcent;
        }
         */

        /*
        public DokumentProwizjaRow(Enova.Business.Old.DB.DokumentHandlowy dokument, Enova.Business.Old.DB.RozrachunekIdx rozrachunek, decimal prowizjaProcent)
        {
            if(dokument == null)
                throw new ArgumentNullException("dokument");
            this.dokumentEF = dokument;
            this.rozrachunekEF = rozrachunek;
            this.prowizjaProcent = prowizjaProcent;
        }
         */


        /*
        private void przeliczZakup()
        {
            wartoscZakupuNetto = 0;
            wartoscCenaAbak = 0;
            bool flag = false;

            if (dokument != null)
            {
                if (dokument.Korekta)
                {
                    wartoscKary = 0;
                    wartoscProwizji = 0;
                    flag = KaryWgCechy && !string.IsNullOrWhiteSpace(CechaKary);
                }

                foreach (var pdh in dokument.DokumentMagazynowyGłówny.Pozycje.Cast<PozycjaDokHandlowego>().ToList())
                {
                    var obroty = pdh.Obroty.Cast<Obrot>().ToList().Where(o => o.Korekta != Enova.API.Magazyny.KorektaObrotu.StornoZasobu);
                    var zakupNetto = obroty.Sum(o => o.Przychod.Wartosc);
                    var cenaAbak = (decimal?)pdh.Features["Prowizja"];
                    wartoscZakupuNetto += zakupNetto;
                    decimal zakupCenaAbak = 0;
                    if (cenaAbak == null || cenaAbak.Value == 0)
                        zakupCenaAbak = zakupNetto;
                    else
                    {
                        var ilosc = dokument.Korekta ? obroty.Sum(o => o.Ilosc.Value) : pdh.Ilosc;
                        zakupCenaAbak = cenaAbak.Value * (decimal)ilosc;
                    }
                    wartoscCenaAbak += zakupCenaAbak;
                    if (dokument.Korekta)
                    {
                        var sprzedazNetto = obroty.Sum(o => o.Rozchod.Wartosc);
                        if (sprzedazNetto != 0)
                        {
                            var prowizja = (sprzedazNetto - zakupCenaAbak) * prowizjaProcent / 100M;
                            wartoscProwizji += prowizja;
                            var wsp = WspolczynnikKary;
                            if (flag)
                            {
                                var s = pdh.Towar.Features[CechaKary] as string;
                                int i = 0;
                                if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s, out i))
                                    wsp = i / 100M;
                            }
                            wartoscKary += prowizja * wsp;
                        }
                    }
                }
            }
        }
        */

        /*
        private void przeliczRozliczenia()
        {
            rozliczono = 0;
            foreach (var p in dokument.Platnosci.Cast<Enova.API.Kasa.Platnosc>().ToList())
            {
                var rozliczenia = p.Rozliczenia.Cast<Enova.API.Kasa.RozliczenieSP>().ToList();
                if (rozliczenia.Count() > 0)
                    rozliczono += rozliczenia.Where(ro => ro.Data <= RozliczeniaDataDo.Value).Sum(ro => ro.KwotaZaplaty.Value) * (dokument.Korekta ? -1 : 1);
            }

        }

        public void Przelicz()
        {
            przeliczZakup();
            if (RozliczeniaDataDo != null)
                przeliczRozliczenia();
        }
         */

        public void PrzeliczRozliczenia(Enova.Business.Old.DB.EnovaContext dc, DateTime rozliczeniaDataDo)
        {
            rozliczono = 0;
            this.RozliczeniaDataDo = rozliczeniaDataDo;
            var sql = string.Format(
                "SELECT SUM(rsp.KwotaZaplatyValue) KwotaZaplaty FROM dbo.Platnosci pl " +
                "INNER JOIN dbo.RozliczeniaSP rsp ON (rsp.Dokument=pl.ID AND rsp.DokumentType='Platnosci') " +
                "WHERE pl.DokumentType = 'DokHandlowe' AND pl.Dokument={0} AND Data <= '{1}'", this.dokumentInfo.ID, rozliczeniaDataDo.ToShortDateString());
            var res = dc.ExecuteStoreQuery<decimal?>(sql).FirstOrDefault();
            if (res != null)
                rozliczono = res.Value;
        }



        #endregion


    }
}
