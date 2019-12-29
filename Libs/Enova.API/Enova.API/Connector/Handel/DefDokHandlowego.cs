using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Handel
{
    internal class DefDokHandlowego : Business.GuidedRow, API.Handel.DefDokHandlowego, API.Core.IDefinicjaDokumentu
    {
        public string Symbol
        {
            get { return GetValue<string>("Symbol"); }
        }

        public string Nazwa
        {
            get { return GetValue<string>("Nazwa"); }
        }

        public bool Blokada
        {
            get { return GetValue<bool>("Blokada"); }
        }

        public API.Towary.DefinicjaCeny Cena
        {
            get { return new Towary.DefinicjaCeny() { EnovaObject = GetValue("Cena") }; }
        }

        public string DomyślnaNumeracja
        {
            get { return FromEnova<string>("DomyślnaNumeracja"); }
        }

        API.Core.TypDokumentu API.Core.IDefinicjaDokumentu.Typ
        {
            get
            {
                return FromEnova<API.Core.TypDokumentu>("Typ", Type.GetType("Soneta.Core.IDefinicjaDokumentu, Soneta.Core"));
            }
        }

        public Type TypDokumentu
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.DefinicjaNumeracji Numeracja
        {
            get { return FromEnova<API.Core.DefinicjaNumeracji>("Numeracja"); }
        }

        public API.Handel.DefRelacjiHandlowej RelacjaMagazynowaDefinicja
        {
            get { return FromEnova<API.Handel.DefRelacjiHandlowej>("RelacjaMagazynowaDefinicja"); }
        }

        public API.Handel.DokumentHandlowy NowyDokument(API.CRM.Kontrahent kontrahent, API.Magazyny.Magazyn magazyn)
        {
            throw new NotImplementedException("Enova.API.Connector.Handel.DefDokHandlowego.NowyDokument()");
            /*
            var dokument = new Soneta.Handel.DokumentHandlowy();
            dokument.Definicja = (Soneta.Handel.DefDokHandlowego)this.Record;
            dokument.Kontrahent = (Soneta.CRM.Kontrahent)((Business.Row)kontrahent).Record;
            if (magazyn != null)
                dokument.Magazyn = (Soneta.Magazyny.Magazyn)((Business.Row)magazyn).Record;
            else
                dokument.Magazyn = Soneta.Magazyny.MagazynyModule.GetInstance(this).Magazyny.Firma;
            Soneta.Handel.HandelModule.GetInstance(this).DokHandlowe.AddRow(dokument);
            dokument.Data = Soneta.Types.Date.Today;
            return new DokumentHandlowy() { Record = dokument };
             */
        }




    }
}
