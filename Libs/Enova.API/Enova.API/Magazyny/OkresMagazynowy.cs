using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;

[assembly: TypeMap("Soneta.Magazyny.OkresMagazynowy, Soneta.Handel", typeof(Enova.API.Magazyny.OkresMagazynowy), typeof(Enova.API.Connector.Magazyny.OkresMagazynowy)),
           RowMap("OkresyMag", typeof(Enova.API.Magazyny.OkresMagazynowy), typeof(Enova.API.Magazyny.MagazynyModule))]

namespace Enova.API.Magazyny
{
    public interface OkresMagazynowy : Business.GuidedRow
    {
        [Browsable(false)]
        [/*Required,*/ Description("Określa okres czasu, dla kt\x00f3rego jest aktualny ten okres magazynowy.")]
        FromTo Okres { get; set; }
        [Description("Określa, czy okres magazynowy jest zamknięty bez prawa do jego modyfikacji.")/*, Caption("Zamknięty")*/]
        bool Zamkniety { get; set; }
        string Info { get; }
        OkresMagazynowy Poprzedni { get; }
        [Description("Informacja o tym, czy okres jest zamknięty w postaci tekstowej.")]
        string ZamkniętyText { get; }
    }
}
