using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Handel
{
    internal class DefDokHandlowych : Business.GuidedTable<API.Handel.DefDokHandlowego>, API.Handel.DefDokHandlowych
    {
        #region Fields

        private API.Handel.DefDokHandlowego fakturaSprzedazy;
        private API.Handel.DefDokHandlowego fakturaProforma;
        private API.Handel.DefDokHandlowego paragon;
        private API.Handel.DefDokHandlowego korektaSprzedazy;
        private API.Handel.DefDokHandlowego zamowienieOdbiorcy;
        private API.Handel.DefDokHandlowego przesuniecie;
        private API.Handel.DefDokHandlowego korektaWZ;
        private API.Handel.DefDokHandlowego wydanieMagazynowe;

        #endregion

        #region Properties

        public override string TableName
        {
            get
            {
                return "DefDokHandlowych";
            }
        }

        public API.Handel.DefDokHandlowego FakturaSprzedazy
        {
            get
            {
                if (this.fakturaSprzedazy == null)
                    this.fakturaSprzedazy = new DefDokHandlowego() { EnovaObject = GetValue("FakturaSprzedaży") };
                return this.fakturaSprzedazy;
            }
        }

        public API.Handel.DefDokHandlowego FakturaProforma
        {
            get
            {
                if (this.fakturaProforma == null)
                    this.fakturaProforma = new DefDokHandlowego() { EnovaObject = GetValue("FakturaProforma") };
                return this.fakturaProforma;
            }
        }

        public API.Handel.DefDokHandlowego Paragon
        {
            get
            {
                if (this.paragon == null)
                    this.paragon = new DefDokHandlowego() { EnovaObject = GetValue("Paragon") };
                return this.paragon;
            }
        }

        public API.Handel.DefDokHandlowego KorektaSprzedazy
        {
            get
            {
                if (this.korektaSprzedazy == null)
                    this.korektaSprzedazy = new DefDokHandlowego() { EnovaObject = GetValue("KorektaSprzedaży") };
                return this.korektaSprzedazy;
            }
        }

        public API.Handel.DefDokHandlowego ZamowienieOdbiorcy
        {
            get
            {
                if (zamowienieOdbiorcy == null)
                    this.zamowienieOdbiorcy = new DefDokHandlowego() { EnovaObject = GetValue("ZamówienieOdbiorcy") };
                return zamowienieOdbiorcy;
            }
        }

        public API.Handel.DefDokHandlowego Przesuniecie
        {
            get
            {
                if (przesuniecie == null)
                    this.przesuniecie = new DefDokHandlowego() { EnovaObject = GetValue("Przesunięcie") };
                return przesuniecie;
            }
        }

        public API.Handel.DefDokHandlowego KorektaWZ
        {
            get
            {
                if (korektaWZ == null)
                    korektaWZ = new DefDokHandlowego() { EnovaObject = GetValue("KorektaWZ") };
                return korektaWZ;
            }
        }

        public API.Handel.DefDokHandlowego WydanieMagazynowe
        {
            get
            {
                if (wydanieMagazynowe == null)
                    wydanieMagazynowe = new DefDokHandlowego() { EnovaObject = GetValue("WydanieMagazynowe") };
                return wydanieMagazynowe;
            }
        }


        public IEnumerable<API.Handel.DefDokHandlowego> this[API.Handel.KategoriaHandlowa kategoria]
        {
            get
            {
                return new WgKategoriiTable(this, kategoria);
            }
        }
        

        #endregion

        #region Methods

        #endregion

        #region Nested Types

        public class WgKategoriiTable : MarshalByRefObject, IEnumerable<API.Handel.DefDokHandlowego>
        {
            private DefDokHandlowych table;
            //private Soneta.Handel.KategoriaHandlowa kategoria;
            private API.Handel.KategoriaHandlowa kategoria;

            public WgKategoriiTable(DefDokHandlowych table, API.Handel.KategoriaHandlowa kategoria)
            {
                this.table = table;
                //this.kategoria = (Soneta.Handel.KategoriaHandlowa)(int)kategoria;
                this.kategoria = kategoria;
            }

            public IEnumerator<API.Handel.DefDokHandlowego> GetEnumerator()
            {
                var kt = Type.GetType("Soneta.Handel.KategoriaHandlowa, Soneta.Handel");
                var k = Enum.ToObject(kt, (int)this.kategoria);
                var list = new List<DefDokHandlowego>();
                var wgKat = table.GetObjValue(table.GetValue("WgKategorii"), "Item", new Type[] { kt }, new object[] { k });
                foreach (var row in (IEnumerable)wgKat)
                    list.Add(new DefDokHandlowego() { EnovaObject = row });
                return list.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }


        }

        #endregion
    }
}
