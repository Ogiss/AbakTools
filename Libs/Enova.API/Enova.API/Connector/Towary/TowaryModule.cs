using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Enova.API.Connector.Module("Towary", typeof(Enova.API.Connector.Towary.TowaryModule), typeof(Enova.API.Towary.TowaryModule))]

namespace Enova.API.Connector.Towary
{
    internal class TowaryModule : Business.Module, API.Towary.TowaryModule
    {
        #region Fields
        private Towary tableTowary;
        private DefinicjeCen tableDefinicjeCen;
        private CenyGrupowe  tableCenyGrupowe;

        #endregion

        #region Properties

        public API.Towary.Towary Towary
        {
            get
            {
                if (tableTowary == null)
                    tableTowary = new Towary() { EnovaObject = GetEnovaTable("Towary"), module = this };
                return tableTowary;
            }
        }

        public API.Towary.DefinicjeCen DefinicjeCen
        {
            get
            {
                if (tableDefinicjeCen == null)
                    tableDefinicjeCen = new DefinicjeCen() { EnovaObject = GetEnovaTable("DefinicjeCen"), module = this };
                return tableDefinicjeCen;
            }
        }

        public API.Towary.CenyGrupowe CenyGrupowe
        {
            get
            {
                if (tableCenyGrupowe == null)
                    tableCenyGrupowe = new CenyGrupowe() { EnovaObject = GetEnovaTable("CenyGrupowe"), module = this };
                return tableCenyGrupowe;
            }
        }

        #endregion

        #region Methods

        public TowaryModule(Business.Session session)
            : base(session, "Towary")
        { }

        public decimal WyliczRabat(Guid kontrahentGuid, Guid towarGuid)
        {
            var kontrahent = this.Session.GetModule<API.CRM.CRMModule>().Kontrahenci[kontrahentGuid];
            if (kontrahent != null)
            {
                var tm = this.Session.GetModule<API.Towary.TowaryModule>();
                var towar = tm.Towary[towarGuid];
                if (towar != null)
                {
                    var defCeny = tm.DefinicjeCen["Hurtowa"];
                    if (defCeny != null)
                    {
                        var worker = Type.GetType("Soneta.Towary.CenyKontrahentaWorker, Soneta.Handel")
                            .GetConstructor(new Type[0]).Invoke(new object[0]);
                        SetObjValue(worker, "Towar", towar.EnovaObject);
                        SetObjValue(worker, "Kontrahent", kontrahent.EnovaObject);
                        SetObjValue(worker, "DefinicjaCeny", defCeny.EnovaObject);
                        return GetObjValue<decimal>(worker, "Rabat");

                    }
                }
            }
            return 0;
        }

        #endregion

        #region NestedTypes


        #endregion
    }
}
