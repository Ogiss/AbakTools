using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Towary
{
    internal class Towar : Business.GuidedRow, API.Towary.Towar
    {
        public bool Blokada
        {
            get { return (bool)GetValue("Blokada"); }
        }

        public string Kod
        {
            get { return (string)GetValue("Kod"); }
        }

        public string Nazwa
        {
            get { return (string)GetValue("Nazwa"); }
        }

        public string EAN
        {
            get { return (string)GetValue("EAN"); }
        }


        public API.Towary.TypTowaru Typ
        {
            get { return (API.Towary.TypTowaru)(int)GetValue("Typ"); }
        }


        public IEnumerable<API.Towary.ElementKompletu> ElementyKompletu
        {
            get
            {
                return new Business.EnovaEnumerable<API.Towary.ElementKompletu>(GetValue("ElementyKompletu"));
            }
        }

        public API.Towary.CenySubTable Ceny
        {
            get { return new CenySubTable { EnovaObject = GetValue("Ceny") }; }
        }

        public API.Business.MemoText Opis
        {
            get
            {
                return FromEnova<API.Business.MemoText>("Opis");
            }
            set
            {
                ToEnova("Opis", value);
            }
        }

        public API.Towary.ICena OstatniaCenaZakupu
        {
            get
            {
                return FromEnova<API.Towary.ICena>("OstatniaCenaZakupu");
            }
        }


        #region Nested Types

        internal class CenySubTable : Business.SubTable, API.Towary.CenySubTable
        {
            public API.Towary.ICena this[string nazwa]
            {
                get { return (API.Towary.ICena)EnovaHelper.FromEnova(GetValue("Item", new object[] { nazwa })); }
            }

            public API.Towary.ICena this[API.Towary.DefinicjaCeny definicja]
            {
                get { return (API.Towary.ICena)EnovaHelper.FromEnova(GetValue("Item", new object[] { EnovaHelper.ToEnova(definicja) })); }
            }
        }

        #endregion



    }
}
