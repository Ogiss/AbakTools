using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Forms
{
    public class GridViewBase : BAL.Forms.GridViewContext
    {
        #region Properties

        public bool ParentFormHideOnClose { get; set; }
       

        #endregion

        #region Methods

        public GridViewBase() : base() { }

        public GridViewBase(BAL.Business.ITable table) : base(table) { }

        protected override void OnParentFormChanged(EventArgs e)
        {
            if (typeof(BAL.Forms.FormBase).IsAssignableFrom(this.ParentForm.GetType()))
                ((BAL.Forms.FormBase)this.ParentForm).HideOnClose = this.ParentFormHideOnClose;

            base.OnParentFormChanged(e);
        }

        #endregion
    }
}
