using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

namespace Enova.Business.Old.DB.Web
{
    public partial class PozycjaZamowieniaView
    {
        decimal? cena;

        public string ProduktNazwaPelna
        {
            get
            {
//                return ProduktNazwa + (string.IsNullOrEmpty(AtrybutNazwa) ? "" : " - " + AtrybutGrupaNazwa + ": " + AtrybutNazwa) +
//                    (string.IsNullOrEmpty(Opis) ? "" : "\r\n" + Opis);
                return ProduktNazwa + AtrybutNazwaPelna +
                    (string.IsNullOrEmpty(Opis) ? "" : "\r\n" + Opis);

            }
        }

        public Decimal Cena
        {
            get
            {
                if (this.cena != null)
                    return this.cena.Value;
                else if (this.CenaRecord != null)
                    return this.CenaRecord.Value;
                return 0M;
            }
            set
            {
                this.cena = value;
            }

        }

        public string AtrybutNazwaPelna
        {
            get
            {
                return string.IsNullOrEmpty(AtrybutNazwa) ? "" : " - " + AtrybutGrupaNazwa + ": " + 
                    (AtrybutPrefix != null ? AtrybutPrefix : "") + AtrybutNazwa + (AtrybutSuffix != null ? AtrybutSuffix : "");
            }
        }

        public decimal? WartoscNetto
        {
            get
            {
                double ilosc = this.Ilosc == null ? 0 : this.Ilosc.Value;
                return decimal.Round(this.Cena * (decimal)ilosc, 2);
            }
        }
    }
}
