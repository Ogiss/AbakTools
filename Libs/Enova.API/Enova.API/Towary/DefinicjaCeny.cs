using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Towary.DefinicjaCeny, Soneta.Handel", typeof(Enova.API.Towary.DefinicjaCeny), typeof (Enova.API.Connector.Towary.DefinicjaCeny)),
           RowMap("DefinicjeCen", typeof(Enova.API.Towary.DefinicjaCeny), typeof(Enova.API.Towary.TowaryModule))]

namespace Enova.API.Towary
{
    public interface DefinicjaCeny : Business.GuidedRow
    {
        [Description("Określa, czy definicja ceny dopuszcza stosowanie promocji."), Category("Grupy")]
        bool AkceptujPromocje { get; set; }

        [Description("Określa, czy dana cena ma być automatycznie aktualizowana po wprowadzeniu dokumentu przychodowego."), Category("Og\x00f3lne")]
        bool AktualizujPrzychodem { get; set; }

        //[Description("Określa wsp\x00f3łczynnik uwzględniany w wyliczanej cenie"), Category("Algorytm")]
        //AlgorytmCeny Algorytm { get; }

        [Category("Og\x00f3lne"), Description("Określa cenę, kt\x00f3ra nie jest już używana przez program.")]
        bool Blokada { get; set; }

        [Description("Określa spos\x00f3b wyliczania ceny kontrahenta."), Obsolete("see PodmiotCeny { get; set; }"), Category("Grupy")]
        bool CenaOdbiorcy { get; set; }

        [Description("Cena będzie dodawana i zapamiętana w towarze."), Category("Og\x00f3lne")]
        bool DodawanaDoTowaru { get; set; }

        //[Description("Określa spos\x00f3b wyliczania ceny indywidualnej.")]
        //AlgorytmRabatu Indywidualna { get; }

        //[Description("Określa drugi spos\x00f3b wyliczania ceny indywidualnej.")]
        //AlgorytmRabatu Indywidualna2 { get; }

        //[Description("Określa trzeci spos\x00f3b wyliczania ceny indywidualnej.")]
        //AlgorytmRabatu Indywidualna3 { get; }

        [Category("Grupy"), Description("Określa, czy cena indywidualna będzie rabatowana.")]
        bool IndywidualnaRabatowana { get; set; }

        //[Description("Algorytm wyliczenia ceny"), Category("Algorytm")]
        //MemoText Metoda { get; set; }

        [/*MaxLength(30), Required,*/ Category("Og\x00f3lne"), Description("Nazwa ceny")]
        string Nazwa { get; set; }

        //[Description("Określa spos\x00f3b wyliczania ceny kontrahenta."), Category("Grupy")]
        //public PodmiotCeny PodmiotCeny { get; set; }

        [Description("Priorytet określający kolejność wyliczania cen towaru."), Category("Og\x00f3lne")]
        int Priorytet { get; set; }

        //[Description("Określa pierwszy rodzaj naliczanego rabatu.")]
        //AlgorytmRabatu Rabat1 { get; }

        //[Description("Określa drugi rodzaj naliczanego rabatu.")]
        //AlgorytmRabatu Rabat2 { get; }

        //[Description("Określa trzeci rodzaj naliczanego rabatu.")]
        //AlgorytmRabatu Rabat3 { get; }

        //[Description("Określa czwarty rodzaj naliczanego rabatu.")]
        //AlgorytmRabatu Rabat4 { get; }

        //[Description("Określa piąty rodzaj naliczanego rabatu.")]
        //AlgorytmRabatu Rabat5 { get; }

        [Category("Grupy"), Description("Określa, czy definicja ceny dopuszcza rabatowanie przeceny.")]
        bool RabatujPromocje { get; set; }

        //[AttributeInheritance, ControlEdit(ControlEditKind.Algorithm)]
        //MemoText Metoda { get; set; }
        
        [Description("Określa, czy cena jest automatycznie przeliczana.")]
        bool Przeliczaj { get; }
        
    }
}
