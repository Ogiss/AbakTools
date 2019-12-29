using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Enova.Business.Old.DB.Web
{
    [Core.DataEditForm("AbakTools.CRM.Forms.KontrahentEditForm, AbakTools.CRM.Forms")]
    public partial class Kontrahent : Core.ISaveChanges, Core.IUndoChanges, Core.IDeleteRecord, Enova.API.CRM.Kontrahent, IComparable, Enova.Business.Old.IDbContext
    {
        public ObjectContext DbContext { get; set; }

        public Kontrahent() : this(null) { }

        public Kontrahent(Enova.Business.Old.DB.Kontrahent enovaKontrahent)
        {
            this.PSID = 0;
            Password = "";
            Imie = "firstname";
            Nazwisko = "lastname";
            GrupaDomyslna = 1;
            Aktywny = true;
            Usuniety = false;
            CzyAgent = false;
            Stamp = DateTime.Now;
            Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew;
            DoSynchronizacji = false;
            SecureKey = Enova.Business.Old.Core.Tools.GenSecureKey();

            if (enovaKontrahent != null)
            {
                Guid = enovaKontrahent.Guid;
                Kod = enovaKontrahent.Kod;
                Nazwa = enovaKontrahent.Nazwa;
                Nip = enovaKontrahent.NIP;
                Email = enovaKontrahent.KontaktEMAIL;
                Telefon = enovaKontrahent.Adres.AdresTelefon;
                Rabat = enovaKontrahent.Rabat;

                string przedtawicielKod = enovaKontrahent.Przedstawiciel;
                Kontrahent przedstawiciel = Enova.Business.Old.Core.ContextManager.WebContext.Kontrahenci.Where(k => k.Kod == przedtawicielKod).FirstOrDefault();
                Przedstawiciel = przedstawiciel;
            }
            
        }

        public Kontrahent(Enova.API.CRM.Kontrahent enovaKontrahent)
        {
            this.PSID = 0;
            Password = "";
            Imie = "firstname";
            Nazwisko = "lastname";
            GrupaDomyslna = 1;
            Aktywny = true;
            Usuniety = false;
            CzyAgent = false;
            Stamp = DateTime.Now;
            Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew;
            DoSynchronizacji = false;
            SecureKey = Enova.Business.Old.Core.Tools.GenSecureKey();

            if (enovaKontrahent != null)
            {
                Guid = enovaKontrahent.Guid;
                Kod = enovaKontrahent.Kod;
                Nazwa = enovaKontrahent.Nazwa;
                Nip = enovaKontrahent.NIP;
                Email = enovaKontrahent.KontaktEMAIL;
                Telefon = enovaKontrahent.Adres.Telefon;
                Rabat = enovaKontrahent.Rabat;

                string przedtawicielKod = enovaKontrahent.Features["przedstawiciel"].ToString();
                Kontrahent przedstawiciel = Enova.Business.Old.Core.ContextManager.WebContext.Kontrahenci.Where(k => k.Kod == przedtawicielKod).FirstOrDefault();
                Przedstawiciel = przedstawiciel;
            }

        }


        public string PrzedstawicielKod
        {
            get
            {
                if (this.Przedstawiciel != null)
                    return this.Przedstawiciel.Kod;
                return null;
            }
        }

        public Adres DomyslnyAdresFaktury
        {
            get
            {
                if(EntityState!= EntityState.Added && EntityState!= EntityState.Detached && !Adresy.IsLoaded)
                    Adresy.Load();

                Adres adres = Adresy.Where(a => a.DomyslnyAdresFaktury == true && (a.Usuniety == null || a.Usuniety == false)).FirstOrDefault();
                if (adres == null)
                    adres = Adresy.Where(a => a.Domyslny == true && (a.Usuniety == null || a.Usuniety == false)).FirstOrDefault();
                if (adres == null)
                    adres = Adresy.Where(a => (a.Usuniety == null || a.Usuniety == false)).FirstOrDefault();
                return adres;
            }
        }

        public Adres DomyslnyAdresWysylki
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !Adresy.IsLoaded)
                    Adresy.Load();

                Adres adres = Adresy.Where(a => a.DomyslnyAdresWysylki == true && 
                    (a.DomyslnyAdresFaktury == null || a.DomyslnyAdresFaktury == false) && (a.Usuniety == null || a.Usuniety == false)).FirstOrDefault();
                if (adres == null)
                    adres = Adresy.Where(a => a.Domyslny == true && (a.Usuniety == null || a.Usuniety == false)).FirstOrDefault();
                if (adres == null)
                    adres = Adresy.Where(a => (a.Usuniety == null || a.Usuniety == false)).FirstOrDefault();
                return adres;
            }
        }

        public Enova.Business.Old.DB.Kontrahent EnovaKontrahent
        {
            get
            {
                if (this.Guid != null)
                {
                    return Enova.Business.Old.Core.ContextManager.DataContext.Kontrahenci
                        .Where(k => k.Guid == this.Guid).FirstOrDefault();
                }
                return null;
            }
        }

        public string HasloWeb
        {
            get { return null; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    this.Password = Core.Tools.GenPassword(value);
            }
        }

        public decimal GetRabat(Produkt towar)
        {
            try
            {
                var service = Enova.API.EnovaService.Instance;
                if (service != null && service.IsLogined)
                {
                    using (var session = service.CreateSession())
                        return session.GetModule<Enova.API.Towary.TowaryModule>().WyliczRabat(this.Guid.Value, towar.EnovaGuid.Value);

                }
            }
            catch { }

            return 0;
        }

        public static Web.Kontrahent GetWebKontrahent(API.CRM.Kontrahent kontrahent)
        {
            return GetWebKontrahent(kontrahent, true);
        }

        public static Enova.Business.Old.DB.Web.Kontrahent GetWebKontrahent(API.CRM.Kontrahent enova, bool insert)
        {
            Web.Kontrahent kontrahent = Enova.Business.Old.Core.ContextManager.WebContext.Kontrahenci.Where(k => k.Guid == enova.Guid).FirstOrDefault();
            if (kontrahent == null)
            {
                kontrahent = Core.ContextManager.WebContext.Kontrahenci.Where(k => k.Kod == enova.Kod && k.Nip == enova.NIP).FirstOrDefault();
                if (kontrahent == null && insert)
                {
                    try
                    {
                        kontrahent = new Web.Kontrahent(enova);
                        Core.ContextManager.WebContext.AddToKontrahenci(kontrahent);
                        kontrahent.Synchronizacja = (int)Types.RowSynchronizeOld.Notsaved;
                        kontrahent.DoSaveChanges(Core.ContextManager.WebContext);

                        //ContextManager.WebContext.SaveChanges();

                        Enova.Business.Old.DB.Web.Adres adres = null;
                        Enova.Business.Old.DB.Web.Adres adresKor = null;

                        if (enova.Adres != null)
                        {
                            adres = new Web.Adres(enova, enova.Adres, 1);
                            kontrahent.Adresy.Add(adres);
                            adres.DoSaveChanges(Core.ContextManager.WebContext);
                        }

                        if (enova.AdresDoKorespondencji != null && !string.IsNullOrEmpty(enova.AdresDoKorespondencji.KodPocztowy))
                        {
                            adresKor = new Web.Adres(enova, enova.AdresDoKorespondencji, 2);
                            kontrahent.Adresy.Add(adresKor);
                            adresKor.DoSaveChanges(Core.ContextManager.WebContext);
                        }

                        if (adresKor == null)
                            adres.DomyslnyAdresWysylki = true;


                        /*
                        foreach (var cenaGrupowa in enova.CenyGrupowe)
                        {
                            Web.GrupaRabatowa grupaRabatowa = ContextManager.WebContext.GrupyRabatowe.Where(gr => gr.GUID == cenaGrupowa.GrupaTowarowa.Guid).FirstOrDefault();
                            if (grupaRabatowa == null)
                                grupaRabatowa = new Web.GrupaRabatowa()
                                {
                                    EnovaStamp = cenaGrupowa.GrupaTowarowa.Stamp,
                                    GUID = cenaGrupowa.GrupaTowarowa.Guid,
                                    Kategoria = cenaGrupowa.GrupaTowarowa.Category,
                                    Wartosc = cenaGrupowa.GrupaTowarowa.Value
                                };

                            kontrahent.GrupyRabatowe.Add(new Web.KontrahentRabatGrupowy()
                            {
                                EnovaStamp = cenaGrupowa.Stamp,
                                GrupaRabatowa = grupaRabatowa,
                                GUID = cenaGrupowa.Guid,
                                Rabat = (decimal)cenaGrupowa.Rabat,
                                RabatZdefiniowany = (bool)cenaGrupowa.RabatZdefiniowany
                            });
                        }
                         */

                        kontrahent.Synchronizacja = (int)Types.RowSynchronizeOld.NotsynchronizedNew;
                        kontrahent.DoSaveChanges(Core.ContextManager.WebContext);
                    }
                    catch (Exception ex)
                    {
                        BAL.Business.AppController.ThrowException(ex);
                    }
                }
                else if (kontrahent != null)
                {
                    kontrahent.Guid = enova.Guid;
                }
                //ContextManager.WebContext.SaveChanges();
            }

            return kontrahent;
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
            dc.Refresh(RefreshMode.StoreWins, this);
        }

        public bool SaveChanges()
        {
            WebContext dc = Core.ContextManager.WebContext;
            Adres adresKor = DomyslnyAdresWysylki;
            if (adresKor != null && adresKor.EntityState == EntityState.Added && string.IsNullOrEmpty(adresKor.KodPocztowy))
                dc.DeleteObject(adresKor);
            dc.SaveChanges();


            return true;
        }

        public bool UndoChanges()
        {
            WebContext dc = Core.ContextManager.WebContext;
            foreach (Adres adres in this.Adresy.ToList())
            {
                if (adres.EntityState == EntityState.Added)
                    dc.DeleteObject(adres);
                else if (adres.EntityState == EntityState.Modified)
                    dc.Refresh(RefreshMode.StoreWins, adres);
            }

            if (this.EntityState == EntityState.Added)
                dc.DeleteObject(this);
            else if (this.EntityState == EntityState.Modified)
                dc.Refresh(RefreshMode.StoreWins, this);

            return true;
        }

        public bool DeleteRecord()
        {
            WebContext dc = Core.ContextManager.WebContext;
            foreach (var adres in this.Adresy.ToList())
            {
                dc.DeleteObject(adres);
            }

            dc.DeleteObject(this);
            dc.SaveChanges();
            return true;
        }

        public override string ToString()
        {
            return this.Kod + " - " + this.Nazwa.Replace("\r\n"," ");
        }

        public int CompareTo(object obj)
        {
            return this.ToString().CompareTo(obj.ToString());
        }

        public Enova.Business.Old.DB.Kontrahent GetKontrahentEnova(EnovaContext dc)
        {
            return dc.Kontrahenci.Where(k => k.Guid == this.Guid).FirstOrDefault();
        }

        Guid Enova.API.Business.GuidedRow.Guid
        {
            get { return this.Guid == null ? System.Guid.Empty : this.Guid.Value; }
        }

        public static Kontrahent GetKontrahent(WebContext dbContext, Enova.API.CRM.Kontrahent enova)
        {
            if (enova != null)
                return dbContext.Kontrahenci.Where(r => r.Guid == enova.Guid).FirstOrDefault();
            return null;
        }

        #region IKontrahentDOD

        public string KontaktEMAIL
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool Blokada
        {
            get { throw new NotImplementedException(); }
        }

        public bool BlokadaSprzedaży
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        decimal Enova.API.CRM.Kontrahent.Rabat
        {
            get { throw new NotImplementedException(); }
        }

        string Enova.API.CRM.Kontrahent.NIP
        {
            get { return this.Nip; }
        }

        public Enova.API.Core.Adres Adres
        {
            get { throw new NotImplementedException(); }
        }

        public Enova.API.Core.Adres AdresDoKorespondencji
        {
            get { throw new NotImplementedException(); }
        }

        public Enova.API.Kasa.FormaPlatnosci SposobZaplaty
        {
            get { throw new NotImplementedException(); }
        }


        

        public string Komunikat
        {
            get { throw new NotImplementedException(); }
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


        public API.Business.Session Session
        {
            get { throw new NotImplementedException(); }
        }


        public object CallMethod(string name, Type[] argsTypes, object[] args)
        {
            throw new NotImplementedException();
        }



        bool API.CRM.Kontrahent.BlokadaSprzedazy
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


        public string KontaktWWW
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

        public string KontaktTelefonKomorkowy
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


        public IEnumerable<API.Towary.CenaGrupowa> CenyGrupowe
        {
            get { throw new NotImplementedException(); }
        }


        public int KontrolaDni
        {
            get { throw new NotImplementedException(); }
        }

        public bool LimitNieograniczony
        {
            get { throw new NotImplementedException(); }
        }

        string API.Kasa.IPodmiotKasowy.NIP
        {
            get { throw new NotImplementedException(); }
        }

        public API.Kasa.IPodmiotKasowy Platnik
        {
            get { throw new NotImplementedException(); }
        }

        public bool PrzeterminowanieNieograniczone
        {
            get { throw new NotImplementedException(); }
        }

        public int Termin
        {
            get { throw new NotImplementedException(); }
        }

        public int TerminPlanowany
        {
            get { throw new NotImplementedException(); }
        }


        public string NazwaFormatowana
        {
            get { throw new NotImplementedException(); }
        }

        public string NazwaPierwszaLinia
        {
            get { throw new NotImplementedException(); }
        }

        decimal API.Core.IPodmiot.Rabat
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.RodzajPodmiotu RodzajPodmiotu
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.StatusPodmiotu StatusPodmiotu
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.TypPodmiotu Typ
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

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


        API.Business.FeatureCollection API.Business.Row.Features
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


        public object GetObjValue(object obj, string name, Type[] types = null, object[] index = null, Type fromType = null)
        {
            throw new NotImplementedException();
        }

        public API.Business.SubTable Rozrachunki
        {
            get { throw new NotImplementedException(); }
        }


        public bool Is<T>()
        {
            throw new NotImplementedException();
        }

        public T As<T>()
        {
            throw new NotImplementedException();
        }


        public string EMAIL
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

        public string EuVAT
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.SubTable Lokalizacje
        {
            get { throw new NotImplementedException(); }
        }

        /*
        string API.Core.IKontrahent.NIP
        {
            get { throw new NotImplementedException(); }
        }
         */

        public API.Business.View OsobyZOsobyKontrahent
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.SubTable Projekty
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.SubTable Urzadzenia
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.SubTable Zadania
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly()
        {
            throw new NotImplementedException();
        }

        API.Business.FeatureCollection API.Business.IRow.Features
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsLive
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.IRow Parent
        {
            get { throw new NotImplementedException(); }
        }

        public string Prefix
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.Row Root
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.Table Table
        {
            get { throw new NotImplementedException(); }
        }

        public int TableHandle
        {
            get { throw new NotImplementedException(); }
        }

        string API.Core.INipHost.NIP
        {
            get { throw new NotImplementedException(); }
        }


        public API.Types.Currency LimitKredytu
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


        public T FromEnova<T>(string name, Type fromType = null)
        {
            throw new NotImplementedException();
        }


        string API.Core.IKontrahent.NIP
        {
            get { throw new NotImplementedException(); }
        }


        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
