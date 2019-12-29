using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Printer
{
    public enum Destinations
    {
//        [Caption("Przykład wywołania ASPX")]
        AspxSample = 0x12,
//        [Caption("Załączniki PDF")]
        AttachmentsPDF = 6,
//        [Caption("Załączniki Microsoft WordPad (RTF)")]
        AttachmentsRTF = 13,
//        [Caption("Załączniki Microsoft Word")]
        AttachmentsWord = 11,
//        [Caption("Standardowe")]
        Default = 0,
//        [Caption("Wyślij email PDF")]
        EmailPDF = 7,
//        [Caption("Internet Explorer")]
        Explorer = 8,
//        [Caption("Zapisać do pliku")]
        File = 0x10,
//        [Caption("Notatnik")]
        Notepad = 14,
//        [Caption("Notatnik (tylko tekst)")]
        NotepadText = 15,
//        [Caption("Dokument PDF")]
        PDF = 4,
//        [Caption("Podgląd wydruku")]
        Preview = 3,
//        [Caption("Podgląd dokumentu PDF")]
        PreviewPDF = 5,
//        [Caption("Drukarka")]
        Printer = 1,
//        [Caption("Drukarka z potwierdzeniem")]
        PrinterPrompt = 2,
//        [Caption("Edytor Microsoft WordPad (RTF)")]
        RTF = 12,
//        [Caption("Uruchom aplikację")]
        RunApplication = 0x11,
//        [Caption("Arkusz kalkulacyjny")]
        Sheet = 9,
//        [Caption("Edytor Microsoft Word")]
        Word = 10
    }
}
