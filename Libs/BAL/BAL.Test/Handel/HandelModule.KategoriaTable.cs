using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Test.Handel
{
    public partial class HandelModule
    {
        private Kategorie tableKategorie;

        public Kategorie Kategorie
        {
            get { return this.tableKategorie; }
        }

        public class KategoriaTable : Table<Kategoria>
        {
        }
    }
}
