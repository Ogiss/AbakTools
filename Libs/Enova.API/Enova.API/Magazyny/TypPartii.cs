using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Magazyny
{
    public enum TypPartii
    {
        Brak = 0,
        Magazynowy = 1,
        //[Hidden]
        TylkoZasób = 0xff00,
        Zamówiony = 4,
        ZamówionyZasób = 0x108,
        //[Hidden]
        ZamówionyZasóbMagazynowy = 260,
        Zarezerwowany = 2,
        //[Hidden]
        ZarezerwowanyZamówienie = 0x10
    }
}
