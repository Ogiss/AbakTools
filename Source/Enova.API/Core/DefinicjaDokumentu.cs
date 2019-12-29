using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;
using Enova.API.Core;


namespace Enova.API.Core
{
    public interface DefinicjaDokumentu : Business.GuidedRow, IDefinicjaDokumentu, /*IRightsSourceEx, IRightsSource, IIsoProceduraHost, IDefinicjaKomunikatuEDIHost,*/ IRow, ISessionable
    {
        // Properties
        [Category("Og\x00f3lne")]
        bool Blokada { get; set; }
        //[Category("Definicja")]
        //MemoText DodatkowyNaglowekDefinicja { get; set; }
        [Category("Og\x00f3lne")]
        bool Domyslna { get; set; }
        [MaxLength(30), Category("Og\x00f3lne")]
        string Nazwa { get; set; }
        [Category("Definicja")]
        DefinicjaNumeracji Numeracja { get; }
        //[Description("Procedura ISO, kt\x00f3ra zostanie wykorzystana do numeracji dokument\x00f3w danego typu"), Category("ISO")]
        //IsoProcedura ProceduraISO { get; set; }
        //[ChildTable("RodzajKomunikatuHost", "Soneta.Core.RodzajKomunikatuHost", "Host")]
        //LpSubTable RodzajeKomunikatow { get; }
        //ISchematPodziałowy SchematPodzialowy { get; set; }
        [Required, Category("Og\x00f3lne"), MaxLength(12)]
        string Symbol { get; set; }
        [Category("Og\x00f3lne"), Required]
        TypDokumentu Typ { get; }
        //[Category("Definicja")]
        //MemoText WidoczneCechy { get; set; }
        bool ZawszePrzeliczajOpisAnalityczny { get; set; }
        [Browsable(false)]
        string DomyślnaNumeracja { get; }
        Type TypDokumentu { get; }



    }
}
