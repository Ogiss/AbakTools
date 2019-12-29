using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB;

namespace Enova.Old.Handel
{
    //[Obsolete("!!!! KONIECZNIE ZAIMPLEMENTOWAĆ MAPOWANIE TYPÓW DEFINICJI RELACJI !!!!",true)]
    public partial class DefRelHandlowych : EnovaGuidedTable<DefRelacjiHandlowej>
    {
        #region Fields
        internal static readonly Guid CyklicznaAUFVGuid;
        internal static readonly Guid CyklicznaUMFVGuid;
        internal static readonly Guid CyklicznaZJFVGuid;
        internal static readonly Guid FakturaParagonuGuid;
        private static readonly Guid FSWZGuid;
        private static readonly Guid FZPZGuid;
        private static readonly Guid KKPLGuid;
        private static readonly Guid KKPLPGuid;
        private static readonly Guid KKPLWGuid;
        private static readonly Guid KMMGuid;
        private static readonly Guid KMMPGuid;
        private static readonly Guid KMMWGuid;
        internal static readonly Guid KopiaGuid;
        internal static readonly Guid KorektaGuid;
        internal static readonly Guid KorektaPZGuid;
        internal static readonly Guid KorektaWZGuid;
        private static readonly Guid KPZFZGuid;
        private static readonly Guid PZFZGuid;
        private static readonly Guid zaliczka_FFZALFF;
        private static readonly Guid zaliczka_FZALFV;
        private static readonly Guid zaliczka_KFZALFV;
        private static readonly Guid ZDZKGuid;
        private static readonly Guid ZOFVGuid;
        #endregion

        #region Properties

        public DefRelacjiHandlowej CyklicznaAUFV
        {
            get
            {
                return this[CyklicznaAUFVGuid];
            }
        }

        public DefRelacjiHandlowej CyklicznaUMFV
        {
            get
            {
                return this[CyklicznaUMFVGuid];
            }
        }

        public DefRelacjiHandlowej CyklicznaZJFV
        {
            get
            {
                return this[CyklicznaZJFVGuid];
            }
        }

        public DefRelacjiHandlowej FSWZ
        {
            get
            {
                return this[FSWZGuid];
            }
        }

        public DefRelacjiHandlowej FZPZ
        {
            get
            {
                return this[FZPZGuid];
            }
        }

        public DefRelacjiHandlowej KKPL
        {
            get
            {
                return this[KKPLGuid];
            }
        }

        public DefRelacjiHandlowej KKPLP
        {
            get
            {
                return this[KKPLPGuid];
            }
        }

        public DefRelacjiHandlowej KKPLW
        {
            get
            {
                return this[KKPLWGuid];
            }
        }

        public DefRelacjiHandlowej KMM
        {
            get
            {
                return this[KMMGuid];
            }
        }

        public DefRelacjiHandlowej KMMP
        {
            get
            {
                return this[KMMPGuid];
            }
        }

        public DefRelacjiHandlowej KMMW
        {
            get
            {
                return this[KMMWGuid];
            }
        }

        public DefRelacjiHandlowej Kopia
        {
            get
            {
                return this[KopiaGuid];
            }
        }

        public DefRelacjiHandlowej Korekta
        {
            get
            {
                return this[KorektaGuid];
            }
        }

        public DefRelacjiHandlowej KorektaPZ
        {
            get
            {
                return this[KorektaPZGuid];
            }
        }

        public DefRelacjiHandlowej KorektaWZ
        {
            get
            {
                return this[KorektaWZGuid];
            }
        }

        public DefRelacjiHandlowej KPZFZ
        {
            get
            {
                return this.RowContainsGuid(KPZFZGuid);
            }
        }

        public DefRelacjiHandlowej PZFZ
        {
            get
            {
                return this[PZFZGuid];
            }
        }

        internal DefRelacjiHandlowej Zaliczka_FFZALFF
        {
            get
            {
                return this[zaliczka_FFZALFF];
            }
        }

        internal DefRelacjiHandlowej Zaliczka_FZALFV
        {
            get
            {
                return this[zaliczka_FZALFV];
            }
        }

        internal DefRelacjiHandlowej Zaliczka_KFZALFV
        {
            get
            {
                return this[zaliczka_KFZALFV];
            }
        }

        public DefRelacjiHandlowej ZDZK
        {
            get
            {
                return this[ZDZKGuid];
            }
        }

        public DefRelacjiHandlowej ZOFV
        {
            get
            {
                return this[ZOFVGuid];
            }
        }



        #endregion

        #region Methods

        static DefRelHandlowych()
        {
            CyklicznaUMFVGuid = new Guid("{00000000-0011-0003-0040-000000000000}");
            CyklicznaZJFVGuid = new Guid("{00000000-0011-0003-0041-000000000000}");
            CyklicznaAUFVGuid = new Guid("{00000000-0011-0003-0043-000000000000}");
            zaliczka_FFZALFF = new Guid("{00000000-0011-0003-0039-000000000000}");
            zaliczka_KFZALFV = new Guid("{00000000-0011-0003-0038-000000000000}");
            zaliczka_FZALFV = new Guid("{00000000-0011-0003-0037-000000000000}");
            KopiaGuid = new Guid("{00000000-0011-0003-002b-000000000000}");
            KorektaGuid = new Guid("{00000000-0011-0003-0001-000000000000}");
            FakturaParagonuGuid = new Guid("{00000000-0011-0003-0009-000000000000}");
            KorektaPZGuid = new Guid("{00000000-0011-0003-000a-000000000000}");
            KorektaWZGuid = new Guid("{00000000-0011-0003-0002-000000000000}");
            FSWZGuid = new Guid("{00000000-0011-0003-0003-000000000000}");
            FZPZGuid = new Guid("{00000000-0011-0003-0005-000000000000}");
            PZFZGuid = new Guid("{00000000-0011-0003-000d-000000000000}");
            KPZFZGuid = new Guid("{00000000-0011-0003-0036-000000000000}");
            ZDZKGuid = new Guid("{00000000-0011-0003-000f-000000000000}");
            KMMGuid = new Guid("{00000000-0011-0003-002c-000000000000}");
            KMMPGuid = new Guid("{00000000-0011-0003-002d-000000000000}");
            KMMWGuid = new Guid("{00000000-0011-0003-002e-000000000000}");
            KKPLGuid = new Guid("{00000000-0011-0003-002f-000000000000}");
            KKPLPGuid = new Guid("{00000000-0011-0003-0030-000000000000}");
            KKPLWGuid = new Guid("{00000000-0011-0003-0031-000000000000}");
            ZOFVGuid = new Guid("{00000000-0011-0003-000e-000000000000}");
        }


        private DefRelacjiHandlowej RowContainsGuid(Guid guid)
        {
            foreach (DefRelacjiHandlowej handlowej in base.Rows)
            {
                if (handlowej.Guid == guid)
                {
                    return handlowej;
                }
            }
            return null;
        }

        protected override ObjectQuery<DefRelacjiHandlowej> CreateQuery()
        {
            return ((HandelModule)this.Module).DataContext.DefRelHandlowych;
        }

        #endregion
    }
}
