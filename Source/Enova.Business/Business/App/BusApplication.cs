using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.App
{
    public class BusApplication
    {
        #region Fields

        private static BusApplication instance;
        private Database database;
        private Login login;

        #endregion

        #region Properties

        public static BusApplication Instance
        {
            get
            {
                if (BusApplication.instance == null)
                    BusApplication.instance = new BusApplication();
                return BusApplication.instance;
            }
        }

        #endregion

        #region Methods

        public void SetLogin(Login value, Session session)
        {
            this.SetLogin(value, session, false);
        }

        public bool SetLogin(Login value, Session session, bool checkOper)
        {
            this.login = value;
            this.database = login.Database;
            return true;
        }

        #endregion
    }
}
