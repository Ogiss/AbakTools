using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB;

namespace Enova.Old.Handel
{
    public partial class DefDokHandlowych : EnovaGuidedTable<DefDokHandlowego>
    {
        #region Fields

        internal static readonly Guid AneksCyklicznejGuid = new Guid("00000000-0011-0002-0045-000000000000");
        internal static readonly Guid AneksDostawyGuid = new Guid("00000000-0011-0002-0046-000000000000");
        internal static readonly Guid DostawaBisGuid = new Guid("00000000-0011-0002-002a-000000000000");
        internal static readonly Guid DostawaGuid = new Guid("00000000-0011-0002-0029-000000000000");
        internal static readonly Guid DostawaWewnętrznaNzGuid = new Guid("00000000-0011-0002-002b-000000000000");
        internal static readonly Guid FakturaArchiwalnaGuid = new Guid("00000000-0011-0002-0022-000000000000");
        internal static readonly Guid FakturaFiskalizowanaGuid = new Guid("00000000-0011-0002-0024-000000000000");
        internal static readonly Guid FakturaFiskalizowanaZaliczkowaGuid = new Guid("00000000-0011-0002-0038-000000000000");
        internal static readonly Guid FakturaParagonuGuid = new Guid("00000000-0011-0002-0004-000000000000");
        internal static readonly Guid FakturaProformaGuid = new Guid("00000000-0011-0002-0027-000000000000");
        internal static readonly Guid FakturaSprzedaży2Guid = new Guid("00000000-0011-0002-004c-000000000000");
        internal static readonly Guid FakturaSprzedażyGuid = new Guid("00000000-0011-0002-0001-000000000000");
        internal static readonly Guid FakturaSprzedażyOpakowańGuid = new Guid("00000000-0011-0002-0057-000000000000");
        internal static readonly Guid FakturaZakupu2Guid = new Guid("00000000-0011-0002-0049-000000000000");
        internal static readonly Guid FakturaZakupuGuid = new Guid("00000000-0011-0002-0007-000000000000");
        internal static readonly Guid FakturaZaliczkowaGuid = new Guid("00000000-0011-0002-0025-000000000000");
        internal static readonly Guid ImportUEGuid = new Guid("00000000-0011-0002-0030-000000000000");
        internal static readonly Guid InwentaryzacjaGuid = new Guid("00000000-0011-0002-0012-000000000000");
        internal static readonly Guid InwentaryzacjaNadwyżkaGuid = new Guid("00000000-0011-0002-0013-000000000000");
        internal static readonly Guid InwentaryzacjaStrataGuid = new Guid("00000000-0011-0002-0014-000000000000");
        internal static readonly Guid KompletacjaGuid = new Guid("00000000-0011-0002-001b-000000000000");
        internal static readonly Guid KompletacjaPrzychódGuid = new Guid("00000000-0011-0002-001d-000000000000");
        internal static readonly Guid KompletacjaRozchódGuid = new Guid("00000000-0011-0002-001c-000000000000");
        internal static readonly Guid KorektaDostawaGuid = new Guid("00000000-0011-0002-0036-000000000000");
        internal static readonly Guid KorektaFakturyZaliczkowejGuid = new Guid("00000000-0011-0002-0028-000000000000");
        internal static readonly Guid KorektaImportUEGuid = new Guid("00000000-0011-0002-0035-000000000000");
        internal static readonly Guid KorektaPrzesunięciePrzychódGuid = new Guid("00000000-0011-0002-003A-000000000000");
        internal static readonly Guid KorektaPrzyjęcieNabyciaGuid = new Guid("00000000-0011-0002-0037-000000000000");
        internal static readonly Guid KorektaPZ2Guid = new Guid("00000000-0011-0002-0047-000000000000");
        internal static readonly Guid KorektaPZGuid = new Guid("00000000-0011-0002-0019-000000000000");
        internal static readonly Guid KorektaPZWZGuid = new Guid("00000000-0011-0002-001a-000000000000");
        internal static readonly Guid KorektaSprzedażyGuid = new Guid("00000000-0011-0002-0003-000000000000");
        internal static readonly Guid KorektaWZ2Guid = new Guid("00000000-0011-0002-004a-000000000000");
        internal static readonly Guid KorektaWZGuid = new Guid("00000000-0011-0002-000a-000000000000");
        internal static readonly Guid KorektaZakupuGuid = new Guid("00000000-0011-0002-0008-000000000000");
        internal static readonly Guid NabycieGuid = new Guid("00000000-0011-0002-0031-000000000000");
        internal static readonly Guid NabycieWewnętrzneNcGuid = new Guid("00000000-0011-0002-0033-000000000000");
        internal static readonly Guid NabycieWewnętrzneNzGuid = new Guid("00000000-0011-0002-0032-000000000000");
        internal static readonly Guid NadwyżkaGuid = new Guid("00000000-0011-0002-0015-000000000000");
        internal static readonly Guid OfertaDostawcyGuid = new Guid("00000000-0011-0002-0020-000000000000");
        internal static readonly Guid OfertaOdbiorcyGuid = new Guid("00000000-0011-0002-0021-000000000000");
        internal static readonly Guid ParagonGuid = new Guid("00000000-0011-0002-0002-000000000000");
        internal static readonly Guid PrzesunięcieGuid = new Guid("00000000-0011-0002-000f-000000000000");
        internal static readonly Guid PrzesunięciePrzychódGuid = new Guid("00000000-0011-0002-0010-000000000000");
        internal static readonly Guid PrzesunięcieRozchódGuid = new Guid("00000000-0011-0002-0011-000000000000");
        internal static readonly Guid PrzyjęcieMagazynowe2Guid = new Guid("00000000-0011-0002-0048-000000000000");
        internal static readonly Guid PrzyjęcieMagazynoweGuid = new Guid("00000000-0011-0002-000b-000000000000");
        internal static readonly Guid PrzyjęcieNabyciaGuid = new Guid("00000000-0011-0006-0006-000000000000");
        internal static readonly Guid PrzyjęcieOpakowańGuid = new Guid("00000000-0011-0002-0052-000000000000");
        internal static readonly Guid PrzyjęcieOpakowańMMGuid = new Guid("00000000-0011-0002-0058-000000000000");
        internal static readonly Guid PrzyjęcieWewnętrzneGuid = new Guid("00000000-0011-0002-000d-000000000000");
        internal static readonly Guid RachunekGuid = new Guid("00000000-0011-0002-0005-000000000000");
        public static readonly Guid Rezerwacja0Guid = new Guid("00000000-0011-0002-0017-000000000000");
        public static readonly Guid RezerwacjaGuid = new Guid("00000000-0011-0002-000c-000000000000");
        public static readonly Guid RezerwacjaMarżaMinGuid = new Guid("00000000-0011-0002-0034-000000000000");
        internal static readonly Guid RozchódWewnętrznyGuid = new Guid("00000000-0011-0002-000e-000000000000");
        internal static readonly Guid StrataGuid = new Guid("00000000-0011-0002-0016-000000000000");
        internal static readonly Guid UmowaCyklicznaGuid = new Guid("00000000-0011-0002-0042-000000000000");
        internal static readonly Guid UmowaDostawyGuid = new Guid("00000000-0011-0002-0043-000000000000");
        internal static readonly Guid WydanieDostawyGuid = new Guid("00000000-0011-0002-002c-000000000000");
        internal static readonly Guid WydanieMagazynowe2Guid = new Guid("00000000-0011-0002-004b-000000000000");
        internal static readonly Guid WydanieMagazynoweGuid = new Guid("00000000-0011-0002-0009-000000000000");
        internal static readonly Guid WydanieOpakowańGuid = new Guid("00000000-0011-0002-0053-000000000000");
        internal static readonly Guid WydanieOpakowańMMGuid = new Guid("00000000-0011-0002-0059-000000000000");
        internal static readonly Guid ZakupArchiwalnyGuid = new Guid("00000000-0011-0002-0023-000000000000");
        internal static readonly Guid ZakupOpakowańGuid = new Guid("00000000-0011-0002-0056-000000000000");
        internal static readonly Guid ZakupZaliczkowyGuid = new Guid("00000000-0011-0002-0026-000000000000");
        internal static readonly Guid ZamówienieDostawcyGuid = new Guid("00000000-0011-0002-001f-000000000000");
        internal static readonly Guid ZamówienieOdbiorcyGuid = new Guid("00000000-0011-0002-001e-000000000000");
        internal static readonly Guid ZapytanieOfertoweDostawcyGuid = new Guid("00000000-0011-0002-0081-000000000000");
        internal static readonly Guid ZapytanieOfertoweOdbiorcyGuid = new Guid("00000000-0011-0002-0080-000000000000");
        internal static readonly Guid ZlecenieJednorazoweGuid = new Guid("00000000-0011-0002-0044-000000000000");
        private static readonly Guid ZmianaParametrowZasobuGuid = new Guid("00000000-0011-0002-005a-000000000000");
        internal static readonly Guid ZwrotPrzyjęciaOpakowańGuid = new Guid("00000000-0011-0002-0055-000000000000");
        internal static readonly Guid ZwrotWydaniaOpakowańGuid = new Guid("00000000-0011-0002-0054-000000000000");

