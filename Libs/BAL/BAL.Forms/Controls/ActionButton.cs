using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using BAL.Types;
using BAL.Business;

namespace BAL.Forms.Controls
{
    public class ActionButton : Button
    {
        #region Fields

        private object action;
        private MethodInfo method;
        private bool waitCursor;

        #endregion

        #region Properties
        #endregion

        #region Methods

        public ActionButton(object action)
        {
            this.action = action;
            this.Text = CaptionAttribute.GetCaption(this.action, this.action.GetType().Name);
            var pinfo = this.action.GetType().GetProperty("Control");
            if (pinfo != null)
                pinfo.SetValue(this.action, this, null);
            method = action.GetType().GetMethod("OnAction");
            pinfo = this.action.GetType().GetProperty("WaitCursorEnable");
            if (pinfo != null)
                this.waitCursor = (bool)pinfo.GetValue(this.action, null);
        }

        protected override void OnClick(EventArgs e)
        {
            if (this.method != null)
            {
                if (this.waitCursor)
                    using (new WaitCursor(this.TopLevelControl))
                        this.method.Invoke(this.action, null);
                else
                    this.method.Invoke(this.action, null);
            }
            base.OnClick(e);
        }

        #endregion
    }
}
