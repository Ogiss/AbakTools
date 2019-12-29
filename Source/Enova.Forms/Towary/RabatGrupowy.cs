using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Enova.Business.Old.DB;

namespace Enova.Forms.Towary
{
    public class RabatGrupowy
    {
        public Enova.API.Towary.CenaGrupowa CenaGrupowa;
        public Enova.API.Business.DictionaryItem GrupaTowarowa;
        public Enova.API.CRM.Kontrahent Kontrahent;
        public string GrupaTowarowaNazwa
        {
            get
            {
                if (GrupaTowarowa != null)
                    return GrupaTowarowa.Value;
                return null;
            }
        }

        public decimal? Rabat
        {
            get
            {
                if (CenaGrupowa != null)
                    return CenaGrupowa.Rabat;
                return null;
            }
            set
            {
                if (CenaGrupowa == null && Kontrahent != null && GrupaTowarowa != null && value != null)
                {
                    var t = Kontrahent.Session.GetModule<API.Towary.TowaryModule>().CenyGrupowe;
                    CenaGrupowa = t.Create(GrupaTowarowa, Kontrahent);
                    t.AddRow(CenaGrupowa);
                }

                if (CenaGrupowa != null)
                    CenaGrupowa.Rabat = value == null ? 0 : value.Value;
                if (CenaGrupowa.Rabat != 0)
                    CenaGrupowa.RabatZdefiniowany = true;
            }
        }

        public bool RabatZdefiniowany
        {
            get
            {
                if (CenaGrupowa != null)
                    return CenaGrupowa.RabatZdefiniowany;
                return false;
            }
            set
            {
                if (CenaGrupowa == null && Kontrahent != null && GrupaTowarowa != null && value)
                {
                    var t = Kontrahent.Session.GetModule<API.Towary.TowaryModule>().CenyGrupowe;
                    CenaGrupowa = t.Create(GrupaTowarowa, Kontrahent);
                    t.AddRow(CenaGrupowa);
                }

                if (CenaGrupowa != null)
                    CenaGrupowa.RabatZdefiniowany = value;
            }
        }
    }
}
