using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms.Controls
{
    public class CheckBox : System.Windows.Forms.CheckBox, BAL.Types.INotifyValueChanged
    {
        #region Methods

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            this.OnValueChanged(new EventArgs());
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, e);
        }

        #endregion

        #region Events

        public event EventHandler ValueChanged;

        #endregion
    }
}
