using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Types;

namespace Enova.Business.Old
{
    public class KontrahenciPrzeterRozrach : Enova.Business.Old.Core.TableBase<RozrachunekRow>
    {
        DateTime? dataAktualności = null;

        private ObjectQuery<RozrachunekRow> query = null;

        public KontrahenciPrzeterRozrach(DateTime? data)
            : base()
        {
            FilterLocal = true;
            SortLocal = true;
            InitQuery(data);

        }

        public void InitQuery(DateTime? data)
        {
            dataAktualności = data;
            DateTime min = new DateTime(1900, 1, 1);
            DateTime max = new DateTime(2099, 12, 31, 0, 0, 0);
            DateTime dataRozlicz = DateTime.Now.Date.AddDays(1);

            base.DataContext = Enova.Business.Old.Core.ContextManager.DataContext;

            query = (ObjectQuery<RozrachunekRow>)(from r in DataContext.RozrachunkiIdx
                                                  join k in DataContext.Kontrahenci on r.Podmiot.Value equals k.ID
                                                  join pk in DataContext.Features on
                                                  new { ParentType = "Kontrahenci", Parent = k.ID, Name = "przedstawiciel" } equals
                                                  new { ParentType = pk.ParentType, Parent = pk.Parent, Name = pk.Name }
                                                  join ww in DataContext.Features on
                                                  new { ParentType = "Kontrahenci", Parent = k.ID, Name = "WINDYKACJA" } equals
                                                  new { ParentType = ww.ParentType, Parent = ww.Parent, Name = ww.Name } into wind
                                                  join zw in DataContext.Features on
                                                  new { ParentType = "Kontrahenci", Parent = k.ID, Name = "ZAKONCZENIE_WINDYKACJI" } equals
                                                  new { ParentType = zw.ParentType, Parent = zw.Parent, Name = zw.Name } into zwind
                                                  where r.Data <= data && r.PodmiotType == "Kontrahenci" && (r.DataRozliczenia == max || wind.FirstOrDefault() != null || k.BlokadaSprzedazy.Value)
                                                  && pk.Data != "--"
                                                  select new
                                                  {
                                                      IDKontrahenta = r.Podmiot,
                                                      KodKontrahenta = k.Kod,
                                                      NazwaKontrahenta = k.NazwaStr,
                                                      PrzdstawicielKontrahent = pk.Data,
                                                      Blokada = k.Blokada,
                                                      BlokadaSprzedaży = k.BlokadaSprzedazy,
                                                      WartośćBrutto = (r.Typ == 10 || r.Typ == 21) ? r.KwotaValue : r.KwotaValue * -1,
                                                      Zapłacono = (r.Typ == 10 || r.Typ == 21) ? r.KwotaRozliczonaValue : r.KwotaRozliczonaValue * -1,
                                                      NumerDokumentu = r.Numer,
                                                      DataDokumentu = r.Data,
                                                      Termin = r.Termin,
                                                      WindykacjaStr = wind.FirstOrDefault().Data,
                                                      ZakończenieWindykacji = zwind.FirstOrDefault().Data
                                                  } into rozrachunki
                                                  group rozrachunki by rozrachunki.IDKontrahenta into g
                                                  select new RozrachunekRow()
                                                  {
                                                      IDKontrahenta = g.Key,
                                                      KodKontrahenta = g.FirstOrDefault().KodKontrahenta,
                                                      NazwaKontrahenta = g.FirstOrDefault().NazwaKontrahenta,
                                                      Blokada = g.FirstOrDefault().Blokada,
                                                      BlokadaSprzedaży = g.FirstOrDefault().BlokadaSprzedaży,
                                                      PrzedstawicielKontrahent = g.FirstOrDefault().PrzdstawicielKontrahent,
                                                      DataDokumentu = g.Min(r => r.DataDokumentu),
                                                      Termin = g.Min(r => r.Termin),
                                                      WartośćBrutto = g.Sum(r => r.WartośćBrutto),
                                                      Zapłacono = g.Sum(r => r.Zapłacono),
                                                      WindykacjaStr = g.FirstOrDefault().WindykacjaStr,
                                                      ZakończenieWindykacji = g.FirstOrDefault().ZakończenieWindykacji
                                                  });


            base.BaseQuery = query;
        }

        public new Enova.Business.Old.DB.EnovaContext DataContext
        {
            get { return (Enova.Business.Old.DB.EnovaContext)base.DataContext; }
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

        public override void ApplyFeatureFilter(Enova.Business.Old.DB.FeatureDef featureDef, string value)
        {
            Filter = "PrzedstawicielKontrahent == '" + value + "'";
        }

        public override void RemoveFeatureFilter()
        {
            Filter = null;
        }

        private decimal? wartoscNierozliczonychZobowiazan(int idKontrahenta)
        {
            try
            {
                DateTime dataRozlicz = DateTime.Now.Date.AddDays(1);
                var kwota = (from r in DataContext.RozrachunkiIdx
                                 where r.Data > dataAktualności && r.DataRozliczenia >= dataRozlicz && r.PodmiotType == "Kontrahenci" && ((r.Typ == 11 && r.Zwrot == true) || (r.Typ == 20 && r.Zwrot == false))  && r.Podmiot == idKontrahenta
                                 select r).Sum(r =>  r.KwotaRozliczonaValue - r.KwotaValue);
                return kwota;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return 0;
            }
                             
        }

        protected override List<RozrachunekRow> PostLoadProcess(List<RozrachunekRow> list)
        {
            foreach (var r in list)
            {
                if (r.IDKontrahenta != null)
                    r.WartoscNierozliczonychZobowiazan = wartoscNierozliczonychZobowiazan(r.IDKontrahenta.Value);
            }

            return list;
        }

    }
}
