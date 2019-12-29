using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using BAL.Types;
using BAL.Business;
using BAL.Forms;

namespace AbakTools.Handel.Forms
{
    public class ReklamacjeViewGridTemplate : GridTemplate
    {
        public override void SetRowStyle(object row)
        {
            try
            {
                var r = (DataGridViewRow)row;
                var z = (Enova.Business.Old.DB.Web.Reklamacja)r.DataBoundItem;

                var hist = z.OstatniaHistoriaDokumentu;
                if (hist != null && hist.Status != null)
                {
                    if (!string.IsNullOrEmpty(hist.Status.Kolor))
                    {
                        var color = ColorTranslator.FromHtml(hist.Status.Kolor);
                        r.DefaultCellStyle.BackColor = color;
                        r.DefaultCellStyle.SelectionBackColor = color;
                        r.DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }

            }
            catch { }
        }
    }
}
