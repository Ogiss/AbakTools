using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects.ELinq;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Enova.Business.Old.DB
{
    public partial class EnovaContext : ISetSession, ISessionable
    {
        #region Fields

        private Session session;

        #endregion

        #region Properties

        Session ISetSession.Session
        {
            set
            {
                this.session = value;
            }
        }

        public Session Session
        {
            get { return this.session; }
        }
    

        #endregion

        #region Methods

        partial void OnContextCreated()
        {
            this.ObjectMaterialized += new ObjectMaterializedEventHandler(EnovaContext_ObjectMaterialized);
            this.ObjectStateManager.ObjectStateManagerChanged+=new CollectionChangeEventHandler(ObjectStateManager_ObjectStateManagerChanged);
        }

        private void EnovaContext_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (e.Entity is IDbContext)
                ((IDbContext)e.Entity).DbContext = this;
            if (this.session != null && e.Entity is ISetSession)
                ((ISetSession)e.Entity).Session = this.session;
            /*
            if (e.Entity is IRowInvoker)
                ((IRowInvoker)e.Entity).OnLoaded();
             */
        }

        private void ObjectStateManager_ObjectStateManagerChanged(object sender, CollectionChangeEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Add)
            {
                if (e.Element is IDbContext)
                    ((IDbContext)e.Element).DbContext = this;
                if (this.session != null && e.Element is ISetSession)
                    ((ISetSession)e.Element).Session = this.session;
                /*
                if (e.Element is IRowInvoker)
                    ((IRowInvoker)e.Element).OnAdded();
                 */
            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                /*
                if (e.Element is IRowInvoker)
                    ((IRowInvoker)e.Element).OnDeleted();
                 */
                if (e.Element is IDbContext)
                    ((IDbContext)e.Element).DbContext = null;
                if (e.Element is ISetSession)
                    ((ISetSession)e.Element).Session = null;
            }
        }

        public void Refresh(object obj)
        {
            this.Refresh(RefreshMode.StoreWins, obj);
        }

        #endregion

        #region Do usuniecia lub przedobienia

        [Obsolete("Do usunięcia")]
        public ObjectQuery<DokHandlowyView> DokHandloweViewByKontrahent(Kontrahent kontrahent, DateTime? dateFrom, DateTime? dateTo, int? definicja)
        {
            return (ObjectQuery<DokHandlowyView>)(from d in kontrahent.DokHandlowe
                                                  where (dateFrom == null || d.Data >= dateFrom)
                                                  && (dateTo == null || d.Data <= dateTo)
                                                  && (definicja == null || d.RelationDefinicja.ID == definicja)
                                                  select new DokHandlowyView()
                                                  {
                                                      ID = d.ID,
                                                      Guid = d.Guid,
                                                      Definicja = d.RelationDefinicja.ID,
                                                      Stan = d.StanInt,
                                                      BazaDanych = "ABAK",
                                                      Data = d.Data,
                                                      KontrahentID = d.Kontrahent.ID,
                                                      KontrahentKod = d.KontrahentKod,
                                                      KontrahentNazwa = d.KontrahentNazwa,
                                                      NumerPelny = d.NumerPelny,
                                                      SumaNetto = d.SumaNetto,
                                                      SumaVat = d.SumaVAT,
                                                      SumaBrutto = d.SumaBrutto
                                                  });
        }

        public ObjectQuery<Obrot> ObrotyByKontrahent(string przedstawiciel, Kontrahent kontrahent)
        {
            int ID = kontrahent == null ? 0 : kontrahent.ID;
            return (ObjectQuery<Obrot>)(from o in Obroty
                                        join fp in Features on
                                        new { ParentType = "Kontrahenci", Parent = o.RozchodKontrahent.ID, Name = "przedstawiciel" } equals
                                        new { ParentType = fp.ParentType, Parent = fp.Parent, Name = fp.Name }
                                        where (przedstawiciel == null || fp.Data == przedstawiciel)
                                        && (ID == 0 || o.RozchodKontrahent.ID == ID)
                                        select o);
        }

        public ObjectQuery<Obrot> ObrotyByKontrahent(string przedstawiciel, Enova.API.CRM.Kontrahent kontrahent)
        {
            int ID = kontrahent == null ? 0 : kontrahent.ID;
            return (ObjectQuery<Obrot>)(from o in Obroty
                                        join fp in Features on
                                        new { ParentType = "Kontrahenci", Parent = o.RozchodKontrahent.ID, Name = "przedstawiciel" } equals
                                        new { ParentType = fp.ParentType, Parent = fp.Parent, Name = fp.Name }
                                        where (przedstawiciel == null || fp.Data == przedstawiciel)
                                        && (ID == 0 || o.RozchodKontrahent.ID == ID)
                                        select o);
        }


        public ObjectQuery<Feature> FeaturesByDef(string parentType, string name)
        {
            return (ObjectQuery<Feature>)Features.Where(f => f.ParentType == parentType && f.Name == name);
        }

        public EwidencjaSP EwidencjaSPBySymbol(string symbol)
        {
            return EwidencjeSP.Where(e => e.Symbol == symbol).FirstOrDefault();
        }

        public EwidencjaSP KasaFirmowa
        {
            get
            {
                return EwidencjaSPBySymbol("FI11");
            }
        }

        public ObjectQuery<EwidencjaSP> PozostałeKasy
        {
            get
            {
                string[] symboleKas = { "BI", "JC", "LP", "LT", "ŁD", "MB", "MG", "MW", "PS", "SZ", "TP" };
                return (ObjectQuery<EwidencjaSP>)(from e in EwidencjeSP
                                                  where e.Symbol == "BI" || e.Symbol == "JC" || e.Symbol == "LP" || e.Symbol == "LT"
                                                  || e.Symbol == "ŁD" || e.Symbol == "MB" || e.Symbol == "MG" || e.Symbol == "MW"
                                                  || e.Symbol == "PS" || e.Symbol == "SZ" || e.Symbol == "TP"
                                                  select e);
            }
        }

        public ObjectQuery<RozrachunekIdx> Rozrachunki
        {
            get
            {
                return (ObjectQuery<RozrachunekIdx>)
                    (from r in RozrachunkiIdx
                     join k in Kontrahenci on
                     new { PodmiotType = r.PodmiotType, Podmiot = r.Podmiot.Value } equals
                     new { PodmiotType = "Kontrahenci", Podmiot = k.ID }
                     where !k.Kod.StartsWith(" ") && !k.Kod.StartsWith("1-") && !k.Kod.StartsWith("2-")
                     select r);
            }
        }

        public decimal Należności
        {
            get
            {
                int n = (int)Enova.Business.Old.Types.TypRozrachunku.Należność;
                decimal? na = (from r in Rozrachunki
                               where r.Typ == n && r.KwotaValue != r.KwotaRozliczonaValue
                               select r).Sum(r => r.KwotaValue - r.KwotaRozliczonaValue);
                return na == null ? 0 : na.Value;
            }
        }

        public decimal Zobowiązania
        {
            get
            {
                int z = (int)Enova.Business.Old.Types.TypRozrachunku.Zobowiązanie;
                decimal? zo = (from r in Rozrachunki
                               where r.Typ == z && r.KwotaValue != r.KwotaRozliczonaValue
                               select r).Sum(r => r.KwotaValue - r.KwotaRozliczonaValue);
                return zo == null ? 0 : zo.Value;

            }
        }

        public Magazyn MagazynBySymbol(string symbol)
        {
            return Magazyny.Where(m => m.Symbol == symbol).FirstOrDefault();
        }

        public Kontrahent KontrahentByKod(string kod)
        {
            return Kontrahenci.Where(k => k.Kod == kod).FirstOrDefault();
        }

        public Operator OperatorByName(string name)
        {
            return Operators.Where(o => o.Name == name).FirstOrDefault();
        }

        public ObjectQuery<Towar> GetTowaryByGrupa(string grupa, string podgrupa)
        {
            return (ObjectQuery<Towar>)(from t in this.Towary
                                         join f in this.Features on t.ID equals f.Parent
                                         where f.ParentType == "Towary" && (grupa == null || f.Name == grupa) && (podgrupa == null || f.Data == podgrupa)
                                         select t);
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
            }
        }

        public Towar GetTowarByBarCode(string barcode)
        {
            var row = this.KodyKreskowe.Where(c => c.ZapisType == "Towary" && c.Kod == barcode).FirstOrDefault();
            if (row != null)
            {
                return this.Towary.Where(t => t.ID == row.Zapis).FirstOrDefault();
            }
            return null;
        }

        public List<Towar> GetTowaryByBarCode(string barcode)
        {
            return (from t in this.Towary
                    join bc in this.KodyKreskowe on t.ID equals bc.Zapis
                    where bc.ZapisType == "Towary" && bc.Kod == barcode
                    select t).ToList();
        }

        #endregion

    }
}
