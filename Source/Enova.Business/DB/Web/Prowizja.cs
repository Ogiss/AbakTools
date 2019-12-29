using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class Prowizja
    {
        static string[] miesiące = new string[]
        {
            "none",
            "Styczeń",
            "Luty",
            "Marzec",
            "Kwiecień",
            "Maj",
            "Czerwiec",
            "Lipiec",
            "Sierpień",
            "Wrzesień",
            "Październik",
            "Listopad",
            "Grudzień"
        };

        public string Okres
        {
            get
            {
                return miesiące[this.Miesiac.Value] + " " + this.Rok.ToString();
            }
        }

        public decimal ProwizjaNettoValue
        {
            get
            {
                return this.ProwizjaSuma.Value + this.Przeterminowane.Value + this.Wysylki.Value + this.Magazynowe.Value;
            }
        }

        public decimal PodatekDochodowy
        {
            get
            {
                return decimal.Round(ProwizjaNettoValue * -0.19M, 2);
            }
        }

        public decimal DoWyplaty
        {
            get
            {
                return ProwizjaNettoValue + PodatekDochodowy;
            }
        }
        
    }
}
