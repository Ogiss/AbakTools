using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Kasa
{
    public class RozrachunkiView : GridViewWithEnovaApi<API.Kasa.RozrachunekIdx>
    {
        #region Fields


        private API.CRM.Kontrahent kontrahent;

        #endregion

        #region Methods

        public RozrachunkiView(string key, API.CRM.Kontrahent kontrahent)
            : base(key)
        {
            this.kontrahent = kontrahent;
        }

        public RozrachunkiView() : this("RozrachunkiEnovaView", null) { }

        protected override API.Business.Table<API.Kasa.RozrachunekIdx> CreateTable(API.Business.Session session)
        {
            return session.GetModule<API.Kasa.KasaModule>().RozrachunkiIdx;
        }

        protected override System.Collections.IList GetRows()
        {
            using (var s = Service.CreateSession())
            {
                    /*
                    var fromTo = new API.Types.FromToOld()
                    {
                        From = DateTime.MinValue,
                        To = DateTime.Now.Date
                    };
                     */
                var fromTo = API.Types.FromTo.Create(DateTime.MinValue, DateTime.MaxValue);

                var table = s.GetModule<Enova.API.Kasa.KasaModule>().RozrachunkiIdx;
                var list = table.Nierozliczone(kontrahent, fromTo, DateTime.Now.Date).ToList();
                /*
                if (this.IsSorted && this.SortProperty != null)
                    list.Sort(new DokHandlowyComparer(this.SortProperty, this.SortDirection));
                 */
                return (System.Collections.IList)list;
            }

        }

        #endregion
    }
}