        #endregion

        #region Properties

        public DefDokHandlowego this[Guid guid]
        {
            get { return this.BaseQuery.Where(r => r.Guid == guid).FirstOrDefault(); }
        }

        public DefDokHandlowego AneksCyklicznej
        {
            get
            {
                return this[(Guid)AneksCyklicznejGuid];
            }
        }

        public DefDokHandlowego AneksDostawy
        {
            get
            {
                return this[(Guid)AneksDostawyGuid];
            }
        }

        public DefDokHandlowego Dostawa
        {
            get
            {
                return this[(Guid)DostawaGuid];
            }
        }

        public DefDokHandlowego DostawaBis
        {
            get
            {
                return this[(Guid)DostawaBisGuid];
            }
        }

        public DefDokHandlowego DostawaWewnętrznaNz
        {
            get
            {
                return this[(Guid)DostawaWewnętrznaNzGuid];
            }
        }

        public DefDokHandlowego FakturaArchiwalna
        {
            get
            {
                return this[(Guid)FakturaArchiwalnaGuid];
            }
        }

        public DefDokHandlowego FakturaFiskalizowana
        {
            get
            {
                return this[(Guid)FakturaFiskalizowanaGuid];
            }
        }

        public DefDokHandlowego FakturaFiskalizowanaZaliczkowa
        {
            get
            {
                return this[(Guid)FakturaFiskalizowanaZaliczkowaGuid];
            }
        }

