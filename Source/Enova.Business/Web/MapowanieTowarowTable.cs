using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;


namespace Enova.Business.Old.Web
{
    public class MapowanieTowarowTable : Core.TableBase<Types.MapowanieTowaruRow>
    {
        protected override List<Types.MapowanieTowaruRow> GetRows()
        {
            var dc = Core.ContextManager.WebContext;

            return (from gm in dc.GuidMaps.Where(r => r.Tabela == "Towary")
                    join st in dc.Produkty.Where(r => r.TowarEnova == true) on gm.Zrodlo equals st.EnovaGuid
                    join dt in dc.Produkty.Where(r => r.TowarEnova == true) on gm.Cel equals dt.EnovaGuid
                    orderby st.Kod
                    select new Enova.Business.Old.Types.MapowanieTowaruRow()
                    {
                        Src = st,
                        Dst = dt
                    }).ToList();

        }
    }
}
