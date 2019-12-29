using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Enova.API.Connector.Module("Magazyny", typeof(Enova.API.Connector.Magazyny.MagazynyModule), typeof(Enova.API.Magazyny.MagazynyModule))]

namespace Enova.API.Connector.Magazyny
{
    internal class MagazynyModule : Business.Module, API.Magazyny.MagazynyModule
    {
        #region Fields

        private Magazyny tableMagazyny;
        private Obroty tableObroty;
        private OkresyMag tableOkresyMag;

        #endregion

        public API.Magazyny.Magazyny Magazyny
        {
            get
            {
                if (tableMagazyny == null)
                    tableMagazyny = new Magazyny() { EnovaObject = GetEnovaTable("Magazyny"), module = this };
                return tableMagazyny;
            }
        }

        public API.Magazyny.Obroty Obroty
        {
            get
            {
                if (tableObroty == null)
                    tableObroty = new Obroty() { EnovaObject = GetEnovaTable("Obroty"), module = this };
                return tableObroty;
            }
        }

        public API.Magazyny.OkresyMag OkresyMag
        {
            get
            {
                if (tableOkresyMag == null)
                    tableOkresyMag = new OkresyMag() { EnovaObject = GetEnovaTable("OkresyMag"), module = this };
                return tableOkresyMag;
            }
        }

        #region Methods

        public MagazynyModule(Business.Session session) : base(session, "Magazyny") { }

        #endregion


    }
}