        public DefDokHandlowego FakturaParagonu
        {
            get
            {
                return this[(Guid)FakturaParagonuGuid];
            }
        }

        public DefDokHandlowego FakturaProforma
        {
            get
            {
                return this[(Guid)FakturaProformaGuid];
            }
        }

        public DefDokHandlowego FakturaSprzedaży
        {
            get
            {
                return this[(Guid)FakturaSprzedażyGuid];
            }
        }

        public DefDokHandlowego FakturaSprzedaży2
        {
            get
            {
                return this[(Guid)FakturaSprzedaży2Guid];
            }
        }

        public DefDokHandlowego FakturaSprzedażyOpakowań
        {
            get
            {
                return this[(Guid)FakturaSprzedażyOpakowańGuid];
            }
        }

        public DefDokHandlowego FakturaZakupu
        {
            get
            {
                return this[(Guid)FakturaZakupuGuid];
            }
        }

        public DefDokHandlowego FakturaZakupu2
        {
            get
            {
                return this[(Guid)FakturaZakupu2Guid];
            }
        }

        public DefDokHandlowego FakturaZaliczkowa
        {
            get
            {
                return this[(Guid)FakturaZaliczkowaGuid];
            }
        }

        public DefDokHandlowego ImportUE
        {
            get
            {
                return this[(Guid)ImportUEGuid];
            }
        }

        public DefDokHandlowego Inwentaryzacja
        {
            get
            {
                return this[(Guid)InwentaryzacjaGuid];
            }
        }

        public DefDokHandlowego InwentaryzacjaNadwyżka
        {
            get
            {
                return this[(Guid)InwentaryzacjaNadwyżkaGuid];
            }
        }

        public DefDokHandlowego InwentaryzacjaStrata
        {
            get
            {
                return this[(Guid)InwentaryzacjaStrataGuid];
            }
        }

