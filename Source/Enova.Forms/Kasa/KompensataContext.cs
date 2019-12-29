using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Forms;
using BAL.Types;

//[assembly: DataContext(typeof(Enova.API.Kasa.IKompensata), typeof(Enova.Forms.Kasa.KompensataContext))]
[assembly: DataContext(typeof(Enova.API.Kasa.Kompensata), typeof(Enova.Forms.Kasa.KompensataContext))]

namespace Enova.Forms.Kasa
{
    public class KompensataContext : DataContextWithEnovaApi
    {
        #region Properties

        public override bool ReadOnly
        {
            get
            {
                return true;
            }
        }

        #endregion

    }
}
