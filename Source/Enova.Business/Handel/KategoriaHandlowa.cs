using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Handel
{
    public enum KategoriaHandlowa
    {
        AneksDoUmowy = 0x135,
        Brak = 0,
        FakturaWewnętrzna = 6,
        //[Hidden]
        HandelOstatni = 100,
        //[Hidden]
        HandelPierwszy = 1,
        Inwentaryzacja = 0x6b,
        Kompletacja = 0x6c,
        KorektaPrzyjęciaMagazynowego = 0x67,
        KorektaPrzyjęciaOpakowań = 0x6f,
        KorektaSprzedaży = 3,
        KorektaWydaniaMagazynowego = 0x69,
        KorektaWydaniaOpakowań = 0x71,
        KorektaZakupu = 5,
        //[Hidden]
        MagazynOstatni = 200,
        //[Hidden]
        MagazynPierwszy = 0x65,
        OfertaDostawcy = 0x133,
        OfertaOdbiorcy = 0x132,
        Pozostałe = 0x131,
        //[Hidden]
        PozostałeOstatni = 400,
        //[Hidden]
        PozostałePierwszy = 0x12d,
        //[Hidden]
        ProdukcjaOstatni = 500,
        //[Hidden]
        ProdukcjaPierwszy = 500,
        PrzesunięcieMagazynowe = 0x6a,
        PrzyjęcieMagazynowe = 0x66,
        PrzyjęcieOpakowań = 110,
        Sprzedaż = 2,
        Umowa = 0x134,
        WewnętrzneMagazynowe = 0x6d,
        Wewnętrzny = 0x130,
        WydanieMagazynowe = 0x68,
        WydanieOpakowań = 0x70,
        Zakup = 4,
        ZamówienieDostawcy = 0x12f,
        ZamówienieOdbiorcy = 0x12e,
        ZapytanieOfertoweDostawcy = 0x137,
        ZapytanieOfertoweOdbiorcy = 310,
        ZlecenieProdukcyjne = 0x1f5
    }
}
