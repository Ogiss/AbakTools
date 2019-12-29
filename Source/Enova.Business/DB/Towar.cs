using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Old.Towary;
using Enova.Old.Handel;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;

namespace Enova.Business.Old.DB
{
    public partial class Towar : IGuidedRow, IDbContext
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
        RowState IRow.State
        {
            get { return this.GetRowState(); }
        }
        ITable IRow.Table
        {
            get { return TowaryModule.GetInstance(this.DataContext).Towary; }
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

        #region Methods

        public override string ToString()
        {
            return this.Kod + " - " + this.Nazwa;
        }

        #endregion


        internal Cena cenaHurtowa = null;
        public Cena CenaHurtowa
        {
            get
            {
                if(cenaHurtowa==null)
                    cenaHurtowa = this.Ceny.CreateSourceQuery().Where(c => c.Definicja.ID == 2).FirstOrDefault();
                return cenaHurtowa;
            }
        }

        public double? CenaHurtowaNetto
        {
            get
            {
                return CenaHurtowa == null ? null : CenaHurtowa.NettoValue;
            }
        }

        public double? CenaHurtowaBrutto
        {
            get
            {
                return CenaHurtowa == null ? null : CenaHurtowa.BruttoValue;
            }
        }

        internal Cena cenaPodstawowa = null;
        public Cena CenaPodstawowa
        {
            get
            {
                if (cenaPodstawowa == null)
                    cenaPodstawowa = this.Ceny.CreateSourceQuery().Where(c => c.Definicja.ID == 1).FirstOrDefault();
                return cenaPodstawowa;
                
            }
        }

        public double? StandardowaIlosc
        {
            get
            {
                var cena = this.CenaHurtowa;
                if (cena != null)
                    return cena.StandardowaIloscValue;
                return null;
            }
        }

        public int Kolejnosc
        {
            get
            {
                if (this.DataContext != null)
                {
                    var feature = this.GetFeatures(this.DataContext, "Kolejność").FirstOrDefault();
                    if (feature != null)
                    {
                        double i;
                        if (double.TryParse(feature.Data.Replace('.',','), out i))
                            return (int)i;
                    }
                }
                return 1000;
            }
        }

        public int KolejnoscNaForm
        {
            get
            {
                if (this.DataContext != null)
                {
                    var feature = this.GetFeatures(this.DataContext, "Kolejność na formularzu").FirstOrDefault();
                    if (feature != null)
                    {
                        this.DataContext.Refresh(RefreshMode.StoreWins, feature);
                        int i;
                        if (int.TryParse(feature.Data.Replace('.', ','), out i))
                            return i;
                    }
                }
                return this.Kolejnosc;
            }
        }

        public string KolorNaForm
        {
            get
            {
                if (this.DataContext != null)
                {
                    var feature = this.GetFeatures(this.DataContext, "Kolor na formularzu").FirstOrDefault();
                    if (feature != null)
                        return feature.Data.Trim();
                }
                return null;
            }
        }

        public static explicit operator Web.Produkt(Towar towar)
        {
            Web.Produkt produkt = Enova.Business.Old.Core.ContextManager.WebContext.Produkty
                .Where(p => p.EnovaGuid == towar.Guid && p.TowarEnova).FirstOrDefault();
            return produkt;
        }

        public DefinicjaStawkiVat StawkaVat
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !DefinicjaStawkiReference.IsLoaded)
                    DefinicjaStawkiReference.Load();
                return DefinicjaStawki;
            }
        }

        public ObjectQuery<Feature> Features
        {
            get
            {
                return (ObjectQuery<Feature>)Enova.Business.Old.Core.ContextManager.DataContext.Features
                    .Where(f => f.ParentType == "Towary" && f.Parent == this.ID);
            }
        }

        public bool RozdzielProdukt
        {
            get
            {
                Feature feature = this.Features.Where(f => f.Name == "ROZDZIEL_PRODUKT").FirstOrDefault();
                if (feature != null && feature.Data.Trim() == "1")
                    return true;
                return false;
            }
        }

        public bool GetOgraniczenieSprzedazyWlaczone(EnovaContext dc)
        {
            if (dc != null)
            {
                Feature feature = this.Features.Where(f => f.Name == "OGRANICZENIE SPRZEDAŻY").FirstOrDefault();
                if (feature != null && feature.Data.Trim() == "1")
                    return true;
            }
            return false;
        }

        public int GetOraniczenieSprzedazyStan(EnovaContext dc)
        {
            if (dc != null)
            {
                Feature feature = this.Features.Where(f => f.Name == "STAN MAGAZYNU").FirstOrDefault();
                int i = 0;
                if (feature != null && int.TryParse(feature.Data, out i))
                    return i;
            }
            return 0;
        }

        public void SetOgraniczenieSprzedazyStan(EnovaContext ec, Web.WebContext lc, int stanMagazynu)
        {
            if (ec != null)
            {
                Feature feature = this.Features.Where(f => f.Name == "STAN MAGAZYNU").FirstOrDefault();
                if (feature != null)
                {
                    feature.Data = stanMagazynu.ToString();
                    ec.SaveChanges();
                    if (stanMagazynu <= 0)
                        this.UstawDostepnosc(ec, lc, false);
                }
            }
        }

        public void UstawDostepnosc(EnovaContext ec, Web.WebContext lc, bool dostepnosc)
        {
        }

        public string GetDostawca()
        {
            return this.Features.Where(f => f.Name == "DOSTAWCY").Select(f => f.Data).FirstOrDefault();
        }

        public string FullName
        {
            get { return this.Kod + " - " + this.Nazwa; }
        }

        public ObjectQuery<Obrot> GetObroty(EnovaContext dc)
        {
            return (ObjectQuery<Obrot>)dc.Obroty.Where(o => o.Towar.ID == this.ID);
        }

        public ObjectQuery<Obrot> GetObroty(EnovaContext dc, DateTime? dataOd, DateTime? dataDo)
        {
            return (ObjectQuery<Obrot>)GetObroty(dc).Where(o => (dataOd == null || o.RozchodData >= dataOd) && (dataDo == null || o.RozchodData <= dataDo));
        }

        public ObjectQuery<Obrot> GetObroty(EnovaContext dc, int idKontrahenta, DateTime? dataOd, DateTime? dataDo)
        {
            return (ObjectQuery<Obrot>)dc.Obroty
                .Where(ob => ob.Towar.ID == this.ID && ob.RozchodKontrahent.ID == idKontrahenta
                    && (dataOd == null || ob.RozchodData >= dataOd) && (dataDo == null || ob.RozchodData <= dataDo));
             
        }

        public ObjectQuery<Obrot> GetObroty(EnovaContext dc, Kontrahent kontrahent, DateTime? dataOd, DateTime? dataDo)
        {
            return (ObjectQuery<Obrot>)GetObroty(dc, dataOd, dataDo).Where(o => o.RozchodKontrahent.ID == kontrahent.ID);
        }

        public ObjectQuery<Feature> GetFeatures(EnovaContext dc)
        {
            return (ObjectQuery<Feature>)dc.Features.Where(f => f.ParentType == "Towary" && f.Parent == this.ID);
        }

        public ObjectQuery<Feature> GetFeatures(EnovaContext dc, string name)
        {
            return (ObjectQuery<Feature>)GetFeatures(dc).Where(f => f.Name == name);
        }

        public Feature GetFeature(EnovaContext dc, string name, int lp = 0)
        {
            return GetFeatures(dc, name).Where(f => f.Lp == lp).FirstOrDefault();
        }

        public Feature SetFeature(EnovaContext dc, string name, string data, string dataKey = null, int lp = 0)
        {
            dataKey = dataKey == null ? data : dataKey;
            var feature = this.GetFeature(dc, name, lp);
            if (feature == null)
            {
                feature = new Feature()
                {
                    Lp = lp,
                    Name = name,
                    Parent = this.ID,
                    ParentType = "Towary"
                };
                dc.Features.AddObject(feature);
            }
            feature.DataKey = dataKey;
            feature.Data = data;
            return feature;
        }

        public Towar GetTowarObliczObrotyZ(EnovaContext dc)
        {
            var feature = dc.Features.Where(f=>f.ParentType == "Towary" && f.Parent == this.ID && f.Name == "OBLICZ OBROTY Z").FirstOrDefault();
            if (feature != null)
            {
                int id = 0;
                if (int.TryParse(feature.Data.Trim(), out id))
                {
                    return dc.Towary.Where(t => t.ID == id).FirstOrDefault();
                }
            }
            return null;
        }

        public string GetDostawca(EnovaContext dc)
        {
            return GetFeatures(dc, "DOSTAWCY").Select(f => f.Data).FirstOrDefault();
        }

        public bool GetNowosc(EnovaContext dc)
        {
            var feature = GetFeatures(dc, "NOWOŚĆ").FirstOrDefault();
            if (feature != null)
            {
                if (feature.Data.Trim() == "1")
                    return true;
            }
            return false;
        }

        public double GetMnoznikObrotow(EnovaContext dc)
        {
            var feature = GetFeatures(dc, "MNOŻNIK OBROTÓW").FirstOrDefault();
            if (feature != null)
            {
                double d;
                if (double.TryParse(feature.Data.Replace('.',','), out d))
                    return d == 0 ? 1 : d;
            }
            return 1;
        }

        public string GetPrefix(EnovaContext dc)
        {
            var feature = GetFeatures(dc, "PREFIX").FirstOrDefault();
            if (feature != null)
                return feature.Data;
            return string.Empty;
        }

        public string GetSuffix(EnovaContext dc)
        {
            var feature = GetFeatures(dc, "SUFFIX").FirstOrDefault();
            if (feature != null)
                return feature.Data;
            return string.Empty;
        }

        public double GetProcentObrotow(EnovaContext dc)
        {
            var feature = GetFeatures(dc, "PROCENT OBROTÓW").FirstOrDefault();
            if (feature != null)
            {
                double d;
                if (double.TryParse(feature.Data.Replace('.', ','), out d))
                    return d == 0 ? 1 : d;
            }
            return 1;
        }

        public double? GetStanMagazynu(EnovaContext dc, int? magId)
        {
            return dc.Zasoby.Where(z => z.Towar.ID == this.ID && z.Okres == 1 && (magId == null || z.Magazyn.ID == magId)).Sum(z => z.IloscValue);
        }

        public static Towar GetByGuid(EnovaContext dc, Guid guid)
        {
            return dc.Towary.Where(t => t.Guid == guid).FirstOrDefault();
        }

        public ObjectQuery<PozycjaDokHandlowego> PozycjeDokHandlowychQuery
        {
            get { return this.PozycjeDokHan.CreateSourceQuery(); }
        }

        private Enova.Old.Handel.PozycjeDokHan pozycjeDokHandlowych = null;
        public Enova.Old.Handel.PozycjeDokHan PozycjeDokHandlowych
        {
            get
            {
                if (this.pozycjeDokHandlowych == null)
                    pozycjeDokHandlowych = new PozycjeDokHan() { BaseQuery = this.PozycjeDokHan.CreateSourceQuery() };
                return this.pozycjeDokHandlowych;

            }
        }

        static string code = @"
        public static class __CompiledExpr__
        {{
            public static {0} Run({1})
            {{
                return {2};
            }}
        }}
        ";

        static MethodInfo ToMethod(string expr, Type[] argTypes, string[] argNames, Type resultType)
        {
            StringBuilder argString = new StringBuilder();
            for (int i = 0; i < argTypes.Length; i++)
            {
                if (i != 0) argString.Append(", ");
                argString.AppendFormat("{0} {1}", argTypes[i].FullName, argNames[i]);
            }
            string finalCode = string.Format(code, resultType != null ? resultType.FullName : "void",
                argString, expr);

            var parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("mscorlib.dll");
            parameters.ReferencedAssemblies.Add(Path.GetFileName(Assembly.GetExecutingAssembly().Location));
            parameters.GenerateInMemory = true;

            var c = new CSharpCodeProvider();
            CompilerResults results = c.CompileAssemblyFromSource(parameters, finalCode);
            var asm = results.CompiledAssembly;
            var compiledType = asm.GetType("__CompiledExpr__");
            return compiledType.GetMethod("Run");
        }


        private static Dictionary<int, MethodInfo> algorytmyZaogrąglaniaObrotów = new Dictionary<int, MethodInfo>();

        private void inicjujAlgorytmZaokraglania(EnovaContext ec)
        {
            var f = GetFeatures(ec, "ALG_ZAOK_OBR").FirstOrDefault();
            MethodInfo minfo = null;
            if (f != null && !string.IsNullOrEmpty(f.Data.Trim()))
            {
                minfo = ToMethod(f.Data, new Type[] { typeof(double) }, new string[] { "ilosc" }, typeof(double));
            }
            algorytmyZaogrąglaniaObrotów[this.ID] = minfo;
        }

        public bool ZaokragnijObroty(EnovaContext ec, ref double ilosc)
        {
            if (!algorytmyZaogrąglaniaObrotów.ContainsKey(this.ID))
                inicjujAlgorytmZaokraglania(ec);

            MethodInfo minfo = algorytmyZaogrąglaniaObrotów[this.ID];
            if (minfo != null)
            {
                ilosc = (double)minfo.Invoke(null, new object[] { ilosc });
                return true;
            }
            return false;
        }
       
    }
}
