using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Enova.Business.Old.DB
{
    public class KontrahentRow : Enova.Business.Old.Core.RowBase
    {
        public Kontrahent Kontrahent
        {
            get { return (Kontrahent)this.ProxyData; }
            set { this.ProxyData = value; }
        }
        public int ID
        {
            get { return Kontrahent.ID; }
        }

        public bool? Blokada
        {
            get { return Kontrahent.Blokada; }
            set { Kontrahent.Blokada = value; }
        }

        public bool? BlokadaSprzedazy
        {
            get { return Kontrahent.BlokadaSprzedazy; }
            set { Kontrahent.BlokadaSprzedazy = value; }
        }

        public string Kod {
            get { return Kontrahent.Kod; }
            set { Kontrahent.Kod = value; }
        }
        public string Nazwa {
            get { return Kontrahent.Nazwa; }
            set { Kontrahent.Nazwa = value; }
        }
        public string NIP { 
            get { return Kontrahent.NIP; }
            set { Kontrahent.NIP = value; }
        }
        public string Email {
            get { return Kontrahent.KontaktEMAIL; }
            set { Kontrahent.KontaktEMAIL = value; }
        }

        public string Ulica { get; set; }
        public string NrDomu { get; set; }
        public string NrLokalu { get; set; }
        public int? KodPocztowy { get; set; }
        public string Miejscowosc { get; set; }
        public string AdresTelefon { get; set; }
        public string Przedstawiciel { get; set; }
    }
}
