using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

namespace Enova.Business.Old.DB.Web
{
    public partial class HistoriaZamowienia
    {
        public HistoriaZamowienia()
        {
            Synchronizacja = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew;
        }

        public StatusZamowienia Status
        {
            get
            {
                if (EntityState != EntityState.Added && EntityState != EntityState.Detached && !StatusRefReference.IsLoaded)
                    StatusRefReference.Load();
                return StatusRef;
            }
            set
            {
                StatusRef = value;
            }
        }

        public override string ToString()
        {
            return DataDodania.Value.ToString() + " - " + Status.Nazwa + "  " + (Operator != null ? "(" + Operator.Nazwa + ")" : "");
        }

    }
}
