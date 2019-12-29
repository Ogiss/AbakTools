using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Enova.Business.Old;
using Enova.Business.Old.DB;
using Enova.Old.Handel;


namespace Enova.Business.Old.DB
{
    public partial class PozycjaDokHandlowego
    {
        #region Nested Types

        [Obsolete("Do zrobienia this[nazwa]")]
        public abstract class PozycjeSubTable : PozycjeDokHan
        {
            #region Fields

            private readonly PozycjaDokHandlowego pozycja;
            private readonly PozRelHandlowej st;

            #endregion

            #region Properties

            public override ObjectQuery<PozycjaDokHandlowego> BaseQuery
            {
                get
                {
                    var query = this.st.BaseQuery.Where(r => r.Dodatkowa == false);
                    if (this.IsPodrzędne)
                    {
                        return (ObjectQuery<PozycjaDokHandlowego>)(from r in query
                                                                   join p in base.BaseQuery on
                                                                   new { Dokument = r.NadrzednyDok.ID, Ident = r.NadrzednaIdent } equals
                                                                   new { Dokument = p.Dokument.ID, Ident = p.Ident }
                                                                   select p);
                    }
                    else
                    {
                        return (ObjectQuery<PozycjaDokHandlowego>)(from r in query
                                                                   join p in base.BaseQuery on
                                                                   new { Dokument = r.PodrzednyDok.ID, Ident = r.PodrzednaIdent } equals
                                                                   new { Dokument = p.Dokument.ID, Ident = p.Ident }
                                                                   select p);
                    }
                }
                set
                {
                    base.BaseQuery = value;
                }
            }

            protected abstract bool IsPodrzędne { get; }

            public PozycjaDokHandlowego this[TypRelacjiHandlowej typ]
            {
                get
                {
                    foreach (PozycjaRelacjiHandlowej handlowej in this.st)
                    {
                        if (!(bool)handlowej.Dodatkowa && (handlowej.Relacja.Typ == typ))
                        {
                            return (this.IsPodrzędne ? handlowej.Nadrzedna : handlowej.Podrzedna);
                        }
                    }
                    return null;
                }
            }

            /*
            public PozycjeWRelacji this[string nazwa]
            {
                get
                {
                    return new PozycjeWRelacji(this, this.st, nazwa, this.pozycja);
                }
            }
             */

            #endregion

            #region Methods

            protected PozycjeSubTable(PozycjaDokHandlowego pozycja, PozRelHandlowej relacje)
            {
                this.pozycja = pozycja;
                this.st = relacje;
            }

            protected static IEnumerable GetNames(EnovaContext dc, bool podrzędne)
            {
                HandelModule instance = HandelModule.GetInstance(dc);
                List<string> c = new List<string>();
                foreach (DefRelacjiHandlowej handlowej in (IEnumerable)instance.DefRelHandlowych)
                {
                    throw new Exception("Brak w nowej bazie DefrelHandlowej.definicjaPodrzednego");
                    /*
                    if ((!(bool)handlowej.Blokada && !(bool)handlowej.DefinicjaNadrzednego.Blokada) && !(bool)handlowej.DefinicjaPodrzednego.Blokada)
                    {
                        c.Add(podrzędne ? handlowej.ZPodrzednego.Nazwa : handlowej.ZNadrzednego.Nazwa);
                    }
                     */
                }
                ArrayList list = new ArrayList(c);
                list.Sort();
                return list;
            }

            internal abstract PozycjaDokHandlowego GetPozycja(PozycjaRelacjiHandlowej pozr, string nazwa);

            #endregion

            #region Nested Types

            public class Nadrzędne : PozycjaDokHandlowego.PozycjeSubTable
            {
                protected override bool IsPodrzędne
                {
                    get
                    {
                        return false;
                    }
                }

                internal Nadrzędne(PozycjaDokHandlowego pozycja, PozRelHandlowej relacje) : base(pozycja, relacje) { }

                public static IEnumerable GetNames(EnovaContext dc)
                {
                    return PozycjaDokHandlowego.PozycjeSubTable.GetNames(dc, false);
                }

                internal override PozycjaDokHandlowego GetPozycja(PozycjaRelacjiHandlowej pozr, string nazwa)
                {
                    if (pozr.Relacja.Definicja.ZNadrzednego.Nazwa == nazwa)
                    {
                        return pozr.Podrzedna;
                    }
                    return null;
                }

            }

            public class Podrzędne : PozycjaDokHandlowego.PozycjeSubTable
            {
                // Methods
                internal Podrzędne(PozycjaDokHandlowego pozycja, PozRelHandlowej st)
                    : base(pozycja, st)
                {
                }

                public static IEnumerable GetNames(EnovaContext dc)
                {
                    return PozycjaDokHandlowego.PozycjeSubTable.GetNames(dc, true);
                }

                internal override PozycjaDokHandlowego GetPozycja(PozycjaRelacjiHandlowej pozr, string nazwa)
                {
                    if (pozr.Relacja.Definicja.ZPodrzednego.Nazwa == nazwa)
                    {
                        return pozr.Nadrzedna;
                    }
                    return null;
                }

                // Properties
                protected override bool IsPodrzędne
                {
                    get
                    {
                        return true;
                    }
                }
            }

            #endregion

        }

        #endregion
    }
}
