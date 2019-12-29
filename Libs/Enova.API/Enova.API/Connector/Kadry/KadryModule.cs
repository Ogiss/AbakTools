using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Enova.API.Connector.Module("Kadry",typeof(Enova.API.Connector.Kadry.KadryModule), typeof(Enova.API.Kadry.KadryModule))]

namespace Enova.API.Connector.Kadry
{
    internal class KadryModule : Business.Module, API.Kadry.KadryModule
    {
        private Pracownicy tablePracownicy;

        public API.Kadry.Pracownicy Pracownicy
        {
            get
            {
                if (tablePracownicy == null)
                    tablePracownicy = new Pracownicy() { EnovaObject = GetEnovaTable("Pracownicy"), module = this };
                return tablePracownicy;
            }
        }

        public KadryModule(Business.Session session) : base(session, "Kadry") { }
    }
}
