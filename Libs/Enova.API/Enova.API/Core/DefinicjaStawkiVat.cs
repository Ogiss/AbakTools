using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Enova.API.Core
{
    public interface DefinicjaStawkiVat : Business.GuidedRow
    {
        // Properties
        [/*MaxLength(12), Required,*/ Category("Og\x00f3lne")]
        string Kod { get; set; }
        [/*MaxLength(80),*/ Category("Og\x00f3lne")]
        string Opis { get; set; }
        //[Category("Og\x00f3lne")]
        //StawkaVat Stawka { get; }
        [Category("Og\x00f3lne")]
        bool Zablokowane { get; set; }
        bool Podstawowa { get; }
        bool WymagaSWW { get; }
    }
}
