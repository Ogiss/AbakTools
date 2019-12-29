using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Extensions;
using BAL.Forms;

namespace Enova.Forms
{
    public abstract class DokumentyViewBase<T> : GridViewWithEnovaApi<T>
         where T : Enova.API.Business.Row
    {
        #region Fields

        protected Enova.Forms.Controls.KontrahentEnovaSelect kotrahentSelect;
        protected BAL.Forms.Controls.DateFromToControl fromToControls;

        #endregion

        #region Properties

        #endregion

        #region methods

        public override IEnumerable<BAL.Business.DataContextParam> GetParams()
        {
            return new BAL.Business.DataContextParam[]
            {
                new BAL.Business.DataContextParam("fromToParam", "Okres:", new PropertyPath(this.GetDataType(),"Data")){ControlType=typeof(BAL.Forms.Controls.DateFromToControl)},
                new BAL.Business.DataContextParam("kontrahentParam", "Kontrahent:", null){ControlType=typeof(Enova.Forms.Controls.KontrahentEnovaSelect)}
            };
        }

        protected override void OnInitParam(DataContextParamEventArgs e)
        {
            switch (e.Param.Name)
            {
                case "fromToParam":
                    fromToControls = e.Control as BAL.Forms.Controls.DateFromToControl;
                    break;
                case "kontrahentParam":
                    kotrahentSelect = e.Control as Enova.Forms.Controls.KontrahentEnovaSelect;
                    break;
            }
        }

        protected override void OnParamValueChanged(DataContextParamEventArgs e)
        {
            this.Reload();
            base.OnParamValueChanged(e);
        }

        #endregion

    }
}
