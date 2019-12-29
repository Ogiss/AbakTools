using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using Enova.Old.Types;
using Enova.Business.Old.DB;
using Enova.Business.Old.DB.Web;
using Enova.Old.Handel;
using Enova.Old.Magazyny;
using Enova.Old.Towary;
//using Enova.API;
//using Enova.API.CRM;
//using Enova.API.Handel;

namespace Enova.Business.Old.Zwroty
{
    public class DokumentAnalizyZwrotu : IEquatable<DokumentAnalizyZwrotu>, IComparable<DokumentAnalizyZwrotu>, /*Enova.API.Handel.IZwrot,*/ INotifyPropertyChanged
    {
        #region Fields

        private AnalizaZwrotu analiza;
        private DokumentHandlowy dokumentHandlowy;
        private PozycjeAnalizyZwrotu pozycje;
        private PozycjeAnalizyZwrotu pozycjeKorygowane;
        private Dictionary<Guid, double> ilosciPoKorektach;
        private Dictionary<Guid, double> ilosciKorygowane;
        private bool? wystawicKorekte;
        private bool? zatwierdzicKorekte;
        private DateTime? dataKorekty;
        private string numerKorekty;

        #endregion

        #region Properties

        public DokumentHandlowy DokumentHandlowy
        {
            get { return this.dokumentHandlowy; }
        }
        public PozycjeAnalizyZwrotu Pozycje
        {
            get { return this.pozycje; }
        }

        public PozycjeAnalizyZwrotu PozycjeKorygowane
        {
            get { return this.pozycjeKorygowane; }
        }

        public Dictionary<Guid, double> IlosciKorygowane
        {
            get { return this.ilosciKorygowane; }
        }

        public DateTime Data
        {
            get { return this.dokumentHandlowy.Data; }
        }
        public string NumerPelny
        {
            get { return this.dokumentHandlowy.NumerPelny; }
        }
        public int IloscPozycji
        {
            get { return this.pozycje.Count; }
        }

        public bool WystawicKorekte
        {
            get
            {
                if (this.wystawicKorekte == null)
                    return this.pozycjeKorygowane.Count > 0;
                return this.wystawicKorekte.Value;
            }
            set
            {
                this.wystawicKorekte = value;
            }
        }

        public bool ZatwierdzicKorekte
        {
            get
            {
                if (this.zatwierdzicKorekte == null)
                    return true;
                return this.zatwierdzicKorekte.Value;
            }
            set
            {
                this.zatwierdzicKorekte = value;
            }
        }

        public DateTime DataKorekty
        {
            get
            {
                if (this.dataKorekty == null)
                    return DateTime.Now.Date;
                return this.dataKorekty.Value;
            }
            set
            {
                this.dataKorekty = value;
                //this.przeliczNumerKorekty();
            }
        }

        public string NumerKorekty
        {
            get
            {
             //   if (string.IsNullOrEmpty(numerKorekty))
               //     this.przeliczNumerKorekty();
                return this.numerKorekty;
            }
            set
            {
                this.numerKorekty = value;
            }
        }
        public Guid KorektaGuid { get; set; }

        #endregion

        #region Methods

        public DokumentAnalizyZwrotu(AnalizaZwrotu analiza, DokumentHandlowy dokumentHandlowy)
        {
            this.analiza = analiza;
            this.dokumentHandlowy = dokumentHandlowy;
            this.pozycje = new PozycjeAnalizyZwrotu();
            this.pozycjeKorygowane = new PozycjeAnalizyZwrotu();
            this.ilosciPoKorektach = new Dictionary<Guid, double>();
            this.ilosciKorygowane = new Dictionary<Guid, double>();
        }

        /*
        private void przeliczNumerKorekty()
        {
            using (var session = Enova.API.EnovaService.Instance.CreateSession())
            {
                this.numerKorekty = session.GetModule<API.Handel.IHandelModule>().PrzeliczSymbolDokumentu(
                    Enova.API.Handel.KategoriaHandlowa.KorektaSprzedaży,
                    new { Data = this.DataKorekty, DataOperacji = this.DataKorekty });

            }
            this.NotifyPropertyChanged("NumerKorekty");
        }
        */

