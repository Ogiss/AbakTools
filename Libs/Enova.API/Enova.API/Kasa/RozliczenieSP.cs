using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.RozliczenieSP, Soneta.Kasa", typeof(Enova.API.Kasa.RozliczenieSP), typeof(Enova.API.Connector.Kasa.RozliczenieSP)),
           RowMap("RozliczeniaSP", typeof(Enova.API.Kasa.RozliczenieSP), typeof(Enova.API.Kasa.KasaModule))]

namespace Enova.API.Kasa
{
    public interface RozliczenieSP : Business.GuidedRow
    {
        DateTime Data { get;  }
        DateTime DataOgraniczeniaNaliczaniaOdsetek { get;  }
        DateTime DataOgraniczeniaNaliczaniaOdsetekOd { get;  }
        Types.Currency KwotaDokumentu { get;  }
        Types.Currency KwotaZaplaty { get;  }
        IRozliczalny Dokument { get; }
        IRozliczalny Zaplata { get; }
        //private SubTable RozliczeniaKS { get; }
        //public IRozliczenieKS RozliczenieKS { get; }
        //[Obsolete("Użyj property RozliczenieSP.RozliczenieKS")]
        //public IRozliczenie RozliczenieSPKS { get; }
        //public IRozliczenieSPKS RozliczenieSPKS2 { get; }
        bool RozniceKursoweSilver { get; }
        //StanNotyOdsetkowej StanNoty { get;  }
        DateTime Termin { get; }
        int Zwloka { get; }

    }
}
