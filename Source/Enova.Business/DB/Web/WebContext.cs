using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Core;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.DB.Web
{
    public partial class WebContext
    {
        #region Fields


        #endregion

        #region Properties

        #endregion

        partial void OnContextCreated()
        {
            this.ObjectMaterialized+=new ObjectMaterializedEventHandler(WebContext_ObjectMaterialized);
            this.ObjectStateManager.ObjectStateManagerChanged+=new System.ComponentModel.CollectionChangeEventHandler(ObjectStateManager_ObjectStateManagerChanged);
        }

        private void WebContext_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (e.Entity is IDbContext)
                ((IDbContext)e.Entity).DbContext = this;

            if (e.Entity is IIsLive)
            {
                ((Enova.Business.Old.IIsLive)e.Entity).IsLive = true;
            }
        }

        private void ObjectStateManager_ObjectStateManagerChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            if (e.Element is IIsLive || e.Element is IDbContext)
            {
                if (e.Action == System.ComponentModel.CollectionChangeAction.Add)
                {
                    if (e.Element is IDbContext)
                        ((IDbContext)e.Element).DbContext = this;
                    if (e.Element is IIsLive)
                        ((IIsLive)e.Element).IsLive = true;
                }
                else if (e.Action == System.ComponentModel.CollectionChangeAction.Remove)
                {
                    if (e.Element is IDbContext)
                        ((IDbContext)e.Element).DbContext = null;
                    if (e.Element is IIsLive)
                        ((IIsLive)e.Element).IsLive = false;
                }
                    
            }
        }

        public void OptimisticSaveChanges()
        {
            bool allSaved = false;
            while (!allSaved)
            {
                try
                {
                    this.SaveChanges();
                    allSaved = true;
                }
                catch (OptimisticConcurrencyException ex)
                {
                    foreach (var e in ex.StateEntries)
                    {
                        if (((EntityObject)e.Entity).EntityState == EntityState.Unchanged)
                            Enova.Business.Old.Core.ContextManager.WebContext.Refresh(RefreshMode.StoreWins, e.Entity);
                        else
                            Enova.Business.Old.Core.ContextManager.WebContext.Refresh(RefreshMode.ClientWins, e.Entity);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                }
            }
        }

        public ObjectQuery<Produkt> GetProduktyByKategoria(KategoriaOld kategoria)
        {
            return GetProduktyByKategoria(kategoria, false);
        }

        public ObjectQuery<Produkt> GetProduktyByKategoria(KategoriaOld kategoria, bool withRoot)
        {
            if (kategoria != null && (withRoot || kategoria.PSID != 1))
            {
                return (ObjectQuery<Produkt>)(from p in kategoria.RelationKategorieProduktow.CreateSourceQuery()
                                              where p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && p.Deleted == false
                                              select p.Produkt);                            
            }
            else
            {
                return (ObjectQuery<Produkt>)Core.ContextManager.WebContext.Produkty;
            }
        }

        public ObjectQuery<ProduktAtrybut> GetProduktyAtrybutyByKategoria(KategoriaOld kategoria)
        {
            if (kategoria != null && kategoria.PSID != 1)
            {
                return (ObjectQuery<ProduktAtrybut>)(from k in kategoria.RelationKategorieProduktow.CreateSourceQuery()
                                                     join pa in ContextManager.WebContext.ProduktyAtrybuty on k.ProduktID equals pa.ID
                                                     where k.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && k.Deleted == false
                                                     select pa);
            }
            else
            {
                return (ObjectQuery<ProduktAtrybut>)ContextManager.WebContext.ProduktyAtrybuty;
            }
        }

        /*
        public ObjectQuery<TowarAtrybut> GetTowarAtrybutByKategoria(Kategoria kategoria)
        {
            if (kategoria != null && kategoria.Wlasciciel != null)
            {
                return 
            }
        }
         */

        public ObjectQuery<Produkt> GetProduktyByEnovaGuid(Guid guid)
        {
            return (ObjectQuery<Produkt>)Core.ContextManager.WebContext.Produkty.Where(pr => pr.EnovaGuid == guid);
        }

        public Produkt GetProduktByEnovaGuid(Guid guid)
        {
            return GetProduktyByEnovaGuid(guid).Where(p => p.TowarEnova == true && (p.Usuniety == null || p.Usuniety == false) && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete ).FirstOrDefault();
        }

        public Guid MapujGuid(Guid zrodlo, string tabela)
        {
            GuidMap guidMap = ContextManager.WebContext.GuidMaps.Where(m => m.Zrodlo == zrodlo && m.Tabela == tabela).FirstOrDefault();
            if (guidMap != null)
                return (Guid)guidMap.Cel;
            return zrodlo;
        }

        public string GetConfigString(string key)
        {
            return Konfiguracje.Where(k => k.Key == key).Select(k => k.Value).FirstOrDefault();
        }

        public int? GetConfigInt(string key)
        {
            string str = GetConfigString(key);
            if (!string.IsNullOrEmpty(str))
            {
                int ret;
                if (int.TryParse(str, out ret))
                    return ret;

            }
            return null;
        }

        public DateTime? GetConfigDate(string key)
        {
            string str = GetConfigString(key);
            if (!string.IsNullOrEmpty(str))
            {
                DateTime ret;
                if (DateTime.TryParse(str, out ret))
                    return ret;
            }
            return null;
        }

        public decimal? GetConfigDecimal(string key, User user = null)
        {
            string s;
            if (user == null)
                s = GetConfigString(key);
            else
                s = user.Konfiguracje.Where(k => k.Key == key).Select(k => k.Value).FirstOrDefault();

            if (!string.IsNullOrEmpty(s))
            {
                decimal d;
                if (decimal.TryParse(s, out d))
                    return d;
            }
            return null;
        }

        public bool? GetConfigBool(string key, User user = null)
        {
            string str;
            if (user == null)
            {
                str = GetConfigString(key);
            }
            else
            {
                str = user.Konfiguracje.Where(k => k.Key == key).Select(k => k.Value).FirstOrDefault();
            }
            if (!string.IsNullOrEmpty(str))
            {
                bool ret;
                if (Boolean.TryParse(str, out ret))
                    return ret;
            }
            return null;
        }
 
        public void SetConfigString(string key, string value)
        {
            Konfiguracja conf = Konfiguracje.Where(k => k.Key == key).FirstOrDefault();
            if (conf == null)
            {
                conf = new Konfiguracja()
                {
                    Key = key
                };
                AddToKonfiguracje(conf);
            }
            conf.Value = value;
        }

        public void SetConfigString(string key, string value, User user)
        {
            Konfiguracja conf = null;
            if (user == null)
                conf = Konfiguracje.Where(k => k.Key == key).FirstOrDefault();
            else
                conf = user.Konfiguracje.Where(k => k.Key == key).FirstOrDefault();
            if (conf == null)
            {
                conf = new Konfiguracja()
                {
                    Key = key,
                    RelationUser = user
                };
                AddToKonfiguracje(conf);
            }
            conf.Value = value;
        }

        public ObjectQuery<PozycjaZamowienia> NiespakowanePozycjeZam(DateTime? dataDo, string pora, int dostawcaID)
        {
            return (ObjectQuery<PozycjaZamowienia>)(from p in Enova.Business.Old.Core.ContextManager.WebContext.PozycjeZamowien
                                                    where p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete &&
                                                    p.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete &&
                                                    p.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.Notsaved && p.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.Synchronizing &&
                                                    p.Zamowienie.NaKiedy != null && p.Zamowienie.NaKiedy < dataDo &&
                                                    (pora == "W" || (pora == "P" && (p.Zamowienie.NaKiedyTyp == "P" || p.Zamowienie.NaKiedyTyp == "R" || p.Zamowienie.Transport == 1)) ||
                                                    (pora == "R" && p.Zamowienie.NaKiedyTyp == "R")) &&
                                                    p.Zamowienie.RelationOstStatus != null && p.Produkt != null && (dostawcaID == 0 || p.Produkt.DostawcaID == dostawcaID) &&
                                                    (p.Zamowienie.RelationOstStatus.Pakowanie.Value || p.Zamowienie.RelationOstStatus.DoMagazynu.Value ||
                                                    p.Zamowienie.RelationOstStatus.NoweZamowienie.Value || p.Zamowienie.RelationOstStatus.Blokada.Value ||
                                                    p.Zamowienie.RelationOstStatus.Wstrzymane.Value)
                                                    select p);
        }

        public ObjectQuery<PozycjaZamowienia> PozycjeZam(DateTime? dataDo, string pora, int dostawcaID, bool nowe, bool doMagazynu, bool blokada,
            bool wstrzymane, bool pakowane, bool spakowane, bool kurier, bool przedstawiciel, bool wyslane)
        {
            return (ObjectQuery<PozycjaZamowienia>)(from p in Enova.Business.Old.Core.ContextManager.WebContext.PozycjeZamowien
                                                    where p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete &&
                                                    p.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete &&
                                                    p.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.Notsaved && p.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.Synchronizing &&
                                                    p.Zamowienie.NaKiedy != null && p.Zamowienie.NaKiedy < dataDo &&
                                                    (pora == "W" || (pora == "P" && (p.Zamowienie.NaKiedyTyp == "P" || p.Zamowienie.NaKiedyTyp == "R" || p.Zamowienie.Transport == 1)) ||
                                                    (pora == "R" && p.Zamowienie.NaKiedyTyp == "R")) &&
                                                    p.Zamowienie.RelationOstStatus != null && p.Produkt != null && (dostawcaID == 0 || p.Produkt.DostawcaID == dostawcaID) &&
                                                    (
                                                    nowe && p.Zamowienie.RelationOstStatus.NoweZamowienie.Value ||
                                                    doMagazynu && p.Zamowienie.RelationOstStatus.DoMagazynu.Value ||
                                                    blokada && p.Zamowienie.RelationOstStatus.Blokada.Value ||
                                                    wstrzymane && p.Zamowienie.RelationOstStatus.Wstrzymane.Value ||
                                                    pakowane && p.Zamowienie.RelationOstStatus.Pakowanie.Value ||
                                                    spakowane && p.Zamowienie.RelationOstStatus.Spakowane.Value ||
                                                    kurier && p.Zamowienie.RelationOstStatus.Kurier.Value ||
                                                    przedstawiciel && p.Zamowienie.RelationOstStatus.Przedstawiciel.Value ||
                                                    wyslane && p.Zamowienie.RelationOstStatus.Wysłane.Value
                                                    )
                                                    select p);
        }

        public ObjectQuery<PozycjaZamowienia> PozycjeZam(DateTime? dataDo, string pora, int dostawcaID, int przedstawicielID, bool nowe, bool doMagazynu, bool blokada,
            bool wstrzymane, bool pakowane, bool spakowane, bool kurier, bool przedstawiciel, bool wyslane)
        {
            return (ObjectQuery<PozycjaZamowienia>)(from p in Enova.Business.Old.Core.ContextManager.WebContext.PozycjeZamowien
                                                    where p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete &&
                                                    (przedstawicielID == 0 || p.Zamowienie.Kontrahent.Przedstawiciel.ID == przedstawicielID) &&
                                                    p.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete &&
                                                    p.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.Notsaved && p.Zamowienie.Synchronizacja != (int)RowSynchronizeOld.Synchronizing &&
                                                    p.Zamowienie.NaKiedy != null && p.Zamowienie.NaKiedy < dataDo &&
                                                    (pora == "W" || (pora == "P" && (p.Zamowienie.NaKiedyTyp == "P" || p.Zamowienie.NaKiedyTyp == "R" || p.Zamowienie.Transport == 1)) ||
                                                    (pora == "R" && p.Zamowienie.NaKiedyTyp == "R")) &&
                                                    p.Zamowienie.RelationOstStatus != null && p.Produkt != null && (dostawcaID == 0 || p.Produkt.DostawcaID == dostawcaID) &&
                                                    (
                                                    nowe && p.Zamowienie.RelationOstStatus.NoweZamowienie.Value ||
                                                    doMagazynu && p.Zamowienie.RelationOstStatus.DoMagazynu.Value ||
                                                    blokada && p.Zamowienie.RelationOstStatus.Blokada.Value ||
                                                    wstrzymane && p.Zamowienie.RelationOstStatus.Wstrzymane.Value ||
                                                    pakowane && p.Zamowienie.RelationOstStatus.Pakowanie.Value ||
                                                    spakowane && p.Zamowienie.RelationOstStatus.Spakowane.Value ||
                                                    kurier && p.Zamowienie.RelationOstStatus.Kurier.Value ||
                                                    przedstawiciel && p.Zamowienie.RelationOstStatus.Przedstawiciel.Value ||
                                                    wyslane && p.Zamowienie.RelationOstStatus.Wysłane.Value
                                                    )
                                                    select p);
        }

        public ObjectQuery<Kontrahent> KontrahenciByPrzedstawiciel(string przedstawiciel)
        {
            if (string.IsNullOrEmpty(przedstawiciel))
                return (ObjectQuery<Kontrahent>)Enova.Business.Old.Core.ContextManager.WebContext.Kontrahenci.Where(k => k.CzyAgent == false);
            else
            {
                return (ObjectQuery<Kontrahent>)Enova.Business.Old.Core.ContextManager.WebContext.Kontrahenci.Where(k => k.Przedstawiciel.Kod == przedstawiciel);
            }
        }

        public ObjectQuery<Zamowienie> ZamowieniaByPrzedstawiciel(string przedstawiciel)
        {
            return (ObjectQuery<Zamowienie>)Enova.Business.Old.Core.ContextManager.WebContext.Zamowienia
                .Where(z => z.Kontrahent.CzyAgent == false && z.Kontrahent.Przedstawiciel.Kod == przedstawiciel);
        }

        public ObjectQuery<PozycjaZamowienia> PozycjeZamByPrzedstawiciel(string przedstawiciel)
        {
            if (string.IsNullOrEmpty(przedstawiciel))
                return Enova.Business.Old.Core.ContextManager.WebContext.PozycjeZamowien;
            else
                return (ObjectQuery<PozycjaZamowienia>)Enova.Business.Old.Core.ContextManager.WebContext.PozycjeZamowien
                    .Where(p => p.Zamowienie.Kontrahent.CzyAgent == false && p.Zamowienie.Kontrahent.Przedstawiciel.Kod == przedstawiciel);
        }

        public string GetProviderConnectionString()
        {
            string str = this.Connection.ConnectionString;
            if (str.ToLower().StartsWith("name="))
            {
                str = str.Substring(5);
                str = System.Configuration.ConfigurationManager.ConnectionStrings[str].ConnectionString;
            }
            return new EntityConnectionStringBuilder(str).ProviderConnectionString;
        }

        public string GetFedExUri()
        {
            return GetConfigString("FEDEX_ADRES_URL");
        }

        public string GetFedExUser()
        {
            return GetConfigString("FEDEX_LOGIN");
        }

        public string GetFedExPassword()
        {
            return GetConfigString("FEDEX_PASSWORD");
        }

        /*
        public string[] GetKategorieSlownika()
        {
            return this.Slownik.GroupBy(r => r.Kategoria).Select(r => r.Key).OrderBy(r => r).ToArray();
        }
         */

        public DB.Web.Operator GetOperatorByNazwa(string nazwa)
        {
            return this.Operatorzy.Where(r => r.Nazwa.ToLower() == nazwa.ToLower()).FirstOrDefault();
        }

    }
}