        public double GetIloscPoKorektach(PozycjaAnalizyZwrotu pozycja)
        {
            if (this.ilosciPoKorektach.ContainsKey(pozycja.Guid))
                return ilosciPoKorektach[pozycja.Guid];
            var pdhs = this.dokumentHandlowy.Pozycje.WgTowar[pozycja.Towar].ToList();
            double ilosc = 0;
            foreach(var pdh in pdhs)
            {
                ilosc += pdh.IloscPoKorektach;
            }
            this.ilosciPoKorektach.Add(pozycja.Guid, ilosc);
            return ilosc;
        }

        public bool SetKorygowany(PozycjaAnalizyZwrotu pozycja, bool enable, bool findBetter = true)
        {
            if (enable)
            {
                if (pozycja.PozostaloDoSkorygowania > 0)
                {
                    if (this.pozycje.Contains(pozycja) && !this.pozycjeKorygowane.Contains(pozycja))
                    {
                        double iloscPoKorektach = this.GetIloscPoKorektach(pozycja);
                        if (iloscPoKorektach > 0)
                        {
                           
                            if (iloscPoKorektach >= pozycja.PozostaloDoSkorygowania)
                            {
                                this.ilosciKorygowane[pozycja.Guid] = pozycja.PozostaloDoSkorygowania;
                                pozycja.iloscSkorygowana += pozycja.PozostaloDoSkorygowania;
                                this.pozycjeKorygowane.Add(pozycja);
                                pozycja.DokumentyKorygowane.Add(this);
                            }
                            else
                            {
                                if (findBetter)
                                {
                                    foreach (var doc in pozycja.DokumentyWgIlosciPozycji)
                                    {
                                        if (doc.Equals(this))
                                            continue;
                                        if (doc.GetIloscPoKorektach(pozycja) >= pozycja.PozostaloDoSkorygowania)
                                            return false;
                                    }
                                }
                                this.ilosciKorygowane[pozycja.Guid] = iloscPoKorektach;
                                pozycja.iloscSkorygowana += iloscPoKorektach;
                                this.pozycjeKorygowane.Add(pozycja);
                                pozycja.DokumentyKorygowane.Add(this);
                            }
                            return true;
                        }

                    }
                }
            }
            else
            {
                if (pozycja.DokumentyKorygowane.Contains(this))
                {
                    pozycja.DokumentyKorygowane.Remove(this);
                    this.pozycjeKorygowane.Remove(pozycja);
                    pozycja.iloscSkorygowana -= this.ilosciKorygowane[pozycja.Guid];
                    this.ilosciKorygowane[pozycja.Guid] = 0;
                    return true;
                }
            }
            
            return false;
        }



