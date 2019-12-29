using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class RozrachunkiIdx : Business.Table<API.Kasa.RozrachunekIdx>, API.Kasa.RozrachunkiIdx
    {
        public override string TableName
        {
            get
            {
                return "RozrachunkiIdx";
            }
        }


        public IEnumerable<API.Kasa.RozrachunekIdx> Nierozliczone(API.Kasa.IPodmiotKasowy podmiot, API.Types.FromTo okres, DateTime aktualny)
        {

            var view = (IEnumerable)CallMethodFull("Nierozliczone", new Type[]{
                Type.GetType("Soneta.Kasa.IPodmiotKasowy, Soneta.Kasa"),
                Type.GetType("Soneta.Types.FromTo, Soneta.Types"),
                Type.GetType("Soneta.Types.Date, Soneta.Types")
            },
            new object[]{
                podmiot.EnovaObject,
                okres.EnovaObject,
                aktualny.ToEnova()
            });

            //return new Business.EnovaEnumerable<API.Kasa.IRozrachunekIdx>(view);
            var list = new List<API.Kasa.RozrachunekIdx>();
            foreach (var row in view)
                list.Add(new RozrachunekIdx() { EnovaObject = row });
            return list;

        }

        public IEnumerable<API.Kasa.RozrachunekIdx> Rozliczone(API.Kasa.IPodmiotKasowy podmiot, API.Types.FromTo okres, DateTime aktualny)
        {
            var view = (IEnumerable)CallMethodFull("Rozliczone", new Type[]{
                Type.GetType("Soneta.Kasa.IPodmiotKasowy, Soneta.Kasa"),
                Type.GetType("Soneta.Types.FromTo, Soneta.Types"),
                Type.GetType("Soneta.Types.Date, Soneta.Types")
            },
new object[]{
                podmiot.EnovaObject,
                okres.EnovaObject,
                aktualny.ToEnova()
            });

            var list = new List<API.Kasa.RozrachunekIdx>();
            foreach (var row in view)
                list.Add(new RozrachunekIdx() { EnovaObject = row });
            return list;

        }

        public IEnumerable<API.Kasa.RozrachunekIdx> Wszystkie(API.Kasa.IPodmiotKasowy podmiot, API.Types.FromTo okres)
        {
            var view = (IEnumerable)CallMethodFull("Wszystkie", new Type[]{
                Type.GetType("Soneta.Kasa.IPodmiotKasowy, Soneta.Kasa"),
                Type.GetType("Soneta.Types.FromTo, Soneta.Types")
            },
new object[]{
                podmiot.EnovaObject,
                okres.EnovaObject
            });

            var list = new List<API.Kasa.RozrachunekIdx>();
            foreach (var row in view)
                list.Add(new RozrachunekIdx() { EnovaObject = row });
            return list;
        }



        public API.Business.SubTable WgPodmiot(API.Kasa.IPodmiotKasowy podmiot)
        {
            return GetSubTable("WgPodmiot", podmiot);
        }
    }
}
