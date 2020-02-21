using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Test.Handel
{
    public partial class HandelModule
    {
        public class KategoriaRow : Row
        {
            private string nazwa;

            public string Nazwa
            {
                get { return this.nazwa; }
                set { SetValue(() => { this.nazwa = value; }, "Nazwa"); }
            }

        }
    }
}