        public Enova.API.Handel.DokumentHandlowy WystawKorekte(DateTime data, bool zatwierdzic,
            Enova.API.IProgressForm progressForm = null, Enova.API.IInfo infoControl = null)
        {
            //return session.GetModule<Enova.API.Handel.IHandelModule>().KorektaDoZwrotu(data, this, zatwierdzic, this.NumerKorekty, progressForm, infoControl);

            API.Handel.DokumentHandlowy korekta = null;
            using (var session = API.EnovaService.Instance.CreateSession())
            {

                bool flag1 = progressForm != null;
                bool flag2 = infoControl != null;
                int step = (int)(100 / (6 + this.Pozycje.Count()));
                var hm = session.GetModule<API.Handel.HandelModule>();
                //var dh = hm.DokHandlowe[((IZwrot)this).DokumentKorygowanyGuid];
                var dh = hm.DokHandlowe[this.DokumentHandlowy.Guid];
                if (dh != null)
                {
                    
                    using (var t = session.CreateTransaction())
                    {
                        if (flag1)
                            progressForm.Text2 = "Tworzę dokument korekty";
                        //var defRelacjiKorekta = (Soneta.Handel.DefRelacjiKorekta)EnovaModule.DefRelHandlowych.Korekta;
                        var defRelacjiKorekta = (API.Handel.DefRelacjiKorekta)hm.DefRelHandlowych.Korekta;
                        bool blokadaSprzedazy = dh.Kontrahent.BlokadaSprzedazy;
                        dh.Kontrahent.BlokadaSprzedazy = false;
                        korekta = defRelacjiKorekta.KorygujDokument(dh);
                        korekta.Data = data;
                        korekta.DataOperacji = data;
                        korekta.Numer.PrzeliczSymbol();
                        //korekta.Opis = string.Format("Korekta do zwrotu nr: {0}", ((IZwrot)this).ID);
                        korekta.Opis = string.Format("Korekta do zwrotu nr: {0}", this.analiza.Zwrot.ID);

                        if (!string.IsNullOrEmpty(this.NumerKorekty) && this.NumerKorekty.IndexOf('*') == -1)
                            korekta.Numer.NumerPelny = this.NumerKorekty;

                        if (flag1)
                            progressForm.PerformStep(step, true);

                        if (flag1)
                            progressForm.Text2 = "Koryguję pozycje";

                        //foreach (var pozycja in ((IZwrot)this).Pozycje)
                        foreach (var pozycja in this.GetPozycjeKorygowaneDokumentu())
                        {
                            var pdh = (Enova.API.Handel.PozycjaDokHandlowego)korekta.PozycjaWgIdent(pozycja.Lp);
                            pdh.Ilosc = pdh.Ilosc + pozycja.Ilosc;

                            var kpdh = pdh.PozycjaKorygowana;
                            if (pdh.Cena != kpdh.Cena)
                                pdh.Cena = kpdh.Cena;

                            if (pdh.Rabat != kpdh.Rabat)
                                pdh.Rabat = kpdh.Rabat;

                            if (flag1)
                                progressForm.PerformStep(step, true);
                        }

                        if (flag1)
                            progressForm.Text2 = "Przeliczam płatności";

                        bool przeliczony = session.GetModule<API.Kasa.KasaModule>().PrzeliczDokumnet(korekta);

                        if (flag1)
                            progressForm.PerformStep(step, true);

                        /*
                        if (zatwierdz)
                            korekta.Stan = Soneta.Handel.StanDokumentuHandlowego.Zatwierdzony;
                         */

                        if (flag1)
                            progressForm.PerformStep(step, true);


                        dh.Kontrahent.BlokadaSprzedazy = blokadaSprzedazy;

                        session.EventsInvoke();

                        if (flag1)
                        {
                            progressForm.Text2 = "Zapis danych do bazy";
                            progressForm.PerformStep(step, true);
                        }

                        t.Commit();

                    }
                    session.Save();
                }
            }

            if (zatwierdzic)
            {
                using (var session = Enova.API.EnovaService.Instance.CreateSession())
                {
                    using (var t = session.CreateTransaction())
                    {
                        korekta = (API.Handel.DokumentHandlowy)session.GetModule<API.Handel.HandelModule>().DokHandlowe[korekta.ID];
                        var zatwierdzanieWorker = API.EnovaService.Instance.CreateObject<API.Handel.ZatwierdzanieWorker>();
                        zatwierdzanieWorker.Dokument = korekta;
                        zatwierdzanieWorker.Zatwierdź();
                        t.Commit();
                    }
                    session.Save();
                }

            }

            return korekta;
        }

        public int CompareTo(DokumentAnalizyZwrotu dok)
        {
            var comp = dok.Data.CompareTo(this.Data);
            if (comp == 0)
                return this.NumerPelny.CompareTo(dok.NumerPelny);
            return comp;
        }

        public bool Equals(DokumentAnalizyZwrotu dok)
        {
            return this.dokumentHandlowy.Guid.Equals(dok.dokumentHandlowy.Guid);
        }

        public override bool Equals(object obj)
        {
            return this.Equals((DokumentAnalizyZwrotu)obj);
        }

        public override int GetHashCode()
        {
            return this.dokumentHandlowy.Guid.GetHashCode();
        }

        public override string ToString()
        {
            return this.dokumentHandlowy.NumerPelny + " : " + this.pozycje.Count;
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }

        #endregion

        public IEnumerable<PozycjaDokHanProxy> GetPozycjeKorygowaneDokumentu()
        {
            List<PozycjaDokHanProxy> pozycje = new List<PozycjaDokHanProxy>();
            foreach (var poz in this.pozycjeKorygowane)
            {
                var pdhs = this.dokumentHandlowy.Pozycje.WgTowar[poz.Towar];
                var ilosc = this.ilosciKorygowane[poz.Guid];
                foreach (var pdh in pdhs)
                {
                    var iloscPoKorektach = pdh.IloscPoKorektach;
                    if (iloscPoKorektach > 0)
                    {
                        var pozycja = new PozycjaDokHanProxy()
                        {
                            Lp = pdh.Lp,
                            Ident = pdh.Ident,
                            Towar = pdh.Towar,
                            Cena = Convert.ToDecimal(pdh.Cena.Value),
                            Rabat = pdh.Rabat,
                            Ilosc = 0D
                        };

                        if (iloscPoKorektach >= ilosc)
                        {
                            pozycja.Ilosc = -ilosc;
                            ilosc = 0;
                        }
                        else
                        {
                            pozycja.Ilosc = -iloscPoKorektach;
                            ilosc -= iloscPoKorektach;
                        }
                        pozycje.Add(pozycja);
                        if (ilosc == 0)
                            break;
                    }
                }
            }
            return pozycje;
        }


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IZwrotDOD Implementation

