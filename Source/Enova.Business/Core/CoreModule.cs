using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;
using Enova.Business.Old.DB;

namespace Enova.Old.Core
{
    public partial class CoreModule : EnovaModule
    {
        #region Fields

        private DaneKontrahentow daneKontrahentowTable;

        #endregion

        #region Properties

        public DaneKontrahentow DaneKontrahentow
        {
            get { return this.daneKontrahentowTable; }
        }

        #endregion

        #region Methods

        public CoreModule(EnovaContext dc)
            : base(dc, "Core")
        {
            this.daneKontrahentowTable = new DaneKontrahentow(dc);
        }

        public static CoreModule GetInstance(EnovaContext dc)
        {
            var module = Module.GetModule("Core");
            if (module == null)
            {
                module = new CoreModule(dc);
                Module.AddModule(module);
            }
            return (CoreModule)module;
        }

        #endregion
    }
}
