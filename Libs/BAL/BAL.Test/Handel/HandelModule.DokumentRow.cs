using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using BAL.Types;
using BAL.Business;

namespace BAL.Test.Handel
{
    public partial class HandelModule
    {
        public class DokumentRow : Row
        {
            #region Fields

            private DateTime data;
            private string numer;
            private int kontrahentID;
            
            #endregion

            #region Properties

            public DateTime Data
            {
                get { return this.data; }
                set { SetValue(() => { this.data = value; }, "Data"); }
            }

            public string Numer
            {
                get { return this.numer; }
                set { SetValue(() => { this.numer = value; }, "Numer"); }
            }

            [Hidden]
            public int KontrahentID
            {
                get { return this.kontrahentID; }
                set { SetValue(() => { this.kontrahentID = value; }, "KontrahentID", "Kontrahent" ); }
            }

            [ForeignKey("KontrahentID")]
            public virtual CRM.Kontrahent Kontrahent { get; set; }

            #endregion

            #region Methods

            protected override void OnAdded(EventArgs e)
            {
                base.OnAdded(e);
                this.Data = DateTime.Now;
            }

            #endregion
        }
    }
}
