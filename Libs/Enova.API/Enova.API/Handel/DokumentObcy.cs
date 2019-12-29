using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Enova.API.Handel
{
    public interface DokumentObcy : Business.SubRow
    {
        Types.Date DataOtrzymania { get; set; }
        DokumentHandlowy Dokument { get; }
        string Numer { get; set; }
    }
}
