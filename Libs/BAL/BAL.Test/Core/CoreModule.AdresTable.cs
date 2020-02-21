using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Test.Core
{
    public partial class CoreModule
    {
        private Adresy tableAdresy;

        public Adresy Adresy
        {
            get { return this.tableAdresy; }
        }

        public class AdresTable : Table<Adres>
        {
        }
    }
}
