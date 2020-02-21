using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

[assembly: AppModule("TestModule", typeof(BAL.Test.Handel.HandelModule), typeof(BAL.Test.Handel.HandelContext))]

namespace BAL.Test.Handel
{
    public partial class HandelModule : Module
    {
        #region Methods

        public HandelModule(Session session)
            : base(session, "TestModule")
        {
            this.tableTowary = new Towary();
            this.tableKategorie = new Kategorie();
            this.tableDokumenty = new Dokumenty();
            this.AddTable(this.tableTowary);
            this.AddTable(this.tableKategorie);
            this.AddTable(this.tableDokumenty);
        }

        public static HandelModule GetInstance(ISessionable session)
        {
            if (session != null && session.Session != null)
                return (HandelModule)session.Session.Modules[typeof(HandelModule)];
            return null;
        }

        #endregion
    }
}
