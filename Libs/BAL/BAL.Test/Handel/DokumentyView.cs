using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;

namespace BAL.Test.Handel
{
    public class DokumentyView : GridViewContext
    {
        #region Fields

        private CRM.Kontrahent kontrahent;
        private Dokumenty table;

        #endregion

        #region Properties

        public override ITable Table
        {
            get
            {
                if (this.kontrahent != null)
                    return ((Dokumenty)base.Table).WgKontrahent[this.kontrahent];
                return base.Table;
            }
        }

        #endregion

        #region Methods


        public override IEnumerable<DataContextParam> GetParams()
        {
            return new DataContextParam[]
            {
                new DataContextParam("kontrahentParam","Kontrahent:", Types.PropertyPath.Create<Dokument>("Kontrahent"))
            };
        }

        protected override void OnParamValueChanged(DataContextParamEventArgs e)
        {
            base.OnParamValueChanged(e);
            if (e.Param.Name == "kontrahentParam")
            {
                this.kontrahent = (CRM.Kontrahent)((IValue)e.Control).Value;
                this.Reload();
            }
        }

        #endregion
    }
}
