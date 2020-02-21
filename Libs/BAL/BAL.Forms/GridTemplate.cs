using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BAL.Forms
{
    public class GridTemplate : IGridTemplate
    {
        #region Methods

        public virtual void SetRowStyle(object row)
        {
            var r = row as DataGridViewRow;
            if (r != null)
            {
                r.DefaultCellStyle.BackColor = (r.Index % 2) == 0 ? Color.LightGray : Color.White;
            }
        }

        #endregion
    }
}
