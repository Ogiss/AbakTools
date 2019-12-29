using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public enum StanEwidencji
    {
        Bufor,
        Wprowadzony,
        Predekretowany,
        Zaksięgowany
    }


    public enum RodzajeRabatów
    {
        Brak = 0,
        IndywidualnyKazdegoTowaru = 1,
        GrupowyKazdegoTowaru = 2,
        IndywidualnyGrupyTowarowej = 3,
        GrupowyGrupyTowarowej = 4,
        GrupowyWszystkichTowarow = 5,
        IndywidualnyWszystkichTowarow = 6
    }


    public enum TypRozrachunku
    {
        Należność = 10,
        Wpłata = 20,
        Wypłata = 0x15,
        Zobowiązanie = 11
    }

    public enum PodmiotType
    {
        Kontahent = 0,
        Pracownik = 1,
        Bank = 2,
        UrządSkarbowy = 3,
        ZUS = 4
    }

    public enum FeatureTypeNumber
    {
        //[Caption("Wielowartościowa")]
        Array = 14,
        //[Caption("Wielohierarchiczna")]
        ArrayOfTrees = 16,
        //[Caption("Warunek")]
        Bool = 1,
        //[Caption("Kwota z walutą")]
        Currency = 9,
        //[Caption("Data")]
        Date = 5,
        //[Caption("Kwota")]
        Decimal = 3,
        //aption("Liczba rzeczywista")]
        Double = 4,
        //[Caption("Liczba z walutą")]
        DoubleCy = 11,
        //[Caption("Ułamek")]
        Fraction = 8,
        //[Caption("Okres dat")]
        FromTo = 7,
        //[Caption("Liczba całkowita")]
        Int = 0,
        //[Caption("Procent")]
        Percent = 10,
        //[Caption("Referencja")]
        Reference = 13,
        //[Caption("Tekst")]
        String = 2,
        //[Caption("Czas")]
        Time = 6,
        //[Caption("Czas z dokładnością do sekundy")]
        TimeSec = 17,
        //[Caption("Hierarchiczna")]
        Tree = 15,
        //[Caption("Miesiąc w roku")]
        YearMonth = 12
    }

    //[DefaultWidth(13)]
    public enum ChangeInfoType
    {
        //[Caption("Zatwierdzony")]
        Accepted = 5,
        //[Caption("Anulowany")]
        Canceled = 11,
        //[Caption("Zmiana hasła")]
        ChangePassword = 10,
        //[Caption("Utworzony")]
        Created = 1,
        //[Caption("Zmiana bieżącej daty")]
        CurrentDayChanged = 12,
        //[Caption("Skasowany")]
        Deleted = 4,
        //[Caption("Eksportowany")]
        Exported = 9,
        //[Caption("Formularz")]
        FormView = 0x29,
        //[Caption("Importowany")]
        Imported = 3,
        //[Caption("Logowanie")]
        Login = 7,
        //[Caption("Wylogowanie")]
        Logout = 8,
        //[Caption("Zmieniony")]
        Modified = 2,
        Predekretowany = 0x15,
        //[Caption("Do bufora")]
        Rejected = 6,
        //[Caption("Raport")]
        Report = 0x2a,
        //[Caption("Widok listy")]
        TableView = 40,
        //[Caption("Inne")]
        User = 0x3e8,
        Wprowadzony = 20,
        WysłanyDoBanku = 30,
        Zaksięgowany = 0x16
    }



 

}
