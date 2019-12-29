using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.Web
{
    public class ZamowieniaView : Enova.Business.Old.Core.TableBase<ZamowienieView>
    {
        public ZamowieniaView(ObjectQuery<ZamowienieView> query)
            : base(query)
        {
        }

        protected override List<ZamowienieView> PostLoadProcess(List<ZamowienieView> list)
        {
            return list.OrderBy(z => z.Kolejnosc).ThenBy(z => z.NaKiedyData).ThenBy(z => z.KolejnoscPora).ToList();
        }
    }
}
