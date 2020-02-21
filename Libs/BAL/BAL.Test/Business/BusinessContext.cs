using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Test.Business
{
    public class BusinessContext : DBContextBase
    {
        public BusinessContext(BAL.Business.App.IDatabase database) : base(database) { }
    }
}