        public DefDokHandlowego Kompletacja
        {
            get
            {
                return this[(Guid)KompletacjaGuid];
            }
        }

        public DefDokHandlowego KompletacjaPrzychód
        {
            get
            {
                return this[(Guid)KompletacjaPrzychódGuid];
            }
        }

        public DefDokHandlowego KompletacjaRozchód
        {
            get
            {
                return this[(Guid)KompletacjaRozchódGuid];
            }
        }

        public DefDokHandlowego KorektaDostawa
        {
            get
            {
                return this[(Guid)KorektaDostawaGuid];
            }
        }

        public DefDokHandlowego KorektaFakturyZaliczkowej
        {
            get
            {
                return this[(Guid)KorektaFakturyZaliczkowejGuid];
            }
        }

        public DefDokHandlowego KorektaImportUE
        {
            get
            {
                return this[(Guid)KorektaImportUEGuid];
            }
        }

        internal DefDokHandlowego KorektaPrzesunięciePrzychód
        {
            get
            {
                return this[(Guid)KorektaPrzesunięciePrzychódGuid];
            }
        }

        public DefDokHandlowego KorektaPrzyjęcieNabycia
        {
            get
            {
                return this[(Guid)KorektaPrzyjęcieNabyciaGuid];
            }
        }

        public DefDokHandlowego KorektaPZ
        {
            get
            {
                return this[(Guid)KorektaPZGuid];
            }
        }

        public DefDokHandlowego KorektaPZ2
        {
            get
            {
                return this[(Guid)KorektaPZ2Guid];
            }
        }

        public DefDokHandlowego KorektaPZWZ
        {
            get
            {
                return this[(Guid)KorektaPZWZGuid];
            }
        }

        public DefDokHandlowego KorektaSprzedaży
        {
            get
            {
                return this[(Guid)KorektaSprzedażyGuid];
            }
        }

        public DefDokHandlowego KorektaWZ
        {
            get
            {
                return this[(Guid)KorektaWZGuid];
            }
        }

        public DefDokHandlowego KorektaWZ2
        {
            get
            {
                return this[(Guid)KorektaWZ2Guid];
            }
        }

        public DefDokHandlowego KorektaZakupu
        {
            get
            {
                return this[(Guid)KorektaZakupuGuid];
            }
        }

        public DefDokHandlowego Nabycie
        {
            get
            {
                return this[(Guid)NabycieGuid];
            }
        }

        public DefDokHandlowego NabycieWewnętrzneNc
        {
            get
            {
                return this[(Guid)NabycieWewnętrzneNcGuid];
            }
        }

        public DefDokHandlowego NabycieWewnętrzneNz
        {
            get
            {
                return this[(Guid)NabycieWewnętrzneNzGuid];
            }
        }

        public DefDokHandlowego Nadwyżka
        {
            get
            {
                return this[(Guid)NadwyżkaGuid];
            }
        }

        public DefDokHandlowego OfertaDostawcy
        {
            get
            {
                return this[(Guid)OfertaDostawcyGuid];
            }
        }

        public DefDokHandlowego OfertaOdbiorcy
        {
            get
            {
                return this[(Guid)OfertaOdbiorcyGuid];
            }
        }

        public DefDokHandlowego Paragon
        {
            get
            {
                return this[(Guid)ParagonGuid];
            }
        }

        public DefDokHandlowego Przesunięcie
        {
            get
            {
                return this[(Guid)PrzesunięcieGuid];
            }
        }

        public DefDokHandlowego PrzesunięciePrzychód
        {
            get
            {
                return this[(Guid)PrzesunięciePrzychódGuid];
            }
        }

        public DefDokHandlowego PrzesunięcieRozchód
        {
            get
            {
                return this[(Guid)PrzesunięcieRozchódGuid];
            }
        }

        public DefDokHandlowego PrzyjęcieMagazynowe
        {
            get
            {
                return this[(Guid)PrzyjęcieMagazynoweGuid];
            }
        }

        public DefDokHandlowego PrzyjęcieMagazynowe2
        {
            get
            {
                return this[(Guid)PrzyjęcieMagazynowe2Guid];
            }
        }

