using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.DB.Web;

[assembly: BAL.Business.DataContext(typeof(Enova.Business.Old.DB.Web.TowarAtrybut), typeof(AbakTools.Towary.Forms.TowarAtrybutContext))]

namespace AbakTools.Towary.Forms
{
    public class TowarAtrybutContext : AbakTools.Forms.DataContextBase
    {
        #region Fields



        #endregion

        #region Methods

        public override void BeginEdit()
        {
            ((TowarAtrybut)this.Current).EnovaContext = this.EnovaContext;
            base.BeginEdit();
        }

        public override void EndEdit()
        {
            base.EndEdit();
            this.EnovaContext.OptimisticSaveChanges();
            ((TowarAtrybut)this.Current).EnovaContext = null;
        }

        #endregion
    }
}
