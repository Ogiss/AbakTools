using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Services
{
    public class RozrachunekProxy
    {
        public Enova.API.Kasa.RozrachunekIdx RozrachunekIdx;

        public Enova.API.Kasa.TypRozrachunku Typ
        {
            get { return RozrachunekIdx.Typ; }
        }

        public Enova.API.Kasa.IRozliczalny Dokument
        {
            get { return RozrachunekIdx.Dokument; }
        }

        public string Numer
        {
            get { return RozrachunekIdx.Numer; }
        }

        public DateTime Data
        {
            get { return RozrachunekIdx.Data; }
        }

        public DateTime Termin
        {
            get { return RozrachunekIdx.Termin; }
        }

        public bool CzyKompensata
        {
            get
            {
                if (Dokument.Is<Enova.API.Kasa.Platnosc>() && Dokument.Dokument != null && Dokument.Dokument.Is<Enova.API.Core.IDokument>())
                    return Dokument.Dokument.As<Enova.API.Core.IDokument>().Definicja.Typ == Enova.API.Core.TypDokumentu.Kompensata;
                return false;
            }
        }

        public decimal? KwotaValue
        {
            get { return RozrachunekIdx.Kwota.Value; }
        }

        public decimal? Kwota
        {
            get
            {
                if (Dokument.Is<Enova.API.Kasa.Platnosc>())
                {
                    if (CzyKompensata)
                    {
                        return Typ == Enova.API.Kasa.TypRozrachunku.Zobowiązanie || Typ == Enova.API.Kasa.TypRozrachunku.Wypłata ? KwotaValue * -1 : KwotaValue;
                    }
                    else
                    {
                        return Zwrot || Typ == Enova.API.Kasa.TypRozrachunku.Zobowiązanie || Typ == Enova.API.Kasa.TypRozrachunku.Wypłata ? KwotaValue * -1 : KwotaValue;
                    }
                }
                else if (Dokument.Is<Enova.API.Kasa.Zaplata>())
                {
                    return ((Enova.API.Kasa.Zaplata)Dokument).Kierunek == Enova.API.Core.KierunekPlatnosci.Przychod ? KwotaValue * -1 : KwotaValue;
                }
                return KwotaValue;
            }

        }

        public bool Zwrot
        {
            get { return RozrachunekIdx.Zwrot; }
        }

        public decimal? KwotaDokumenty
        {
            get
            {
                switch (Typ)
                {
                    case Enova.API.Kasa.TypRozrachunku.Należność:
                    case Enova.API.Kasa.TypRozrachunku.Zobowiązanie:
                        return CzyKompensata ? (decimal?)null : Kwota.Value;
                    default: return null;
                }
            }
        }

        public decimal? KwotaDokumentyDostawcy
        {
            get
            {
                if (CzyKompensata)
                    return null;
                decimal kwota = 0;
                switch (Typ)
                {
                    case Enova.API.Kasa.TypRozrachunku.Należność:
                        kwota = KwotaValue == null ? 0 : KwotaValue.Value;
                        break;
                    case Enova.API.Kasa.TypRozrachunku.Zobowiązanie:
                        kwota = KwotaValue == null ? 0 : -KwotaValue.Value;
                        break;
                    default:
                        return null;
                }
                return Zwrot ? -kwota : kwota;
            }
        }

        public decimal? KwotaZapłaty
        {
            get
            {
                switch (Typ)
                {
                    case Enova.API.Kasa.TypRozrachunku.Wpłata:
                        return KwotaValue;
                    case Enova.API.Kasa.TypRozrachunku.Wypłata:
                        return -KwotaValue;
                    case Enova.API.Kasa.TypRozrachunku.Należność:
                    case Enova.API.Kasa.TypRozrachunku.Zobowiązanie:
                        return CzyKompensata ? -Kwota.Value : (decimal?)null;
                    default:
                        return null;
                }
            }
        }

        public decimal? KwotaZapłatyDostawcy
        {
            get
            {
                switch (Typ)
                {
                    case Enova.API.Kasa.TypRozrachunku.Wpłata:
                        return KwotaValue;
                    case Enova.API.Kasa.TypRozrachunku.Wypłata:
                        return -KwotaValue;
                    case Enova.API.Kasa.TypRozrachunku.Należność:
                    case Enova.API.Kasa.TypRozrachunku.Zobowiązanie:
                        return CzyKompensata ? -Kwota.Value : (decimal?)null;
                    default:
                        return null;
                }
            }
        }

        public decimal? Należność
        {
            get
            {
                switch (Typ)
                {
                    case Enova.API.Kasa.TypRozrachunku.Wypłata:
                    case Enova.API.Kasa.TypRozrachunku.Należność:
                        return Zwrot ? null : KwotaValue;
                    case Enova.API.Kasa.TypRozrachunku.Wpłata:
                    case Enova.API.Kasa.TypRozrachunku.Zobowiązanie:
                        return Zwrot ? -KwotaValue : null;
                }
                return null;
            }
        }

        public decimal? Zobowiązanie
        {
            get
            {
                switch (Typ)
                {
                    case Enova.API.Kasa.TypRozrachunku.Wypłata:
                    case Enova.API.Kasa.TypRozrachunku.Należność:
                        return Zwrot ? -KwotaValue : null;
                    case Enova.API.Kasa.TypRozrachunku.Wpłata:
                    case Enova.API.Kasa.TypRozrachunku.Zobowiązanie:
                        return Zwrot ? null : KwotaValue;
                }
                return null;
            }
        }



    }
}
