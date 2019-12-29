using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB;

namespace Enova.Business.Old
{
    /*
    public class KontrahenciProxyTable : TableBase<KontrahentRow>
    {
        public KontrahenciProxyTable()
            : base()
        {
            //FilterLocal = true;
            //SortLocal = true;

            RefreshContext = true;

            EnovaContext dc = ContextManager.DataContext;
            BaseQuery = (ObjectQuery<KontrahentRow>)(from k in dc.Kontrahenci
                                                     join a in dc.Adresy on
                                                     new { Host = k.ID, HostType = "Kontrahenci", Typ = 1 } equals
                                                     new { Host = a.Host, HostType = a.HostType, Typ = a.Typ }
                                                     join f in dc.Features on
                                                     new { ParentType = "Kontrahenci", Parent = k.ID, Name = "przedstawiciel" } equals
                                                     new { ParentType = f.ParentType, Parent = f.Parent, Name = f.Name }
                                                     select new KontrahentRow()
                                                     {
                                                         ProxyData = k,
                                                         Przedstawiciel = f.Data,
                                                         Ulica = a.AdresUlica,
                                                         NrDomu = a.AdresNrDomu,
                                                         NrLokalu = a.AdresNrLokalu,
                                                         KodPocztowy = a.AdresKodPocztowy,
                                                         Miejscowosc = a.AdresMiejscowosc,
                                                         AdresTelefon = a.AdresTelefon
                                                     });
        }

        public override int Find(PropertyDescriptor property, object key)
        {
            int len = ((string)key).Length;
            string s = ((string)key).ToLower();
            //int ret = -1;
            for (var i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];
                if (!string.IsNullOrEmpty(row.Kod) && row.Kod.Length >= len && row.Kod.ToLower().StartsWith(s))
                    return i;
            }

            for (var i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];
                if ((!string.IsNullOrEmpty(row.Nazwa) && row.Nazwa.Length >= len && row.Nazwa.Substring(0, len).ToLower() == s) ||
                    (!string.IsNullOrEmpty(row.NIP) && row.NIP.Length >= len && row.NIP.Substring(0, len) == s))
                    return i;
            }
            return -1;
        }

        public override bool SupportFeatures
        {
            get
            {
                return true;
            }
        }

        public override string FeaturesTableName
        {
            get
            {
                return "Kontrahenci";
            }
        }


        public override void ApplyFeatureFilter(FeatureDef featureDef, string value)
        {
            EnovaContext dc = ContextManager.DataContext;
            if (featureDef.IsTree)
            {
                BaseQuery = (ObjectQuery<KontrahentRow>)(from k in dc.Kontrahenci
                                                         join a in dc.Adresy on
                                                         new { Host = k.ID, HostType = "Kontrahenci", Typ = 1 } equals
                                                         new { Host = a.Host, HostType = a.HostType, Typ = a.Typ }
                                                         join fp in dc.Features on
                                                         new { ParentType = "Kontrahenci", Parent = k.ID, Name = "przedstawiciel" } equals
                                                         new { ParentType = fp.ParentType, Parent = fp.Parent, Name = fp.Name }
                                                         join f in dc.Features on
                                                         new { ParentType = "Kontrahenci", Parent = k.ID, Name = featureDef.Name } equals
                                                         new { ParentType = f.ParentType, Parent = f.Parent, Name = f.Name }
                                                         where f.Data.StartsWith(value)
                                                         select new
                                                         {
                                                             ID = k.ID,
                                                             Kontrahent = k,
                                                             Adres = a,
                                                             Przedstawiciel = fp
                                                         } into kontrah
                                                         group kontrah by kontrah.ID into gk
                                                         select new KontrahentRow()
                                                         {
                                                             ProxyData = gk.FirstOrDefault().Kontrahent,
                                                             Przedstawiciel = gk.FirstOrDefault().Przedstawiciel.Data,
                                                             Ulica = gk.FirstOrDefault().Adres.AdresUlica,
                                                             NrDomu = gk.FirstOrDefault().Adres.AdresNrDomu,
                                                             NrLokalu = gk.FirstOrDefault().Adres.AdresNrLokalu,
                                                             KodPocztowy = gk.FirstOrDefault().Adres.AdresKodPocztowy,
                                                             Miejscowosc = gk.FirstOrDefault().Adres.AdresMiejscowosc,
                                                             AdresTelefon = gk.FirstOrDefault().Adres.AdresTelefon
                                                         });

            }
            else
            {
                BaseQuery = (ObjectQuery<KontrahentRow>)(from k in dc.Kontrahenci
                                                         join a in dc.Adresy on
                                                         new { Host = k.ID, HostType = "Kontrahenci", Typ = 1 } equals
                                                         new { Host = a.Host, HostType = a.HostType, Typ = a.Typ }
                                                         join fp in dc.Features on
                                                         new { ParentType = "Kontrahenci", Parent = k.ID, Name = "przedstawiciel" } equals
                                                         new { ParentType = fp.ParentType, Parent = fp.Parent, Name = fp.Name }
                                                         join f in dc.Features on
                                                         new { ParentType = "Kontrahenci", Parent = k.ID, Name = featureDef.Name } equals
                                                         new { ParentType = f.ParentType, Parent = f.Parent, Name = f.Name }
                                                         where f.Data == value
                                                         select new KontrahentRow()
                                                         {
                                                             ProxyData = k,
                                                             Przedstawiciel = fp.Data,
                                                             Ulica = a.AdresUlica,
                                                             NrDomu = a.AdresNrDomu,
                                                             NrLokalu = a.AdresNrLokalu,
                                                             KodPocztowy = a.AdresKodPocztowy,
                                                             Miejscowosc = a.AdresMiejscowosc,
                                                             AdresTelefon = a.AdresTelefon
                                                         });
            }
            base.ApplyFeatureFilter(featureDef, value);
        }

        public override void RemoveFeatureFilter()
        {
            EnovaContext dc = ContextManager.DataContext;
            BaseQuery = (ObjectQuery<KontrahentRow>)(from k in dc.Kontrahenci
                                                     join a in dc.Adresy on
                                                     new { Host = k.ID, HostType = "Kontrahenci", Typ = 1 } equals
                                                     new { Host = a.Host, HostType = a.HostType, Typ = a.Typ }
                                                     join f in dc.Features on
                                                     new { ParentType = "Kontrahenci", Parent = k.ID, Name = "przedstawiciel" } equals
                                                     new { ParentType = f.ParentType, Parent = f.Parent, Name = f.Name }
                                                     select new KontrahentRow()
                                                     {
                                                         ProxyData = k,
                                                         Przedstawiciel = f.Data,
                                                         Ulica = a.AdresUlica,
                                                         NrDomu = a.AdresNrDomu,
                                                         NrLokalu = a.AdresNrLokalu,
                                                         KodPocztowy = a.AdresKodPocztowy,
                                                         Miejscowosc = a.AdresMiejscowosc,
                                                         AdresTelefon = a.AdresTelefon
                                                     });
            base.RemoveFeatureFilter();
        }

        public override Type GetElementType()
        {
            return typeof(Kontrahent);
        }

    }
     */
}
