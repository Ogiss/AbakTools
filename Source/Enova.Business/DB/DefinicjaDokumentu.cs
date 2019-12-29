using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Old.Core;

namespace Enova.Business.Old.DB
{
    public partial class DefinicjaDokumentu
    {
        #region Properties

        public TypDokumentu Typ
        {
            get { return (TypDokumentu)this.TypInt; }
        }


        #endregion

        #region Do usuniecia lub przerobienia

        public bool CzyKompensata
        {
            get
            {
                if (Guid.ToString() == "00000000-0003-0003-0008-000000000000")
                    return true;
                return false;
            }
        }

        #endregion
    }
}
