using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Test.Handel
{
    public partial class HandelModule
    {
        private Towary tableTowary;

        public Towary Towary
        {
            get
            {
                return this.tableTowary;
            }
        }

        [TableName("Towary", typeof(Towar))]
        public class TowarTable : Table<Towar>
        {

            private WgKategoriaKey keyWgKategoria;

            public WgKategoriaKey WgKategoria
            {
                get { return this.keyWgKategoria; }
            }


            public TowarTable()
            {
                this.keyWgKategoria = new WgKategoriaKey(this);
            }

            #region Nested Types

            public class WgKategoriaKey : Key<Towar>
            {
                public Towary this[Kategoria kategoria]
                {
                    get
                    {
                        return this.CreateSubtable<Towary>(kategoria);
                    }
                }

                public WgKategoriaKey(Table<Towar> table)
                    : base(table)
                {
                    this.InitField(r => r.Kategoria);
                }
            }

            #endregion
        }
    }
}
