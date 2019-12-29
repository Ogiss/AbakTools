using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Towary
{
    public class RabatyGrupowe : IEnumerable<RabatGrupowy>
    {
        #region Fields

        private List<RabatGrupowy> list;
        //private Dictionary<int, Enova.API.Towary.ICenaGrupowa> cenyGrupowe;


        #endregion

        #region Properties

        public API.CRM.Kontrahent Kontrahent { get; set; }
        public API.Business.FeatureDefinition FeatureDefinition { get; set; }

        public IEnumerable<RabatGrupowy> Rows
        {
            get
            {
                if (list == null)
                    initList();
                return list;
            }
        }

        /*
        public Dictionary<int, Enova.API.Towary.ICenaGrupowa> CenyGrupowe
        {
            get
            {
                if (this.cenyGrupowe == null)
                    this.initCenyGrupowe();
                return this.cenyGrupowe;
            }
        }
         */

        #endregion

        #region Methods

        private void initList()
        {
            list = new List<RabatGrupowy>();
            if (FeatureDefinition != null && Kontrahent != null)
            {
                foreach (var item in FeatureDefinition.DictionaryList.OrderBy(r => r.Value))
                {
                    var tm = Kontrahent.Session.GetModule<API.Towary.TowaryModule>();
                    
                    var rg = new RabatGrupowy()
                    {
                        GrupaTowarowa = item,
                        Kontrahent = Kontrahent,
                        //CenaGrupowa = CenyGrupowe.ContainsKey(item.ID) ? CenyGrupowe[item.ID] : null
                        CenaGrupowa = tm.CenyGrupowe[Kontrahent, null, item]
                    };
                    list.Add(rg);
                }
            }
        }

        /*
        private void initCenyGrupowe()
        {
            cenyGrupowe = new Dictionary<int,API.Towary.ICenaGrupowa>();
            if (this.Kontrahent != null)
            {
                foreach (var r in Kontrahent.Session.GetModule<API.Towary.ITowaryModule>().CenyGrupowe)
                {
                    if (r.GrupaTowarowa != null)
                        cenyGrupowe[r.GrupaTowarowa.ID] = r;
                }
            }
        }
         */

        public IEnumerator<RabatGrupowy> GetEnumerator()
        {
            return this.Rows.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
