using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Text;
using Enova.Business.Old.Core;
using Enova.Old.CRM;
using System.Windows.Forms;
using System.Transactions;
using Enova.Business.Old;

namespace Enova.Business.Old.DB
{
    [DataEditForm("Enova.Forms.CRM.KontrahentEditForm, Enova.Forms"), DefaultOrder("Kod")]
    public partial class Kontrahent : IGuidedRow, IContextSaveChanges, IPodmiot, IRozrachunkiQuery, IValidation, IDeleteRecord, IDbContext, Enova.API.CRM.Kontrahent
    {

        #region Properties

        ObjectContext IDbContext.DbContext { get; set; }
        public EnovaContext DataContext
        {
            get { return (EnovaContext)((IDbContext)this).DbContext; }
        }


        IRow IRow.Parent
        {
            get { return null; }
        }
        IRow IRow.Root
        {
            get { return this; }
        }
        string IRow.Prefix
        {
            get { return ""; }
        }
        ITable IRow.Table
        {
            get { return CRMModule.GetInstance(this.DataContext).Kontrahenci; }
        }
        RowState IRow.State
        {
            get { return this.GetRowState(); }
        }
        public bool IsLive
        {
            get { return this.GetIsLive(); }
        }
        public bool IsReadOnly()
        {
            return false;
        }

        #endregion

        #region Do usunięcia lub przerobienia

        public Kontrahent() { }

        public override string ToString()
        {
            return this.Kod;
        }

        public string Nazwa
        {
            get
            { 
                return NazwaStr.Replace('\n', ' ');
            }
            set
            {
                NazwaStr = value.Replace("\r\n", "\n");
            }
        }

        public string NazwaMultiline
        {
            get
            { 
                return NazwaStr.Replace("\n", "\r\n");
            }
            set
            {
                NazwaStr = value.Replace("\r\n", "\n");
            }
        }

        private Adres adres = null;
        public Adres Adres
        {
            get
            {
                if (adres == null && EntityState != EntityState.Added && EntityState != EntityState.Detached)
                {
                    adres = Enova.Business.Old.Core.ContextManager.DataContext.Adresy
                        .Where(a => a.HostType == "Kontrahenci" && a.Host == ID && a.Typ == 1).FirstOrDefault();
                }
                if (adres == null)
                {
                    adres = new Adres()
                    {
                        HostType = "Kontrahenci",
                        Host = ID,
                        Typ = 1
                    };
                }
                return adres;
            }
        }

        internal string adresTelefon = null;
        public bool AdresTelefonIsLoaded = false;
        public string AdresTelefon
        {
            get
            {
                return adresTelefon;
            }
        }

        private Adres adresKor = null;
        public Adres AdresKorespondencyjny
        {
            get
            {
                if (adresKor == null && EntityState != EntityState.Added && EntityState != EntityState.Detached)
                {
                    adresKor = Enova.Business.Old.Core.ContextManager.DataContext.Adresy
                        .Where(a => a.HostType == "Kontrahenci" && a.Host == ID && a.Typ == 2).FirstOrDefault();
                }
                if (adresKor == null)
                {
                    adresKor = new Adres()
                    {
                        HostType = "Kontrahenci",
                        Host = ID,
                        Typ = 2
                    };
                }
                return adresKor;
            }
        }

