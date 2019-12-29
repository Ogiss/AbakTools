using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.Core;

namespace Enova.Business.Old.Types
{
    [DataEditForm("AbakTools.Finanse.Forms.RozrachunekEditForm, AbakTools.Finanse.Forms")]
    public class RozrachunekRow
    {
        public int? IDKontrahenta { get; set; }
        public string KodKontrahenta { get; set; }
        public Enova.Business.Old.DB.Kontrahent Kontrahent
        {
            get
            {
                if (IDKontrahenta != null)
                {
                    return Enova.Business.Old.Core.ContextManager.DataContext.Kontrahenci
                        .Where(k => k.ID == IDKontrahenta).FirstOrDefault();
                }
                return null;
            }
        }
        public bool? Blokada { get; set; }
        public bool? BlokadaSprzedaży { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string NazwaKontrahentaLine
        {
            get { return string.IsNullOrEmpty(NazwaKontrahenta) ? null : NazwaKontrahenta.Replace("\n", " "); }
        }
        public string PrzedstawicielKontrahent { get; set; }
        public string PrzedstawicielDokument { get; set; }
        public string NumerDokumentu { get; set; }
        public DateTime? DataDokumentu { get; set; }
        public DateTime? Termin { get; set; }
        public decimal? WartośćBrutto { get; set; }
        public decimal? WartośćVat { get; set; }
        public decimal WartośćNetto { get; set; }
        public decimal? Zapłacono { get; set; }
        public decimal? Pozostało
        {
            get
            {
                if (WartośćBrutto != null && Zapłacono != null)
                {
                    return WartośćBrutto.Value - Zapłacono.Value;
                }
                return null;
            }
        }
        public decimal? WartoscNierozliczonychZobowiazan { get; set; }
        public decimal? PozostaloPoRozliczeniu
        {
            get
            {
                if (Pozostało != null)
                {
                    if (WartoscNierozliczonychZobowiazan != null)
                        return Pozostało.Value + WartoscNierozliczonychZobowiazan.Value;
                    return Pozostało;
                }
                return null;
                    
            }
        }
        public int? Kierunek { get; set; }
        private string windykacjaStr = null;
        public string WindykacjaStr {
            get { return windykacjaStr; }
            set
            {
                if (windykacjaStr != value)
                {
                    windykacjaStr = value;
                    windykacja = null;
                }
            }
        }
        public string ZakończenieWindykacji { get; set; }
        private Dictionary<string, string> windykacja = null;
        public Dictionary<string, string> Windykacja
        {
            get
            {
                if (windykacja == null)
                {
                    windykacja = new Dictionary<string, string>();
                    if (!string.IsNullOrEmpty(WindykacjaStr))
                    {
                        string[] parts = WindykacjaStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var p in parts)
                        {
                            string[] kvp = p.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                            windykacja.Add(kvp[0], kvp[1]);
                        }
                    }
                }

                return windykacja;
            }
        }

        private string Q(string s)
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }

        public string TE {
            get
            {
                if (Windykacja.ContainsKey("TE"))
                    return Q(Windykacja["TE"]);
                return null;
            }
        }
        public string W1 {
            get
            {
                if (Windykacja.ContainsKey("W1"))
                    return Q(Windykacja["W1"]);
                return null;
            }
        }
        public string W2
        {
            get
            {
                if (Windykacja.ContainsKey("W2"))
                    return Q(Windykacja["W2"]);
                return null;
            }
        }
        public string W3 {
            get
            {
                if (Windykacja.ContainsKey("W3"))
                    return Q(Windykacja["W3"]);
                return null;
            }
        }
        public string UT {
            get
            {
                if (Windykacja.ContainsKey("UT"))
                    return Q(Windykacja["UT"]);
                return null;
            }
        }
        public string SP {
            get
            {
                if (Windykacja.ContainsKey("SP"))
                    return Q(Windykacja["SP"]);
                return null;
            }
        }
        public string WW {
            get
            {
                if (Windykacja.ContainsKey("WW"))
                    return Q(Windykacja["WW"]);
                return null;
            }
        }
    }
}
