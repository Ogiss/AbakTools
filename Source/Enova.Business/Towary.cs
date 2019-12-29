using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Enova.Business.Old.DB;

namespace Enova.Business.Old
{
    public class Towary : Enova.Business.Old.Core.TableBase<TowarRow>
    {
        private ObjectQuery<TowarRow> query;

        public Towary(ObjectQuery<Towar> query) : base(query) { }
        public Towary() : base() 
        {
            EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
            DataContext = dc;
            query = (ObjectQuery<TowarRow>)(from t in dc.Towary.Where(r=>r.Blokada == null || r.Blokada == false)
                                                /*from z in t.Zasoby
                                                where z.Okres == 1 && z.Magazyny.ID == 1*/
                                                /*where t.Blokada == false*/
                                                select new
                                                {
                                                    ID = t.ID,
                                                    MagazynID = 1/*z.Magazyny.ID*/,
                                                    Kod = t.Kod,
                                                    Blokada = t.Blokada == null ? false : t.Blokada.Value,
                                                    Nazwa = t.Nazwa,
                                                    CenaPodstawowaNetto = t.Ceny.Where(c => c.Definicja.ID == 1).FirstOrDefault().NettoValue,
                                                    CenaPodstawowaBrutto = t.Ceny.Where(c => c.Definicja.ID == 1).FirstOrDefault().BruttoValue,
                                                    CenaHurtowaNetto = t.Ceny.Where(c => c.Definicja.ID == 2).FirstOrDefault().NettoValue,
                                                    CenaHurtowaBrutto = t.Ceny.Where(c => c.Definicja.ID == 2).FirstOrDefault().BruttoValue,
                                                    Ilość = /*z.IloscValue*/ t.Zasoby.Where(z=>z.Okres == 1 && z.Magazyn.ID == 1).Sum(z=>z.IloscValue)
                                                } into zasoby
                                                group zasoby by new { zasoby.ID, zasoby.MagazynID } into g
                                                select new TowarRow()
                                                {
                                                    ID = g.Key.ID,
                                                    blokada = g.FirstOrDefault().Blokada,
                                                    MagazynID = g.Key.MagazynID,
                                                    Kod = g.FirstOrDefault().Kod,
                                                    Nazwa = g.FirstOrDefault().Nazwa,
                                                    CenaPodstawowaNetto = g.FirstOrDefault().CenaPodstawowaNetto,
                                                    CenaPodstawowaBrutto = g.FirstOrDefault().CenaPodstawowaBrutto,
                                                    CenaHurtowaNetto = g.FirstOrDefault().CenaHurtowaNetto,
                                                    CenaHurtowaBrutto = g.FirstOrDefault().CenaHurtowaBrutto,
                                                    Ilość = g.Sum(za => za.Ilość)
                                                });

            BaseQuery = query;
        }

        public static implicit operator Towary(ObjectQuery<Towar> query)
        {
            return new Towary(query);
        }

        public override string FeaturesTableName
        {
            get
            {
                return "Towary";
            }
        }

        public override bool SupportFeatures
        {
            get
            {
                return true;
            }
        }

        public override void ApplyFeatureFilter(FeatureDef featureDef, string value)
        {
            if (DataContext != null)
            {
                Enova.Business.Old.DB.EnovaContext dc = DataContext as Enova.Business.Old.DB.EnovaContext;
                string name = featureDef.Name;

                if (featureDef.IsTree)
                {
                    BaseQuery = (ObjectQuery<TowarRow>)(from t in query
                                                        from f in dc.Features
                                                        where t.ID == f.Parent && f.ParentType == "Towary" && f.Name == name && f.Data.StartsWith(value)
                                                        group t by t.ID into ut
                                                        select ut.FirstOrDefault());
                }
                else
                {
                    BaseQuery = (ObjectQuery<TowarRow>)(from t in query
                                                        from f in dc.Features
                                                        where t.ID == f.Parent && f.ParentType == "Towary" && f.Name == name && f.Data == value
                                                        select t);
                }
                                                          
            }
            base.ApplyFeatureFilter(featureDef,value);
        }

        public override void RemoveFeatureFilter()
        {
            BaseQuery = query;
            base.RemoveFeatureFilter();
        }

        public override int Find(System.ComponentModel.PropertyDescriptor property, object key)
        {
            int len = ((string)key).Length;
            string s = ((string)key).ToLower();
            for (var i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];
                if ((!string.IsNullOrEmpty(row.Kod) && row.Kod.Length >= len && row.Kod.Substring(0, len).ToLower() == s) ||
                    (!string.IsNullOrEmpty(row.Nazwa) && row.Nazwa.Length >= len && row.Nazwa.Substring(0, len).ToLower() == s))
                    return i;
            }
            return -1;
        }

    }
}