        public ObjectQuery<RozrachunekIdx> RozrachunkiQuery
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached)
                    return (ObjectQuery<RozrachunekIdx>)Enova.Business.Old.Core.ContextManager.DataContext.RozrachunkiIdx
                        .Where(r => r.PodmiotType == "Kontrahenci" && r.Podmiot == ID);
                return null;
            }
        }

        public ObjectQuery<RozrachunekIdx> RozrachunkiWpłaty
        {
            get
            {
                return (ObjectQuery<RozrachunekIdx>)this.RozrachunkiQuery.Where(r => r.Typ == 20);
            }
        }

        public ObjectQuery<RozrachunekIdx> RozrachunkiWypłaty
        {
            get
            {
                return (ObjectQuery<RozrachunekIdx>)this.RozrachunkiQuery.Where(r => r.Typ == 21);
            }
        }

        private Enova.Business.Old.Rozrachunki rozrachunki = null;
        public Enova.Business.Old.Rozrachunki Rozrachunki
        {
            get
            {
                if (rozrachunki == null)
                    rozrachunki = new Enova.Business.Old.Rozrachunki(RozrachunkiQuery);
                return rozrachunki;
            }
        }

        public ObjectQuery<RozliczenieSP> Rozliczenia
        {
            get
            {
                return (ObjectQuery<RozliczenieSP>)Enova.Business.Old.Core.ContextManager.DataContext.RozliczeniaSP
                    .Where(r => r.PodmiotType == "kontrahenci" && r.PodmiotID == ID);
            }
        }

        public ObjectQuery<Feature> Features
        {
            get
            {
                return (ObjectQuery<Feature>)Enova.Business.Old.Core.ContextManager.DataContext.Features
                    .Where(f => f.ParentType == "Kontrahenci" && f.Parent == ID);
            }
        }

        internal Feature przedstawiciel = null;
        public string Przedstawiciel
        {
            get
            {
                if (przedstawiciel == null)
                {
                    if(EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                        przedstawiciel = this.Features.Where(f => f.Name == "przedstawiciel").FirstOrDefault();
                }

                if (przedstawiciel != null)
                    return przedstawiciel.Data;
                return null;
            }
            set
            {
                this.przedstawiciel = new Feature()
                {
                    ParentType = "kontrahenci",
                    Name = "przedstawiciel",
                    Lp = 0,
                    DataKey = value,
                    Data = value
                };
            }
        }

        private List<Feature> trasy = null;
        public List<Feature> Trasy
        {
            get
            {
                if (trasy == null)
                {
                    if (EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                    {
                        trasy = this.Features.Where(f => f.Name == "TRASY").ToList();
                    }
                    else
                    {
                        trasy = new List<Feature>();
                    }
                }

                return trasy;
            }
        }

        public string RabatProcent
        {
            get
            {
                if (Rabat != null)
                {
                    return decimal.Round((Rabat.Value * 100M), 2).ToString() + "%";
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    decimal? rabat = decimal.Parse(value.Replace('%', ' ').Trim().Replace(',', '.')) / 100M;
                    if (rabat != Rabat)
                    {
                        Rabat = rabat;
                    }
                }
                else
                {
                    Rabat = 0;
                }

            }
        }

        public decimal GetRabat(EnovaContext ec, Guid towarGuid)
        {
            decimal rabat = this.Rabat != null ? this.Rabat.Value : 0;

            var features = (from f in ec.Features
                            join t in ec.Towary on
                                new { ParentType = f.ParentType, Parent = f.Parent } equals
                                new { ParentType = "Towary", Parent = t.ID }
                            where t.Guid == towarGuid
                            select f).ToList();

            foreach (var feature in features)
            {
                var fdef = ec.FeatureDefs.Where(fd => fd.TableName == "Towary" && fd.Name == feature.Name).FirstOrDefault();
                if (fdef != null && fdef.Group == true && fdef.StrictDictionary == true)
                {
                    var dict = ec.DictionarySet.Where(d => d.Category == "F." + fdef.Dictionary && d.Value == feature.Data).FirstOrDefault();
                    if (dict != null)
                    {
                        var cenaGrupowa = this.CenyGrupowe.Where(cg => cg.GrupaTowarowa.ID == dict.ID).FirstOrDefault();
                        if (cenaGrupowa != null && cenaGrupowa.RabatZdefiniowany == true)
                            rabat = cenaGrupowa.Rabat.Value;
                    }
                }
            }

            return rabat;
        }

        public decimal GetRabat(EnovaContext ec, string towarKod)
        {
            decimal rabat = this.Rabat != null ? this.Rabat.Value : 0;

            var features = (from f in ec.Features
                            join t in ec.Towary on
                                new { ParentType = f.ParentType, Parent = f.Parent } equals
                                new { ParentType = "Towary", Parent = t.ID }
                            where t.Kod == towarKod
                            select f).ToList();

            foreach (var feature in features)
            {
                var fdef = ec.FeatureDefs.Where(fd => fd.TableName == "Towary" && fd.Name == feature.Name).FirstOrDefault();
                if (fdef != null && fdef.Group == true && fdef.StrictDictionary == true)
                {
                    var dict = ec.DictionarySet.Where(d => d.Category == "F." + fdef.Dictionary && d.Value == feature.Data).FirstOrDefault();
                    if (dict != null)
                    {
                        var cenaGrupowa = this.CenyGrupowe.Where(cg => cg.GrupaTowarowa.ID == dict.ID).FirstOrDefault();
                        if (cenaGrupowa != null && cenaGrupowa.RabatZdefiniowany == true)
                            rabat = cenaGrupowa.Rabat.Value;
                    }
                }
            }

            return rabat;
        }


        public static decimal GetRabat(EnovaContext ec, Guid kontrahentGuid, string towarKod)
        {
            Kontrahent kontrahent = ec.Kontrahenci.Where(k => k.Guid == kontrahentGuid).FirstOrDefault();
            if (kontrahent != null)
                return kontrahent.GetRabat(ec, towarKod);
            return 0;
        }

        public static explicit operator Web.Kontrahent(Kontrahent kontrahent)
        {
            return kontrahent.GetWebKontrahent();
        }

        public Web.Kontrahent GetWebKontrahent()
        {
            return GetWebKontrahent(true);
        }

        public Enova.Business.Old.DB.Web.Kontrahent GetWebKontrahent(bool insert)
        {
            Web.Kontrahent kontrahent = ContextManager.WebContext.Kontrahenci.Where(k => k.Guid == this.Guid).FirstOrDefault();
            if (kontrahent == null)
            {
                kontrahent = ContextManager.WebContext.Kontrahenci.Where(k => k.Kod == this.Kod && k.Nip == this.NIP).FirstOrDefault();
                if (kontrahent == null && insert)
                {
                    try
                    {
                        kontrahent = new Web.Kontrahent(this);
                        ContextManager.WebContext.AddToKontrahenci(kontrahent);
                        kontrahent.Synchronizacja = (int)Types.RowSynchronizeOld.Notsaved;
                        kontrahent.DoSaveChanges(ContextManager.WebContext);

                        //ContextManager.WebContext.SaveChanges();

                        Enova.Business.Old.DB.Web.Adres adres = null;
                        Enova.Business.Old.DB.Web.Adres adresKor = null;

                        if (this.Adres != null)
                        {
                            adres = new Web.Adres(this.Adres);
                            kontrahent.Adresy.Add(adres);
                            adres.DoSaveChanges(ContextManager.WebContext);
                        }

                        if (this.AdresKorespondencyjny != null && this.AdresKorespondencyjny.AdresKodPocztowy != null && this.AdresKorespondencyjny.AdresKodPocztowy.Value > 0)
                        {
                            adresKor = new Web.Adres(this.AdresKorespondencyjny);
                            kontrahent.Adresy.Add(adresKor);
                            adresKor.DoSaveChanges(ContextManager.WebContext);
                        }

                        if (adresKor == null)
                            adres.DomyslnyAdresWysylki = true;



                        foreach (var cenaGrupowa in this.CenyGrupowe)
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

                        kontrahent.Synchronizacja = (int)Types.RowSynchronizeOld.NotsynchronizedNew;
                        kontrahent.DoSaveChanges(ContextManager.WebContext);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (kontrahent != null)
                {
                    kontrahent.Guid = this.Guid;
                }
                //ContextManager.WebContext.SaveChanges();
            }

            return kontrahent;
        }

        private List<KontaktOsoba> kontaktyOsoby = null;
        public bool KontaktyOsobyIsloaded = false;

        public void KontaktyOsobyLoad()
        {
            kontaktyOsoby = ContextManager.DataContext.KontaktyOsoby.Where(k => k.KontrahentType == "Kontrahenci" && k.KontrahentID == this.ID).ToList();
            KontaktyOsobyIsloaded = true;
        }

        public List<KontaktOsoba> KontaktyOsoby
        {
            get
            {
                if (kontaktyOsoby == null && EntityState != EntityState.Added && EntityState != EntityState.Detached && !KontaktyOsobyIsloaded)
                    KontaktyOsobyLoad();

                if (kontaktyOsoby == null)
                    kontaktyOsoby = new List<KontaktOsoba>();

                return kontaktyOsoby;
            }
        }

        #region Windykacja

        private string windykacjaStr = null;
        public string WindykacjaStr
        {
            get
            {
                if (string.IsNullOrEmpty(windykacjaStr))
                {
                    var feature = this.Features.Where(f => f.Name == "WINDYKACJA").FirstOrDefault();
                    if (feature != null)
                    {
                        windykacjaStr = feature.Data;
                    }
                }
                return windykacjaStr;
            }
        }

        private bool windykacjaChanged = false;
        public bool WindykacjaChanged
        {
            get { return windykacjaChanged; }
        }
        private Dictionary<string, string> windykacja = null;
        public Dictionary<string, string> Windykacja
        {
            get
            {
                if (windykacja == null || windykacja.Count == 0)
                {
                    if (windykacja == null)
                        windykacja = new Dictionary<string, string>();
                    string ww = WindykacjaStr;
                    if (ww != null)
                    {
                        string[] pairs = ww.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var p in pairs)
                        {
                            string[] keyValue = p.Split(new char[] { '=' });
                            if (keyValue.Count() == 2)
                                windykacja.Add(keyValue[0], keyValue[1]);
                        }
                    }
                }

                return windykacja;
            }
        }

        public string PierwszeWezwanie
        {
            get
            {
                if (Windykacja.ContainsKey("W1"))
                {
                    return Windykacja["W1"];
                }
                return null;
            }

            set
            {
                Windykacja["W1"] = value;
                windykacjaChanged = true;
            }
        }

        public string DrugieWezwanie
        {
            get
            {
                if (Windykacja.ContainsKey("W2"))
                {
                    return Windykacja["W2"];
                }
                return null;
            }

            set
            {
                Windykacja["W2"] = value;
                windykacjaChanged = true;
            }
        }

        public string TrzecieWezwanie
        {
            get
            {
                if (Windykacja.ContainsKey("W3"))
                {
                    return Windykacja["W3"];
                }
                return null;
            }

            set
            {
                Windykacja["W3"] = value;
                windykacjaChanged = true;
            }
        }

        public string RozmowaTelefoniczna
        {
            get
            {
                if (Windykacja.ContainsKey("TE"))
                {
                    return Windykacja["TE"];
                }
                return null;
            }

            set
            {
                Windykacja["TE"] = value;
                windykacjaChanged = true;
            }
        }

        public string UstalonyTermin
        {
            get
            {
                if (Windykacja.ContainsKey("UT"))
                {
                    return Windykacja["UT"];
                }
                return null;
            }

            set
            {
                Windykacja["UT"] = value;
                windykacjaChanged = true;
            }
        }

        public string SprawaPrzekazana
        {
            get
            {
                if (Windykacja.ContainsKey("SP"))
                {
                    return Windykacja["SP"];
                }
                return null;
            }

            set
            {
                Windykacja["SP"] = value;
                windykacjaChanged = true;
            }
        }

        public string WstrzymanieWindykacji
        {
            get
            {
                if (Windykacja.ContainsKey("WW"))
                {
                    return Windykacja["WW"];
                }
                return null;
            }

            set
            {
                Windykacja["WW"] = value;
                windykacjaChanged = true;
            }
        }

        private string zakończenieWindykacji = null;
        public bool ZakończenieWindykacjiChanged = false;
        public string ZakończenieWindykacji
        {
            get
            {
                if (zakończenieWindykacji == null)
                {
                    Feature feature = Features.Where(f => f.Name == "ZAKONCZENIE_WINDYKACJI").FirstOrDefault();
                    if (feature != null)
                    {
                        zakończenieWindykacji = feature.Data;
                    }
                }
                return zakończenieWindykacji;
            }
            set
            {
                if (zakończenieWindykacji != value)
                {
                    zakończenieWindykacji = value;
                    ZakończenieWindykacjiChanged = true;
                }
            }
        }


        #endregion

        #region IContextSaveChanges implementation

        public string CreateWindykacjaStr()
        {
            string str = "";
            foreach (KeyValuePair<string, string> kvp in Windykacja)
            {
                if (!string.IsNullOrEmpty(kvp.Value))
                    str += kvp.Key + "=" + kvp.Value + ";";
            }
            return str;
        }

        public bool SaveChanges(System.Data.Objects.ObjectContext dataContext)
        {
            var dc = (Enova.Business.Old.DB.EnovaContext)dataContext;
            if (this.EntityState == EntityState.Detached)
            {
                if (Enova.Business.Old.DB.Web.User.LoginedUser.CheckPerissions(true, null))
                {
                    Operator op = null;
                    if (!string.IsNullOrEmpty(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin))
                    {
                        op = dc.OperatorByName(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin);
                    }
                    else
                    {
                        op = dc.OperatorByName(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin);
                    }

                    dc.AddToKontrahenci(this);
                    //dc.SaveChanges();
                    dc.OptimisticSaveChanges();
                    this.Adres.Host = this.ID;
                    this.AdresKorespondencyjny.Host = this.ID;
                    dc.AddToAdresy(this.Adres);
                    dc.AddToAdresy(this.AdresKorespondencyjny);
                    if (this.przedstawiciel != null && this.przedstawiciel.EntityState == EntityState.Detached)
                    {
                        this.przedstawiciel.Parent = this.ID;
                        dc.AddToFeatures(this.przedstawiciel);
                    }

                    //dc.SaveChanges();
                    dc.OptimisticSaveChanges();

                    if (op != null)
                    {
                        ChangeInfo ci = new ChangeInfo()
                        {
                            Operator = op,
                            SourceTable = "Kontrahenci",
                            SourceGuid = this.Guid,
                            Type = (int)Enova.Business.Old.Types.ChangeInfoType.Created,
                            Time = DateTime.Now,
                            Info = string.Empty,
                            WebUser = null,
                            Data = null
                        };

                        dc.AddToChangeInfos(ci);
                    }

                    foreach (var k in KontaktyOsoby)
                    {
                        k.Kontrahent = this;
                        //                           if (k.EntityState == EntityState.Detached)
                        //                               ContextManager.DataContext.AddToKontaktyOsoby(k);
                    }

                    //dc.SaveChanges();
                    dc.OptimisticSaveChanges();

                }

            }
            else
            {

                if (Adres.EntityState == EntityState.Detached)
                    ContextManager.DataContext.AddToAdresy(this.Adres);

                if (AdresKorespondencyjny.EntityState == EntityState.Detached)
                    ContextManager.DataContext.AddToAdresy(AdresKorespondencyjny);

                if (windykacjaChanged)
                {
                    string str = CreateWindykacjaStr();

                    Feature feature = Features.Where(f => f.Name == "WINDYKACJA").FirstOrDefault();
                    if (feature == null)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            feature = new Feature()
                            {
                                ParentType = "Kontrahenci",
                                Parent = ID,
                                Name = "WINDYKACJA",
                                Lp = 0,
                                DataKey = str.Length <= 30 ? str : str.Substring(0, 30),
                                Data = str
                            };
                            dc.AddToFeatures(feature);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            feature.DataKey = str.Length <= 30 ? str : str.Substring(0, 30);
                            feature.Data = str;
                        }
                        else
                        {
                            dc.DeleteObject(feature);
                        }
                    }
                }
                if (ZakończenieWindykacjiChanged)
                {
                    Feature feature = Features.Where(f => f.Name == "ZAKONCZENIE_WINDYKACJI").FirstOrDefault();
                    if (feature == null)
                    {
                        if (!string.IsNullOrEmpty(zakończenieWindykacji))
                        {
                            feature = new Feature()
                            {
                                ParentType = "Kontrahenci",
                                Parent = ID,
                                Name = "ZAKONCZENIE_WINDYKACJI",
                                Lp = 0,
                                DataKey = zakończenieWindykacji.Length <= 30 ? zakończenieWindykacji : zakończenieWindykacji.Substring(0, 30),
                                Data = zakończenieWindykacji
                            };
                            dc.AddToFeatures(feature);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(zakończenieWindykacji))
                        {
                            feature.DataKey = zakończenieWindykacji.Length <= 30 ? zakończenieWindykacji : zakończenieWindykacji.Substring(0, 30);
                            feature.Data = zakończenieWindykacji;
                        }
                        else
                        {
                            dc.DeleteObject(feature);
                        }
                    }
                }


                //dc.SaveChanges();
                dc.OptimisticSaveChanges();

                foreach (var k in KontaktyOsoby)
                {
                    k.Kontrahent = this;
                    //if (k.EntityState == EntityState.Detached)
                    //  ContextManager.DataContext.AddToKontaktyOsoby(k);
                }

                //dc.SaveChanges();
                dc.OptimisticSaveChanges();
            }

            return true;
        }

        #endregion

        #region IDeleteRecord Implementation

        public bool DeleteRecord()
        {
            if (Enova.Business.Old.DB.Web.User.LoginedUser.CheckPerissions(true, null))
            {
                EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
                dc.DeleteObject(this);
                foreach (var fe in dc.Features.Where(f => f.ParentType == "Kontrahenci" && f.Parent == this.ID).ToList())
                {
                    dc.DeleteObject(fe);
                }

                Operator op = null;
                if (!string.IsNullOrEmpty(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin))
                {
                    op = dc.OperatorByName(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin);
                }
                else
                {
                    op = dc.OperatorByName(Enova.Business.Old.DB.Web.User.LoginedUser.Login);
                }

                if (op != null)
                {
                    dc.AddToChangeInfos(new ChangeInfo()
                    {
                        Operator = op,
                        SourceGuid = this.Guid,
                        SourceTable = "Kontrahenci",
                        Type = (int)Enova.Business.Old.Types.ChangeInfoType.Deleted,
                        Time = DateTime.Now,
                        Info = this.Nazwa + " (" + this.Kod + ")"
                    });
                }

                foreach (var k in this.KontaktyOsoby)
                {
                    if (k.EntityState != EntityState.Deleted && k.EntityState != EntityState.Detached)
                        ContextManager.DataContext.DeleteObject(k);
                }

                dc.SaveChanges();
            }
            return true;
        }

        #endregion

        #region IValidation Implementation

        string _validationError = null;
        string IValidation.ValidationError
        {
            get { return _validationError; }
        }

        object _validationInfo = null;
        object IValidation.ValidationInfo
        {
            get
            {
                return this._validationInfo;
            }
        }

        bool IValidation.IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Kod))
                {
                    this._validationError = "Pole \"Kod\" nie może być puste";
                    this._validationInfo = "Kod";
                    return false;
                }

                if (string.IsNullOrEmpty(this.Nazwa))
                {
                    this._validationError = "Pole \"Nazwa\" nie może byc puste";
                    this._validationInfo = "Nazwa";
                    return false;
                }
                return true;
            }
        }

        #endregion

        #endregion



        bool Enova.API.CRM.Kontrahent.Blokada
        {
            get { throw new NotImplementedException(); }
        }

        bool Enova.API.CRM.Kontrahent.BlokadaSprzedazy
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        bool Enova.API.CRM.Kontrahent.BlokadaSprzedaży
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        decimal Enova.API.CRM.Kontrahent.Rabat
        {
            get { throw new NotImplementedException(); }
        }


        Enova.API.Core.Adres Enova.API.CRM.Kontrahent.Adres
        {
            get { throw new NotImplementedException(); }
        }

        Enova.API.Core.Adres Enova.API.CRM.Kontrahent.AdresDoKorespondencji
        {
            get { throw new NotImplementedException(); }
        }


        public Enova.API.Kasa.FormaPlatnosci SposobZaplaty
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.Session Session
        {
            get { throw new NotImplementedException(); }
        }


        public object CallMethod(string name, Type[] argsTypes, object[] args)
        {
            throw new NotImplementedException();
        }


        IEnumerable<API.Towary.CenaGrupowa> API.CRM.Kontrahent.CenyGrupowe
        {
            get { throw new NotImplementedException(); }
        }

        API.Core.Adres API.Kasa.IPodmiotKasowy.Adres
        {
            get { throw new NotImplementedException(); }
        }

        int API.Kasa.IPodmiotKasowy.KontrolaDni
        {
            get { throw new NotImplementedException(); }
        }

        public bool LimitNieograniczony
        {
            get { throw new NotImplementedException(); }
        }

        API.Kasa.IPodmiotKasowy API.Kasa.IPodmiotKasowy.Platnik
        {
            get { throw new NotImplementedException(); }
        }

        public bool PrzeterminowanieNieograniczone
        {
            get { throw new NotImplementedException(); }
        }

        int API.Kasa.IPodmiotKasowy.Termin
        {
            get { throw new NotImplementedException(); }
        }

        int API.Kasa.IPodmiotKasowy.TerminPlanowany
        {
            get { throw new NotImplementedException(); }
        }

        API.Core.Adres API.Core.IPodmiot.Adres
        {
            get { throw new NotImplementedException(); }
        }

        bool API.Core.IPodmiot.Blokada
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

        API.Core.RodzajPodmiotu API.Core.IPodmiot.RodzajPodmiotu
        {
            get { throw new NotImplementedException(); }
        }

        API.Core.StatusPodmiotu API.Core.IPodmiot.StatusPodmiotu
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.TypPodmiotu Typ
        {
            get { throw new NotImplementedException(); }
        }

        public object EnovaObject
        {
            get { throw new NotImplementedException(); }
        }

        public T GetValue<T>(string name)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string name, object value)
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

        API.Business.SubTable API.Kasa.IPodmiotRozrachunki.Rozrachunki
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

        /*
        API.Core.Adres API.Core.IKontrahent.Adres
        {
            get { throw new NotImplementedException(); }
        }
         */

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

        public API.Business.SubTable Lokalizacje
        {
            get { throw new NotImplementedException(); }
        }

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


        API.Business.FeatureCollection API.Business.IRow.Features
        {
            get { throw new NotImplementedException(); }
        }

        API.Business.IRow API.Business.IRow.Parent
        {
            get { throw new NotImplementedException(); }
        }

        string API.Business.IRow.Prefix
        {
            get { throw new NotImplementedException(); }
        }

        API.Business.Row API.Business.IRow.Root
        {
            get { throw new NotImplementedException(); }
        }

        API.Business.Table API.Business.IRow.Table
        {
            get { throw new NotImplementedException(); }
        }

        public int TableHandle
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

        API.Core.Adres API.Core.IKontrahent.Adres
        {
            get { throw new NotImplementedException(); }
        }


        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
