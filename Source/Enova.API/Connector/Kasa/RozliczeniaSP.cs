using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class RozliczeniaSP : Business.GuidedTable<API.Kasa.RozliczenieSP>, API.Kasa.RozliczeniaSP
    {
        public override string TableName
        {
            get
            {
                return "RozliczeniaSP";
            }
        }

        public IEnumerable<API.Kasa.RozliczenieSP> this[API.Kasa.IPodmiotKasowy podmiot]
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<API.Kasa.RozliczenieSP> this[API.Kasa.IPodmiotKasowy podmiot, API.Types.FromTo okres]
        {
            get
            {
                var view = this.CreateView();
                view.Condition &= new API.Business.FieldCondition.Equal("Podmiot", podmiot);
                view.Condition &= new API.Business.FieldCondition.GreaterEqual("Data", okres.From.EnovaObject);
                view.Condition &= new API.Business.FieldCondition.LessEqual("Data", okres.To.EnovaObject);
                return new Business.EnovaEnumerable<API.Kasa.RozliczenieSP>(view);
                

            }
        }


        public API.Business.SubTable WgZaplata(API.Kasa.IRozliczalny zaplata)
        {
            return GetSubTable("WgZaplata", zaplata);
        }
    }
}
