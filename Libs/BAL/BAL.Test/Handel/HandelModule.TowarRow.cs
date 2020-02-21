using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;

namespace BAL.Test.Handel
{
    public partial class HandelModule
    {
        public class TowarRow : Row
        {
            private string kod;
            public string Kod {
                get { return this.kod; }
                set { this.SetValue(() => { kod = value; }, "Kod"); }
            }

            private string nazwa;
            public string Nazwa {
                get { return this.nazwa; }
                set { this.SetValue(() => { nazwa = value; }, "Nazwa"); }
            }

            private decimal cena;
            public Decimal Cena
            {
                get { return this.cena; }
                set
                {
                    this.SetValue(() => { cena = value; }, "Cena");
                }
            }

            private int? kategoriaID;
            public int? KategoriaID
            {
                get { return this.kategoriaID; }
                set { SetValue(() => { this.kategoriaID = value; }, "KategoriaID"); }
            }

            [ParamControl(typeof(BAL.Forms.Controls.ComboBox))]
            public virtual Kategoria Kategoria { get; set; }
                
        }
    }
}
