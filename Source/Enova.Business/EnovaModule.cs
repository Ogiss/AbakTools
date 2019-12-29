using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public class EnovaModule : Module
    {

        //TEST

        #region Properties

        new public DB.EnovaContext DataContext
        {
            get { return (DB.EnovaContext)base.DataContext; }
        }

        #endregion

        #region Methods

        public EnovaModule(DB.EnovaContext dc, string name) : base(dc, name) { }

        public EnovaModule(Session session, string name) : base(session, name) { }

        #endregion
    }
}
