using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Handel.PozycjaDokHandlowego, Soneta.Handel", typeof(Enova.API.Handel.PozycjaDokHandlowego), typeof(Enova.API.Connector.Handel.PozycjaDokHandlowego)),
           RowMap("PozycjeDokHan", typeof(Enova.API.Handel.PozycjaDokHandlowego), typeof(Enova.API.Handel.HandelModule))]

namespace Enova.API.Handel
{
    public interface PozycjaDokHandlowego : Business.Row
    {
        int Lp { get; }
        int Ident { get; }
        Towary.Towar Towar { get; set; }
        double Ilosc { get; set; }
        double IloscMagazynu { get; set; }
        decimal? Cena { get; set; }
        decimal? Rabat { get; set; }
        bool Korekta { get; }
        BruttoNetto Suma { get; }
        PozycjaDokHandlowego PozycjaKorygowana { get; }
        //[ChildTable("Obrot", "Soneta.Magazyn.Obrot", null), Description("Obroty związane bezpośrednio z pozycją dokumentu.")]
        Business.SubTable Obroty { get; }
        //[Description("Wszystkie obroty związane z pozycją dokumentu, łącznie z obrotami dokument\x00f3w zależnych (bez storno zasobu)."), ChildTable("Obrot", "Soneta.Magazyn.Obrot", null)]
        //public ListWithView ObrotyWszystkie { get; }
        //[Description("Wszystkie obroty związane z pozycją dokumentu, łącznie z obrotami dokument\x00f3w zależnych i korygujących (bez storno zasobu)."), ChildTable("Obrot", "Soneta.Magazyn.Obrot", null)]
        //public ListWithView ObrotyWszystkieZKorektami { get; }
        Towary.DefinicjaCeny DefinicjaCeny { get; }
        void UstawCenę(Towary.WyliczenieCeny wylicz, Towary.DefinicjaCeny definicjaCeny, bool wymuśZmianęIlości);

    }
}
