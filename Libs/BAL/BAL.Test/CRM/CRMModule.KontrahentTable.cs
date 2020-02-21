using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Test.CRM
{
    public partial class CRMModule 
    {
        private Kontrahenci tableKontrahenci;

        public Kontrahenci Kontrahenci
        {
            get { return this.tableKontrahenci; }
        }

        public class KontrahentTable : Table<Kontrahent>
        {
        }
    }
}
