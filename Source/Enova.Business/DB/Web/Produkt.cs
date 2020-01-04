using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;
using System.IO;

namespace Enova.Business.Old.DB.Web
{
    [DataEditForm("AbakTools.Towary.Forms.TowarEditForm, AbakTools.Handel.Forms")]
    public partial class Produkt : ISaveChanges, IUndoChanges, IDeleteRecord, IValidation, Enova.API.Towary.Towar, IDbContext, ICloneable
    {
        #region Fields

        private Zasob zasob;
        private ZasobEx zasobEx;

        #endregion

        public ObjectContext DbContext { get; set; }

        public override string ToString()
        {
            return Kod + " - " + Nazwa;
        }

        Guid Enova.API.Business.GuidedRow.Guid
        {
            get
            {
                return this.EnovaGuid.Value;
            }
        }

        public StawkaVat StawkaVat
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !RelationStawkaVatReference.IsLoaded)
                    RelationStawkaVatReference.Load();
                return RelationStawkaVat;
            }
            set
            {
                RelationStawkaVat = value;
            }
        }

        public GrupaAtrybutow GrupaAtrybutow
        {
            get
            {
                AtrybutProduktu atrybut = AtrybutyProduktu.FirstOrDefault();
                if (atrybut != null)
                    return atrybut.GrupaAtrybutow;
                return null;
            }
        }

        public Zdjecie GetZdjecie()
        {
            Zdjecie zdjecie = this.Zdjecia.Where(z => z.Okladka == true && z.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).FirstOrDefault();
            if (zdjecie == null)
                zdjecie = this.Zdjecia.FirstOrDefault();
            return zdjecie;
        }

        public string SynchronizacjaStr
        {
            get
            {
                if (this.Synchronizacja == null)
                    return "NULL";
                switch ((RowSynchronizeOld)this.Synchronizacja)
                {
                    case RowSynchronizeOld.Notsaved:
                        return "Not saved";
                    case RowSynchronizeOld.NotsynchronizedDelete:
                        return "Delete";
                    case RowSynchronizeOld.NotsynchronizedEdit:
                        return "Edit";
                    case RowSynchronizeOld.NotsynchronizedNew:
                        return "New";
                    case RowSynchronizeOld.Synchronized:
                        return "Synchronized";
                    case RowSynchronizeOld.Synchronizing:
                        return "Synchronizing";
                    default:
                        return "Unknow";
                }
            }
        }

        public string CenaNettoStr
        {
            get
            {
                if (this.Cena != null)
                {
                    return decimal.Round(this.Cena.Value, 2).ToString() + " zł";
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    decimal d;
                    if (decimal.TryParse(value.Replace("zł", "").Trim(), out d))
                        this.Cena = d;
                }
                else
                {
                    this.Cena = null;
                }
            }
        }

        public decimal? CenaBrutto
        {
            get
            {
                if (this.Cena != null && this.StawkaVat != null)
                {
                    return decimal.Round(this.Cena.Value * (1M + (decimal)this.StawkaVat.Procent/100M), 2);
                }
                return null;
            }
        }

        public string CenaBruttoStr
        {
            get
            {
                if (this.CenaBrutto != null)
                {
                    return this.CenaBrutto.ToString() + " zł"; 
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                }
                else
                {
                    
                }
            }
        }

        public string FullName
        {
            get { return this.Kod + " - " + this.Nazwa; }
        }

        public Zasob Zasob
        {
            get
            {
                if (zasob == null)
                    zasob = this.Zasoby.Where(r => r.AtrybutID == null).FirstOrDefault();
                return zasob;
            }
        }

        public bool Blokada
        {
            get
            {
                return this.Zasob != null ? this.Zasob.Blokada : false;
            }
            set
            {
                if (this.Zasob != null && this.Zasob.Blokada != value)
                    this.Zasob.Blokada = value;
                    
            }
        }

        public bool Dostepny
        {
            get
            {
                return this.Zasob?.Dostepny ?? DostepnyOld;
            }
            set
            {
                DostepnyOld = value;
                if (this.Zasob != null && this.Zasob.Dostepny != value)
                    this.Zasob.Dostepny = value;
            }
        }

        public Produkt()
        {
            this.Usuniety = false;
        }

        public static Produkt CreateEnovaTowar(WebContext dc, Enova.Business.Old.DB.Towar towar)
        {
            DateTime stamp = DateTime.Now;
            Enova.Business.Old.DB.Web.StawkaVat stawkaVat = (Enova.Business.Old.DB.Web.StawkaVat)towar.DefinicjaStawki;

            var produkt = new Enova.Business.Old.DB.Web.Produkt()
            {
                EnovaGuid = towar.Guid,
                GUID = Guid.NewGuid(),
                Kod = towar.Kod,
                Nazwa = towar.Nazwa,
                AktywnyOld = true,
                Cena = (decimal)towar.CenaHurtowaNetto,
                DataAktualizacji = stamp,
                DataDodania = stamp,
                Gotowy = false,
                Indexed = true,
                KrotkiOpis = "",
                LinkRewrite = Enova.Business.Old.Core.Tools.LinkRewrite(towar.Nazwa),
                MetaOpis = "",
                MetaTytul = "",
                Opis = "",
                Podprodukt = false,
                ProduktGrupujacy = false,
                PSID = 0,
                Stamp = stamp,
                Stan = 0,
                Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew,
                Widoczny = true,
                WlascicielID = 0,
                StawkaVat = stawkaVat,
                TowarEnova = true,
                EnovaStamp = towar.Stamp,
                Dostepny = true,
                Usuniety = false
            };

            Enova.Business.Old.Core.ContextManager.WebContext.AddToProdukty(produkt);

            var grupy = Enova.Business.Old.DB.FeatureDef.GrupyRabatowe;
            var wc = Enova.Business.Old.Core.ContextManager.WebContext;

            foreach (var gr in grupy)
            {
                var features = towar.Features.Where(f => f.Name == gr.Name).ToList();
                foreach (var feature in features)
                {
                    var dictionary = gr.DictionarySet.Where(d => d.Value == feature.Data).FirstOrDefault();
                    if (dictionary != null)
                    {
                        var grupaRabatowa = wc.GrupyRabatowe.Where(r => r.GUID == dictionary.Guid).FirstOrDefault();
                        if (grupaRabatowa != null)
                        {
                            var towarGrupa = wc.TowarGrupyRabatowe.Where(tgr => tgr.TowarID == produkt.ID && tgr.GrupaRabatowaID == grupaRabatowa.ID).FirstOrDefault();
                            if (towarGrupa == null)
                            {
                                towarGrupa = new Enova.Business.Old.DB.Web.TowarGrupaRabatowa()
                                {
                                    Towar = produkt,
                                    GrupaRabatowa = grupaRabatowa
                                };
                                wc.AddToTowarGrupyRabatowe(towarGrupa);
                            }
                        }
                    }
                }
            }

            wc.OptimisticSaveChanges();
            wc.Refresh(RefreshMode.StoreWins, produkt);
            return produkt;
        }

        public decimal? CenaNettoOpk
        {
            get
            {
                if (Cena != null)
                {
                    int mnoznik = this.JednostkaMiary == null ? 1 : (int)this.JednostkaMiary.Mnoznik;
                    return decimal.Round(Cena.Value * mnoznik, 2);
                }
                return null;
            }
        }

        public string CenaNettoOpkStr
        {
            get
            {
                if (CenaNettoOpk != null)
                {
                    return CenaNettoOpk.ToString() + " zł";
                }
                return null;
            }
        }

        public IQueryable<Feature> GetFeatures(EnovaContext ec)
        {
            return (IQueryable<Feature>)(
                from f in ec.Features
                join t in ec.Towary on
                new { Parent = f.Parent, ParentType = f.ParentType } equals
                new { Parent = t.ID, ParentType = "Towary" }
                where f.Lp == 0 && t.Guid == this.EnovaGuid.Value
                select f);

        }

        public Feature GetFeature(EnovaContext ec, string featureName)
        {
            if (ec != null && featureName != null)
                return this.GetFeatures(ec).Where(f => f.Name == featureName).FirstOrDefault();
            return null;
        }

        public Feature SetFeature(EnovaContext ec, string featureName, string data, string dataKey = null, int lp=0)
        {
            var t = this.GetTowarEnova(ec);
            if (t != null)
                return t.SetFeature(ec, featureName, data, dataKey, lp);
            return null;
        }

        public bool GetRozdzielProduktNaZamowieniu(EnovaContext ec)
        {
            var feature = GetFeature(ec, "ROZDZIEL NA ZAMÓWIENIU");
            if (feature != null)
                return feature.Data.Trim() == "1" ? true : false;
            return false;
        }

        public Enova.Business.Old.DB.Towar GetTowarEnova(EnovaContext ec)
        {
            return ec.Towary.Where(t => t.Guid == this.EnovaGuid).FirstOrDefault();
        }

        public bool GetOgraniczenieSprzedazyWlaczone(EnovaContext ec)
        {
            if (ec != null && this.EnovaGuid != null)
            {
                /*
                var feature = (from f in ec.Features
                               join t in ec.Towary on
                               new { Parent = f.Parent, ParentType = f.ParentType } equals
                               new { Parent = t.ID, ParentType = "Towary" }
                               where f.Lp == 0 && f.Name == "OGRANICZENIE SPRZEDAŻY" && t.Guid == this.EnovaGuid
                               select f).FirstOrDefault();
                 */

                var feature = this.GetFeature(ec, "OGRANICZENIE SPRZEDAŻY");

                if (feature != null && feature.Data.Trim() == "1")
                    return true;

            }
            return false;
        }

        public int GetOgraniczenieSprzedazyStan(EnovaContext ec)
        {
            if (ec != null && this.EnovaGuid != null)
            {
                var feature = this.GetFeature(ec, "STAN MAGAZYNU");
                int i = 0;
                if (feature != null && int.TryParse(feature.Data, out i))
                    return i;
            }
            return 0;
        }

        public void SetOgraniczenieSprzedazyStan(EnovaContext ec, int stanMagazynu)
        {
            if (ec != null && this.EnovaGuid!= null)
            {
                var feature = this.GetFeature(ec, "STAN MAGAZYNU");
                if (feature != null)
                {
                    feature.Data = stanMagazynu.ToString();
                    ec.SaveChanges();
                }
            }
        }

        public void UstawDostepnosc(WebContext lc, bool dostepnosc, bool saveChanges = true)
        {
            if (lc != null && this.EnovaGuid != null)
            {
                foreach (var towar in lc.Produkty.Where(p => (p.Usuniety == null || p.Usuniety == false) && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && p.EnovaGuid == this.EnovaGuid.Value).ToList())
                {
                    towar.Dostepny = dostepnosc;
                }
                if (saveChanges)
                    lc.SaveChanges();
            }
        }

        /*
        partial void OnDostepnyChanged()
        {
            if (this.Synchronizacja == (int)RowSynchronize.Synchronized || this.Synchronizacja == (int)RowSynchronize.NotsynchronizedEdit)
            {
                this.Gotowy = true;
            }
        }
         */

        public int GetEnovaKolejnosc(EnovaContext ec)
        {
            if (ec != null && this.EnovaGuid != null)
            {
            }
            return 1000;
        }

        public bool SaveChanges()
        {
            RowSynchronizeOld synch = (RowSynchronizeOld)this.Synchronizacja;
            this.Synchronizacja = (int)RowSynchronizeOld.Notsaved;

            if (this.EntityState == EntityState.Detached)
                Enova.Business.Old.Core.ContextManager.WebContext.AddToProdukty(this);

            if (this.EntityState == EntityState.Added)
            {
                synch = RowSynchronizeOld.NotsynchronizedNew;
            }
            else if (synch == RowSynchronizeOld.Synchronized)
            {
                this.Stamp = DateTime.Now;
                synch = RowSynchronizeOld.NotsynchronizedEdit;
            }

            Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();

            Enova.Business.Old.Core.ContextManager.WebContext.Refresh(RefreshMode.StoreWins, this);

            foreach (var z in this.Zdjecia)
            {
                if (!string.IsNullOrEmpty(z.FileName))
                {
                    if (File.Exists(z.FileName))
                    {
                        File.Move(z.FileName, @"Z:\AbakSoft\EnovaTools\img\p\" + this.ID.ToString() + "-" + z.ID.ToString() + ".jpg");
                        z.FileName = null;
                    }
                    if (z.Synchronizacja == (int)RowSynchronizeOld.Synchronized)
                        z.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                }
            }

            this.Synchronizacja = (byte)synch;
            Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
            Enova.Business.Old.Core.ContextManager.WebContext.Refresh(RefreshMode.StoreWins, this);

            return true;
        }

        public bool UndoChanges()
        {
            var dc = Enova.Business.Old.Core.ContextManager.WebContext;
            if (this.EntityState == EntityState.Added)
            {
                foreach (var k in this.KategorieProduktu.ToList())
                {
                    dc.DeleteObject(k);
                }
                foreach (var a in this.AtrybutyProduktu.ToList())
                {
                    foreach (var ka in a.KombinacjeAtrybutu.ToList())
                    {
                        dc.DeleteObject(ka);
                    }
                    foreach (var az in a.ProduktyAtrybutyZdjecia.ToList())
                    {
                        dc.DeleteObject(az);
                    }
                    dc.DeleteObject(a);
                }
                foreach (var z in this.Zdjecia.ToList())
                {
                    if (!string.IsNullOrEmpty(z.FileName) && File.Exists(z.FileName))
                        File.Delete(z.FileName);
                    dc.DeleteObject(z);
                }
                foreach (var gr in this.TowarGrupyRabatowe.ToList())
                    dc.DeleteObject(gr);
            }
            else
            {
                foreach (var ap in this.AtrybutyProduktu.Where(ap => ap.EntityState == EntityState.Added).ToList())
                {
                    foreach (var z in ap.ProduktyAtrybutyZdjecia.ToList())
                        dc.DeleteObject(z);
                    foreach (var k in ap.KombinacjeAtrybutu.ToList())
                        dc.DeleteObject(k);
                    dc.DeleteObject(ap);
                }
                foreach (var ap in this.AtrybutyProduktu.ToList())
                {
                    foreach (var paz in ap.ProduktyAtrybutyZdjecia.ToList())
                        paz.IsDeleted = false;
                }
                this.KategorieProduktu.Load();
                this.AtrybutyProduktu.Load();
                this.Zdjecia.Load();
                this.TowarGrupyRabatowe.Load();
                dc.Refresh(RefreshMode.StoreWins, this);
            }
            return true;
        }

        public bool DeleteRecord()
        {
            if ((this.Synchronizacja == (int)RowSynchronizeOld.NotsynchronizedNew && (this.Gotowy == null || this.Gotowy == false)) || this.Synchronizacja == (int)RowSynchronizeOld.Notsaved)
            {
                var dc = Enova.Business.Old.Core.ContextManager.WebContext;
                foreach (var k in this.KategorieProduktu.ToList())
                {
                    dc.DeleteObject(k);
                }
                const string IMG_PATH = @"Z:\AbakSoft\EnovaTools\img\p\";
                string TMP_PATH = Directory.GetCurrentDirectory() + "\\tmp\\";
                foreach (var z in Zdjecia.ToList())
                {
                    string filePath = IMG_PATH + this.ID.ToString() + "-" + z.ID.ToString() + ".jpg";
                    if (!string.IsNullOrEmpty(z.FileName) && File.Exists(z.FileName))
                        File.Delete(z.FileName);
                    else if (File.Exists(filePath))
                        File.Delete(filePath);
                    dc.DeleteObject(z);
                }

                foreach (var a in this.AtrybutyProduktu.ToList())
                {
                    foreach (var ka in a.KombinacjeAtrybutu.ToList())
                        dc.DeleteObject(ka);
                    dc.DeleteObject(a);
                }
                foreach (var gr in this.TowarGrupyRabatowe.ToList())
                    dc.DeleteObject(gr);
                dc.DeleteObject(this);
                dc.OptimisticSaveChanges();
            }
            else
            {
                this.Stamp = DateTime.Now;
                this.Usuniety = true;
                this.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
            }
            return true;
        }

        partial void OnNazwaChanged()
        {
            if (!string.IsNullOrEmpty(this.Nazwa))
                this.LinkRewrite = Enova.Business.Old.Core.Tools.LinkRewrite(this.Nazwa);
        }

        #region IValidation Implementation

        public bool IsValid
        {
            get
            {
                if (this.KategorieProduktu.Where(k=>k.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).Count() == 0)
                {
                    validationError = "Nie przydzielono kategorii do produktu";
                    return false;
                }

                if (this.Zdjecia.Where(z => z.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).Count() > 0 &&
                    this.Zdjecia.Where(z => z.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && z.Okladka == true).Count() == 0)
                {
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(
                        "Produkt nie posiada wybranego głównego zdjęcia.\r\nCzy napewno chcesz zapisać rekord?", "EnovaTools", System.Windows.Forms.MessageBoxButtons.YesNo,
                         System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                    if (result == System.Windows.Forms.DialogResult.No)
                        return false;
                }

                validationError = null;
                return true;
            }
        }

        private string validationError = null;
        public string ValidationError
        {
            get
            {
                return validationError;
            }
        }

        public object ValidationInfo
        {
            get
            {
                return null;
            }
        }

        #endregion


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

        public string EAN
        {
            get { throw new NotImplementedException(); }
        }

        public object Clone()
        {
            var rec = new Produkt();
            rec.DbContext = this.DbContext;
            rec.AktywnyOld = this.AktywnyOld;
            rec.Cena = this.Cena;
            rec.DataDodania = DateTime.Now;
            rec.DataAktualizacji = DateTime.Now;
            rec.DisableAVList = this.DisableAVList;
            rec.Dostawca = this.Dostawca;
            rec.Dostepny = this.Dostepny;
            rec.EnovaGuid = this.EnovaGuid;
            rec.EnovaStamp = this.EnovaStamp;
            rec.Gotowy = false;
            rec.GUID = Guid.NewGuid();
            rec.Ilosc =1;
            rec.Indexed = false;
            rec.JednostkaMiary = this.JednostkaMiary;
            rec.Kod = this.Kod;
            rec.KodDostawcy = this.KodDostawcy;
            rec.KrotkiOpis = this.KrotkiOpis;
            rec.LinkRewrite = this.LinkRewrite;
            rec.MetaOpis = this.MetaOpis;
            rec.MetaSlowa = this.MetaSlowa;
            rec.MetaTytul = this.MetaTytul;
            rec.Nazwa = this.Nazwa;
            rec.Opis = this.Opis;
            rec.OutOfStock = this.OutOfStock;
            rec.Podprodukt = false;
            rec.ProduktGrupujacy = false;
            rec.PSID = 0;
            rec.RelationStawkaVat = this.RelationStawkaVat;
            rec.Stamp = DateTime.Now;
            rec.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
            rec.TowarEnova = this.TowarEnova;
            rec.Usuniety = this.Usuniety;
            rec.VisibleAV = this.VisibleAV;

            foreach (var kp in this.KategorieProduktu.Where(r=>r.Deleted == false && r.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList())
            {
                var nkp = new KategoriaProdukt();
                nkp.Produkt = rec;
                nkp.Gotowy = false;
                nkp.Guid = Guid.NewGuid();
                
                nkp.KategoriaOld = kp.KategoriaOld;
                nkp.Pozycja = kp.Pozycja;
                nkp.Stamp = DateTime.Now;
                nkp.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
                rec.KategorieProduktu.Add(nkp);
            }

            return rec;
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


        public API.Towary.TypTowaru Typ
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<API.Towary.ElementKompletu> ElementyKompletu
        {
            get { throw new NotImplementedException(); }
        }


        public API.Towary.CenySubTable Ceny
        {
            get { throw new NotImplementedException(); }
        }


        public object GetObjValue(object obj, string name, Type[] types = null, object[] index = null, Type fromType = null)
        {
            throw new NotImplementedException();
        }


        public bool Is<T>()
        {
            throw new NotImplementedException();
        }

        public T As<T>()
        {
            throw new NotImplementedException();
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


        public T FromEnova<T>(string name, Type fromType = null)
        {
            throw new NotImplementedException();
        }


        API.Business.MemoText API.Towary.Towar.Opis
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


        public void Refresh()
        {
            throw new NotImplementedException();
        }


        public API.Towary.ICena OstatniaCenaZakupu
        {
            get { throw new NotImplementedException(); }
        }

    }
}
