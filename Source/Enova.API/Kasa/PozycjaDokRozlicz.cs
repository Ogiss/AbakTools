using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.Core;

[assembly:
    TypeMap("Soneta.Kasa.PozycjaDokRozlicz, Soneta.Kasa", typeof(Enova.API.Kasa.PozycjaDokRozlicz), typeof(Enova.API.Connector.Kasa.PozycjaDokRozlicz)),
    RowMap("PozDokRozlicz", typeof(Enova.API.Kasa.PozycjaDokRozlicz), typeof(Enova.API.Kasa.KasaModule))]


namespace Enova.API.Kasa
{
    public interface PozycjaDokRozlicz : Row
    {
        [Required]
        Date DataDokumentu { get; set; }
        Date DataZaplaty { get; set; }
        [Required, Category("Og\x00f3lne")]
        DokRozliczBase Dokument { get; }
        [MaxLength(40), Required]
        string NumerDokumentu { get; set; }
        [Category("Dokument"), MaxLength(80)]
        string Opis { get; set; }
        [Category("Og\x00f3lne")]
        IRozliczalny Platnosc { get; set; }
        Percent Procent { get; set; }
        [Category("Og\x00f3lne")]
        RozliczenieSP Rozliczenie { get; set; }
        Date TerminZaplaty { get; set; }
        [Required, Category("Og\x00f3lne")]
        TypDokumentu Typ { get; }
        IRozliczalny DokRozliczany { get; }
        string NumerEwidencji { get; }



    }
}
