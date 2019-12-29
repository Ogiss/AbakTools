using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.Kompensata+DefDok, Soneta.Kasa", null, typeof(Enova.API.Connector.Core.DefinicjaDokumentu))]

namespace Enova.API.Kasa
{
    public interface DefDokRozliczeniowego : Core.DefinicjaDokumentu/*, IVirtualComponent*/
    {

        // Methods
        Business.View GetListDefinicjaEwidencji();

        // Properties
        //[Category("Og\x00f3lne"), Description("Rozszerzenie aktywne")]
        //public DokEwidencji.DefEwidencji DefinicjaEwidencji { get; set; }
    }

}
