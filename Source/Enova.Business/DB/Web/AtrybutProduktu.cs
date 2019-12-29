using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects.DataClasses;

namespace Enova.Business.Old.DB.Web
{
    public partial class AtrybutProduktu
    {

        public Atrybut Atrybut
        {
            get
            {
                try
                {

                    KombinacjaAtrybutu ka = this.KombinacjeAtrybutu.FirstOrDefault();
                    if (ka != null)
                        return ka.Atrybut;
                }
                catch
                {
                }
                return null;
            }
        }

        public string AtrybutNazwa
        {
            get
            {
                if (Atrybut != null)
                    return Atrybut.Nazwa;
                return null;
            }
        }

        public GrupaAtrybutow GrupaAtrybutow
        {
            get
            {
                if (Atrybut != null)
                    return Atrybut.GrupaAtrybutow;
                return null;
            }
        }

        public string GrupaNazwaPubliczna
        {
            get
            {
                if (Atrybut != null)
                    return Atrybut.GrupaAtrybutow.NazwaPubliczna;
                return null;
            }
        }

        public string AtrybutPrefix
        {
            get
            {
                KombinacjaAtrybutu ka = KombinacjeAtrybutu.FirstOrDefault();
                if (ka != null)
                    return ka.Prefix;
                return null;
            }
            set
            {
                KombinacjaAtrybutu ka = KombinacjeAtrybutu.FirstOrDefault();
                if (ka != null)
                    ka.Prefix = value;
            }
        }

        public string AtrybutSuffix
        {
            get
            {
                KombinacjaAtrybutu ka = KombinacjeAtrybutu.FirstOrDefault();
                if (ka != null)
                    return ka.Suffix;
                return null;
            }
            set
            {
                KombinacjaAtrybutu ka = KombinacjeAtrybutu.FirstOrDefault();
                if (ka != null)
                    ka.Suffix = value;
            }
        }

        public Zasob Zasob
        {
            get
            {
                return this.Zasoby.FirstOrDefault();
            }
        }

        public bool Dostepny
        {
            get
            {
                return Zasob != null ? Zasob.Dostepny : true;
            }
            set
            {
                if (Zasob != null && Zasob.Dostepny != value)
                    Zasob.Dostepny = value;
            }
        }
    }
}
