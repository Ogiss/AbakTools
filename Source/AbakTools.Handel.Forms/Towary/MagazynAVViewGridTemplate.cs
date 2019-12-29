using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.DB.Web;

namespace AbakTools.Towary.Forms
{
    public class MagazynAVViewGridTemplate : GridTemplate
    {
        #region Methods

        public override void SetRowStyle(object row)
        {
            var r = (DataGridViewRow)row;
            var ta = (TowarAtrybut)r.DataBoundItem;
            if (!ta.Aktywny)
            {
                r.DefaultCellStyle.ForeColor = Color.Gray;
                r.DefaultCellStyle.SelectionForeColor = Color.Gray;
            }
            else if (!ta.Dostepny)
            {
                r.DefaultCellStyle.ForeColor = Color.Red;
                r.DefaultCellStyle.SelectionForeColor = Color.Red;
            }

            if (ta.Aktywny && ta.Dostepny)
            {
                r.DefaultCellStyle.ForeColor = Color.Black;
                r.DefaultCellStyle.SelectionForeColor = Color.White;
            }
        }

        #endregion
    }
}