        /*
        public Guid DokumentKorygowanyGuid
        {
            get { return this.DokumentHandlowy.Guid; }
        }
         */

        /*
        public IKontrahent Kontrahent
        {
            //get { return new KontrahentDOD { Record = this.DokumentHandlowy.Kontrahent }; }
            get { return this.DokumentHandlowy.Kontrahent; }
        }
         */

        /*
        public IEnumerable<Enova.API.Handel.PozycjaDokHanDOD> GetPozycjeKorygowaneDokumentu()
        {
            List<Enova.API.Handel.PozycjaDokHanDOD> pozycje = new List<PozycjaDokHanDOD>();
            foreach (var poz in this.pozycjeKorygowane)
            {
                var pdhs = this.dokumentHandlowy.Pozycje.WgTowar[poz.Towar];
                var ilosc = this.ilosciKorygowane[poz.Guid];
                foreach (var pdh in pdhs)
                {
                    var iloscPoKorektach = pdh.IloscPoKorektach;
                    if (iloscPoKorektach > 0)
                    {
                        var pozycja = new Enova.API.Handel.PozycjaDokHanDOD()
                        {
                            Lp = pdh.Lp,
                            Ident = pdh.Ident,
                            Towar = new Enova.API.Towary.TowarDOD { Record = pdh.Towar },
                            Cena = Convert.ToDecimal(pdh.Cena.Value),
                            Rabat = pdh.Rabat
                        };

                        if (iloscPoKorektach >= ilosc)
                        {
                            pozycja.Ilosc = -ilosc;
                            ilosc = 0;
                        }
                        else
                        {
                            pozycja.Ilosc = -iloscPoKorektach;
                            ilosc -= iloscPoKorektach;
                        }
                        pozycje.Add(pozycja);
                        if (ilosc == 0)
                            break;
                    }
                }
            }
            return pozycje;
        }
         */

        #endregion

        #region Nested Types

        public class IloscPozycjiComparer : IComparer<DokumentAnalizyZwrotu>
        {
            public int Compare(DokumentAnalizyZwrotu d1, DokumentAnalizyZwrotu d2)
            {
                var c = d2.IloscPozycji.CompareTo(d1.IloscPozycji);
                if (c == 0)
                    return d2.Data.CompareTo(d1.Data);
                return c;
            }
        }

        public class PozycjaDokHanProxy
        {
            public int Lp { get; set; }
            public int Ident { get; set; }
            public Towar Towar { get; set; }
            public decimal Cena { get; set; }
            public decimal? Rabat { get; set; }
            public double Ilosc { get; set; }

        }

        #endregion

        #region Do Usunięcia

        /*

        IEnumerable<IPozycjaDokHandlowego> IDokumentHandlowy.Pozycje
        {
            get
            {
                //return GetPozycjeKorygowaneDokumentu();
                throw new NotImplementedException();
            }
        }

        public Guid Guid
        {
            get { return Guid.Empty; }
        }

        public int ID
        {
            get { return this.analiza.Zwrot.ID; }
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

        public IDefDokHandlowego Definicja
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

        IKontrahent IDokumentHandlowy.Kontrahent
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

        public IPozycjaDokHandlowego NowaPozycja(API.Towary.ITowar towar, double ilosc)
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


        public System.Collections.IEnumerable Platnosci
        {
            get { throw new NotImplementedException(); }
        }


        public IKontrahent Odbiorca
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


        public API.Types.Date DataOperacji
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

        public API.Core.NumerDokumentu Numer
        {
            get { throw new NotImplementedException(); }
        }

        public IPozycjaDokHandlowego PozycjaWgIdent(int ident)
        {
            throw new NotImplementedException();
        }
         */

        #endregion
    }
}
