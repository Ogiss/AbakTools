using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Enova.API.Connector.Module("Core", typeof(Enova.API.Connector.Core.CoreModule), typeof(Enova.API.Core.CoreModule))]

namespace Enova.API.Connector.Core
{
    internal class CoreModule : Business.Module, API.Core.CoreModule
    {
        private DokEwidencja tableDokEwidencja;

        public API.Core.DokEwidencja DokEwidencja
        {
            get
            {
                if (tableDokEwidencja == null)
                    tableDokEwidencja = new DokEwidencja() { EnovaObject = GetEnovaTable("DokEwidencja"), module = this };
                return tableDokEwidencja;
            }
        }

        public CoreModule(Business.Session session) : base(session, "Core") { }
    }
}
