using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Configuration
{
    public class UnknownDatabaseException : BAL.Business.BusException
    {
        private string databaseName;

        public string DatabaseName
        {
            get { return databaseName; }
        }

        public UnknownDatabaseException(string databaseName) :
            base("Unknown database {0}", databaseName)
        {
            this.databaseName = databaseName;
        }
    }
}
