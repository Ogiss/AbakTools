using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.Types;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Core;


namespace Enova.Business.Old.DB
{
    public partial class Adres
    {
        public Adres()
        {
            this.Host = 0;
            this.HostType = string.Empty;
            this.Typ = 1;
            this.AdresKodPocztowy = 0;
            this.AdresZagranicznyKodPocztowy = string.Empty;
            this.AdresPoczta = string.Empty;
            this.AdresMiejscowosc = string.Empty;
            this.AdresGmina = string.Empty;
            this.AdresPowiat = string.Empty;
            this.AdresWojewodztwo = 0;
            this.AdresKraj = string.Empty;
            this.AdresKodKraju = string.Empty;
            this.AdresUlica = string.Empty;
            this.AdresNrDomu = string.Empty;
            this.AdresNrLokalu = string.Empty;
            this.AdresTelefon = string.Empty;
            this.AdresFaks = string.Empty;
            this.Stamp = BitConverter.GetBytes(DateTime.Now.Ticks);
        }

         public KodPocztowy KodPocztowy
        {
            get
            {
                return (KodPocztowy)AdresKodPocztowy;
            }
            set
            {
                AdresKodPocztowy = (int)value;
            }
        }

        public string KodPocztowyStr
        {
            get
            {
                if (this.AdresKodPocztowy != null && this.AdresKodPocztowy != 0)
                {
                    string str = this.AdresKodPocztowy.Value.ToString().PadLeft(5,'0');

                    return str.Substring(0, 2) + "-" + str.Substring(2);
                }
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int kod = 0;
                    if (int.TryParse(value.Replace("-", string.Empty), out kod))
                    {
                        this.AdresKodPocztowy = kod;
                    }
                }
            }
        }

        public bool IsSet
        {
            get
            {
                return AdresKodPocztowy != 0;
            }
        }

        public string AdresUlicaPelna
        {
            get
            {
                return AdresUlica + (string.IsNullOrEmpty(AdresNrDomu) ? "" : " " + AdresNrDomu + (string.IsNullOrEmpty(AdresNrLokalu) ? "" : "/" + AdresNrLokalu));
            }
        }

        private object kontrahent = null;
        public bool KontrahentIsLoaded = false;
        public void KontrahentLoad()
        {
            switch (HostType)
            {
                case "ZUSY":
                    kontrahent = ContextManager.DataContext.ZUSY.Where(z => z.ID == this.Host).FirstOrDefault();
                    break;
                case "Kontrahenci":
                    kontrahent = ContextManager.DataContext.Kontrahenci.Where(k => k.ID == this.Host).FirstOrDefault();
                    break;
                case "UrzedySkarbowe":
                    kontrahent = ContextManager.DataContext.UrzędySkarbowe.Where(u => u.ID == this.Host).FirstOrDefault();
                    break;

            }
        }

        public object Kontrahent
        {
            get
            {
                if (kontrahent == null && !KontrahentIsLoaded)
                    KontrahentLoad();
            return kontrahent;
            }
        }
    }
}
