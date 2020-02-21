using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Test.Handel
{
    public partial class HandelModule
    {
        private Dokumenty tableDokumenty;

        public Dokumenty Dokumenty
        {
            get { return this.tableDokumenty; }
        }

        public partial class DokumentTable : Table<Dokument>
        {
            private WgKontrahentKey keyWgKontrahent;

            public WgKontrahentKey WgKontrahent
            {
                get { return this.keyWgKontrahent; }
            }

            public DokumentTable()
            {
                this.keyWgKontrahent = new WgKontrahentKey(this);
            }

            public override View CreateView()
            {
                return this.CreateView<DokumentyView>();
            }

            public class WgKontrahentKey : Key<Dokument>
            {
                public Dokumenty this[CRM.Kontrahent kontrahent]
                {
                    get
                    {
                        return CreateSubtable<Dokumenty>(kontrahent);
                    }
                }

                public WgKontrahentKey(Table<Dokument> table)
                    : base(table)
                {
                    this.InitField(r => r.Kontrahent);
                }
            }
        }
    }
}
