using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Handel.DokumentHandlowy, Soneta.Handel", typeof(Enova.API.Handel.DokumentHandlowy), typeof(Enova.API.Connector.Handel.DokumentHandlowy)),
           RowMap("DokHandlowe", typeof(Enova.API.Handel.DokumentHandlowy), typeof(Enova.API.Handel.HandelModule))]

namespace Enova.API.Handel
{
    [TableName("DokHandlowe")]
    public interface DokumentHandlowy : Business.GuidedRow,Core.IDokumentHandlowy, Core.IDokumentPodmiotu, Core.IDokumentKsiegowalny
    {
        int ID { get; }
        DefDokHandlowego Definicja { get; set; }
        Types.Date Data { get; set; }
        Types.Date DataOperacji { get; set; }
        Core.NumerDokumentu Numer { get; }
        string NumerPelny { get; }
        CRM.Kontrahent Kontrahent { get; set; }
        CRM.Kontrahent Odbiorca { get; set; }
        Magazyny.Magazyn Magazyn { get; set; }
        Magazyny.Magazyn MagazynDo { get; set; }
        IEnumerable<PozycjaDokHandlowego> Pozycje { get; }
        IEnumerable Platnosci { get; }
        Types.Currency WartoscNetto { get; }
        Types.Currency WartoscVat { get; }
        Types.Currency WartoscBrutto { get; }
        bool Korekta { get; }
        DokumentHandlowy DokumentKorygowany { get; }
        DokumentHandlowy DokumentMagazynowyGłówny { get; }
        bool Zatwierdzony { get; }
        bool Anulowany { get; }
        bool Bufor { get; }
        StanDokumentuHandlowego Stan { get; set; }
        string Opis { get; set; }
        Business.SubTable NadrzedneRelacje { get; }
        Business.SubTable PodrzedneRelacje { get; }

        PozycjaDokHandlowego PozycjaWgIdent(int ident);
        PozycjaDokHandlowego NowaPozycja(Towary.Towar towar, double ilosc);

    }
}
