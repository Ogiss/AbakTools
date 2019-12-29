using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.Types
{
    public class RabatGrupowy
    {
        public bool Changed = false;
        public string Grupa { get; set; }
        public CenaGrupowa CenaGrupowa = null;

        public int? GrupaTowarowaID { get; set; }


        public string GrupaTowarowaNazwa { get; set; }
        public decimal? Rabat { get; set; }
        public string RabatProcent
        {
            get
            {
                if (Rabat != null)
                {
                    return decimal.Round((Rabat.Value * 100M), 2).ToString() + "%";
                }
                return null;

            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    decimal? rabat = decimal.Parse(value.Replace('%', ' ').Trim()) / 100M;
                    if (rabat != Rabat)
                    {
                        Rabat = rabat;
                        Zdefiniowany = true;
                        Changed = true;
                    }
                }
                else
                {
                    if (Rabat != null)
                    {
                        Rabat = null;
                        Zdefiniowany = false;
                        Changed = true;
                    }
                }
                
            }
        }
        public bool? Zdefiniowany { get; set; }
        public bool? RabatZdefiniowany
        {
            get
            {
                return Zdefiniowany;
            }
            set
            {
                if (Zdefiniowany != value)
                {
                    Zdefiniowany = value;
                    Changed = true;
                }
            }
        }
    }
}
