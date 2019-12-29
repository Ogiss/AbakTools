using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Enova.API.Connector.Module("CRM", typeof(Enova.API.Connector.CRM.CRMModule), typeof(Enova.API.CRM.CRMModule))]

namespace Enova.API.Connector.CRM
{
    internal class CRMModule : Business.Module, API.CRM.CRMModule
    {
        #region Fields

        private Kontrahenci tableKontrahenci;
        private Banki tableBanki;
        private UrzedySkarbowe tableUrzedySkarbowe;
        private ZUSY tableZUSY;


        #endregion

        #region Properties

        public API.CRM.Kontrahenci Kontrahenci
        {
            get
            {
                if (tableKontrahenci == null)
                    tableKontrahenci = new Kontrahenci() { EnovaObject = GetEnovaTable("Kontrahenci"), module = this };
                return tableKontrahenci;
            }
        }

        public API.CRM.Banki Banki
        {
            get
            {
                if (tableBanki == null)
                    tableBanki = new Banki() { EnovaObject = GetEnovaTable("Banki"), module = this };
                return tableBanki;
            }
        }

        public API.CRM.UrzedySkarbowe UrzedySkarbowe
        {
            get
            {
                if (tableUrzedySkarbowe == null)
                    tableUrzedySkarbowe = new UrzedySkarbowe() { EnovaObject = GetEnovaTable("UrzedySkarbowe"), module = this };
                return tableUrzedySkarbowe;
            }
        }

        public API.CRM.ZUSY ZUSY
        {
            get
            {
                if (tableZUSY == null)
                    tableZUSY = new ZUSY() { EnovaObject = GetEnovaTable("ZUSY"), module = this };
                return tableZUSY;
            }
        }

        #endregion

        #region Methods

        public CRMModule(Business.Session session) : base(session, "CRM") { }

        #endregion
    }
}
