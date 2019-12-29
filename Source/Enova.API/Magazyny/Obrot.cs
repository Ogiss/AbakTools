using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Types;
using Enova.API.Towary;
using Enova.API.CRM;

[assembly: TypeMap("Soneta.Magazyny.Obrot, Soneta.Handel", typeof(Enova.API.Magazyny.Obrot), typeof(Enova.API.Connector.Magazyny.Obrot)),
           RowMap("Obroty", typeof(Enova.API.Magazyny.Obrot), typeof(Enova.API.Magazyny.MagazynyModule))]

namespace Enova.API.Magazyny
{
    public interface Obrot : Business.Row
    {
        [Description("Czas (rozchodu) powstania obrotu.")]
        Time Czas { get; }
        
        [Description("Data (rozchodu) powstania obrotu.")]
        Date Data { get; }
        
        [Description("Ilość towaru z partii towaru użytej w tyn obrocie. ")/*, Caption("Ilość")*/]
        Quantity Ilosc { get; set; }
        
        [Description("Określa spos\x00f3b powstania obrotu, czy jest to obr\x00f3t stornujący inne oborty.")]
        KorektaObrotu Korekta { get; set; }
        
        [Description("Magazyn, w kt\x00f3rym został naliczony ten obr\x00f3t magazynowy.")/*, Required*/]
        Magazyn Magazyn { get; }
        
        [Description("Okres magazynowy, w kt\x00f3rym został naliczony ten obr\x00f3t magazynowy (marża).")/*, Required*/]
        OkresMagazynowy Okres { get; }
        
        PartiaTowaru this[KierunekPartii kierunek] { get; }

        [Description("Informacja o przychodowej partii towaru.")/*, Caption("Przych\x00f3d")*/]
        PartiaTowaru Przychod { get; }

        [Description("Informacja o rozchodowej partii towaru.")/*, Caption("Rozch\x00f3d")*/]
        PartiaTowaru Rozchod { get; }
        
        [Description("Kontrahent, od kt\x00f3rego pochodzi partia towaru użyta w tym obrocie.")]
        Kontrahent PrzychodKontrahent { get; }

        [Description("Kontrahent, kt\x00f3remu została sprzedana partia towaru z tego obrotu.")]
        Kontrahent RozchodKontrahent { get; }

        [Description("Określa obr\x00f3t, kt\x00f3ry został stornowany przez ten obr\x00f3t.")]
        Obrot Stornowany { get; set; }

        [Description("Towar, dla ktrego zostałł naliczony obr\x00f3t magazynowy")/*, Required*/]
        Towar Towar { get; }

        [Description("Wartość marży uzyskana na obrocie towaru, r\x00f3żnica Rozch\x00f3d-Przych\x00f3d.")]
        decimal Marża { get; }

        [Description("Wartość marży uzyskana na jednostce towaru.")]
        double MarżaJednostkowa { get; }

        [Description("Sprawdza dany nie spełnia warunku minimalnej marży.")]
        bool MinimalnaMarża { get; }

        [Description("Procent marży uzyskanej na obrocie towaru (liczony wstecz od wartości rozchodu).")]
        Percent ProcentMarży { get; }

        [Description("Procent narzutu na obrocie towaru dodanego do wartości przychodu.")]
        Percent ProcentNarzutu { get; }

        [Description("Zwraca obr\x00f3t stornujący ten zapis stornowany.")]
        Obrot Stornujący { get; }

        [Description("Wskazuje ujemną marżę uzyskaną na obrocie z dokładnością do zaokrąglonej ceny, czyli Rozch\x00f3d<Przych\x00f3d.")]
        bool UjemnaMarża { get; }

        bool Zamkniety { get; }
    }
}
