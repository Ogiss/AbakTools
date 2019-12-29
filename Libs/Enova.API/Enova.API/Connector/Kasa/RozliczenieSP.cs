using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class RozliczenieSP : Business.GuidedRow, API.Kasa.RozliczenieSP
    {
        public DateTime Data
        {
            get { return GetValue<DateTime>("Data"); }
        }

        public DateTime DataOgraniczeniaNaliczaniaOdsetek
        {
            get { return GetValue<DateTime>("DataOgraniczeniaNaliczaniaOdsetek"); }
        }

        public DateTime DataOgraniczeniaNaliczaniaOdsetekOd
        {
            get { return GetValue<DateTime>("DataOgraniczeniaNaliczaniaOdsetekOd"); }
        }

        public API.Types.Currency KwotaDokumentu
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("KwotaDokumentu")); }
        }

        public API.Types.Currency KwotaZaplaty
        {
            get { return EnovaHelper.FromEnova<API.Types.Currency>(GetValue("KwotaZaplaty")); }
        }

        public API.Kasa.IRozliczalny Dokument
        {
            get { return FromEnova<API.Kasa.IRozliczalny>("Dokument"); }
        }

        public API.Kasa.IRozliczalny Zaplata
        {
            get { return FromEnova<API.Kasa.IRozliczalny>("Zaplata"); }
        }

        public bool RozniceKursoweSilver
        {
            get { return (bool)GetValue("RozniceKursoweSilver"); }
        }

        public DateTime Termin
        {
            get { return GetValue<DateTime>("Termin"); }
        }

        public int Zwloka
        {
            get { return (int)GetValue("Zwloka"); }
        }
    }
}
