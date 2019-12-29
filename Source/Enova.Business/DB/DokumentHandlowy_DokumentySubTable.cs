using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB;
using Enova.Old.Handel;

namespace Enova.Business.Old.DB
{
    public partial class DokumentHandlowy
    {
        #region Properties


        #endregion

        #region Nested Types
        public abstract class DokumentySubTable : DokHandlowe
        {
            #region Fields

            private readonly DokumentHandlowy dh;
            private readonly RelacjeHandlowe rh;

            #endregion

            #region Properties


            public abstract bool IsPodrzedne { get; }

            public DokumentHandlowy this[TypRelacjiHandlowej typ]
            {
                get
                {
                    DokumentHandlowy handlowy = null;
                    foreach (RelacjaHandlowa handlowa in this.rh)
                    {
                        if (handlowa.Typ == typ)
                        {
                            handlowy = this.IsPodrzedne ? handlowa.Nadrzedny : handlowa.Podrzedny;
                            if ((bool)handlowa.Glowna)
                            {
                                return handlowy;
                            }
                        }
                    }
                    return handlowy;
                }
            }

            #endregion

            #region Methods

            public DokumentySubTable(RelacjeHandlowe rh, DokumentHandlowy dh)
            {
                this.dh = dh;
                this.rh = rh;
            }

            internal abstract DokumentHandlowy GetDokument(RelacjaHandlowa rel, string name);

            protected static IEnumerable GetNames(EnovaContext dc, bool podrzędne)
            {
                HandelModule instance = HandelModule.GetInstance(dc);
                List<string> c = new List<string>();
                foreach (DefRelacjiHandlowej handlowej in (IEnumerable)instance.DefRelHandlowych)
                {
                    throw new Exception("Brak w nowej bazie definicji DefRelHandlowej.DefinicjaPodrzednego");
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

            #endregion

            #region Nested Types

            public class Nadrzedne : DokumentySubTable
            {
                #region Properties

                public override ObjectQuery<DokumentHandlowy> BaseQuery
                {
                    get
                    {
                        return (ObjectQuery<DokumentHandlowy>)rh.Where(r => r.Podrzedny.ID == dh.ID).Select(r => r.Nadrzedny);
                    }
                    set
                    {
                        base.BaseQuery = value;
                    }
                }

                public override bool IsPodrzedne
                {
                    get { return true; }
                }

                #endregion

                #region Methods

                public Nadrzedne(RelacjeHandlowe rh, DokumentHandlowy dh)
                    : base(rh, dh)
                {
                }

                internal override DokumentHandlowy GetDokument(RelacjaHandlowa rel, string name)
                {
                    if (rel.Definicja.ZPodrzednego.Nazwa == name)
                    {
                        return rel.Nadrzedny;
                    }
                    return null;
                }

                public static IEnumerable GetNames(EnovaContext dc)
                {
                    return DokumentHandlowy.DokumentySubTable.GetNames(dc, true);
                }

                #endregion
            }

            public class Podrzedne : DokumentySubTable
            {
                #region Properties

                public override ObjectQuery<DokumentHandlowy> BaseQuery
                {
                    get
                    {
                        return (ObjectQuery<DokumentHandlowy>)rh.Where(r => r.Nadrzedny.ID == dh.ID).Select(r => r.Podrzedny);
                    }
                    set
                    {
                        base.BaseQuery = value;
                    }
                }

                public override bool IsPodrzedne
                {
                    get { return false; }
                }

                #endregion

                #region Methods

                public Podrzedne(RelacjeHandlowe rh, DokumentHandlowy dh)
                    : base(rh, dh)
                {
                }

                internal override DokumentHandlowy GetDokument(RelacjaHandlowa rel, string name)
                {
                    if (rel.Definicja.ZNadrzednego.Nazwa == name)
                    {
                        return rel.Podrzedny;
                    }
                    return null;
                }

                public static IEnumerable GetNames(EnovaContext dc)
                {
                    return DokumentHandlowy.DokumentySubTable.GetNames(dc, false);
                }

                #endregion
            }

            #endregion
        }
        #endregion
    }
}
