using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Forms;

//[assembly: DataContext(typeof(AbakTools.Handel.Reklamacja), typeof(AbakTools.Handel.Forms.ReklamacjeEditContext))]
[assembly: DataContext(typeof(Enova.Business.Old.DB.Web.Reklamacja), typeof(AbakTools.Handel.Forms.ReklamacjeEditContext))]

namespace AbakTools.Handel.Forms
{
    public class ReklamacjeEditContext : AbakTools.Forms.DataContextBase
    {
        public Enova.Business.Old.DB.Web.Reklamacja RozdzielonaReklamacja { get; set; }

        #region Methods

        public override void BeginEdit()
        {
            base.BeginEdit();
        }

        public override void EndEdit()
        {
            base.EndEdit();
            if (this.RozdzielonaReklamacja != null)
            {
                FormManager.Alert("Dodano nową reklamację o numerze: " + RozdzielonaReklamacja.Numer.NumerPelny);
                this.Reload();
            }
        }

        #endregion
    }
}
