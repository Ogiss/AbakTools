using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;

namespace Enova.Forms.CRM
{
    public class KontrahenciViewGridTemplate : GridTemplate
    {
        public override void SetRowStyle(object row)
        {
            var r = (System.Windows.Forms.DataGridViewRow)row;
            var k = (Enova.API.CRM.Kontrahent)r.DataBoundItem;
            if (k.BlokadaSprzedazy)
                r.DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
            else
                base.SetRowStyle(row);
        }
    }
}