        public DefDokHandlowego PrzyjęcieNabycia
        {
            get
            {
                return this[(Guid)PrzyjęcieNabyciaGuid];
            }
        }

        public DefDokHandlowego PrzyjęcieOpakowań
        {
            get
            {
                return this[(Guid)PrzyjęcieOpakowańGuid];
            }
        }

        public DefDokHandlowego PrzyjęcieOpakowańMM
        {
            get
            {
                return this[(Guid)PrzyjęcieOpakowańMMGuid];
            }
        }

        public DefDokHandlowego PrzyjęcieWewnętrzne
        {
            get
            {
                return this[(Guid)PrzyjęcieWewnętrzneGuid];
            }
        }

        public DefDokHandlowego Rachunek
        {
            get
            {
                return this[(Guid)RachunekGuid];
            }
        }

        public DefDokHandlowego RozchódWewnętrzny
        {
            get
            {
                return this[(Guid)RozchódWewnętrznyGuid];
            }
        }

        public DefDokHandlowego Strata
        {
            get
            {
                return this[(Guid)StrataGuid];
            }
        }

        public DefDokHandlowego UmowaCykliczna
        {
            get
            {
                return this[(Guid)UmowaCyklicznaGuid];
            }
        }

        public DefDokHandlowego UmowaDostawy
        {
            get
            {
                return this[(Guid)UmowaDostawyGuid];
            }
        }

        public DefDokHandlowego WydanieDostawy
        {
            get
            {
                return this[(Guid)WydanieDostawyGuid];
            }
        }

        public DefDokHandlowego WydanieMagazynowe
        {
            get
            {
                return this[(Guid)WydanieMagazynoweGuid];
            }
        }

        public DefDokHandlowego WydanieMagazynowe2
        {
            get
            {
                return this[(Guid)WydanieMagazynowe2Guid];
            }
        }

        public DefDokHandlowego WydanieOpakowań
        {
            get
            {
                return this[(Guid)WydanieOpakowańGuid];
            }
        }

        public DefDokHandlowego WydanieOpakowańMM
        {
            get
            {
                return this[(Guid)WydanieOpakowańMMGuid];
            }
        }

        public DefDokHandlowego ZakupArchiwalny
        {
            get
            {
                return this[(Guid)ZakupArchiwalnyGuid];
            }
        }

        public DefDokHandlowego ZakupOpakowań
        {
            get
            {
                return this[(Guid)ZakupOpakowańGuid];
            }
        }

        public DefDokHandlowego ZakupZaliczkowy
        {
            get
            {
                return this[(Guid)ZakupZaliczkowyGuid];
            }
        }

        public DefDokHandlowego ZamówienieDostawcy
        {
            get
            {
                return this[(Guid)ZamówienieDostawcyGuid];
            }
        }

        public DefDokHandlowego ZamówienieOdbiorcy
        {
            get
            {
                return this[(Guid)ZamówienieOdbiorcyGuid];
            }
        }

        public DefDokHandlowego ZapytanieOfertoweDostawcy
        {
            get
            {
                return this[(Guid)ZapytanieOfertoweDostawcyGuid];
            }
        }

        public DefDokHandlowego ZapytanieOfertoweOdbiorcy
        {
            get
            {
                return this[(Guid)ZapytanieOfertoweOdbiorcyGuid];
            }
        }

        public DefDokHandlowego ZlecenieJednorazowe
        {
            get
            {
                return this[(Guid)ZlecenieJednorazoweGuid];
            }
        }

        public DefDokHandlowego ZmianaParametrowZasobu
        {
            get
            {
                return this[(Guid)ZmianaParametrowZasobuGuid];
            }
        }

        public DefDokHandlowego ZwrotPrzyjęciaOpakowań
        {
            get
            {
                return this[(Guid)ZwrotPrzyjęciaOpakowańGuid];
            }
        }

        public DefDokHandlowego ZwrotWydaniaOpakowań
        {
            get
            {
                return this[(Guid)ZwrotWydaniaOpakowańGuid];
            }
        }

        #endregion

        #region Methods

        protected override System.Data.Objects.ObjectQuery<DefDokHandlowego> CreateQuery()
        {
            return ((HandelModule)this.Module).DataContext.DefDokHandlowych;
        }


        #endregion

    }
}
