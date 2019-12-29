using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;

[assembly: TypeMap("Soneta.Towary.Cena, Soneta.Handel", typeof(Enova.API.Towary.Cena), typeof(Enova.API.Connector.Towary.Cena))]

namespace Enova.API.Towary
{
    public interface Cena : Business.Row, ICena
    {
        [Description("Kwota brutto ceny"), Category("Og\x00f3lne")]
        DoubleCy Brutto { get; set; }

        [Description("Definicja ceny określająca spos\x00f3b wyliczania ceny"), Category("Og\x00f3lne")/*, Required*/]
        DefinicjaCeny Definicja { get; }

        [Description("Umożliwia edycję ceny jeżeli jest ona wyliczana na podstawie innej ceny."), Category("Og\x00f3lne")]
        bool Korygowana { get; set; }

        [Category("Og\x00f3lne"), Description("Kwota netto ceny")]
        DoubleCy Netto { get; set; }

        [Category("Og\x00f3lne"), Description("Wymusza wzajemne przeliczanie ceny netto i brutto.")/*, Caption("Połącz")*/]
        bool Polacz { get; set; }

        [Description("Standardowa ilość i jednostka dla ceny"), Category("Og\x00f3lne")]
        double StandardowaIlosc { get; set; }

        [/*Required,*/ Description("Towar, kt\x00f3remu przyporządkowana jest cena."), Category("Og\x00f3lne")]
        Towar Towar { get; }

        //public Jednostka Jednostka { get; set; }

        [Description("Marża wyliczona w stosunku do ostatniej ceny zakupu (netto)")/*, Caption("Marża")*/]
        DoubleCy MarżaOstatniejCeny { get; set; }

        [Description("Procent marży liczony od tej ceny (wstecz) w stosunku do ostatniej ceny zakupu.")/*, Caption("Marża %")*/]
        Percent ProcentMarżyOstatniejCeny { get; set; }

        [/*Caption("Narzut %"),*/ Description("Procent narzutu dodawany do ostatniej ceny zakupu.")]
        Percent ProcentNarzutuOstatniejCeny { get; set; }
        

    }
}
