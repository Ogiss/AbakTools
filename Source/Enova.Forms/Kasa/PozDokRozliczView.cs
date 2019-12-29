using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Kasa
{
    //public class PozDokRozliczView : GridViewWithEnovaApi<Enova.API.Kasa.IPozycjaDokRozlicz>
    public class PozDokRozliczView : GridViewWithEnovaApi<Enova.API.Kasa.PozycjaDokRozlicz>
    {
        #region Fields

        //private Enova.API.Kasa.DokRozlicz dokument;
        private Enova.API.Kasa.DokRozliczBase dokument;
        private string key;

        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return key + (SelectionMode ? "Select" : "");
            }
        }

        public override bool ReadOnly
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        //public PozDokRozliczView(string key, Enova.API.Kasa.DokRozlicz dokument)
        public PozDokRozliczView(string key, Enova.API.Kasa.DokRozliczBase dokument)
        {
            this.key = key;
            this.dokument = dokument;
        }

        //protected override API.Business.Table<API.Kasa.IPozycjaDokRozlicz> CreateTable(API.Business.Session session)
        protected override API.Business.Table<API.Kasa.PozycjaDokRozlicz> CreateTable(API.Business.Session session)
        {
            throw new NotImplementedException();
        }

        protected override System.Collections.IList GetRows()
        {
            if (dokument != null)
                return dokument.Pozycje.Cast<API.Kasa.PozycjaDokRozlicz>().ToList();
            return base.GetRows();
        }

        #endregion
    }
}
