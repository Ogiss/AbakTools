using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public class ReklamacjeTable : Enova.Business.Old.ITable
    {

        public IQueryable BaseQuery
        {
            get { throw new NotImplementedException(); }
        }

        public string TableName
        {
            get { return "Reklamacje"; }
        }

        public void Adding(Module module)
        {
            throw new NotImplementedException();
        }
    }
}
