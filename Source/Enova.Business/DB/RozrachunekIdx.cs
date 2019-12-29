using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.DB
{
    public partial class RozrachunekIdx
    {
        private object dokument = null;
        public object Dokument
        {
            get
            {
                if (dokument == null && EntityState != EntityState.Added && EntityState != EntityState.Detached)
                {
                    if (DokumentType == "Platnosci")
                    {
                        dokument = Enova.Business.Old.Core.ContextManager.DataContext.Platnosci
                            .Where(p => p.ID == DokumentID).FirstOrDefault();
                    }
                    else if (DokumentType == "Zaplaty")
                    {
                        dokument = Enova.Business.Old.Core.ContextManager.DataContext.Zaplaty
                            .Where(z => z.ID == DokumentID).FirstOrDefault();
                    }
                }
                else if (dokument == null)
                {
                    if (DokumentType == "Platnosci")
                    {
                        dokument = new Enova.Business.Old.DB.Platnosc();
                    }
                    else if (DokumentType == "Zaplaty")
                    {
                        dokument = new Enova.Business.Old.DB.Zaplata();
                    }
                }
                return dokument;
            }
        }

        public decimal? Kwota
        {
            get
            {
                if (Dokument is Enova.Business.Old.DB.Platnosc)
                {
                    if (this.CzyKompensata)
                    {
                        return TypRozrachunku == TypRozrachunku.Zobowiązanie || TypRozrachunku == TypRozrachunku.Wypłata ? KwotaValue * -1 : KwotaValue;
                    }
                    else
                    {
                        return Zwrot == true || TypRozrachunku == TypRozrachunku.Zobowiązanie || TypRozrachunku == TypRozrachunku.Wypłata ? KwotaValue * -1 : KwotaValue;
                    }
                }
                else if (Dokument is Enova.Business.Old.DB.Zaplata)
                {
                    return ((Enova.Business.Old.DB.Zaplata)Dokument).Kierunek == 1 ? KwotaValue * -1 : KwotaValue;
                }
                return KwotaValue;
            }
        }

        public decimal? KwotaRozliczona
        {
            get
            {
                if (Dokument is Enova.Business.Old.DB.Platnosc)
                {
                    if (this.CzyKompensata)
                    {
                        return TypRozrachunku == Types.TypRozrachunku.Zobowiązanie || TypRozrachunku == Types.TypRozrachunku.Wypłata ? KwotaRozliczonaValue * -1 : KwotaRozliczonaValue;
                    }
                    else
                    {
                        return Zwrot == true || TypRozrachunku == Types.TypRozrachunku.Zobowiązanie || TypRozrachunku == Types.TypRozrachunku.Wypłata ? KwotaRozliczonaValue * -1 : KwotaRozliczonaValue;
                    }
                }
                else if (Dokument is Enova.Business.Old.DB.Zaplata)
                {
                    return ((Enova.Business.Old.DB.Zaplata)Dokument).Kierunek == 1 ? KwotaRozliczonaValue * -1 : KwotaRozliczonaValue;
                }
                return KwotaRozliczonaValue;
            }
        }

        public decimal? Pozostało
        {
            get
            {
                decimal? kwota = Kwota;
                decimal? kwotaRozliczona = KwotaRozliczona;
                if (kwota != null && kwotaRozliczona != null)
                {
                    return decimal.Round(kwota.Value - kwotaRozliczona.Value, 2);
                }
                return null;
            }
        }

        public TypRozrachunku TypRozrachunku
        {
            get { return (TypRozrachunku)Typ; }
        }

        public decimal? Należność
        {
            get
            {
                switch (TypRozrachunku)
                {
                    case TypRozrachunku.Wypłata:
                    case TypRozrachunku.Należność:
                        return Zwrot.Value ? null : KwotaValue;
                    case TypRozrachunku.Wpłata:
                    case TypRozrachunku.Zobowiązanie:
                        return Zwrot.Value ? -KwotaValue : null;
                }
                return null;
            }
        }

        public decimal? Zobowiązanie
        {
            get
            {
                switch (TypRozrachunku)
                {
                    case TypRozrachunku.Wypłata:
                    case TypRozrachunku.Należność:
                        return Zwrot.Value ? -KwotaValue : null;
                    case TypRozrachunku.Wpłata:
                    case TypRozrachunku.Zobowiązanie:
                        return Zwrot.Value ? null : KwotaValue;
                }
                return null;
            }
        }

        public bool CzyKompensata
        {
            get
            {
                if (DokumentType == "Platnosci")
                {
                    Platnosc platnosc = (Platnosc)Dokument;
                    if (platnosc.DokumentType == "DokRozliczeniowe")
                    {
                        DokumentRozliczeniowy dr = (DokumentRozliczeniowy)platnosc.Dokument;
                        if (dr.Definicja != null)
                            return dr.Definicja.CzyKompensata;
                    }
                }
                return false;
            }
        }

        public decimal? KwotaDokumenty
        {
            get
            {
                switch (TypRozrachunku)
                {
                    case TypRozrachunku.Należność:
                    case TypRozrachunku.Zobowiązanie:
                        return CzyKompensata ? (decimal?)null : Kwota.Value;
                    default:
                        return null;
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
                switch (TypRozrachunku)
                {
                    case TypRozrachunku.Należność:
                        kwota = KwotaValue == null ? 0 : KwotaValue.Value;
                        break;
                    case TypRozrachunku.Zobowiązanie:
                        kwota = KwotaValue == null ? 0 : -KwotaValue.Value;
                        break;
                    default:
                        return null;
                }
                return Zwrot.Value ? -kwota : kwota;
            }
        }

        public decimal? KwotaZapłaty
        {
            get
            {
                switch (TypRozrachunku)
                {
                    case TypRozrachunku.Wpłata:
                        return KwotaValue;
                    case TypRozrachunku.Wypłata:
                        return -KwotaValue;
                    case TypRozrachunku.Należność:
                    case TypRozrachunku.Zobowiązanie:
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
                switch (TypRozrachunku)
                {
                    case TypRozrachunku.Wpłata:
                        return KwotaValue;
                    case TypRozrachunku.Wypłata:
                        return -KwotaValue;
                    case TypRozrachunku.Należność:
                    case TypRozrachunku.Zobowiązanie:
                        return CzyKompensata ? -Kwota.Value : (decimal?)null;
                    default:
                        return null;
                }
            }
        }


        public string Sezon
        {
            get
            {
                if (this.DokumentType == "Platnosci")
                {
                    Platnosc platnosc = Enova.Business.Old.Core.ContextManager.DataContext.Platnosci
                        .Where(p => p.ID == this.DokumentID).FirstOrDefault();
                    if (platnosc != null && platnosc.DokumentType == "DokHandlowe")
                    {
                        return Enova.Business.Old.Core.ContextManager.DataContext.Features
                            .Where(f => f.Parent == platnosc.DokumentID && f.ParentType == "DokHandlowe" && f.Name == "SEZON").Select(f => f.Data).FirstOrDefault();
                    }
                }
                return null;
            }
        }

    }
}
